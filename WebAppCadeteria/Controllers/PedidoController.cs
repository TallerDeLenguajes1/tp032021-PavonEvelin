using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models;
using WebApp_Cadeteria.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace WebApp_Cadeteria.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IRepositorioPedido repoPedidos;
        private readonly IRepositorioCadete repoCadetes;
        private readonly RepositorioCliente repoCliente;
        private readonly IMapper mapper;

        //private readonly DBTemporal _DB;
        //private readonly DatosPedido DatosPedido;
        public PedidoController(ILogger<PedidoController> logger, IRepositorioPedido RepoPedidos, IRepositorioCadete RepoCadetes,RepositorioCliente RepoCliente, IMapper mapper)
        {
            _logger = logger;
            repoPedidos = RepoPedidos;
            repoCadetes = RepoCadetes;
            repoCliente = RepoCliente;
            this.mapper = mapper;
            //_DB = DB;
            //DatosPedido = datosPedido;
        }
         
        public IActionResult Index()
        {
            var ListarPedidosViewModel = new ListarPedidosViewModel();
            ListarPedidosViewModel.listaCadetes  = mapper.Map<List<CadeteViewModel>>(repoCadetes.getAll());
            ListarPedidosViewModel.listaPedidos =mapper.Map<List<PedidoViewModel>>(repoPedidos.getAll());
            foreach (var item in ListarPedidosViewModel.listaPedidos)
            {
                item.idCadete = repoPedidos.TieneElPedidoUnCadete(item.Numero);
            }
            return View(ListarPedidosViewModel);
        }

        public IActionResult CrearPedido()
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (rol == "Admin" || rol == "Cadete")
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult altaPedidos(PedidoViewModel pedidoVM, string nombreCliente, string direccion, string telefono)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Pedidos newPedido = mapper.Map<Pedidos>(pedidoVM);
                    newPedido.Cliente = repoCliente.GetCliente(nombreCliente, direccion);
                    if (newPedido.Cliente == null)
                    {
                        newPedido.Cliente.Nombre = nombreCliente;
                        newPedido.Cliente.Telefono = telefono;
                        newPedido.Cliente.Direccion = direccion;
                        repoCliente.SaveCliente(newPedido.Cliente);
                        newPedido.Cliente = repoCliente.GetCliente(nombreCliente, direccion);
                    }
                    repoPedidos.SavePedido(newPedido);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(CrearPedido));
                }
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                return RedirectToAction(nameof(CrearPedido));
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