using log4net;
using log4net.Config;
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
        private static readonly ILog log = LogManager.GetLogger(typeof(RepositorioAccesosImp));
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
                    
                    usuario.NombreCompleto = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                    usuario.ApellidoPaterno = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    usuario.ApellidoMaterno = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    usuario.NombreUsuario = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    usuario.Contrasena = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    usuario.FK_Puesto = !reader.IsDBNull(5) ? Int32.Parse(reader.GetString(5) == "" ? "0" : reader.GetString(5)) : 0;
                    usuario.FK_Coordinacion = !reader.IsDBNull(6) ? Int32.Parse(reader.GetString(6) == "" ? "0" : reader.GetString(6)) : 0;
                    usuario.FK_Cat_Municipio = !reader.IsDBNull(7) ? Int32.Parse(reader.GetString(7) == "" ? "0" : reader.GetString(7)) : 0;
                    usuario.FK_Cat_Perfil = !reader.IsDBNull(8) ? Int32.Parse(reader.GetString(8) == "" ? "0" : reader.GetString(8)) : 0;
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
            return usuario;
        }

        public string AccesoUsuarioPerfilPortal(string consulta)
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

                    usuario.FK_Cat_Perfil = !reader.IsDBNull(21) ? reader.GetInt32(21) : 0;                  
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
            return usuario.FK_Cat_Perfil +"";
        }

        public bool existeAccesoUsuarioPortal(string consulta)
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

                    usuario.NombreCompleto = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                    usuario.NombreUsuario = !reader.IsDBNull(3) ? reader.GetString(3) : "";
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
            return !String.IsNullOrEmpty(usuario.NombreCompleto) ? true : false;
        }

        public string getRoleNameUsuario(string consulta)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = Constantes.DataSource,
                UserID = Constantes.UserID,
                Password = Constantes.Password,
                InitialCatalog = Constantes.InitialCatalogW
            };

            String rol = "";

            var connectionString = builder.ConnectionString;
            var connection = new SqlConnection(connectionString);

            try
            {


                connection.Open();

                var command = new SqlCommand(consulta, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    rol = !reader.IsDBNull(0) ? reader.GetString(0) : "";                    
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
            return rol;
        }

        public int cerrarSesion(string consulta)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = Constantes.DataSource,
                UserID = Constantes.UserID,
                Password = Constantes.Password,
                InitialCatalog = Constantes.InitialCatalogW
            };
            

            var connectionString = builder.ConnectionString;
            var connection = new SqlConnection(connectionString);

            try
            {


                connection.Open();

                var command = new SqlCommand(consulta, connection);
                return command.ExecuteNonQuery();                

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
            return 0;
        }
    }
}