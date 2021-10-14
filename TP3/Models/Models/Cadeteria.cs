using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCadeteria
{
    public class Cadeteria
    {
        private string nombre;
        private List<Cadete> listadoDeCadetes;

        public string Nombre { get => nombre; set => nombre = value; }
        public List<Cadete> ListadoDeCadetes { get => listadoDeCadetes; set => listadoDeCadetes = value; }

        public Cadeteria(string nombreCadeteria)
        {
            this.nombre = nombreCadeteria;
            listadoDeCadetes = new List<Cadete>();
        }

        public void AgregarCadete(Cadete cadete)
        {
            listadoDeCadetes.Add(cadete);
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
