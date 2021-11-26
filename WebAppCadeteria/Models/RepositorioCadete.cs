using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace WebApp_Cadeteria.Models
{
    public class RepositorioCadete : IRepositorioCadete
    {
        private readonly string connectionString;
        //private readonly SQLiteConnection conexion;

        public RepositorioCadete(string connectionString)
        {
            this.connectionString = connectionString;
            //conexion = new SQLiteConnection(connectionString);
        }
        /*
        public List<Cadete> getAllCadetes()
        {
            List<Cadete> listaCadetes = new List<Cadete>();

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();

                    string instrucionSQL = "SELECT * FROM Cadetes WHERE cadeteActivo = 1;";
                    using (SQLiteCommand command = new SQLiteCommand(instrucionSQL, conexion))
                    {
                        var DataReader = command.ExecuteReader();

                        while (DataReader.Read())
                        {
                            Cadete cadete = new Cadete()
                            {
                                Id = Convert.ToInt32(DataReader["cadeteID"]),
                                Nombre = DataReader["cadeteNombre"].ToString(),
                                Telefono = DataReader["cadeteTelefono"].ToString(),
                                Direccion = DataReader["cadeteDireccion"].ToString(),
                            };

                            listaCadetes.Add(cadete);
                        }

                        DataReader.Close();
                    }

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                var mensaje = "Mensaje de error" + ex.Message;
            }

            return listaCadetes;
        }*/

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

        public void SaveCadete(Cadete cadete)
        {
            string SQLQuery = "INSERT INTO cadetes VALUES(@nombre_cadete, @direccion_cadete, @telefono_cadete,1,1)";
            using(SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                using(SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    //command.Parameters.AddWithValue("@id_cadete", cadete.id);
                    command.Parameters.AddWithValue("@nombre_cadete", cadete.Nombre);
                    command.Parameters.AddWithValue("@direccion_cadete", cadete.Direccion);
                    command.Parameters.AddWithValue("@telefono_cadete", cadete.Telefono);
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
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

                    }
                }
                conexion.Close();
            }
            return idCadete;
        }

        //public void ActualizarDatos
    }

}