using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cadeteria.Models.Entities;

namespace WebApp_Cadeteria.Models.Repositories.RepositoriesSQLite
{
    public class RepositorioUsuarioSQLite : IRepositorioUsuario
    {
        private readonly string connectionString;
        private readonly Logger log;

        //private readonly SQLiteConnection conexion;

        public RepositorioUsuarioSQLite(string connectionString, Logger log)
        {
            this.connectionString = connectionString;
            this.log = log;
            //conexion = new SQLiteConnection(connectionString);
        }

        public bool ValidateUser(Usuario usuario)
        {
            bool validated = false;

            try
            {
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
                    }
                    conexion.Close();
                }
                log.Info("Se valido el usuario " + usuario.Nombre + " exitosamente");
                return validated;
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al validar el usuario " + usuario.Nombre, mensaje);
                throw;
            }

        }

        public void SaveUser(Usuario usuario)
        {
            string SQLQuery = "INSERT INTO usuarios(userName, password, nombre_usuario, direccion_usuario, telefono_usuario, " +
                "rol_usuario, activo_usuario) VALUES(@userName, @password, @nombre_usuario, @direccion_usuario, @telefono_usuario, @rol_usuario, 1)";

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        conexion.Open();
                        command.Parameters.AddWithValue("@userName", usuario.UserName);
                        command.Parameters.AddWithValue("@password", usuario.Password);
                        command.Parameters.AddWithValue("@nombre_usuario", usuario.Nombre);
                        command.Parameters.AddWithValue("@direccion_usuario", usuario.Direccion);
                        command.Parameters.AddWithValue("@telefono_usuario", usuario.Telefono);
                        command.Parameters.AddWithValue("@rol_usuario", usuario.Rol);
                        command.ExecuteNonQuery();
                    }
                    conexion.Close();
                }
                log.Info("El usuario " + usuario.Id + " se guardo exitosamente");
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al guardar el usuario " + usuario.Id, mensaje);
                throw;
            }
        }

        public void UpdateUser(Usuario usuario)
        {
            string SQLQuery = "UPDATE usuarios SET userName = @userName, password = @password, nombre_usuario = @nombre_usuario, " +
                "direccion_usuario = @direccion_usuario, telefono_usuario = @telefono_usuario WHERE id_usuario = @id_usuario";

            try
            {
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
                log.Info("Se modificaron los datos del usuario " + usuario.Id + " exitosamente");
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al modificar los datos del usuario " + usuario.Id, mensaje);
                throw;
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

            try
            {
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
                log.Info("Se desactivo el usuario " + id_usuario + " exitosamente");
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al desactivar el usuario " + id_usuario, mensaje);
                throw;
            }
            
        }

        public int GetUserID(Usuario usuario)
        {
            int idUsuario = -1;

            try
            {
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
                    log.Info("Se obtuvo el id del usuario " + usuario.Nombre + " exitosamente");
                    return idUsuario;
                }
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al obtener el id del usuario " + usuario.Nombre, mensaje);
                throw;
            }
            
        }

        public Usuario GetUser(string userName, string password)
        {
            Usuario user = new Usuario();

            try
            {
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
                    log.Info("Se obtuvo el usuario " + userName + " exitosamente");
                    return user;
                }
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                log.Error("Ocurrio un error al obtener el usuario " + userName, mensaje);
                throw;
            }

        }
    }
}
