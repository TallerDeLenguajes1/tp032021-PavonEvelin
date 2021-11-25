using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models.ViewModels
{
   
    public class UsuarioViewModel
    {
        public enum Roles
        {
            Admin = 2,
            Cadete = 1,
            Cliente = 0
        }

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

        [Required(ErrorMessage = "El campo Rol es requerido")]
        private Rol rol;

        public int Id { get => id; set => id = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public Rol Rol { get => rol; set => rol = value; }
    }
}
