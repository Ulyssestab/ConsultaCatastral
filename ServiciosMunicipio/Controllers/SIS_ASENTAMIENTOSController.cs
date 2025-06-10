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
    public class SIS_ASENTAMIENTOSController : Controller
    {
        // GET: SIS_ASENTAMIENTOS/Asentamiento/5
        public ActionResult Asentamiento(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var r = new SIS_ASENTAMIENTOS_Dao().obtenerAsentamiento(id);

            return Json(r ,JsonRequestBehavior.AllowGet);
        }

        // Post: SIS_ASENTAMIENTOS/Asentamiento/ AjaxResultados -Fraccionamiento o Colonia
        [System.Web.Http.HttpPost]
        public ActionResult Asentamientos([FromBody] SIS_ASENTAMIENTOS asentamiento)
        {

            if (asentamiento.CVE_MUNICIPIO == null && asentamiento.CVE_LOCALIDAD == null && asentamiento.NOMBRE_ASENTAMIENTO == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var r = new SIS_ASENTAMIENTOS_Dao().obtenerAsentamientos(asentamiento);

            return Json(r);
        }

        // Post: SIS_ASENTAMIENTOS/NombreAsentamientos/ AjaxResultados -Fraccionamiento o Colonia
        [System.Web.Http.HttpPost]
        public ActionResult NombreAsentamientos([FromBody] SIS_ASENTAMIENTOS asentamiento)
        {

            if (asentamiento.CVE_MUNICIPIO == null && asentamiento.CVE_LOCALIDAD == null && asentamiento.CVE_ASENTAMIENTO == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var r = new SIS_ASENTAMIENTOS_Dao().obtenerCatalogoNombreAsentamiento(asentamiento);

            return Json(r);
        }

    }
}
