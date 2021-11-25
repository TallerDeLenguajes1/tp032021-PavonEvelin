﻿using AutoMapper;
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
    public class CadeteController : Controller
    {
        static int id;
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
            var listadoCadetesVM = mapper.Map<List<CadeteViewModel>>(repoCadetes.getAll());
            
            return View(listadoCadetesVM);
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
            repoCadetes.DeleteCadete(id);
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

        public IActionResult ModificarCadete2(int id, string nombre, string direccion, string telefono)
        {
            Cadete cadeteAModificar = null;
            foreach (var cadete in repoCadetes.getAll())
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
                //_DB.ModificarCadete(cadeteAModificar);
                repoCadetes.UpdateCadete(cadeteAModificar);
            }
            return Redirect("Index");
        }
    }
}