using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApp_Cadeteria.Models
{
    public class DBTemporal
    {
        //public Cadeteria Cadeteria { get; set; }

        string pathCadetes = @"C:\RepoGit-Taller2\tp032021-PavonEvelin\TP3\WebApp-Cadeteria\Archivos\Cadetes.json";
        string pathPedidos = @"C:\RepoGit-Taller2\tp032021-PavonEvelin\TP3\WebApp-Cadeteria\Archivos\Pedidos.json";
        public DBTemporal()
        {

            if (!File.Exists(pathCadetes))
            {
                // File.Create(path);
                using (FileStream miArchivo = new FileStream(pathCadetes, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(miArchivo))
                    {
                        writer.Write("");
                        writer.Close();
                        writer.Dispose();
                    }
                }
            }
        }
        public void SaveCadete(Cadete cadete)
        {
            try
            {
                List<Cadete> cadetes = GetCadetes();
                cadetes.Add(cadete);
                string CadetesJson = JsonSerializer.Serialize(cadetes);
                using (FileStream archivo = new FileStream(pathCadetes, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(archivo))
                    {
                        writer.Write(CadetesJson);
                        writer.Close();
                        writer.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }

        public List<Cadete> GetCadetes()
        {
            List<Cadete> CadetesJson = null;

            if (!File.Exists(pathCadetes))
            {
                using (FileStream miArchivo = new FileStream(pathCadetes, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(miArchivo))
                    {
                        writer.Write("");
                        writer.Close();
                        writer.Dispose();
                    }
                }
            }

            try
            {
                using (FileStream archivo = new FileStream(pathCadetes, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(archivo))
                    {
                        string cadetes = reader.ReadToEnd();
                        reader.Close();
                        reader.Dispose();
                        if (cadetes != "")
                        {
                            CadetesJson = JsonSerializer.Deserialize<List<Cadete>>(cadetes);
                        }
                        else
                        {
                            CadetesJson = new List<Cadete>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return CadetesJson;
        }

        public void SavePedido(Pedidos pedido)
        {
            try
            {
                List<Pedidos> pedidos = GetPedidos();
                pedidos.Add(pedido);
                string PedidosJson = JsonSerializer.Serialize(pedidos);
                using (FileStream archivo = new FileStream(pathPedidos, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(archivo))
                    {
                        writer.Write(PedidosJson);
                        writer.Close();
                        writer.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }

        public List<Pedidos> GetPedidos()
        {
            List<Pedidos> PedidosJson = null;

            if (!File.Exists(pathPedidos))
            {
                using (FileStream miArchivo = new FileStream(pathPedidos, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(miArchivo))
                    {
                        writer.Write("");
                        writer.Close();
                        writer.Dispose();
                    }
                }
            }

            try
            {
                using (FileStream archivo = new FileStream(pathPedidos, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(archivo))
                    {
                        string pedidos = reader.ReadToEnd();
                        reader.Close();
                        reader.Dispose();
                        if (pedidos != "")
                        {
                            PedidosJson = JsonSerializer.Deserialize<List<Pedidos>>(pedidos);
                        }
                        else
                        {
                            PedidosJson = new List<Pedidos>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return PedidosJson;
        }
    }
}
