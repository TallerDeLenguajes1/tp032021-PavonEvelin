using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models
{
    public enum Rol
    {
        Admin = 2,
        Cadete = 1,
        Cliente = 0
    }
    public class Usuario
    {
        private int id;
        private string userName;
        private string password;
        private string nombre;
        private Rol rol;

        public int Id { get => id; set => id = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public Rol Rol { get => rol; set => rol = value; }
    }
}