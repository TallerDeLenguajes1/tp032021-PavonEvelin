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
            return View(_DB);
        }

        public IActionResult CrearPedido()
        {
            return View(_DB.GetCadetes());
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
                    //_DB.Cadeteria.Pedidos.Add(pedido);
                    _DB.SavePedido(pedido);
                }
                return Redirect("Index");
            }
            catch (Exception)
            {
                Console.WriteLine("Ingreso de datos invalido\n");
                throw;
            }
        }

        public IActionResult AsignarCadeteAPedido(int IdCadete, int IdPedido)
        {
            QuitarPedidoDeCadete(IdPedido);
            if (IdCadete != 0)
            {
                Cadete cadete = _DB.GetCadetes().Where(a => a.Id == IdCadete).First();
                Pedidos pedido = _DB.GetPedidos().Where(a => a.Numero == IdPedido).First();
                cadete.ListaDePedidos.Add(pedido);
            }
            
            return Redirect("Index");
        }

        public void QuitarPedidoDeCadete(int IdPedido)
        {
            Pedidos pedido = _DB.GetPedidos().Where(pedid => pedid.Numero == IdPedido).First();
            foreach (var cadete in _DB.GetCadetes())
            {
                cadete.ListaDePedidos.Remove(pedido);
            }
        }
    }
}
