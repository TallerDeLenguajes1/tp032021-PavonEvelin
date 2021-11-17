using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models
{
    public class Usuarios
    {
        private string nombre;
        private int id;
        private string password;

        public string Nombre { get => nombre; set => nombre = value; }
        public int Id { get => id; set => id = value; }
        public string Password { get => password; set => password = value; }
    }
}