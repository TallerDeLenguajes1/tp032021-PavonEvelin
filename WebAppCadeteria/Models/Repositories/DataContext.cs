using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models.Repositories
{
    public class DataContext
    {
        public IRepositorioCadete Cadetes { get; set; }
        public IRepositorioPedido Pedidos { get; set; }

        public IRepositorioUsuario Usuarios { get; set; }
        public IRepositorioCliente Clientes { get; set; }

        public DataContext(IRepositorioCadete Cadetes, IRepositorioPedido Pedidos, IRepositorioUsuario Usuarios, IRepositorioCliente Clientes)
        {
            this.Cadetes = Cadetes;
            this.Pedidos = Pedidos;
            this.Usuarios = Usuarios;
            this.Clientes = Clientes;
        }
    }
}
