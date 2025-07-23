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
    public class RepositorioSIS_ASENTAMIENTOImp : RepositorioSIS_ASENTAMIENTO
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));
        public SIS_ASENTAMIENTOS obtenerAsentamiento(String consulta, String municipio)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = Utilerias.Constantes.DataSource,
                UserID = Constantes.UserID,
                Password = Constantes.Password,
                InitialCatalog = Constantes.InitialCatalogM + municipio
            };

            SIS_ASENTAMIENTOS elemento = new SIS_ASENTAMIENTOS();

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
                    elemento.CLAVEUNICA = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0;
                    elemento.STATUSREGISTROTABLA = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    elemento.ALTAREGISTROTABLA = !reader.IsDBNull(3) ? reader.GetDateTime(3).ToUniversalTime() : new DateTime();
                    elemento.BAJAREGISTROTABLA = !reader.IsDBNull(4) ? reader.GetDateTime(4).ToUniversalTime() : new DateTime();
                    elemento.HORAALTAREGISTROTABLA = !reader.IsDBNull(5) ? reader.GetString(5) + "" : "";
                    elemento.HORABAJAREGISTROTABLA = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    elemento.USUARIOALTA = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    elemento.USUARIOBAJA = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    elemento.TERMINALALTA = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    elemento.TERMINALBAJA = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    elemento.CVE_ENTIDAD = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    elemento.CVE_REGION_CATASTRAL = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    elemento.CVE_MUNICIPIO = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                    elemento.CVE_ZONA_CATASTRAL = !reader.IsDBNull(14) ? reader.GetString(14) + "" : 0 + "";
                    elemento.CVE_LOCALIDAD = !reader.IsDBNull(15) ? reader.GetString(15) : "";
                    elemento.CVE_SECTOR_CATASTRAL = !reader.IsDBNull(16) ? reader.GetString(16) : "";
                    elemento.CVE_ASENTAMIENTO = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    elemento.TIPO_ASENTAMIENTO = !reader.IsDBNull(18) ? reader.GetString(18) : "";
                    elemento.NOMBRE_ASENTAMIENTO = !reader.IsDBNull(19) ? reader.GetString(19) : "";
                    elemento.CP = !reader.IsDBNull(20) ? reader.GetString(20) : "";
                    elemento.NOMBRE_COMPLETO_ASENTAMIENTO = !reader.IsDBNull(21) ? reader.GetString(21) : "";
                    elemento.OBSERVACIONES = !reader.IsDBNull(22) ? reader.GetString(22) : "";

                }

            }
            catch (SqlException e)
            {
                Console.WriteLine($"SQL Error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
            }
            return elemento;
        }

        public List<SIS_ASENTAMIENTOS> obtenerAsentamientos(String consulta, String municipio)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = Utilerias.Constantes.DataSource,
                UserID = Constantes.UserID,
                Password = Constantes.Password,
                InitialCatalog = Constantes.InitialCatalogM + municipio
            };

            SIS_ASENTAMIENTOS elemento = new SIS_ASENTAMIENTOS();
            List<SIS_ASENTAMIENTOS> lista = new List<SIS_ASENTAMIENTOS>();
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
                    elemento.CLAVEUNICA = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0;
                    elemento.STATUSREGISTROTABLA = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    elemento.ALTAREGISTROTABLA = !reader.IsDBNull(3) ? reader.GetDateTime(3).ToUniversalTime() : new DateTime();
                    elemento.BAJAREGISTROTABLA = !reader.IsDBNull(4) ? reader.GetDateTime(4).ToUniversalTime() : new DateTime();
                    elemento.HORAALTAREGISTROTABLA = !reader.IsDBNull(5) ? reader.GetString(5) + "" : "";
                    elemento.HORABAJAREGISTROTABLA = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    elemento.USUARIOALTA = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    elemento.USUARIOBAJA = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    elemento.TERMINALALTA = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    elemento.TERMINALBAJA = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    elemento.CVE_ENTIDAD = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    elemento.CVE_REGION_CATASTRAL = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    elemento.CVE_MUNICIPIO = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                    elemento.CVE_ZONA_CATASTRAL = !reader.IsDBNull(14) ? reader.GetString(14) + "" : 0 + "";
                    elemento.CVE_LOCALIDAD = !reader.IsDBNull(15) ? reader.GetString(15) : "";
                    elemento.CVE_SECTOR_CATASTRAL = !reader.IsDBNull(16) ? reader.GetString(16) : "";
                    elemento.CVE_ASENTAMIENTO = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    elemento.TIPO_ASENTAMIENTO = !reader.IsDBNull(18) ? reader.GetString(18) : "";
                    elemento.NOMBRE_ASENTAMIENTO = !reader.IsDBNull(19) ? reader.GetString(19) : "";
                    elemento.CP = !reader.IsDBNull(20) ? reader.GetString(20) : "";
                    elemento.NOMBRE_COMPLETO_ASENTAMIENTO = !reader.IsDBNull(21) ? reader.GetString(21) : "";
                    elemento.OBSERVACIONES = !reader.IsDBNull(22) ? reader.GetString(22) : "";
                    lista.Add(elemento);
                    elemento = new SIS_ASENTAMIENTOS();
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine($"SQL Error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
            }
            return lista;

        }

        public string obtenerCatalogoNombreAsentamiento(String consulta, string municipio)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = Utilerias.Constantes.DataSource,
                UserID = Constantes.UserID,
                Password = Constantes.Password,
                InitialCatalog = Constantes.InitialCatalogM + municipio
            };

            String nombre = "";
       
            var connectionString = builder.ConnectionString;
            var connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();

                var command = new SqlCommand(consulta, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {                   
                   nombre =  !reader.IsDBNull(21) ? reader.GetString(21) : ""; //Nombre_Completo_asentamiento
                    break;
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine($"SQL Error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
            }
            return nombre;
        }

        public int obtenerTotalNombreAsentamiento(String consulta, string municipio)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = Utilerias.Constantes.DataSource,
                UserID = Constantes.UserID,
                Password = Constantes.Password,
                InitialCatalog = Constantes.InitialCatalogM + municipio
            };

            int total = 0;

            var connectionString = builder.ConnectionString;
            var connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();

                var command = new SqlCommand(consulta, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    total = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0; //total
                    break;
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine($"SQL Error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
            }
            return total;
        }
    }
}