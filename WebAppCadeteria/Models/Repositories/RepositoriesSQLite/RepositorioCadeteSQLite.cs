using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using WebApp_Cadeteria.Models.Entities;
using Microsoft.Extensions.Logging;
using NLog;

namespace WebApp_Cadeteria.Models.Repositories.RepositoriesSQLite
{
    public class RepositorioCadeteSQLite : IRepositorioCadete
    {
        private readonly string connectionString;
        private readonly Logger log;

        //private readonly SQLiteConnection conexion;

        public RepositorioCadeteSQLite(string connectionString, Logger log)
        {
            this.connectionString = connectionString;
            this.log = log;
            //conexion = new SQLiteConnection(connectionString);
        }

        public List<Cadete> getAll()
        {
            List<Cadete> ListadoCadetes = new List<Cadete>();

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string SQLQuery = "SELECT * FROM cadetes WHERE activo_cadete = 1;";
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        using (SQLiteDataReader DataReader = command.ExecuteReader())
                        {
                            while (DataReader.Read())
                            {
                                Cadete cadete = new Cadete(Convert.ToInt32(DataReader["id_cadete"]),
                                                            DataReader["nombre_cadete"].ToString(),
                                                            DataReader["direccion_cadete"].ToString(),
                                                            DataReader["telefono_cadete"].ToString());
                                ListadoCadetes.Add(cadete);
                            }
                            DataReader.Close();
                        }
                    }
                    conexion.Close();

                }
                return ListadoCadetes;
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                throw;
            }
            
        }

        public Cadete GetCadetePorId(int idCadete)
        {
            Cadete cadeteADevolver = new Cadete();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string SQLQuery = "SELECT * FROM cadetes WHERE id_cadete = @idCadete";
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@idCadete", idCadete);
                        using (SQLiteDataReader DataReader = command.ExecuteReader())
                        {
                            if (DataReader.Read())
                            {

                                cadeteADevolver = new Cadete(Convert.ToInt32(DataReader["id_cadete"]),
                                                            DataReader["nombre_cadete"].ToString(),
                                                            DataReader["direccion_cadete"].ToString(),
                                                            DataReader["telefono_cadete"].ToString());
                            }
                            DataReader.Close();
                        }
                    }
                    conexion.Close();
                }
                return cadeteADevolver;
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
                throw;
            }
            
        }

        public void SaveCadete(Cadete cadete, int id_usuario)
        {
            string SQLQuery = "INSERT INTO cadetes(nombre_cadete, direccion_cadete, telefono_cadete, activo_cadete, id_cadeteria, " +
                "id_usuario) VALUES(@nombre_cadete, @direccion_cadete, @telefono_cadete,1,1, @id_usuario)";
            using(SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                using(SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    //command.Parameters.AddWithValue("@id_cadete", cadete.id);
                    command.Parameters.AddWithValue("@nombre_cadete", cadete.Nombre);
                    command.Parameters.AddWithValue("@direccion_cadete", cadete.Direccion);
                    command.Parameters.AddWithValue("@telefono_cadete", cadete.Telefono);
                    command.Parameters.AddWithValue("@id_usuario", id_usuario);
                    conexion.Open();
                    command.ExecuteNonQuery();
 
                }
                conexion.Close();
            }
        }

        public void UpdateCadete(Cadete cadete)
        {
            string SQLQuery = "UPDATE cadetes SET nombre_cadete = @nombre_cadete, direccion_cadete = @direccion_cadete, telefono_cadete = @telefono_cadete WHERE id_cadete = @id_cadete";
            using(SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                using(SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@nombre_cadete", cadete.Nombre);
                    command.Parameters.AddWithValue("@direccion_cadete", cadete.Direccion);
                    command.Parameters.AddWithValue("@telefono_cadete", cadete.Telefono);
                    command.Parameters.AddWithValue("@id_cadete", cadete.Id);
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }

        public void DeleteCadete(int p_id_cadete)
        {
            string SQLQuery = "UPDATE cadetes SET activo_cadete = 0 WHERE id_cadete = @id_cadete";
            using(SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                using(SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@id_cadete", p_id_cadete);
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }

        public int GetIdCadeteByIdUser(int idUsuario)
        {
            int idCadete = -1;
            using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                string SQLQuery = "SELECT * FROM cadetes WHERE id_usuario = @idUsuario";

                conexion.Open();
                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);
                    using (SQLiteDataReader DataReader = command.ExecuteReader())
                    {
                        if (DataReader.Read())
                        {
                            idCadete = Convert.ToInt32(DataReader["id_cadete"]);
                        }
                        DataReader.Close();
                    }
                }
                conexion.Close();
            }
            return idCadete;
        }

        //public void ActualizarDatos
    }

}