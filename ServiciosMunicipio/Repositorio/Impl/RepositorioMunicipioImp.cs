using log4net;
using ServiciosMunicipio.Controllers;
using ServiciosMunicipio.Models;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosMunicipio.Repositorio.Impl
{
    public class RepositorioMunicipioImp : RepositorioMunicipio
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));
        public int ObtenerTotal(String consulta, String municipio)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "10.1.2.126",
                UserID = "CatastroSA",
                Password = "CatAdmin#",
                InitialCatalog = "GDB01" + municipio
            };

            int resultado = 0;

            var connectionString = builder.ConnectionString;
            var connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();

                var command = new SqlCommand(consulta, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    resultado = reader.GetInt32(0);
                }

            }
            catch (SqlException e)
            {
                log.Error($"SQL Error: {e.Message}");
            }
            catch (Exception e)
            {
                log.Error($"SQL Error: {e.Message}");
            }
            finally
            {
                connection.Close();
            }
            return resultado;
        }

        public List<Resultados> ObtenerLista(String consulta, String municipio)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = Constantes.DataSource,
                UserID = Constantes.UserID,
                Password = Constantes.Password,
                InitialCatalog = Constantes.InitialCatalogM + municipio
            };
            
            List<Resultados> lista = new List<Resultados>();
            Resultados elemento = new Resultados();

            var connectionString = builder.ConnectionString;
            var connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();

                var command = new SqlCommand(consulta, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    
                    elemento.CVE_CAT_EST = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    elemento.CVE_CAT_ORI = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    elemento.clavePredial = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    elemento.NOMBRE_O_RAZON_SOCIAL = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    elemento.APELLIDO_PATERNO = (!reader.IsDBNull(5) ? reader.GetString(5) : "");
                    elemento.APELLIDO_MATERNO = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    elemento.NOM_LOCALIDAD = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    elemento.NOMBRE_COMPLETO_ASENTAMIENTO = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    elemento.NOMBRE_COMPLETO_VIALIDAD = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    elemento.NUMERO_EXTERIOR = !reader.IsDBNull(10) ? reader.GetString(10) : "";

                    if (elemento.APELLIDO_MATERNO == "<NULL>" || elemento.APELLIDO_MATERNO == "<Null>")
                    {
                        elemento.APELLIDO_MATERNO = "";
                    }
                    if (elemento.APELLIDO_PATERNO == "<NULL>" || elemento.APELLIDO_PATERNO == "<Null>")
                    {
                        elemento.APELLIDO_PATERNO = "";
                    }

                    lista.Add(elemento);
                    elemento = new Resultados();
                }                

            }
            catch (SqlException e)
            {
                log.Error($"SQL Error: {e.Message}");
            }
            catch (Exception e)
            {
                log.Error($"SQL Error: {e.Message}");
            }
            finally
            {
                connection.Close();
            }
            return lista;
        }
    }
}
