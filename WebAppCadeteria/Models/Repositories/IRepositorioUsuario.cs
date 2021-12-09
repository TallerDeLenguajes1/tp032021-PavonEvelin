using WebApp_Cadeteria.Models.Entities;

namespace WebApp_Cadeteria.Models.Repositories
{
    public interface IRepositorioUsuario
    {
        void DeleteUser(int id_usuario);
        Usuario GetUser(string userName, string password);
        int GetUserID(Usuario usuario);
        void SaveUser(Usuario usuario);
        void UpdateUser(Usuario usuario);
        bool ValidateUser(Usuario usuario);
    }
}