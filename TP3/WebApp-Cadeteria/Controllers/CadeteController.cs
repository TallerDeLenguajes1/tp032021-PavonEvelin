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
        //private readonly DBTemporal _DB;
        private readonly RepositorioCadete repoCadetes;

        public CadeteController(ILogger<CadeteController> logger, RepositorioCadete RepoCadetes)
        {
            _logger = logger;
            //_DB = DB;
            repoCadetes = RepoCadetes;
        }
        public IActionResult Index()
        {
            return View(repoCadetes.getAll());
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
                    id = (repoCadetes.getAll().Count() + 1);
                    //id = (_DB.GetCadetes().Count() + 1);
                    Cadete cadete = new Cadete(id, nombre, direccion, telefono);
                    repoCadetes.SaveCadete(cadete);
                    //_DB.Cadeteria.Cadetes.Add(cadete);
                    //_DB.SaveCadete(cadete);
                }
                return Redirect("Index");
            }
            catch (Exception)
            {
                Console.WriteLine("Ingreso de datos invalido\n");
                throw;
            }

        }

        public IActionResult EliminarCadete(int id)
        {
            //_DB.EliminarCadete(id);
            return Redirect("Index");
        }

        public IActionResult ModificarCadete(int id)
        {
            Cadete cadeteADevolver = null;
            foreach (var cadete in repoCadetes.getAll())
            {
                if (cadete.Id == id)
                {
                    cadeteADevolver = cadete;
                    break;
                }
            }

            if (cadeteADevolver != null)
            {
                return View(cadeteADevolver);
            }
            else
            {
                return Redirect("Index");
            }
            
        }

        /*

        public IActionResult ModificarCadete2(int id, string nombre, string direccion, string telefono)
        {
            Cadete cadeteAModificar = null;
            foreach (var cadete in _DB.GetCadetes())
            {
                if (cadete.Id == id)
                {
                    cadeteAModificar = cadete;
                    break;
                }
            }

            if (cadeteAModificar != null)
            {
                cadeteAModificar.Nombre = nombre;
                cadeteAModificar.Direccion = direccion;
                cadeteAModificar.Telefono = telefono;
                _DB.ModificarCadete(cadeteAModificar);
            }

            return Redirect("Index");
        }*/
    }
}