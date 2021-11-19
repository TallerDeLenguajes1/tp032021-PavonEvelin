using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models;
using WebApp_Cadeteria.Models.ViewModels;
using AutoMapper;

namespace WebApp_Cadeteria.Controllers
{
    public class PedidoController : Controller
    {

        static int numPedido;
        //static int idCliente = 0;
        private readonly ILogger<PedidoController> _logger;
        private readonly IRepositorioPedido repoPedidos;
        private readonly IRepositorioCadete repoCadetes;
        private readonly IMapper mapper;

        //private readonly DBTemporal _DB;
        //private readonly DatosPedido DatosPedido;
        public PedidoController(ILogger<PedidoController> logger, IRepositorioPedido RepoPedidos, IRepositorioCadete RepoCadetes, IMapper mapper)
        {
            _logger = logger;
            repoPedidos = RepoPedidos;
            repoCadetes = RepoCadetes;
            this.mapper = mapper;
            //_DB = DB;
            //DatosPedido = datosPedido;
        }

        public IActionResult Index()
        {
            var ListarPedidosViewModel = new ListarPedidosViewModel();
            ListarPedidosViewModel.listaCadetes  = mapper.Map<List<CadeteViewModel>>(repoCadetes.getAll());
            ListarPedidosViewModel.listaPedidos =mapper.Map<List<PedidosViewModel>>(repoPedidos.getAll());
            foreach (var item in ListarPedidosViewModel.listaPedidos)
            {
                item.idCadete = repoPedidos.TieneElPedidoUnCadete(item.Numero);
            }
            return View(ListarPedidosViewModel);
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
            QuitarPedidoDeCadete(IdPedido);
            if (IdCadete != 0)
            {
                repoPedidos.AsignarCadeteAlPedido(IdCadete, IdPedido);
                //cadete.ListaDePedidos.Add(pedido);
                //_DB.AsignarPedidoAlCadete(cadete);
            }
            
            return Redirect("Index");
        }

        public void QuitarPedidoDeCadete(int IdPedido)
        {
            repoPedidos.QuitarPedidoAlCadete(IdPedido);
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