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
using WebApp_Cadeteria.Models.ViewModels;

namespace WebApp_Cadeteria.Controllers
{
    
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly RepositorioUsuario repoUsuarios;
        private readonly IRepositorioCadete repoCadetes;
        private readonly IMapper mapper;

        public UsuarioController(ILogger<UsuarioController> logger, RepositorioUsuario RepoUsuarios, IRepositorioCadete RepoCadetes, IMapper mapper)
        {
            _logger = logger;
            repoUsuarios = RepoUsuarios;
            repoCadetes = RepoCadetes;
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

                if (repoUsuarios.ValidateUser(mapper.Map<Usuario>(usuario)))
                {
                    Usuario user = repoUsuarios.GetUser(usuario.UserName, usuario.Password);
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
                            int id_cadete = repoCadetes.GetIdCadeteByIdUser(usuario.Id);
                            var cadeteVM = mapper.Map<CadeteViewModel>(repoCadetes.GetCadetePorId(id_cadete));
                            return View("../Cadete/MostrarCadeteUsuario", cadeteVM);
                        /*case "Cliente":
                            //return View("../Cliente/MostrarClienteUsuario", mapper.Map<CadeteViewModel>(repoCadetes.GetCadetePorId(id_cadete)));
                            break;*/
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
                        repoUsuarios.SaveUser(nuevoUser);
                        switch (nuevoUser.Rol)
                        {
                            case "Cadete":
                                repoCadetes.SaveCadete(mapper.Map<Cadete>(nuevoUser), nuevoUser.Id); //duda
                                break;
                            //case Roles.Cliente:
                            default:
                                break;
                        }
                        return RedirectToAction("Index", "Cadete");
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