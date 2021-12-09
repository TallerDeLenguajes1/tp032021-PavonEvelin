using WebApp_Cadeteria.Models.Entities;

namespace WebApp_Cadeteria.Models.Repositories
{
    public interface IRepositorioUsuario
    {
        bool ValidateUser(Usuario usuario);
        void SaveUser(Usuario usuario);
        void UpdateUser(Usuario usuario);
        void DeleteUser(int id_usuario);
        int GetUserID(Usuario usuario);
        Usuario GetUser(string userName, string password);
    }
}