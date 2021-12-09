using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models
{
    public class RepositorioCliente
    {
        private readonly string connectionString;
        //private readonly SQLiteConnection conexion;

        public RepositorioCliente(string connectionString)
        {
            this.connectionString = connectionString;
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
                return ListadoClientes;
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
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
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
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
                return cliente;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                throw;
            }
        }
        public int GetIdCliente(string nombre_cliente, string direccion)
        {
            int idCliente = -1;

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
                return idCliente;
            }
        }

        public int GetIdClienteByIdUser(int idUsuario)
        {
            int idCliente = -1;
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
            return idCliente;
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
                return clienteADevolver;
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                throw;
            }

        }

        public void UpdateCliente(Cliente cliente)
        {
            string SQLQuery = "UPDATE clientes SET nombre_cliente = @nombre_cliente, direccion_cliente = @direccion_cliente, " +
                "telefono_cliente = @telefono_cliente WHERE id_cliente = @id_cliente";
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
        }

        public void DeleteCliente(int id_cliente)
        {
            string SQLQuery = "UPDATE clientes SET activo_cliente = 0 WHERE id_cliente = @id_cliente";
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
        }
    }
}
