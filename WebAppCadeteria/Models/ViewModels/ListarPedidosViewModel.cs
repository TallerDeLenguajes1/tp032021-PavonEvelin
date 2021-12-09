using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models.ViewModels
{
    public class ListarPedidosViewModel
    {
        public List<CadeteViewModel> listaCadetes { get; set; } 
        public List<PedidoViewModel> listaPedidos { get; set; }
    }
}
