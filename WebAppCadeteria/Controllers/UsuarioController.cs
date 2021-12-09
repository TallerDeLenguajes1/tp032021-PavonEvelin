using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp_Cadeteria.Models;
using WebApp_Cadeteria.Models.Entities;
using WebApp_Cadeteria.Models.Repositories;
using WebApp_Cadeteria.Models.ViewModels.UsuarioViewModels;
using WebApp_Cadeteria.Models.ViewModels.CadeteViewModels;
using WebApp_Cadeteria.Models.ViewModels.ClienteViewModels;

namespace WebApp_Cadeteria.Controllers
{
    
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public UsuarioController(ILogger<UsuarioController> logger, DataContext DataContext, IMapper mapper)
        {
            _logger = logger;
            dataContext = DataContext;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpPost]
        public IActionResult Login(UsuarioViewModel usuario) 
        {
            try
            {

                if (dataContext.Usuarios.ValidateUser(mapper.Map<Usuario>(usuario)))
                {
                    Usuario user = dataContext.Usuarios.GetUser(usuario.UserName, usuario.Password);
                    //HttpContext.Session.SetString("UserName", usuario.UserName);
                    HttpContext.Session.SetString("Rol", user.Rol);
                    /*
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario")))
                    {
                        HttpContext.Session.SetString("Usuario", usuario.Nombre);
                        HttpContext.Session.SetString("Rol", usuario.Rol.ToString());
                    }*/
                    //var username = HttpContext.Session.GetString("Usuario");
                    var rol = HttpContext.Session.GetString("Rol");
                    

                    switch (user.Rol)
                    {

                        case "Admin":
                            return View("../Administrador/Admin");
                        case "Cadete":
                            int id_cadete = dataContext.Cadetes.GetIdCadeteByIdUser(user.Id);
                            var cadeteVM = mapper.Map<CadeteViewModel>(dataContext.Cadetes.GetCadetePorId(id_cadete));
                            return View("../Cadete/MostrarCadeteUsuario", cadeteVM);
                        case "Cliente":
                            int id_cliente = dataContext.Clientes.GetIdClienteByIdUser(user.Id);
                            var clienteVM = mapper.Map<ClienteViewModel>(dataContext.Clientes.GetClientePorId(id_cliente));
                            return View("../Cliente/MostrarClienteUsuario", clienteVM);
                            break;
                        default:
                            return View();
                            break;
                    }
                }
                else
                {
                    return View(nameof(Login));
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                return View(nameof(Login));
            }
        }

        public IActionResult CrearUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AltaUsuario(UsuarioViewModel usuario)
        { 
            try
            {
                if (ModelState.IsValid)
                {
                    if (usuario.Rol == "Cadete" || usuario.Rol == "Cliente")
                    {
                        Usuario nuevoUser = mapper.Map<Usuario>(usuario);
                        dataContext.Usuarios.SaveUser(nuevoUser);
                        int idUser = dataContext.Usuarios.GetUserID(nuevoUser);

                        switch (nuevoUser.Rol)
                        {
                            case "Cadete":
                                Cadete newCadete = new Cadete();
                                newCadete.Nombre = nuevoUser.Nombre;
                                newCadete.Direccion = nuevoUser.Direccion;
                                newCadete.Telefono = nuevoUser.Telefono;
                                dataContext.Cadetes.SaveCadete(newCadete, idUser);
                                //repoCadetes.SaveCadete(mapper.Map<Cadete>(nuevoUser), nuevoUser.Id); //duda
                                break;
                            case "Cliente":
                                Cliente newCliente = new Cliente();
                                newCliente.Nombre = nuevoUser.Nombre;
                                newCliente.Direccion = nuevoUser.Direccion;
                                newCliente.Telefono = nuevoUser.Telefono;
                                dataContext.Clientes.SaveCliente(newCliente, idUser);
                                break;
                            default:
                                break;
                        }
                        return View("Index");
                    }
                    else
                    {
                        return RedirectToAction("CrearUsuario");
                    }
                    
                }
                else
                {
                    return RedirectToAction("CrearUsuario");
                }
                
            }
            catch (Exception e)
            {
                string error = e.Message;
                return RedirectToAction("CrearUsuario");
            }
        }
    }
}