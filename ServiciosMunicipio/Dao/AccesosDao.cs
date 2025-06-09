using ServiciosMunicipio.Models;
using ServiciosMunicipio.Repositorio.Impl;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Dao
{
    public class AccesosDao
    {
        private WFTRAMITESEntities db = new WFTRAMITESEntities();
        private RepositorioAccesosImp dao = new RepositorioAccesosImp();
        public Boolean AccesoUsuario(String nombreUsuario)
        {
            Boolean existe = false;
            List<Cat_Acceso> acceso = db.Cat_Acceso.SqlQuery("SELECT * FROM dbo.Cat_Acceso where NombreUsuario = '" + @nombreUsuario + "'").ToList<Cat_Acceso>();
            if (acceso.Count > 0) 
            {
                existe = acceso[0].NombreUsuario != null ? true : false;
            }
            return existe;
        }

        public String AccesoUsuarioPerfil(String nombreUsuario)
        {
            String r = "";
            List<Usuario> acceso = db.Usuario.SqlQuery("SELECT * FROM dbo.Usuario where NombreUsuario = '" + @nombreUsuario + "' and Habilitado = 'true'").ToList<Usuario>();
            if (acceso.Count > 0)
            {
                r = acceso[0].FK_Cat_Perfil != null ? acceso[0].FK_Cat_Perfil + "" : "";
            }
            return r;
        }

        public Usuario existeAccesoUsuario(String usuario, String password) //existUserAccess
        {
            usuario = AntiInjectionSQL.quitarComillas(usuario, Constantes.LONG_MAX_NOM_USUARIO);
            String consulta = "IF (\n" +
                    "	SELECT count(*) \n" +
                    "	FROM [WFTramites].[dbo].[Usuario] \n" +
                    "	WHERE [NombreUsuario]='" + usuario + "' and Habilitado='true') > 0\n" +
                    "	BEGIN   IF (\n" +
                    "		SELECT count(*) \n" +
                    "		FROM [WFTramites].[dbo].[Usuario] \n" +
                    "		WHERE [NombreUsuario]='" + usuario + "' and Habilitado='true' and SesionActivaConsulta = 0) > 0    \n" +
                    "	BEGIN\n" +
                    "		IF (SELECT COUNT(*)\n" +

                    "		FROM WFTramites.dbo.Usuario      \n" +
                    "		WHERE NombreUsuario='" + usuario + "'\n" +
                    "			AND Habilitado='true' \n" +
                    "			AND SesionActivaConsulta = 0  \n" +
                    "			AND Contrasena = '" + password + "') > 0\n" +
                    "		BEGIN\n" +
                    "      IF(SELECT COUNT(*)FROM WFTRAMITES.DBO.Usuario WHERE NombreUsuario='" + usuario + "' AND Habilitado=1 and SistemaVisualizador=1)>0" +
                    "      BEGIN" +
                    "			SELECT NombreCompleto,ApellidoPaterno,ApellidoMaterno,NombreUsuario,Contrasena,FK_Puesto,FK_Coordinacion,FK_Cat_Municipio,FK_Cat_Perfil\n" +
                    "			FROM WFTramites.dbo.Usuario      \n" +
                    "			WHERE NombreUsuario='" + usuario + "'\n" +
                    "				AND Habilitado='true' \n" +
                    "				AND SesionActivaConsulta = 0  \n" +
                    "				AND Contrasena = '" + password + "'\n" +
                    "			UPDATE [WFTramites].[dbo].[Usuario]\n" +
                    "			SET SesionActivaConsulta = 1\n" +
                    "			WHERE NombreUsuario = '" + usuario + "'\n" +
                    "		END\n" +
                    "ELSE\n" +
                    "BEGIN\n" +
                    "SELECT '' AS NombreCompleto,\n " +
                    "'' AS ApellidoPaterno, \n" +
                    "'' AS ApellidoMaterno, \n" +
                    "'USUARIO' AS NombreUsuario, \n" +
                    "'NO_HABLITADO' AS Contrasena,\n" +
                    "'' AS FK_Puesto,\n" +
                    "'' AS FK_Coordinacion, \n" +
                    "'' AS FK_Cat_Municipio, \n" +
                    "'' AS FK_Cat_Perfil \n" +
                    "END \n" +
                    "END \n" +
                    "		ELSE\n" +
                    "		BEGIN\n" +
                    "			SELECT '' AS NombreCompleto, \n" +
                    "			'' AS ApellidoPaterno, \n" +
                    "			'' AS ApellidoMaterno, \n" +
                    "			'PASSWORD' AS NombreUsuario, \n" +
                    "			'NO_CORRECTO' AS Contrasena, \n" +
                    "			'' AS FK_Puesto,\n" +
                    "			'' AS FK_Coordinacion, \n" +
                    "			'' AS FK_Cat_Municipio, \n" +
                    "                       '' AS FK_Cat_Perfil \n" +
                    "		END\n" +
                    "	END   \n" +
                    "	ELSE\n" +
                    "	BEGIN\n" +
                    "       SELECT '' AS NombreCompleto, \n" +
                    "	   '' AS ApellidoPaterno, \n" +
                    "	   '' AS ApellidoMaterno, \n" +
                    "	   'EL_USUARIO' AS NombreUsuario, \n" +
                    "	   'ESTA_EN_SESION' AS Contrasena, \n" +
                    "	   '' AS FK_Puesto,\n" +
                    "          '' AS FK_Coordinacion, \n" +
                    "	   '' AS FK_Cat_Municipio, \n" +
                    "          '' AS FK_Cat_Perfil \n" +
                    "	END\n" +
                    "END";
            
            return dao.existeAccesoUsuario(consulta);
        }
        
    }
}