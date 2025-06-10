using ServiciosMunicipio.Dao;
using ServiciosMunicipio.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiciosMunicipio.Controllers
{
    public class DetallePredioController : Controller
    {
        private DetallePredioDao dao = new DetallePredioDao();
        // GET: DetallePredio/Resultado
        public ActionResult Resultado(String claveCatastralEst, String municipio)
        {            
            DetallePredio detallePredio = dao.getDetallePredio(claveCatastralEst, municipio);
            return Json(detallePredio, JsonRequestBehavior.AllowGet);
        }
        // GET: DetallePredio/Tramites
        public ActionResult Tramites(String claveCatastralEst)
        {            
            List<Tramite> tramites = dao.obtenerTramites(claveCatastralEst);
            return Json(tramites, JsonRequestBehavior.AllowGet);
        }

        // GET: DetallePredio/TareasTramites
        public ActionResult DetalleTareasTramites(String numeroTramite)
        {
            List<TareasTramite> tramites = dao.obtenerDetalleTareasTramite(numeroTramite);
            return Json(tramites, JsonRequestBehavior.AllowGet);
        }
    }
}