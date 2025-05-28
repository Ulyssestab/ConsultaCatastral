using InstitutoCatastralAGS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstitutoCatastralAGS.Repositorio.Impl
{
    public class RepositorioMunicipioImp : RepositorioMunicipio
    {
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

        public List<Resultados> ObtenerLista(String consulta, String municipio)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "10.1.2.126",
                UserID = "CatastroSA",
                Password = "CatAdmin#",
                InitialCatalog = "GDB01" + municipio
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
                    elemento.CVE_CAT_EST = reader.GetString(1);
                    elemento.CVE_CAT_ORI = reader.GetString(2);
                    elemento.clavePredial = reader.GetString(3);
                    elemento.NOMBRE_O_RAZON_SOCIAL = reader.GetString(4);
                    elemento.APELLIDO_PATERNO = reader.GetString(5);
                    elemento.APELLIDO_MATERNO = reader.GetString(6);
                    elemento.NOM_LOCALIDAD = reader.GetString(7);
                    elemento.NOMBRE_COMPLETO_ASENTAMIENTO = reader.GetString(8);
                    elemento.NOMBRE_COMPLETO_VIALIDAD = reader.GetString(9);
                    elemento.NUMERO_EXTERIOR = reader.GetString(10);
                    lista.Add(elemento);
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
