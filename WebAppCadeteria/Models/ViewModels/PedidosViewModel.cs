using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models.ViewModels
{
    public class PedidosViewModel
    {
        private int numero;
        private string observacion;
        private string estado;
        private Cliente cliente;

        public int Numero { get => numero; set => numero = value; }
        public string Observacion { get => observacion; set => observacion = value; }
        public string Estado { get => estado; set => estado = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
       
        public int idCadete {get; set;}

    }
}
