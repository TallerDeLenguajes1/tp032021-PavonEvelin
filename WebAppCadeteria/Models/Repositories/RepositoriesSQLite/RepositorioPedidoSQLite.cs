using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models.Entities;

namespace WebApp_Cadeteria.Models.Repositories.RepositoriesSQLite
{
    public class RepositorioPedidoSQLite : IRepositorioPedido
    {
        private readonly string connectionString;
        private readonly Logger log;

        //private readonly SQLiteConnection conexion;

        public RepositorioPedidoSQLite(string connectionString, Logger log)
        {
            this.connectionString = connectionString;
            this.log = log;
            //conexion = new SQLiteConnection(connectionString);
        }

        public List<Pedidos> getAll()
        {
            List<Pedidos> ListadoPedidos = new List<Pedidos>();

            try
            {
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
                            ListadoPedidos.Add(pedido);
                        }
                        conexion.Close();
                    }
                }
                log.Info("Se obtuvieron los datos de los pedidos con exito");
                return ListadoPedidos;
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al obtener los datos de los pedidos ", mensaje);
                throw;
            }

            
        }

        public void SavePedido(Pedidos pedido)
        {
            string SQLQuery = "INSERT INTO pedidos VALUES(@id_pedido, @observacion, @estado_pedido, 1,@id_cliente, NULL)";

            try
            {
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
                log.Info("El pedido " + pedido.Numero + " se guardo exitosamente");
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al guardar el pedido " + pedido.Numero, mensaje);
                throw;
            }
            
        }

        public Pedidos GetPedidoPorId(int idPedido)
        {
            Pedidos pedidoADevolver = new Pedidos();

            try
            {
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
                        }
                        conexion.Close();
                    }
                }
                log.Info("Se obtuvieron los datos del pedido " + idPedido + " exitosamente");
                return pedidoADevolver;
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al obtener los datos del pedido " + idPedido, mensaje);
                throw;
            }
            
        }
        
        public int TieneElPedidoUnCadete( int idPedido)
        {
            int idCadete = -1;
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string SQLQuery = "SELECT id_cadete FROM pedidos WHERE id_pedido = @idPedido";
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@idPedido", idPedido);
                        command.ExecuteNonQuery();
                        using (SQLiteDataReader DataReader = command.ExecuteReader())
                        {
                            if (DataReader.Read())
                            {
                                var idCadeteObject = DataReader["id_cadete"];
                                if (idCadeteObject != null)
                                {
                                    idCadete = Convert.ToInt32(idCadeteObject);
                                }
                            }
                            DataReader.Close();
                        }
                    }
                    conexion.Close();
                }
                log.Info("Se obtuvo el id del cadete que tiene el pedido " + idPedido + " exitosamente");
                return idCadete;
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al obtener el id del cadete que tiene el pedido " + idPedido, mensaje);
                throw;
            }
            
            
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
                log.Info("Se asigno el cadete" + idCadete+ "al pedido" +idPedido + "exitosamente");
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al asignar el cadete" + idCadete + "al pedido" + idPedido, mensaje);
                throw; ;
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
                log.Info("Se quito el cadete al pedido" + idPedido + "exitosamente");
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al quitar el cadete al pedido" + idPedido, mensaje);
                throw; ;
            }

        }

        public void UpdatePedido(Pedidos pedido)
        {
            string SQLQuery = "UPDATE pedidos SET observacion = @observacion, estado_pedido = @estado_pedido, " +
                "id_cliente = @id_cliente WHERE id_pedido = @id_pedido";

            try
            {
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
                log.Info("Se modifico el pedido" + pedido.Numero + "exitosamente");
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al modificar los datos del pedido " + pedido.Numero, mensaje);
                throw;
            }
            
        }

        public void DeletePedido(Pedidos pedido)
        {
            string SQLQuery = "UPDATE pedidos SET activo_pedido = 0 WHERE id_pedido = @id_pedido";

            try
            {
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
                log.Info("Se desactivo el pedido " + pedido.Numero + " exitosamente");
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al desactivar el pedido " + pedido.Numero, mensaje);
                throw;
            }
            
        }
    }
}