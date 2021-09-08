using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria
{
    public class Cadete
    {
        static int jornal = 0;
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;
        private List<Pedidos> listaDePedidos;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        internal List<Pedidos> ListaDePedidos { get => listaDePedidos; set => listaDePedidos = value; }

        public Cadete(int id, string nombre, string direccion, string telefono)
        {
            this.id = id;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
        }

        public void CargarPedido(Pedidos p_pedido)
        {
            listaDePedidos.Add(p_pedido);
            jornal += 100;
        }

        public void mostrarInfo()
        {
            Console.WriteLine($"Id: {this.id}\n");
            Console.WriteLine($"Id: {this.nombre}\n");
            Console.WriteLine($"Id: {this.direccion}\n");
            Console.WriteLine($"Id: {this.telefono}\n");
        }
    }
}
