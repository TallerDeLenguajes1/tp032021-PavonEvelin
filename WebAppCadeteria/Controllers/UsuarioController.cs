using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
                    switch (usuario.Rol)
                    {
                        case Roles.Admin:
                            return View("../Admin/AdminPage");
                        case Roles.Cadete:
                            int id_cadete = repoCadetes.GetIdCadeteByIdUser(usuario.Id);
                            return
                        case Rol.Cliente:
                            return 
                        default:
                            return View();
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
    }
}