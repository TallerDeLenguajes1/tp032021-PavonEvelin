using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models;
using WebApp_Cadeteria.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebApp_Cadeteria.Controllers
{
    public class CadeteController : Controller
    {
        //static int id;
        private readonly ILogger<CadeteController> _logger;
        //private readonly DBTemporal _DB;
        private readonly IRepositorioCadete repoCadetes;
        private readonly IMapper mapper;

        public CadeteController(ILogger<CadeteController> logger, IRepositorioCadete RepoCadetes, IMapper mapper)
        {
            _logger = logger;
            //_DB = DB;
            repoCadetes = RepoCadetes;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            List<Cadete> listadoCadetes = repoCadetes.getAll();
            var listaCadetesViewModel = mapper.Map<List<CadeteViewModel>>(listadoCadetes);
            return View(listaCadetesViewModel);
        }


        public IActionResult CrearCadete()
        {
            string rol = HttpContext.Session.GetString("Rol");
            if(rol == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            //Enum.TryParse(rol, out Roles myStatus);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            catch (Exception)
            {
                //usar nlog
                throw;
            }

        }

        public IActionResult EliminarCadete(int id)
        {
            repoCadetes.DeleteCadete(id);
            return Redirect("Index");
        }

        public IActionResult ModificarCadete(int id)
        {
            CadeteViewModel cadeteAModificar = mapper.Map<CadeteViewModel>(repoCadetes.GetCadetePorId(id));
            if (cadeteAModificar != null)
            {
                return View(cadeteAModificar);
            }
            else
            {
                return Redirect("Index");
            }

        }

        [HttpPost]
        public IActionResult ModificarCadete2(CadeteViewModel cadete)
        {
            Cadete cadeteAModificar = mapper.Map<Cadete>(cadete);

            if (cadeteAModificar != null)
            {
                repoCadetes.UpdateCadete(cadeteAModificar);
            }
            return Redirect("Index");
        }
    }
}