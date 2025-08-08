using ServiciosMunicipio.Models;
using ServiciosMunicipio.Repositorio.Impl;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public Boolean AccesoUsuarioPortal(String nombreUsuario)
        {
            Boolean existe = false;
            String consulta = "SELECT * FROM dbo.Cat_Acceso where NombreUsuario = '" + @nombreUsuario + "'";
            existe = dao.existeAccesoUsuarioPortal(consulta);                        
            return existe;
        }

        public String AccesoUsuarioPerfilPortal(String nombreUsuario)
        {
            String r = "";
            String consulta = "SELECT * FROM dbo.Usuario where NombreUsuario = '" + @nombreUsuario + "' and Habilitado = 'true'";
            r = dao.AccesoUsuarioPerfilPortal(consulta);     
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
        
        public String getMunicipioUsuario(string username)
        {
            String tipo = "";
            username = AntiInjectionSQL.quitarComillas(username, Constantes.LONG_MAX_NOM_USUARIO);

            //Defino la consulta a realizar
            String consulta =
           "DECLARE @TIPO_U VARCHAR(5)\n" +
           "SELECT @TIPO_U = U.FK_Puesto\n" +
           "FROM dbo.USUARIO AS U \n" +
           "WHERE U.NombreUsuario = '" + username + "'\n" +
           "IF @TIPO_U = 3\n" +
           "BEGIN\n" +
           "	SELECT '000' AS CVE_MUNICIPIO, 'EXTERNO' AS NOM_MUNICIPIO\n" +
           "END\n" +
           "ELSE\n" +
           "BEGIN\n" +
           "	DECLARE @ROLE_NAME VARCHAR(50)\n" +
           "	SELECT @ROLE_NAME = RVP.ROLE_NAME\n" +
           "	FROM dbo.Usuario AS U\n" +
           "		INNER JOIN dbo.ROLE_CONSULTA_VISIBILIDAD_PERFIL AS RVP ON RVP.ID_PERFIL = U.FK_Cat_Perfil\n" +
           "	WHERE U.NombreUsuario = '" + username + "'\n" +
           "	IF @ROLE_NAME = 'ROLE_SEDUM' OR @ROLE_NAME = 'ROLE_MUNICIPIO'\n" +
           "	BEGIN\n" +
           "		SELECT MC.CVE_MUNICIPIO, MC.NOM_MUNICIPIO\n" +
           "		FROM dbo.Usuario AS U\n" +
           "			INNER JOIN dbo.MUNICIPIO_CONSULTA AS MC ON MC.CVE_MUNICIPIO = U.FK_Cat_Municipio\n" +
           "		WHERE U.NombreUsuario = '" + username + "'\n" +
           "	END\n" +
           "	ELSE\n" +
           "	BEGIN\n" +
           "		SELECT distinct SMC.CVE_MUNICIPIO, MC.NOM_MUNICIPIO\n" +
           "		FROM dbo.Usuario AS U\n" +
           "			INNER JOIN dbo.Cat_Perfil AS CP ON CP.Id = U.FK_Cat_Perfil\n" +
           "			INNER JOIN dbo.REL_PERFILMUNICIPIOCONSULTA AS SMC ON U.FK_Cat_Perfil = SMC.ID_PERFIL\n" +
           "			INNER JOIN dbo.MUNICIPIO_CONSULTA AS MC ON MC.CVE_MUNICIPIO = SMC.CVE_MUNICIPIO\n" +
           "		WHERE U.NombreUsuario = '" + username + "' ORDER BY MC.NOM_MUNICIPIO\n" +
           "	END\n" +
           "END";
            tipo = dao.getRoleNameUsuario(consulta);
            return tipo;
        }

        public bool validarPerfil(String User_Perfil)
        {
            return User_Perfil.Equals("1") || User_Perfil.Equals("2") || User_Perfil.Equals("1002") || User_Perfil.Equals("5009")
                                        || User_Perfil.Equals("6013") || User_Perfil.Equals("6014") || User_Perfil.Equals("6015") || User_Perfil.Equals("6016")
                                        || User_Perfil.Equals("6021") || User_Perfil.Equals("7021") || User_Perfil.Equals("8023") || User_Perfil.Equals("8024")
                                        || User_Perfil.Equals("8025") || User_Perfil.Equals("9025") || User_Perfil.Equals("9026") || User_Perfil.Equals("9027")
                                        || User_Perfil.Equals("9028") || User_Perfil.Equals("9029");
        }

        public String getRoleNameUsuario(string username)
        {
            String rol = "";
            username = AntiInjectionSQL.quitarComillas(username, Constantes.LONG_MAX_NOM_USUARIO);
                        
            //Defino la consulta a realizar
            String consulta = "SELECT RC.ROLE_NAME\n" +
                "FROM dbo.ROLE_CONSULTA_VISIBILIDAD_PERFIL AS RC\n" +
                "LEFT JOIN dbo.Cat_Perfil AS CP ON RC.ID_PERFIL = CP.Id\n" +
                "LEFT JOIN dbo.Usuario AS U ON U.FK_Cat_Perfil = CP.Id\n" +
                "WHERE U.NombreUsuario = '" + username + "'";
            rol =  dao.getRoleNameUsuario(consulta);
            return rol;
        }

        public int tipoUsuario(Usuario user)
        {
            if (user.FK_Puesto > 0)
            {
                return user.FK_Puesto;
            }
            return 0;
        }

        public int totalPredios(String username)
        {
            return db.UsuarioPropietario.SqlQuery("  SELECT * as npredios FROM [dbo].[UsuarioPropietario] as up where "
                                                 + "  [FK_NombreUsuario]='" + @username + "'").ToList().Count;
        }
    }

}