using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models.ViewModels
{
    public class CrearCadeteViewModel
    {
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [Display(Name = "Nombre")]
        [StringLength(100)]
        public string nombre { get; set; }

        [StringLength(80)]
        public string direccion { get; set; }

        [StringLength(15)]
        public string telefono { get; set; }
    }
}
