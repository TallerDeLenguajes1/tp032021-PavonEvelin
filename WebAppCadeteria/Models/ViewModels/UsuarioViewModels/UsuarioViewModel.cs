using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models.ViewModels.UsuarioViewModels
{
    /*
    public enum Rol
    {
        Admin = 2,
        Cadete = 1,
        Cliente = 0
    }*/
    public class UsuarioViewModel
    {
        

        private int id;


        [Required(ErrorMessage = "El campo Nombre de usuario es requerido")]
        [Display(Name = "Usuario")]
        [StringLength(30)]
        private string userName;


        [Required(ErrorMessage = "El campo Password es requerido")]
        [Display(Name = "Password")]
        [StringLength(80)]
        private string password;


        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [Display(Name = "Nombre")]
        [StringLength(100)]
        private string nombre;

        [StringLength(80)]
        private string direccion;

        [StringLength(15)]
        private string telefono;

        [Required(ErrorMessage = "El campo Rol es requerido")]
        [StringLength(7)]
        private string rol;

        public int Id { get => id; set => id = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Rol { get => rol; set => rol = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
    }
}
