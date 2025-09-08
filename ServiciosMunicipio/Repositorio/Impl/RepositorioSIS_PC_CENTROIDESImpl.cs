using ServiciosMunicipio.Models.Entidades;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Repositorio.Impl
{
    public class RepositorioSIS_PC_CENTROIDESImpl : RepositorioSIS_PC_CENTROIDES
    {
        public SIS_PC_CENTROIDES ObtenerCoordenadas(string municipio, string CVE_CAT_EST)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = Utilerias.Constantes.DataSource,
                UserID = Constantes.UserID,
                Password = Constantes.Password,
                InitialCatalog = Constantes.InitialCatalogM + municipio
            };

            SIS_PC_CENTROIDES elemento = new SIS_PC_CENTROIDES();

            var connectionString = builder.ConnectionString;
            var connection = new SqlConnection(connectionString);
            String consulta = "select OBJECTID, CENT_PRED_X, CENT_PRED_Y from GDB01001.sde.SIS_PC_CENTROIDES where CVE_CAT_EST = '" + CVE_CAT_EST + "'";
            try
            {

                connection.Open();

                var command = new SqlCommand(consulta, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    elemento.OBJECTID = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
                    elemento.CENT_PRED_X = !reader.IsDBNull(1) ? reader.GetDecimal(1) : 0;
                    elemento.CENT_PRED_Y = !reader.IsDBNull(2) ? reader.GetDecimal(2) : 0;                    
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

         public List<SIS_PC_CENTROIDES> ObtenerLista(string consulta, string municipio, string CVE_CAT_EST)
        {
            throw new NotImplementedException();
        }
    }
}