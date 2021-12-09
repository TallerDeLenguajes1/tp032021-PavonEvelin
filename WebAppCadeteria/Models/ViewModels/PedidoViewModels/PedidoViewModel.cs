using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models.ViewModels.ClienteViewModels;

namespace WebApp_Cadeteria.Models.ViewModels.PedidoViewModels
{
    public class PedidoViewModel
    {
        private int numero;
        private string observacion;
        private string estado;
        private ClienteViewModel cliente;
        public int idCadete { get; set; }

        public int Numero { get => numero; set => numero = value; }
        public string Observacion { get => observacion; set => observacion = value; }
        public string Estado { get => estado; set => estado = value; }
        public ClienteViewModel Cliente { get => cliente; set => cliente = value; }
       


    }
}
