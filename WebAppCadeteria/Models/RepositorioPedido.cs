using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models
{
    public class RepositorioPedido : IRepositorioPedido
    {
        private readonly string connectionString;
        //private readonly SQLiteConnection conexion;

        public RepositorioPedido(string connectionString)
        {
            this.connectionString = connectionString;
            //conexion = new SQLiteConnection(connectionString);
        }

        public List<Pedidos> getAll()
        {
            List<Pedidos> ListadoPedidos = new List<Pedidos>();

            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                conexion.Open();
                string SQLQuery = "SELECT * FROM pedidos INNER JOIN clientes USING('id_cliente')";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion);
                using (SQLiteDataReader DataReader = command.ExecuteReader())
                {
                    while (DataReader.Read())
                    {
                        Pedidos pedido = new Pedidos(Convert.ToInt32(DataReader["id_pedido"]),
                                                    DataReader["observacion"].ToString(),
                                                    DataReader["estado_pedido"].ToString(),
                                                    Convert.ToInt32(DataReader["id_cliente"]),
                                                    DataReader["nombre_cliente"].ToString(),
                                                    DataReader["direccion_cliente"].ToString(),
                                                    DataReader["telefono_cliente"].ToString());
                        /*var pedido2 = new Pedidos()
                        {
                          Numero =  Convert.ToInt32(DataReader["id_pedido"]),
                          Observacion =  DataReader["observacion"].ToString(),
                          Estado = DataReader["estado_pedido"].ToString(),
                          Cliente = 
                        };*/
                        ListadoPedidos.Add(pedido);
                    }
                    conexion.Close();
                }
            }
            return ListadoPedidos;
        }

        public void SavePedido(Pedidos pedido)
        {
            string SQLQuery = "INSERT INTO pedidos VALUES(@id_pedido, @observacion, @estado_pedido, 1,@id_cliente, NULL)";
            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@id_pedido", pedido.Numero);
                    command.Parameters.AddWithValue("@observacion", pedido.Observacion);
                    command.Parameters.AddWithValue("@estado_pedido", pedido.Estado);
                    command.Parameters.AddWithValue("@id_cliente", pedido.Cliente.Id);
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }

        public Pedidos GetPedidoPorId(int idPedido)
        {
            Pedidos pedidoADevolver = new Pedidos();
            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                conexion.Open();
                string SQLQuery = "SELECT * FROM pedidos WHERE id_pedido = @idPedido";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion);
                using (SQLiteDataReader DataReader = command.ExecuteReader())
                {
                    if (DataReader.Read())
                    {
                        pedidoADevolver = new Pedidos(Convert.ToInt32(DataReader["id_pedido"]),
                                                    DataReader["observacion"].ToString(),
                                                    DataReader["estado_pedido"].ToString(),
                                                    Convert.ToInt32(DataReader["id_cliente"]),
                                                    DataReader["nombre_cliente"].ToString(),
                                                    DataReader["direccion_cliente"].ToString(),
                                                    DataReader["telefono_cliente"].ToString());
                        //pedidoADevolver = pedido;
                    }
                    conexion.Close();
                }
            }
            return pedidoADevolver;
        }
        
        public int TieneElPedidoUnCadete( int idPedido)
        {
            int idCadete = -1; 
            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                conexion.Open();
                string SQLQuery = "SELECT id_cadete FROM pedidos WHERE id_pedido = @idPedido";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion);
                using (SQLiteDataReader DataReader = command.ExecuteReader())
                {
                    command.Parameters.AddWithValue("@idPedido", idPedido);
                    if (DataReader.Read())
                    {
                        var idCadeteObject = DataReader["id_cadete"];
                        if (idCadeteObject!=null)
                        {
                            idCadete = Convert.ToInt32(idCadeteObject);
                        }
                    }
                    DataReader.Close();
                    conexion.Close();
                }
            }
            return idCadete;
        }

        public void AsignarCadeteAlPedido(int idCadete, int idPedido)
        {
            try
            {
                string SQLQuery = "UPDATE pedidos SET id_cadete = @id_cadete WHERE id_pedido = @id_pedido";
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@id_cadete", idCadete);
                        command.Parameters.AddWithValue("@id_pedido", idPedido);
                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }

        }

        public void QuitarPedidoAlCadete(int idPedido)
        {
            try
            {
                string SQLQuery = "UPDATE pedidos SET id_cadete = NULL WHERE id_pedido = @id_pedido";
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@id_pedido", idPedido);
                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }

        }

        public void UpdatePedido(Pedidos pedido)
        {
            string SQLQuery = "UPDATE pedidos SET observacion = @observacion, estado_pedido = @estado_pedido, id_cliente = @id_cliente WHERE id_pedido = @id_pedido";
            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@observacion", pedido.Observacion);
                    command.Parameters.AddWithValue("@estado_pedido", pedido.Estado);
                    command.Parameters.AddWithValue("@id_cliente", pedido.Cliente.Id);
                    command.Parameters.AddWithValue("@id_pedido", pedido.Numero);
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }

        public void DeletePedido(Pedidos pedido)
        {
            string SQLQuery = "UPDATE pedidos SET activo_pedido = 0 WHERE id_pedido = @id_pedido";
            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@id_pedido", pedido.Numero);
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }

    }
}