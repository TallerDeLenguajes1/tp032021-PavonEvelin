using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models.ViewModels;

namespace WebApp_Cadeteria.Models
{
    public class MapperCadete
    {
        public MapperCadete()
        {
        }

        public CadeteViewModel MapperCadeteToVM(Cadete cadete)
        {
            return new CadeteViewModel()
            {
                Id = cadete.Id,
                Nombre = cadete.Nombre,
                Direccion = cadete.Direccion,
                Telefono = cadete.Telefono
            };
        }
    }
}
