using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace WebApp_Cadeteria.Models
{
    public class RepositorioCadete
    {
        private readonly string connectionString;
        //private readonly SQLiteConnection conexion;

        public RepositorioCadete(string connectionString)
        {
            this.connectionString = connectionString;
            //conexion = new SQLiteConnection(connectionString);
        }

        public List<Cadete> getAll()
        {
            List<Cadete> ListadoCadetes = new List<Cadete>();

            using(SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                conexion.Open();
                string SQLQuery = "SELECT * FROM cadetes WHERE activo_cadete = 1;";
                SQLiteCommand command = new SQLiteCommand(SQLQuery,conexion);
                using(SQLiteDataReader DataReader = command.ExecuteReader())
                {
                    while(DataReader.Read())
                    {
                        Cadete cadete = new Cadete(Convert.ToInt32(DataReader["id_cadete"]),DataReader["nombre_cadete"].ToString(), DataReader["direccion_cadete"].ToString(), DataReader["telefono_cadete"].ToString());
                        ListadoCadetes.Add(cadete);
                    }
                    conexion.Close();
                }
            }
            return ListadoCadetes;
        }

        public void SaveCadete(Cadete cadete)
        {
            string SQLQuery = "INSERT INTO Cadetes VALUES(@id_cadete, @nombre_cadete, @direccion_cadete, @telefono_cadete,1,1)";
            using(SQLiteConnection conexion = new SQLiteConnection(connectionString))
            {
                using(SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                {
                    command.Parameters.AddWithValue("@id_cadete", cadete.id);
                    command.Parameters.AddWithValue("@nombre_cadete", cadete.nombre);
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
    }
}