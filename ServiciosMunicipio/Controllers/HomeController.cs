using log4net;
using log4net.Config;
using ServiciosMunicipio.Dao;
using ServiciosMunicipio.Models;
using ServiciosMunicipio.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult Acceso(String nombreUsuario)
        {

            return Json(dao.AccesoUsuario(nombreUsuario), JsonRequestBehavior.AllowGet);
        }

        //Home/ObtenerAcceso
        public ActionResult ObtenerAcceso(String nombreUsuario, String pass)
        {

            return Json(dao.existeAccesoUsuario(nombreUsuario, pass), JsonRequestBehavior.AllowGet);
        }

        //Home/AccesoUsuarioPerfil
        public ActionResult AccesoUsuarioPerfil(String nombreUsuario)
        {
            return Json(dao.AccesoUsuarioPerfil(nombreUsuario), JsonRequestBehavior.AllowGet);
        }
        //Home/AccesoUsuarioPerfil
        public ActionResult Formas()
        {
            List<Forma> formas = new List<Forma>();
            formas.Add(new Forma { accion = "POST", nombre = "formxCatastralEst", controlador = "ClaveCatastral" });
            formas.Add(new Forma { accion = "POST", nombre = "formxCatastralOri", controlador = "ClaveCatastralOriginal" });
            formas.Add(new Forma { accion = "POST", nombre = "formxPredial", controlador = "ClavePredial" });
            formas.Add(new Forma { accion = "POST", nombre = "formxFolioReal", controlador = "FolioReal" });
            formas.Add(new Forma { accion = "POST", nombre = "formxUbicacion", controlador = "Ubicacion" });
            formas.Add(new Forma { accion = "POST", nombre = "formxPerFisica", controlador = "NombrePropietarioPersonaFisica" });
            formas.Add(new Forma { accion = "POST", nombre = "formxPerMoral", controlador = "NombrePropietarioPersonaMoral" });
            return Json(formas, JsonRequestBehavior.AllowGet);
        }
    }
}
