using System.Collections.Generic;

namespace WebApp_Cadeteria.Models
{
    public interface IRepositorioPedido
    {
        void AsignarCadeteAlPedido(Cadete cadete, Pedidos pedido);
        void DeletePedido(Pedidos pedido);
        List<Pedidos> getAll();
        void QuitarPedidoAlCadete(Pedidos pedido);
        void SavePedido(Pedidos pedido);
        void UpdatePedido(Pedidos pedido);
    }
}