using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models;

namespace WebApp_Cadeteria.Controllers
{
    public class CadeteriaController : Controller
    {
        private readonly ILogger<CadeteriaController> _logger;
        private readonly DBTemporal dB;
        public CadeteriaController(ILogger<CadeteriaController> logger, DBTemporal DB)
        {
            _logger = logger;
            dB = DB;
        }
        /*
        public IActionResult Index()
        {
            return View(dB.Cadeteria.Cadetes);
        }*/
    }
}
