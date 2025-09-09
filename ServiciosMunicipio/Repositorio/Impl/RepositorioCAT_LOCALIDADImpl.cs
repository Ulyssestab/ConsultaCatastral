using log4net;
using ServiciosMunicipio.Controllers;
using ServiciosMunicipio.Models;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Repositorio.Impl
{
    public class RepositorioCAT_LOCALIDADImpl : RepositorioCAT_LOCALIDAD
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));
        public List<CAT_LOCALIDAD> obtenerLocalidades(string id, string municipio)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = Constantes.DataSource,
                UserID = Constantes.UserID,
                Password = Constantes.Password,
                InitialCatalog = Constantes.InitialCatalogM + municipio
            };
            string consulta = "SELECT * "
                    + "FROM  sde.CAT_LOCALIDAD  where CVE_ENTIDAD = '01' "
                    + "and CVE_MUNICIPIO = '"+ @municipio + "' "
                    + "and NOM_LOCALIDAD like('%" + @id + "%') "
                    + "order by NOM_LOCALIDAD";
            List<CAT_LOCALIDAD> lista = new List<CAT_LOCALIDAD>();
            CAT_LOCALIDAD elemento = new CAT_LOCALIDAD();

            var connectionString = builder.ConnectionString;
            var connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();

                var command = new SqlCommand(consulta, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    elemento.OBJECTID = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
                    elemento.CVE_ENTIDAD = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    elemento.NOM_ENTIDAD = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    elemento.CVE_MUNICIPIO = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    elemento.NOM_MUNICIPIO = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    elemento.CVE_LOCALIDAD = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    elemento.NOM_LOCALIDAD = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    lista.Add(elemento);
                    elemento = new CAT_LOCALIDAD();
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

        public List<CAT_LOCALIDAD> obtenerLocalidades(string clave_localidad, string nombre_localidad, string municipio)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = Constantes.DataSource,
                UserID = Constantes.UserID,
                Password = Constantes.Password,
                InitialCatalog = Constantes.InitialCatalogM + municipio
            };
            string consulta = "";
            if (clave_localidad != null && clave_localidad != "")
            {
                consulta = "SELECT * "
                + "FROM  sde.CAT_LOCALIDAD  "
                + "where CVE_ENTIDAD = '01' "
                + "and CVE_MUNICIPIO = '" + municipio + "' "
                + "and CVE_LOCALIDAD like('%" + @clave_localidad + "%') "
                + "order by CVE_LOCALIDAD";
            }
            else if (nombre_localidad != null && nombre_localidad != "")
            {
                consulta = "SELECT * "
                + "FROM  sde.CAT_LOCALIDAD  where CVE_ENTIDAD = '01' "
                + "and CVE_MUNICIPIO = '"+municipio+"' "
                + "and NOM_LOCALIDAD like('%" + @nombre_localidad + "%') "
                + "order by NOM_LOCALIDAD";
            }
            List<CAT_LOCALIDAD> lista = new List<CAT_LOCALIDAD>();
            CAT_LOCALIDAD elemento = new CAT_LOCALIDAD();

            var connectionString = builder.ConnectionString;
            var connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();

                var command = new SqlCommand(consulta, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    elemento.OBJECTID = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
                    elemento.CVE_ENTIDAD = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    elemento.NOM_ENTIDAD = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    elemento.CVE_MUNICIPIO = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    elemento.NOM_MUNICIPIO = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    elemento.CVE_LOCALIDAD = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    elemento.NOM_LOCALIDAD = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    lista.Add(elemento);
                    elemento = new CAT_LOCALIDAD();
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