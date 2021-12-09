using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models.ViewModels.PedidoViewModels;

namespace WebApp_Cadeteria.Models.ViewModels.CadeteViewModels
{
    public class CadeteViewModel
    {
        private int id;

        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [Display(Name = "Nombre")]
        [StringLength(100)]
        private string nombre;
        
        [StringLength(80)]
        private string direccion;
        
        [StringLength(15)]
        private string telefono;

        private List<PedidoViewModel> listaDePedidos;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public List<PedidoViewModel> ListaDePedidos { get => listaDePedidos; set => listaDePedidos = value; }
    }
}
