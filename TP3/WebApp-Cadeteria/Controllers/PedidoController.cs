using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models;

namespace WebApp_Cadeteria.Controllers
{
    public class PedidoController : Controller
    {
        
        static int numPedido = 0;
        static int idCliente = 0;
        private readonly ILogger<PedidoController> _logger;
        private readonly DBTemporal _DB;
        public PedidoController(ILogger<PedidoController> logger, DBTemporal DB)
        {
            _logger = logger;
            _DB = DB;
        }
        
        public IActionResult Index()
        {
            return View(_DB.Cadeteria.Pedidos);
        }

        public IActionResult CrearPedido()
        {
            return View(_DB.Cadeteria.Cadetes);
        }

        public IActionResult altaPedidos(string observacion, string estado, string nombreCliente, string direccion, string telefono, string nombreCadete)
        {
            try
            {
                if (observacion != null && nombreCliente != null && estado != null && nombreCadete != null)
                {
                    
                    idCliente++;
                    numPedido++;
                    Pedidos pedido = new Pedidos(numPedido, observacion, estado, idCliente, nombreCliente, direccion, telefono);
                    _DB.Cadeteria.Pedidos.Add(pedido);
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
