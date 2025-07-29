using ServiciosMunicipio.Models;
using ServiciosMunicipio.Repositorio.Impl;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public Usuario validarCredenciales(String username, String password)
        {
            
            
            Usuario user = null;
            String error = "";
            String activo;
            String sistema ="";
            String tipo_usuario;

            if (Utilidades.cadenasValidate(username) && Utilidades.cadenasValidate(password))
            {
                Boolean acceso_user = AccesoUsuario(username);
                String User_Perfil = AccesoUsuarioPerfil(username);
                //existUser(username, EcriptarString.getStringMessageDigest(password, EcriptarString.SHA1));
                //user = Buscar.existUserAccess(username, EcriptarString.getStringMessageDigest(password, EcriptarString.SHA1));
                user = existeAccesoUsuario(username, password);
                if (user != null && !String.IsNullOrEmpty(user.NombreUsuario))
                {
                    //Existe y se obtuvo la información si no está en sesión                          
                    if (!user.NombreUsuario.Equals("PASSWORD") && !user.Contrasena.Equals("NO CORRECTO"))
                    {
                        if (!user.NombreUsuario.Equals("USUARIO") && !user.Contrasena.Equals("NO_HABLITADO"))
                        {
                            DateTime fechaActual2 = DateTime.Now;
                            int Dia2 = ((int)fechaActual2.DayOfWeek);
                            int Hora2 = fechaActual2.Hour;

                            if (acceso_user == true && (Dia2 >= 2 && Dia2 <= 6) && (Hora2 >= 8 && Hora2 <= 19))
                            {
                                activo = "SI";
                                sistema = "/vistas/interno/index.jsp";
                                tipo_usuario = 2 + "";

                                /*
                                HttpSession sesion = request.getSession(true);
                                sesion.setAttribute("idsession", request.getRequestedSessionId());
                                sesion.setAttribute("username", user.getNombreUsuario());
                                sesion.setAttribute("activo", activo);
                                sesion.setAttribute("tipo_usuario", tipo_usuario);
                                sesion.setAttribute("mPioMapa", mPioMapa);
                                sesion.setAttribute("ROLE", Buscar.getRoleNameUsuario(username));
                                sesion.setAttribute("MUNICIPIOS_LIST", Buscar.getMunicipioUsuario(username));*/

                                //Se agrega al objeto sesion el role de usuario para las validaciones de visibilidad                               
                                return user;
                            }
                            if (!user.NombreUsuario.Equals("EL_USUARIO") && !user.Contrasena.Equals("ESTA_EN_SESION"))
                            {                                
                                //Si usuario es hallado en la base de datos
                                DateTime fechaActual = DateTime.Now;
                                int Dia = ((int)fechaActual2.DayOfWeek);
                                int Hora = fechaActual2.Hour;

                                if (User_Perfil.Equals("1") || User_Perfil.Equals("2") || User_Perfil.Equals("1002") || User_Perfil.Equals("5009")
                                        || User_Perfil.Equals("6013") || User_Perfil.Equals("6014") || User_Perfil.Equals("6015") || User_Perfil.Equals("6016")
                                        || User_Perfil.Equals("6021") || User_Perfil.Equals("7021") || User_Perfil.Equals("8023") || User_Perfil.Equals("8024")
                                        || User_Perfil.Equals("8025") || User_Perfil.Equals("9025") || User_Perfil.Equals("9026") || User_Perfil.Equals("9027")
                                        || User_Perfil.Equals("9028") || User_Perfil.Equals("9029"))
                                {
                                    if ((Dia >= 2 && Dia <= 6) && (Hora >= 8 && Hora <= 19))
                                    {
                                        activo = "SI";
                                        sistema = "/vistas/interno/index.jsp";
                                        tipo_usuario = tipoUsuario(user) + "";

                                      /*  System.out.println(tipo_usuario);
                                        HttpSession sesion = request.getSession(true);
                                        sesion.setAttribute("idsession", request.getRequestedSessionId());
                                        sesion.setAttribute("username", user.getNombreUsuario());
                                        sesion.setAttribute("activo", activo);
                                        sesion.setAttribute("tipo_usuario", tipo_usuario);
                                        sesion.setAttribute("mPioMapa", mPioMapa);
                                        sesion.setAttribute("ROLE", Buscar.getRoleNameUsuario(username));
                                        sesion.setAttribute("MUNICIPIOS_LIST", Buscar.getMunicipioUsuario(username));*/

                                        //Se agrega al objeto sesion el role de usuario para las validaciones de visibilidad
                                        //getServletConfig().getServletContext().getRequestDispatcher(sistema).forward(request, response);
                                        return user;
                                    }
                                    else
                                    {
                                        error = "Este usuario no tiene acceso.";
                                        activo = "NO";
                                       // request.setAttribute("error", error);
                                       // getServletConfig().getServletContext().getRequestDispatcher("/").forward(request, response);
                                        return user;
                                    }
                                }
                                else
                                {
                                    switch (tipoUsuario(user)) 
                                    {
                                        case 1:
                                            activo = "SI";
                                            sistema = "/vistas/interno/index.jsp";
                                            tipo_usuario = tipoUsuario(user) + "";
                                            break;
                                        case 2:
                                            activo = "SI";
                                            sistema = "/vistas/interno/index.jsp";
                                            tipo_usuario = tipoUsuario(user) + "";
                                            break;
                                        case 3:
                                            int totalPredios = db.UsuarioPropietario.SqlQuery("  SELECT * as npredios FROM [dbo].[UsuarioPropietario] as up where "
                                                + "  [FK_NombreUsuario]='" + @username + "'").ToList().Count;
                                            if (totalPredios > 0)
                                            {
                                                activo = "SI";
                                                tipo_usuario = tipoUsuario(user) + "";
                                                sistema = "/vistas/externo/index.jsp";
                                            }
                                            else
                                            {
                                                error = "Este usuario no tiene predios relacionados";
                                               // request.setAttribute("error", error);
                                               // getServletConfig().getServletContext().getRequestDispatcher("/").forward(request, response);
                                                return user;
                                            }
                                            break;
                                        default:
                                            error = "Este usuario no tiene acceso.";
                                            activo = "NO";
//                                            request.setAttribute("error", error);
 //                                           getServletConfig().getServletContext().getRequestDispatcher("/").forward(request, response);
                                            return user;   
                                    }
                                    if (activo.Equals("SI"))
                                    {
                                     /*   System.out.println(tipo_usuario);
                                        HttpSession sesion = request.getSession(true);
                                        sesion.setAttribute("idsession", request.getRequestedSessionId());
                                        sesion.setAttribute("username", user.getNombreUsuario());
                                        sesion.setAttribute("activo", activo);
                                        sesion.setAttribute("tipo_usuario", tipo_usuario);
                                        sesion.setAttribute("mPioMapa", mPioMapa);
                                        sesion.setAttribute("ROLE", Buscar.getRoleNameUsuario(username));
                                        sesion.setAttribute("MUNICIPIOS_LIST", Buscar.getMunicipioUsuario(username));*/
                                    }
                                    //Se agrega al objeto sesion el role de usuario para las validaciones de visibilidad
                                    //getServletConfig().getServletContext().getRequestDispatcher(sistema).forward(request, response);
                                    return user;
                                }
                            }
                            error = "El usuario " + username + " ya tiene una sesion activa.";
                        }

                        else
                        {
                            error = "El usuario " + username + " no tiene permisos para ingresar al sistema";
                        }

                    }
                    else
                    {
                        error = "La contraseña es incorrecta.";
                    }

                }
                else
                {
                    error = "Esta cuenta de usuario no existe.";
                }
                ///////////////EN EXIST USER
            }
            else
            {
                error = "Usuario o Contraseña no válidos.";
            }

            return user;
        }
        private int tipoUsuario(Usuario user)
        {
            if (user.FK_Puesto > 0)
            {
                return user.FK_Puesto;
            }
            return 0;
        }
    }

}