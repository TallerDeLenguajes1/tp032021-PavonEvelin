using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models
{
    public interface IRepositorioCadete
    {
        List<Cadete> getAll();
        Cadete GetCadetePorId(int idCadete);
        void SaveCadete(Cadete cadete, int id_usuario);
        void UpdateCadete(Cadete cadete);
        void DeleteCadete(int p_id_cadete);

        int GetIdCadeteByIdUser(int idUsuario);
    }
}