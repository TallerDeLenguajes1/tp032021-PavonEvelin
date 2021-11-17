using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models
{
    public interface IRepositorioCadete
    {
        List<Cadete> getAll();
        void SaveCadete(Cadete cadete);
        void UpdateCadete(Cadete cadete);
        void DeleteCadete(int p_id_cadete); 
    }
}