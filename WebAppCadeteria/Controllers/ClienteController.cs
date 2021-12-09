using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models;
using WebApp_Cadeteria.Models.ViewModels;

namespace WebApp_Cadeteria.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ILogger<UsuarioController> logger;
        private readonly RepositorioUsuario repoUsuarios;
        private readonly IRepositorioCadete repoCadetes;
        private readonly RepositorioCliente repoClientes;
        private readonly IMapper mapper;

        public ClienteController(ILogger<UsuarioController> logger, RepositorioUsuario RepoUsuarios, IRepositorioCadete RepoCadetes, RepositorioCliente RepoClientes, IMapper mapper)
        {
            this.logger = logger;
            repoUsuarios = RepoUsuarios;
            repoCadetes = RepoCadetes;
            repoClientes = RepoClientes;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            List<Cliente> listadoClientes = repoClientes.getAll();
            var listaClientesViewModel = mapper.Map<List<ClienteViewModel>>(listadoClientes);
            return View(listaClientesViewModel);
        }

        public IActionResult CrearCliente()
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (rol == "Admin")
            {
                return RedirectToAction("CrearUsuario", "Usuario");
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            //Enum.TryParse(rol, out Roles myStatus);

        }

        public IActionResult ModificarCliente(int id)
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (rol == "Admin" || rol == "Cliente")
            {
                ClienteViewModel clienteAModificar = mapper.Map<ClienteViewModel>(repoClientes.GetClientePorId(id));
                if (clienteAModificar != null)
                {
                    return View(clienteAModificar);
                }
                else
                {
                    return Redirect("Index");
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult ModificarCliente2(ClienteViewModel cliente)
        {
            Cliente clienteAModificar = mapper.Map<Cliente>(cliente);

            if (clienteAModificar != null)
            {
                repoClientes.UpdateCliente(clienteAModificar);
            }
            return Redirect("Index");
        }

        public IActionResult EliminarCliente(int id)
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (rol == "Admin")
            {
                repoClientes.DeleteCliente(id);
                return Redirect("Index");
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
