using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models.Entities
{
    public class Cadeteria
    {
        public List<Cadete> Cadetes { get; set; }
        public List<Pedidos> Pedidos { get; set; }

        public Cadeteria()
        {
            Cadetes = new List<Cadete>();
            Pedidos = new List<Pedidos>();
        }
    }
}
