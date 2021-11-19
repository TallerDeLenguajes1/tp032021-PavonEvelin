﻿using System.Collections.Generic;

namespace WebApp_Cadeteria.Models
{
    public interface IRepositorioPedido
    {
        public void AsignarCadeteAlPedido(int idCadete, int idPedido);
        void DeletePedido(Pedidos pedido);
        List<Pedidos> getAll();
        void QuitarPedidoAlCadete(int idPedido);
        void SavePedido(Pedidos pedido);
        void UpdatePedido(Pedidos pedido);
        Pedidos GetPedidoPorId(int idPedido);
        int TieneElPedidoUnCadete( int idPedido);
        
    }
}