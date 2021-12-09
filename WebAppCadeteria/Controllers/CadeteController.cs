using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models.Entities;
using WebApp_Cadeteria.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebApp_Cadeteria.Models.Repositories;
using WebApp_Cadeteria.Models.ViewModels.CadeteViewModels;

namespace WebApp_Cadeteria.Controllers
{
    public class CadeteController : Controller
    {
        private readonly ILogger<CadeteController> _logger;
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public CadeteController(ILogger<CadeteController> logger, DataContext DataContext, IMapper mapper)
        {
            _logger = logger;
            dataContext = DataContext;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            List<Cadete> listadoCadetes = dataContext.Cadetes.getAll();
            var listaCadetesViewModel = mapper.Map<List<CadeteViewModel>>(listadoCadetes);
            return View(listaCadetesViewModel);
        }


        public IActionResult CrearCadete()
        {
            string rol = HttpContext.Session.GetString("Rol");
            if(rol == "Admin")
            {
                return RedirectToAction("CrearUsuario", "Usuario");
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            //Enum.TryParse(rol, out Roles myStatus);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        /*
        public IActionResult altaCadetes(CadeteViewModel cadete)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Cadete NewCadete = mapper.Map<Cadete>(cadete);
                    repoCadetes.SaveCadete(NewCadete);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(CrearCadete));
                }
                
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                return RedirectToAction(nameof(CrearCadete));
                //throw;
            }

        }*/

        public IActionResult EliminarCadete(int id)
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (rol == "Admin")
            {
                dataContext.Cadetes.DeleteCadete(id);
                return Redirect("Index");
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
        }

        public IActionResult ModificarCadete(int id)
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (rol == "Admin" || rol == "Cadete")
            {
                CadeteViewModel cadeteAModificar = mapper.Map<CadeteViewModel>(dataContext.Cadetes.GetCadetePorId(id));
                if (cadeteAModificar != null)
                {
                    return View(cadeteAModificar);
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
        public IActionResult ModificarCadete2(CadeteViewModel cadete)
        {
            Cadete cadeteAModificar = mapper.Map<Cadete>(cadete);

            if (cadeteAModificar != null)
            {
                dataContext.Cadetes.UpdateCadete(cadeteAModificar);
            }
            return Redirect("Index");
        }
    }
}