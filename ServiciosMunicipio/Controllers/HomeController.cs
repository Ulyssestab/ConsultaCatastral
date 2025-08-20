using log4net;
using log4net.Config;
using ServiciosMunicipio.Dao;
using ServiciosMunicipio.Models;
using ServiciosMunicipio.Models.Entidades;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ServiciosMunicipio.Controllers
{
    public class HomeController : Controller
    {
        private AccesosDao dao = new AccesosDao();
        private Servicios_Consulta_Cat_Dao daoServ = new Servicios_Consulta_Cat_Dao();
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));

        //Home/Index    
        public ActionResult Index()
        {
            log.Info("Clase de inicio --");
            log.Info("Clase de inicio --");
            log.Debug("Debug logging");
            log.Info("Info logging");
            log.Warn("Warn logging");
            log.Error("Error logging");
            log.Fatal("Fatal logging");
            ViewBag.Title = "Home Page";
            List<Servicios_Consulta_Cat> lista = daoServ.obtenerLista();
              
            ViewBag.Resultados = lista;
            return View();
        }

        //Home/Acceso
        [System.Web.Http.HttpPost]
        public ActionResult Acceso([FromBody] String nombreUsuario)
        {
            String s = nombreUsuario;
            Boolean acceso = dao.AccesoUsuario(nombreUsuario);
            return Json(acceso, JsonRequestBehavior.AllowGet);
        }


        //Home/ObtenerAcceso
        public ActionResult ObtenerAcceso(String nombreUsuario, String pass)
        {
            Usuario user = new Usuario();

            if (String.IsNullOrEmpty(nombreUsuario) || String.IsNullOrEmpty(pass))
            {
                return Json("ERROR", JsonRequestBehavior.AllowGet);
            }
            else 
            {
                return Json(validarCredenciales(nombreUsuario, pass), JsonRequestBehavior.AllowGet);
            }
           
        }

        //Home/AccesoUsuarioPerfil
        public ActionResult AccesoUsuarioPerfil(String nombreUsuario)
        {
            return Json(dao.AccesoUsuarioPerfil(nombreUsuario), JsonRequestBehavior.AllowGet);
        }
  
        public Usuario validarCredenciales(String username, String password)
        {


            Usuario user = null;            
            String activo;            
            String tipo_usuario;
            String mPioMapa = "";

            if (Utilidades.cadenasValidate(username) && Utilidades.cadenasValidate(password))
            {
                Boolean acceso_user = dao.AccesoUsuario(username);
                String User_Perfil = dao.AccesoUsuarioPerfil(username);
                
                user = dao.existeAccesoUsuario(username, password);
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
                          
                                tipo_usuario = 2 + "";
                                if (this.Session != null) { 
                                    this.Session["idsession"] = Session.SessionID; 
                                    this.Session["username"] = user.NombreUsuario;
                                    this.Session["activo"] = activo;
                                    this.Session["tipo_usuario"] = tipo_usuario;
                                    this.Session["mPioMapa"] = mPioMapa;
                                    this.Session["ROLE"] = dao.getRoleNameUsuario(username);
                                    this.Session["MUNICIPIOS_LIST"] = dao.getMunicipioUsuario(username);
                                }                                

                                //Se agrega al objeto sesion el role de usuario para las validaciones de visibilidad                               
                                return user;
                            }
                            if (!user.NombreUsuario.Equals("EL_USUARIO") && !user.Contrasena.Equals("ESTA_EN_SESION"))
                            {
                                //Si usuario es hallado en la base de datos
                                DateTime fechaActual = DateTime.Now;
                                int Dia = ((int)fechaActual2.DayOfWeek);
                                int Hora = fechaActual2.Hour;

                                if (dao.validarPerfil(User_Perfil))
                                {
                                    if ((Dia >= 2 && Dia <= 6) && (Hora >= 8 && Hora <= 19))
                                    {
                                        activo = "SI";                                        
                                        tipo_usuario = dao.tipoUsuario(user) + "";

                                        if (this.Session != null)
                                        {
                                            this.Session["idsession"] = Session.SessionID; //request.getRequestedSessionId()
                                            this.Session["username"] = user.NombreUsuario;
                                            this.Session["activo"] = activo;
                                            this.Session["tipo_usuario"] = tipo_usuario;
                                            this.Session["mPioMapa"] = mPioMapa;
                                            this.Session["ROLE"] = dao.getRoleNameUsuario(username);
                                            this.Session["MUNICIPIOS_LIST"] = dao.getMunicipioUsuario(username);
                                        }

                                        //Se agrega al objeto sesion el role de usuario para las validaciones de visibilidad                                      
                                        return user;
                                    }
                                    else
                                    {
                                        user.VersionSDE = "Este usuario no tiene acceso.";
                                        activo = "NO";
                                        if (this.Session != null)
                                        {
                                            this.Session["error"] = "Error";
                                        }
                                        return user;
                                    }
                                }
                                else
                                {
                                    switch (dao.tipoUsuario(user))
                                    {
                                        case 1:
                                            activo = "SI";                                        
                                            tipo_usuario = dao.tipoUsuario(user) + "";
                                            break;
                                        case 2:
                                            activo = "SI";                                            
                                            tipo_usuario = dao.tipoUsuario(user) + "";
                                            break;
                                        case 3:
                                            int totalPredios = dao.totalPredios(username);
                                            if (totalPredios > 0)
                                            {
                                                activo = "SI";
                                                tipo_usuario = dao.tipoUsuario(user) + "";                                                
                                            }
                                            else
                                            {
                                                if (this.Session != null) 
                                                {
                                                    this.Session["error"] = "Este usuario no tiene predios relacionados";
                                                }                                                                                                
                                                return user;
                                            }
                                            break;
                                        default:
                                            if (this.Session != null)
                                            {
                                                this.Session["error"] = "Este usuario no tiene acceso.";
                                            }                                            
                                            activo = "NO";
                                            return user;
                                    }
                                    if (activo.Equals("SI"))
                                    {
                                        if (this.Session != null)
                                        {
                                            this.Session["idsession"] = Session.SessionID; 
                                            this.Session["username"] = user.NombreUsuario;
                                            this.Session["activo"] = activo;
                                            this.Session["tipo_usuario"] = tipo_usuario;
                                            this.Session["mPioMapa"] = mPioMapa;
                                            this.Session["ROLE"] = dao.getRoleNameUsuario(username);
                                            this.Session["MUNICIPIOS_LIST"] = dao.getMunicipioUsuario(username);
                                        }                                        
                                    }
                                    return user;
                                }
                            }
                            if (this.Session != null)
                            {
                                this.Session["error"] = "El usuario " + username + " ya tiene una sesion activa.";
                            }                            
                        }

                        else
                        {
                            if (this.Session != null)
                            {
                                this.Session["error"] = "El usuario " + username + " no tiene permisos para ingresar al sistema";
                            }
                        }

                    }
                    else
                    {
                        if (this.Session != null)
                        {
                            this.Session["error"] = "La contraseña es incorrecta.";
                        }
                    }

                }
                else
                {
                    if (this.Session != null)
                    {
                        this.Session["error"] = "Esta cuenta de usuario no existe.";
                    }
                }
                ///////////////EN EXIST USER
            }
            else
            {
                if (this.Session != null)
                {
                    this.Session["error"] = "Usuario o Contraseña no válidos.";
                }
            }

            return user;
        }
    }

}
