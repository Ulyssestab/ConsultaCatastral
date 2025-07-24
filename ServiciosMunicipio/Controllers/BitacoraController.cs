using log4net;
using ServiciosMunicipio.Dao;
using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ServiciosMunicipio.Controllers
{
    public class BitacoraController : Controller
    {
        private BitacoraDao dao = new BitacoraDao();
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Bitacora/InsertarRegistroEnBitacora
        [System.Web.Http.HttpPost]
        public ActionResult InsertarRegistroEnBitacora([FromBody] BitacoraAccesoSistemas bitacora) {
            log.Info("Prueba de log --");
            String respuesta = dao.insertarDatos(bitacora);
            return Json(respuesta);
        }
    }
}