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
        // GET: DetallePredio
        public ActionResult Resultado(String claveCatastralEst, String municipio)
        {
            DetallePredioDao dao = new DetallePredioDao();
            DetallePredio detallePredio = dao.getDetallePredio(claveCatastralEst, municipio);
            return Json(detallePredio, JsonRequestBehavior.AllowGet);
        }
    }
}