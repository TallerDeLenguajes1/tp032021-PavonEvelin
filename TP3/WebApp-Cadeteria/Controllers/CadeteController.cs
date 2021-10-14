using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models;

namespace WebApp_Cadeteria.Controllers
{
    public class CadeteController : Controller
    {
        static int id;
        private readonly ILogger<CadeteController> _logger;
        private readonly DBTemporal _DB;
        public CadeteController(ILogger<CadeteController> logger, DBTemporal DB)
        {
            _logger = logger;
            _DB = DB;
        }
        public IActionResult Index()
        {
            return View(_DB.GetCadetes());
        }

        public IActionResult CrearCadete()
        {
            return View();
        }

        public IActionResult altaCadetes(string nombre, string direccion, string telefono)
        {
            try
            {
                if (nombre != null)
                {
                    id = (_DB.GetCadetes().Count() + 1);
                    Cadete cadete = new Cadete(id, nombre, direccion, telefono);
                    //_DB.Cadeteria.Cadetes.Add(cadete);
                    _DB.SaveCadete(cadete);
                }
                return Redirect("Index");
            }
            catch (Exception)
            {
                Console.WriteLine("Ingreso de datos invalido\n");
                throw;
            }

        }
    }
}
