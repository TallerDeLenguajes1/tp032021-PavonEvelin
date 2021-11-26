using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models
{
    /*
    public enum Rol
    {
        Admin = 2,
        Cadete = 1,
        Cliente = 0
    }*/
    public class Usuario
    {
        private int id;
        private string userName;
        private string password;
        private string nombre;
        private string direccion;
        private string telefono;
        private string rol;

        public Usuario()
        {

        }

        public Usuario(int id, string userName, string password, string nombre, string direccion, string telefono, string rol)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.rol = rol;
        }
        public int Id { get => id; set => id = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Rol { get => rol; set => rol = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
    }
}