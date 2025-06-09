using ServiciosMunicipio.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiciosMunicipio.Controllers
{
    public class HomeController : Controller
    {
        AccesosDao dao = new AccesosDao();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

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
    }
}
