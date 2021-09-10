using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models;


namespace WebApp_Cadeteria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public string altaCadetes(string nombreCadeteria, int id, string nombre, string direccion, string telefono)
        {
            try
            {
                Cadeteria cadeteria = new Cadeteria(nombreCadeteria, id, nombre, direccion, telefono);
                cadeteria.agregarCadete(id, nombre, direccion, telefono);
                //Console.WriteLine("Agregado exitoso!\n\n");
                cadeteria.mostrarCadetes();
                return "Agregado exitoso!\n";
            }
            catch (Exception)
            {
                //Console.WriteLine("Ingreso de datos invalido\n");
                return "Ingreso de datos invalido\n";
            }
            
        }

        public IActionResult mostrarCadetes()
        {
   
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
