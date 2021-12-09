using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models.Entities;

namespace WebApp_Cadeteria.Models.Repositories.RepositoriesSQLite
{
    public class RepositorioClienteSQLite : IRepositorioCliente
    {
        private readonly string connectionString;
        private readonly Logger log;

        //private readonly SQLiteConnection conexion;

        public RepositorioClienteSQLite(string connectionString, Logger log)
        {
            this.connectionString = connectionString;
            this.log = log;
            //conexion = new SQLiteConnection(connectionString);
        }

        public List<Cliente> getAll()
        {
            List<Cliente> ListadoClientes = new List<Cliente>();

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string SQLQuery = "SELECT * FROM clientes WHERE activo_cliente = 1;";
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        using (SQLiteDataReader DataReader = command.ExecuteReader())
                        {
                            while (DataReader.Read())
                            {
                                Cliente cliente = new Cliente(Convert.ToInt32(DataReader["id_cliente"]),
                                                            DataReader["nombre_cliente"].ToString(),
                                                            DataReader["direccion_cliente"].ToString(),
                                                            DataReader["telefono_cliente"].ToString());
                                ListadoClientes.Add(cliente);
                            }
                            DataReader.Close();
                        }
                    }
                    conexion.Close();

                }
                log.Info("Se obtuvieron los datos de los clientes con exito");
                return ListadoClientes;
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al obtener los datos de los clientes ", mensaje);
                throw;
            }

        }

        public void SaveCliente(Cliente cliente, int id_usuario)
        {
            string SQLQuery = "INSERT INTO clientes(nombre_cliente, direccion_cliente, telefono_cliente, activo_cliente, id_usuario) " +
                "VALUES(@nombre_cliente, @direccion_cliente, @telefono_cliente,1, @id_usuario)";

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre_cliente", cliente.Nombre);
                        command.Parameters.AddWithValue("@direccion_cliente", cliente.Direccion);
                        command.Parameters.AddWithValue("@telefono_cliente", cliente.Telefono);
                        command.Parameters.AddWithValue("@id_usuario", id_usuario);
                        command.ExecuteNonQuery();
                    }
                    conexion.Close();
                }
                log.Info("El cliente " + cliente.Id + " se guardo exitosamente");
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al guardar el cliente " + cliente.Id, mensaje);
                throw;
            }

        }

        public Cliente GetCliente(string nombre, string direccion)
        {
            Cliente cliente = new Cliente();
            string SQLQuery = "SELECT * FROM clientes WHERE nombre_cliente = @nombre_cliente " +
                "AND direccion_cliente = @direccion_cliente;";

            try
            {
                using (var conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre_cliente", nombre);
                        command.Parameters.AddWithValue("@direccion_cliente", direccion);
                        conexion.Open();

                        SQLiteDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            cliente.Id = Convert.ToInt32(dataReader["id_cliente"]);
                            cliente.Nombre = dataReader["nombre_cliente"].ToString();
                            cliente.Direccion = dataReader["direccion_cliente"].ToString();
                            cliente.Telefono = dataReader["telefono_cliente"].ToString();

                        }
                        dataReader.Close();

                    }
                    conexion.Close();
                }
                log.Info("Se obtuvo el cliente " + nombre + " exitosamente");
                return cliente;
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al obtener el cliente " + nombre, mensaje);
                throw;
            }
        }
        public int GetIdCliente(string nombre_cliente, string direccion)
        {
            int idCliente = -1;

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string SQLQuery = "SELECT id_cliente FROM clientes WHERE nombre_cliente = @nombre_cliente AND " +
                        "direccion_cliente = @direccion_cliente";

                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre_cliente", nombre_cliente);
                        command.Parameters.AddWithValue("@direccion_cliente", direccion);

                        using (SQLiteDataReader DataReader = command.ExecuteReader())
                        {
                            if (DataReader.Read())
                            {
                                idCliente = Convert.ToInt32(DataReader["id_cliente"]);
                            }
                            DataReader.Close();
                        }
                        conexion.Close();
                    }
                    log.Info("Se obtuvo el id del cliente " + nombre_cliente + " exitosamente");
                    return idCliente;
                }
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al obtener el id del cliente " + nombre_cliente, mensaje);
                throw;
            }

            
        }

        public int GetIdClienteByIdUser(int idUsuario)
        {
            int idCliente = -1;

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    string SQLQuery = "SELECT * FROM clientes WHERE id_usuario = @idUsuario";

                    conexion.Open();
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@idUsuario", idUsuario);
                        using (SQLiteDataReader DataReader = command.ExecuteReader())
                        {
                            if (DataReader.Read())
                            {
                                idCliente = Convert.ToInt32(DataReader["id_cliente"]);
                            }
                            DataReader.Close();
                        }
                    }
                    conexion.Close();
                }
                log.Info("Se obtuvo el id del cliente con id de usuario" +idUsuario+ " exitosamente");
                return idCliente;
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al obtener el id del cliente con id de usuario" + idUsuario, mensaje);
                throw;
            }
            
        }

        public Cliente GetClientePorId(int idCliente)
        {
            Cliente clienteADevolver = new Cliente();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string SQLQuery = "SELECT * FROM clientes WHERE id_cliente = @idCliente";
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@idCliente", idCliente);
                        using (SQLiteDataReader DataReader = command.ExecuteReader())
                        {
                            if (DataReader.Read())
                            {

                                clienteADevolver = new Cliente(Convert.ToInt32(DataReader["id_cliente"]),
                                                            DataReader["nombre_cliente"].ToString(),
                                                            DataReader["direccion_cliente"].ToString(),
                                                            DataReader["telefono_cliente"].ToString());
                            }
                            DataReader.Close();
                        }
                    }
                    conexion.Close();
                }
                log.Info("Se obtuvieron los datos del cliente " +idCliente+ " exitosamente");
                return clienteADevolver;
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al obtener los datos del cliente " + idCliente, mensaje);
                throw;
            }

        }

        public void UpdateCliente(Cliente cliente)
        {
            string SQLQuery = "UPDATE clientes SET nombre_cliente = @nombre_cliente, direccion_cliente = @direccion_cliente, " +
                "telefono_cliente = @telefono_cliente WHERE id_cliente = @id_cliente";

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@nombre_cliente", cliente.Nombre);
                        command.Parameters.AddWithValue("@direccion_cliente", cliente.Direccion);
                        command.Parameters.AddWithValue("@telefono_cliente", cliente.Telefono);
                        command.Parameters.AddWithValue("@id_cliente", cliente.Id);
                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
                log.Info("Se modificaron los datos del cliente " + cliente.Id + " exitosamente");
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al modificar los datos del cliente " + cliente.Id, mensaje);
                throw;
            }
            
        }

        public void DeleteCliente(int id_cliente)
        {
            string SQLQuery = "UPDATE clientes SET activo_cliente = 0 WHERE id_cliente = @id_cliente";

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@id_cliente", id_cliente);
                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
                log.Info("Se desactivo el cliente " + id_cliente + " exitosamente");
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al desactivar el cliente " + id_cliente, mensaje);
                throw;
            }
            
        }
    }
}
