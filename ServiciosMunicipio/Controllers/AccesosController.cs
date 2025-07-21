using ServiciosMunicipio.Dao;
using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiciosMunicipio.Controllers
{
    public class AccesosController : Controller
    {
        private AccesosDao dao;
        // GET: Accesos
        public ActionResult ObtenerUsuario(String usuario, String password)
        {
            dao = new AccesosDao();
            Usuario usuarioApp = dao.existeAccesoUsuario(usuario, password);
            return Json(usuarioApp, JsonRequestBehavior.AllowGet);
        }
    }
}