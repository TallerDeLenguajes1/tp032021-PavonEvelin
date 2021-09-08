using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadeteria
{
    class Cadeteria
    {
        private string nombre;
        private List<Cadete> listadoDeCadetes;

        public string Nombre { get => nombre; set => nombre = value; }
        internal List<Cadete> ListadoDeCadetes { get => listadoDeCadetes; set => listadoDeCadetes = value; }

        public Cadeteria(string nombre)
        {
            this.nombre = nombre;
        }

        public void agregarCadete(int id, string nombre, string direccion, string telefono)
        {
            Cadete c = new Cadete(id, nombre, direccion, telefono);
            listadoDeCadetes.Add(c);
        }
    }
}
