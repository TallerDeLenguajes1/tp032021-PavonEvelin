using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Cadeteria.Models
{
    public class RepositorioUsuario
    {
        private readonly string connectionString;
        //private readonly SQLiteConnection conexion;

        public RepositorioUsuario(string connectionString)
        {
            this.connectionString = connectionString;
            //conexion = new SQLiteConnection(connectionString);
        }

        public bool ValidateUser(Usuario usuario)
        {
            bool validated = false;

            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                conexion.Open();
                string SQLQuery = "SELECT * FROM usuarios WHERE userName = @userName AND password = @password;";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@userName", usuario.UserName);
                    command.Parameters.AddWithValue("@password", usuario.Password);

                    using (SQLiteDataReader DataReader = command.ExecuteReader())
                    {
                        if (DataReader.Read())
                        {
                            validated = true;
                        }
                    }
                    conexion.Close();
                }
                return validated;
            }
        }

        public void SaveUser(Usuario usuario)
        {
            string SQLQuery = "INSERT INTO usuarios VALUES(@userName, @password, @nombre_usuario, @direccion_usuario, " +
                "@telefono_usuario, @rol_usuario, 1)";
            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@userName", usuario.UserName);
                    command.Parameters.AddWithValue("@password", usuario.Password);
                    command.Parameters.AddWithValue("@nombre_usuario", usuario.Nombre);
                    command.Parameters.AddWithValue("@direccion_usuario", usuario.Direccion);
                    command.Parameters.AddWithValue("@telefono_usuario", usuario.Telefono);
                    command.Parameters.AddWithValue("@rol_usuario", usuario.Rol);
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }

        public void UpdateUser(Usuario usuario)
        {
            string SQLQuery = "UPDATE usuarios SET userName = @userName, password = @password, nombre_usuario = @nombre_usuario, " +
                "direccion_usuario = @direccion_usuario, telefono_usuario = @telefono_usuario WHERE id_usuario = @id_usuario";
            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@userName", usuario.UserName);
                    command.Parameters.AddWithValue("@password", usuario.Password);
                    command.Parameters.AddWithValue("@nombre_usuario", usuario.Nombre);
                    command.Parameters.AddWithValue("@direccion_usuario", usuario.Nombre);
                    command.Parameters.AddWithValue("@telefono_usuario", usuario.Nombre);
                    command.Parameters.AddWithValue("@id_usuario", usuario.Id);
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }

        /*
        public void UpdateRolUser(Usuario usuario)
        {
            string SQLQuery = "UPDATE usuarios SET rol_usuario = @rol_usuario WHERE id_usuario = @id_usuario";
            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@rol_usuario", usuario.Rol);
                    command.Parameters.AddWithValue("@id_usuario", usuario.Id);
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }*/

        public void DeleteUser(int id_usuario)
        {
            string SQLQuery = "UPDATE usuarios SET activo_usuario = 0 WHERE id_usuario = @id_usuario";
            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@id_usuario", id_usuario);
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }

        public int GetUserID(Usuario usuario)
        {
            int idUsuario = -1;

            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                conexion.Open();
                string SQLQuery = "SELECT * FROM usuarios WHERE userName = @userName AND password = @password";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@userName", usuario.UserName);
                    command.Parameters.AddWithValue("@password", usuario.Password);

                    using (SQLiteDataReader DataReader = command.ExecuteReader())
                    {
                        if (DataReader.Read())
                        {
                            idUsuario = Convert.ToInt32(DataReader["id_usuario"]);
                        }
                        DataReader.Close();
                    }
                    conexion.Close();
                }
                return idUsuario;
            }
        }

        public Usuario GetUser(string userName, string password)
        {
            Usuario user = new Usuario();

            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                conexion.Open();
                string SQLQuery = "SELECT * FROM usuarios WHERE userName = @userName AND password = @password";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@password", password);

                    using (SQLiteDataReader DataReader = command.ExecuteReader())
                    {
                        if (DataReader.Read())
                        {
                            user = new Usuario(Convert.ToInt32(DataReader["id_usuario"]),
                                                            DataReader["userName"].ToString(),
                                                            DataReader["password"].ToString(),
                                                            DataReader["nombre_usuario"].ToString(),
                                                            DataReader["direccion_usuario"].ToString(),
                                                            DataReader["telefono_usuario"].ToString(),
                                                            DataReader["rol_usuario"].ToString());
                        }
                        DataReader.Close();
                    }
                    conexion.Close();
                }
                return user;
            }
        }
    }
}
