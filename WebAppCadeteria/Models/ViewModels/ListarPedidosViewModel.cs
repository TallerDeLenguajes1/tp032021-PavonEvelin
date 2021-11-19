using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models.ViewModels
{
    public class ListarPedidosViewModel
    {
        public RepositorioCadete repoCadete { get; set; }
        public RepositorioPedido repoPedido { get; set; }
    }
}
