using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ServiciosMunicipio.Dao;
using ServiciosMunicipio.Models;

namespace ServiciosMunicipio.Controllers
{
    public class CAT_LOCALIDADController : Controller
    {
        CAT_LOCALIDAD_Dao dao = new CAT_LOCALIDAD_Dao();

        // GET: CAT_LOCALIDAD/Localidades?clave_localidad&nombre_localidad AjaxResultados -- Localidad
        public ActionResult Localidades(String clave_localidad, String nombre_localidad, String municipio)
        {
            if (clave_localidad == null && nombre_localidad == null && municipio == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            } 
            else if (String.IsNullOrEmpty(municipio)) 
            {
                return Json( new List<CAT_LOCALIDAD>(), JsonRequestBehavior.AllowGet);
            }
            var r = dao.obtenerLocalidades(clave_localidad, nombre_localidad, municipio);

            this.ViewBag.Resultados = r;
            return Json(r, JsonRequestBehavior.AllowGet);          
        }


        // GET: CAT_LOCALIDAD/NombreLocalidad/ AjaxResultados -- Localidad
        public ActionResult NombreLocalidad(String CVE_MUNICIPIO, String CVE_LOCALIDAD)
        {
            if (CVE_MUNICIPIO == null && CVE_LOCALIDAD == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var r = dao.obtenerCatalogoNombreLocalidad(CVE_MUNICIPIO, CVE_LOCALIDAD);
 
            return Json( r, JsonRequestBehavior.AllowGet);
        }


        // GET: CAT_LOCALIDAD/CatalogoLocalidades/CVE_MUN AjaxResultados -- Localidad
        public ActionResult CatalogoLocalidades(String CVE_MUNICIPIO)
        {
            if (CVE_MUNICIPIO == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var r = dao.catalogoLocalidad(CVE_MUNICIPIO);

            return Json(r, JsonRequestBehavior.AllowGet);
        }

    }
}
