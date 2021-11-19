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
    public class PedidoController : Controller
    {

        static int numPedido;
        //static int idCliente = 0;
        private readonly ILogger<PedidoController> _logger;
        private readonly RepositorioPedido repoPedidos;

        //private readonly DBTemporal _DB;
        //private readonly DatosPedido DatosPedido;
        public PedidoController(ILogger<PedidoController> logger, RepositorioPedido RepoPedidos)
        {
            _logger = logger;
            repoPedidos = RepoPedidos;
            //_DB = DB;
            //DatosPedido = datosPedido;
        }

        public IActionResult Index()
        {
            return View(new ListarPedidosViewModel());
        }

        public IActionResult CrearPedido()
        {
            return View();
        }

        public IActionResult altaPedidos(string observacion, string estado, int idCliente, string nombreCliente, string direccion, string telefono)
        {
            try
            {
                if (observacion != null && nombreCliente != null && estado != null)
                {
                    numPedido = (repoPedidos.getAll().Count() + 1);
                    Pedidos pedido = new Pedidos(numPedido, observacion, estado, idCliente, nombreCliente, direccion, telefono);
                    repoPedidos.SavePedido(pedido);
                    //_DB.SavePedido(pedido);
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
            ListarPedidosViewModel cadetesPedidosVM = new ListarPedidosViewModel(); //Mmmmmmmm
            QuitarPedidoDeCadete(IdPedido);
            if (IdCadete != 0)
            {
                Cadete cadete = cadetesPedidosVM.repoCadete.GetCadetePorId(IdCadete);
                Pedidos pedido = repoPedidos.GetPedidoPorId(IdPedido);
                repoPedidos.AsignarCadeteAlPedido(cadete, pedido);
                //cadete.ListaDePedidos.Add(pedido);
                //_DB.AsignarPedidoAlCadete(cadete);
            }
            
            return Redirect("Index");
        }

        public void QuitarPedidoDeCadete(int IdPedido)
        {
            Pedidos pedido = repoPedidos.GetPedidoPorId(IdPedido);
            repoPedidos.QuitarPedidoAlCadete(pedido);
            /*
            foreach (var cadete in _DB.GetCadetes())
            {
                cadete.ListaDePedidos.Remove(pedido);
            }

            _DB.GetCadetes().ForEach(cad => cad.ListaDePedidos.Remove(pedido));
            */
        }
    }
}