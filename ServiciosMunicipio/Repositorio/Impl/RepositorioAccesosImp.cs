using ServiciosMunicipio.Models;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Repositorio.Impl
{
    public class RepositorioAccesosImp : RepositorioAccesos
    {
        public Usuario existeAccesoUsuario(string consulta)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = Constantes.DataSource,
                UserID = Constantes.UserID,
                Password = Constantes.Password,
                InitialCatalog = Constantes.InitialCatalogW
            };

            Usuario usuario = new Usuario();

            var connectionString = builder.ConnectionString;
            var connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();

                var command = new SqlCommand(consulta, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    usuario.NombreUsuario = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                    usuario.NombreCompleto = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    usuario.FK_Puesto = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0;
                    usuario.FK_Coordinacion = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                    usuario.Contrasena = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    usuario.FK_Cat_Perfil = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
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
            return usuario;
        }

    }
}