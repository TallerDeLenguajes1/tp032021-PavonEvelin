using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Controllers
{
    public class CadeteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public void altaCadetes(string nombreCadeteria, int id, string nombre, string direccion, string telefono)
        {
            try
            {
                Cadeteria cadeteria = new Cadeteria(nombreCadeteria, id, nombre, direccion, telefono);
                Console.WriteLine("Agregado exitoso!\n\n");
                cadeteria.mostrarCadetes();
                
            }
            catch (Exception)
            {
                Console.WriteLine("Ingreso de datos invalido\n");
            
            }

        }
    }
}
