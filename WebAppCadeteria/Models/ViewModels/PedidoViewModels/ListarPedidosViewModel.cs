using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models.ViewModels.CadeteViewModels;

namespace WebApp_Cadeteria.Models.ViewModels.PedidoViewModels
{
    public class ListarPedidosViewModel
    {
        public List<CadeteViewModel> listaCadetes { get; set; } 
        public List<PedidoViewModel> listaPedidos { get; set; }
    }
}
