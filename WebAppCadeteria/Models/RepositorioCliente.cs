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

        
        public void SaveCliente(Cliente cliente)
        {
            string SQLQuery = "INSERT INTO clientes(nombre_cliente, direccion_cliente, telefono_cliente " +
                "VALUES(@nombre_cliente, @direccion_cliente, @telefono_cliente)";
            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    //command.Parameters.AddWithValue("@id_cadete", cadete.id);
                    command.Parameters.AddWithValue("@nombre_cliente", cliente.Nombre);
                    command.Parameters.AddWithValue("@direccion_cliente", cliente.Direccion);
                    command.Parameters.AddWithValue("@telefono_cliente", cliente.Telefono);
                    //command.Parameters.AddWithValue("@id_usuario", id_usuario);
                    conexion.Open();
                    command.ExecuteNonQuery();

                }
                conexion.Close();
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
    }
}
