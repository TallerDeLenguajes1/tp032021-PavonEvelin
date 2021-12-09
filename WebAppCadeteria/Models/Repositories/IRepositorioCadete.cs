using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models.Entities;

namespace WebApp_Cadeteria.Models.Repositories
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