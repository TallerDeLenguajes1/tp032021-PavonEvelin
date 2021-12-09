using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models;
using Microsoft.AspNetCore.Http;
using WebApp_Cadeteria.Models.Repositories;
using WebApp_Cadeteria.Models.ViewModels.PedidoViewModels;
using WebApp_Cadeteria.Models.ViewModels.CadeteViewModels;
using WebApp_Cadeteria.Models.Entities;

namespace WebApp_Cadeteria.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        //private readonly DBTemporal _DB;
        //private readonly DatosPedido DatosPedido;
        public PedidoController(ILogger<PedidoController> logger, DataContext DataContext, IMapper mapper)
        {
            _logger = logger;
            dataContext = DataContext;
            this.mapper = mapper;
        }
         
        public IActionResult Index()
        {
            var ListarPedidosViewModel = new ListarPedidosViewModel();
            ListarPedidosViewModel.listaCadetes  = mapper.Map<List<CadeteViewModel>>(dataContext.Cadetes.getAll());
            ListarPedidosViewModel.listaPedidos =mapper.Map<List<PedidoViewModel>>(dataContext.Pedidos.getAll());
            foreach (var item in ListarPedidosViewModel.listaPedidos)
            {
                item.idCadete = dataContext.Pedidos.TieneElPedidoUnCadete(item.Numero);
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
                    newPedido.Cliente = dataContext.Clientes.GetCliente(nombreCliente, direccion);
                    if (newPedido.Cliente == null)
                    {
                        newPedido.Cliente.Nombre = nombreCliente;
                        newPedido.Cliente.Telefono = telefono;
                        newPedido.Cliente.Direccion = direccion;
                        dataContext.Clientes.SaveCliente(newPedido.Cliente, -1);
                        newPedido.Cliente = dataContext.Clientes.GetCliente(nombreCliente, direccion);
                    }
                    dataContext.Pedidos.SavePedido(newPedido);
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
                dataContext.Pedidos.AsignarCadeteAlPedido(IdCadete, IdPedido);
                //cadete.ListaDePedidos.Add(pedido);
                //_DB.AsignarPedidoAlCadete(cadete);
            }
            
            return Redirect("Index");
        }

        public void QuitarPedidoDeCadete(int IdPedido)
        {
            dataContext.Pedidos.QuitarPedidoAlCadete(IdPedido);
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