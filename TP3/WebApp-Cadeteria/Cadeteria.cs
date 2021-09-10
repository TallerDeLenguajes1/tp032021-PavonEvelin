using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria
{
    public class Cadeteria
    {
        private string nombre;
        private List<Cadete> listadoDeCadetes;

        public string Nombre { get => nombre; set => nombre = value; }
        internal List<Cadete> ListadoDeCadetes { get => listadoDeCadetes; set => listadoDeCadetes = value; }

        public Cadeteria(string nombreCadeteria, int id, string nombre, string direccion, string telefono)
        {
            this.nombre = nombreCadeteria;
            Cadete c = new Cadete(id, nombre, direccion, telefono);
            listadoDeCadetes.Add(c);
        }


        public void mostrarCadetes()
        {
            Console.WriteLine("Listado de cadetes");
            foreach (var c in listadoDeCadetes)
            {
                c.mostrarInfo();
            }
        }
    }
}
