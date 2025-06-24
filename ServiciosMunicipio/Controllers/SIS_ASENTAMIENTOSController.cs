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
        SIS_ASENTAMIENTOS_Dao buscar = new SIS_ASENTAMIENTOS_Dao();
        // GET: SIS_ASENTAMIENTOS/Asentamiento/5
        public ActionResult Asentamiento(int id, String municipio)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var r = buscar.obtenerAsentamiento(id, municipio);

            return Json(r ,JsonRequestBehavior.AllowGet);
        }

        // Post: SIS_ASENTAMIENTOS/Asentamientos/ AjaxResultados -Fraccionamiento o Colonia
        [System.Web.Http.HttpPost]
        public ActionResult Asentamientos([FromBody] SIS_ASENTAMIENTOS asentamiento)
        {
            int total = 0;
            List<SIS_ASENTAMIENTOS> r = new List<SIS_ASENTAMIENTOS>();
            if (asentamiento != null && asentamiento.CVE_MUNICIPIO != null && asentamiento.CVE_LOCALIDAD != null && asentamiento.NOMBRE_ASENTAMIENTO != null)
            {
                total = buscar.obtenerTotalNombreAsentamiento(asentamiento);
                if (total > 0) 
                {
                    r = buscar.obtenerAsentamientos(asentamiento);
                }
                
                return Json(r);
                
            }
            else
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            
        }

        // Post: SIS_ASENTAMIENTOS/NombreAsentamientos/ AjaxResultados -Fraccionamiento o Colonia
        [System.Web.Http.HttpPost]
        public ActionResult NombreAsentamientos([FromBody] SIS_ASENTAMIENTOS asentamiento)
        {
            if (asentamiento != null && asentamiento.CVE_MUNICIPIO != null && asentamiento.CVE_LOCALIDAD != null && asentamiento.NOMBRE_ASENTAMIENTO != null)
            {
                var r = buscar.obtenerCatalogoNombreAsentamiento(asentamiento);
                return Json(r);

            }
            else
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

        }

    }
}
