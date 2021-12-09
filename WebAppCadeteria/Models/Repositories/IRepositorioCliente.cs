using System.Collections.Generic;
using WebApp_Cadeteria.Models.Entities;

namespace WebApp_Cadeteria.Models.Repositories
{
    public interface IRepositorioCliente
    {
        void DeleteCliente(int id_cliente);
        List<Cliente> getAll();
        Cliente GetCliente(string nombre, string direccion);
        Cliente GetClientePorId(int idCliente);
        int GetIdCliente(string nombre_cliente, string direccion);
        int GetIdClienteByIdUser(int idUsuario);
        void SaveCliente(Cliente cliente, int id_usuario);
        void UpdateCliente(Cliente cliente);
    }
}