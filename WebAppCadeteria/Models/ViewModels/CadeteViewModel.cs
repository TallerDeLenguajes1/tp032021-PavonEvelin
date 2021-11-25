using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models.ViewModels
{
    public class CadeteViewModel
    {
        public int id;

        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [Display(Name = "Nombre")]
        [StringLength(100)]
        public string nombre;
        
        [StringLength(80)]
        public string direccion;
        
        [StringLength(15)]
        public string telefono;
        
        public List<PedidosViewModel> listaDePedidos;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public List<PedidosViewModel> ListaDePedidos { get => listaDePedidos; set => listaDePedidos = value; }
    }
}
