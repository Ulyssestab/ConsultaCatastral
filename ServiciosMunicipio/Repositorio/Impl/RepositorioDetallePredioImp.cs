using ServiciosMunicipio.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Repositorio.Impl
{
    public class RepositorioDetallePredioImp : RepositorioDetallePredio
    {
        public DetallePredio ObtenerElemento(string consulta, string municipio)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "10.1.2.126",
                UserID = "CatastroSA",
                Password = "CatAdmin#",
                InitialCatalog = "GDB01" + municipio
            };

            DetallePredio elemento = new DetallePredio();

            var connectionString = builder.ConnectionString;
            var connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();

                var command = new SqlCommand(consulta, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    elemento.CVE_CAT_EST = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                    elemento.CVE_CAT_ORI = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    elemento.clavePredial = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    elemento.NOM_MUNICIPIO = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    elemento.NOM_LOCALIDAD = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    elemento.Domicilio_Notificaion = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    elemento.FOLIO_REAL = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    elemento.NOMBRE_COMPLETO_ASENTAMIENTO = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    elemento.NOMBRE_COMPLETO_VIALIDAD = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    elemento.NUMERO_EXTERIOR = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    elemento.VALOR_CATASTRAL = !reader.IsDBNull(10) ? reader.GetString(10) + "" : 0+ "";
                    elemento.VALOR_UNITARIO_TERRENO = !reader.IsDBNull(11) ? reader.GetDecimal(11) + "" : Decimal.MinValue + "";
                    elemento.NOMBRE_O_RAZON_SOCIAL = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    elemento.APELLIDO_PATERNO = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                    elemento.APELLIDO_MATERNO = !reader.IsDBNull(14) ? reader.GetString(14) : "";                    
                    elemento.SUP_TERRENO_ESCRITURAS = !reader.IsDBNull(15) ? reader.GetDecimal(15) + "" : Decimal.MinValue + "";
                    elemento.SUP_CONSTRUCCION = !reader.IsDBNull(16) ? reader.GetDecimal(16) + "" : Decimal.MinValue + "";
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

        public int ObtenerTotal(string consulta, string municipio)
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
            return resultado;
        }

        public List<Tramite> obtenerTramites(string consulta)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "10.1.2.126",
                UserID = "CatastroSA",
                Password = "CatAdmin#",
                InitialCatalog = "WFTRAMITES"
            };

            Tramite elemento = new Tramite();
            List<Tramite> lista = new List<Tramite>();

            var connectionString = builder.ConnectionString;
            var connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();

                var command = new SqlCommand(consulta, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    elemento.NumeroTramite = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                    elemento.estatus = !reader.IsDBNull(1) ? reader.GetBoolean(1) + "" : "";
                    elemento.NombreTramite = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    lista.Add(elemento);
                    elemento = new Tramite();
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

        public  List<TareasTramite> obtenerListaTareasTramites(String consulta)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "10.1.2.126",
                UserID = "CatastroSA",
                Password = "CatAdmin#",
                InitialCatalog = "WFTRAMITES"
            };

            TareasTramite elemento = new TareasTramite();
            List<TareasTramite> lista = new List<TareasTramite>();

            var connectionString = builder.ConnectionString;
            var connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();

                var command = new SqlCommand(consulta, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    elemento.Tarea = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                    elemento.Orden = !reader.IsDBNull(1) ? reader.GetInt32(1) + "" : "";
                    elemento.estatus = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    lista.Add(elemento);
                    elemento = new TareasTramite();
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
    }
}