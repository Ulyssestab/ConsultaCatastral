using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace ServiciosMunicipio.Conexion
{
    public class ConexionAsentamientosBD
    {        
        public int Conexion(String consulta, String municipio)
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
    }
}