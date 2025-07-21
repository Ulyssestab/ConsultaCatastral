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
    public class RegistrosEncontradosController : Controller
    {
        private RegistrosEncontradosDao Buscar = new RegistrosEncontradosDao();
        private Utilidades util = new Utilidades();
        private BitacoraDao dao = new BitacoraDao();        

        // GET: RegistrosEncontrados
        public ActionResult Index()
        {
            return View();
        }

        // POST: RegistrosEncontrados/ClaveCatastral
        [System.Web.Http.HttpPost]
        public ActionResult ClaveCatastral([FromBody] ClaveCatastral ClaveCatastral)
        {
            //Formulario: Menu Tramite, clave catastral estandar                                    
            int offset= 0;
            String usuario = "";
            //Uno la clave catastral estandar enviada desde el formulario
            String claveCatastralEstandar = ClaveCatastral.entidadCE
                + util.completarCeros(3, ClaveCatastral.regionCE)
                + ClaveCatastral.municipioCE
                + util.completarCeros(2, ClaveCatastral.zonaCE)
                + util.completarCeros(4, ClaveCatastral.localidadCE)
                + util.completarCeros(3, ClaveCatastral.sectorCE)
                + util.completarCeros(3, ClaveCatastral.manzanaCE)
                + util.completarCeros(5, ClaveCatastral.predioCE)
                + util.completarCeros(2, ClaveCatastral.edificioCE)
                + util.completarCeros(4, ClaveCatastral.unidadCE);
            //Hago la consulta a la base de datos para ver si existen registros asociados a dicha clave                        
            List<Resultados> resultados = Buscar.obtenerClavesEstandar(claveCatastralEstandar, ClaveCatastral.municipioCE, offset);
            dao.guardarRegistro(claveCatastralEstandar, "", "Clave Catastral Estandar", Request != null ? Request.UserHostAddress : "0:0:0:0",
                Request != null ? Request.UserHostName : "ninguno", Request != null ? Request.Url.ToString() : "///////", usuario, "", "", "");
            return Json(resultados);
        }

        // POST: RegistrosEncontrados/ClaveCatastralOriginal
        [System.Web.Http.HttpPost]
        public ActionResult ClaveCatastralOriginal([FromBody] ClaveCatastralOriginal ClaveCatastralOriginal)
        {
            //Formulario: Menu Tramite, clave catastral estandar                                    
            int offset = 0;
            int pag = 0;
            String usuario = "";
            String claveCatastralOriginal = "";
            List<Resultados> resultados = new List<Resultados>();
            if (ClaveCatastralOriginal != null)
            {
                claveCatastralOriginal = ClaveCatastralOriginal.campoMunicipioOri
                + util.completarCeros(3, ClaveCatastralOriginal.campLocalidadOri)
                + util.completarCeros(2, ClaveCatastralOriginal.campSectorOri)
                + util.completarCeros(4, ClaveCatastralOriginal.campManzanaOri)
                + util.completarCeros(3, ClaveCatastralOriginal.campPredioOri)
                + util.completarCeros(3, ClaveCatastralOriginal.campCondominioOri);

                pag = Buscar.numClavesOriginales(claveCatastralOriginal);
                if (pag > 0) {
                    //Hago la consulta a la base de datos para ver si existen registros asociados a dicha clave                        
                    resultados = Buscar.obtenerClavesOriginal(claveCatastralOriginal, offset, pag);
                }
            }
            
            dao.guardarRegistro("", claveCatastralOriginal, "Clave Catastral Original", Request != null ? Request.UserHostAddress : "0:0:0:0",
                Request != null ? Request.UserHostName : "ninguno", Request != null ? Request.Url.ToString() : "///////", usuario, "", "", "");

            return Json(resultados);
        }

        // POST: RegistrosEncontrados/ClavePredial
        public ActionResult ClavePredial(ClavePredial clavePredial)
        {
            List<Resultados> resultados =  null;
            if (clavePredial != null && (!String.IsNullOrEmpty(clavePredial.numCuentaPredial) || !String.IsNullOrEmpty(clavePredial.claveCuentaPredial))) 
            {
                resultados = Buscar.obtenerClavesPredial(clavePredial);
            }
            String usuario = "";
            dao.guardarRegistro("","", "Clave Predial : " + clavePredial.claveCuentaPredial + clavePredial.numCuentaPredial, Request != null ? Request.UserHostAddress : "0:0:0:0",
                Request != null ? Request.UserHostName : "ninguno", Request != null ? Request.Url.ToString() : "///////", usuario, "", "", "");
            return Json(resultados);
        }

        // GET: RegistrosEncontrados/FolioReal
        public ActionResult FolioReal(String folioReal, String municipioCE, int offset)
        {
            List<Resultados> resultados = new List<Resultados>();
            int num = 0;
            //Hago la consulta a la base de datos para ver si existen registros asociados a dicha clave                        
            if (!String.IsNullOrEmpty(folioReal)) 
            {
                num = Buscar.numFolioreal(folioReal, municipioCE);
                if (num > 0) 
                {
                    resultados = Buscar.obtenerResultadoFolioReal(folioReal, municipioCE, offset);
                }
            }
            String usuario = "";
            dao.guardarRegistro("", "", "Folio Real : " + folioReal, Request != null ? Request.UserHostAddress : "0:0:0:0",
                Request != null ? Request.UserHostName : "ninguno", Request != null ? Request.Url.ToString() : "///////", usuario, "", "", "");
            return Json(resultados, JsonRequestBehavior.AllowGet); 
        }

        // GET: RegistrosEncontrados/TotalFolioReal
        public ActionResult TotalFolioReal(String folioReal, String municipioCE)
        {
            int resultado = 0;
            //Hago la consulta a la base de datos para ver si existen registros asociados a dicha clave                        
            if (!String.IsNullOrEmpty(folioReal))
            {
                resultado = Buscar.numFolioreal(folioReal, municipioCE);
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        // POST: RegistrosEncontrados/UbicacionPredio
        [System.Web.Http.HttpPost]
        public ActionResult UbicacionPredio([FromBody] UbicacionPredio ubicacionPredio, int pag) 
        {
            List<Resultados> resultados = new List<Resultados>();
            int max = 0;
            if (ubicacionPredio != null && !String.IsNullOrEmpty(ubicacionPredio.municipio) && !String.IsNullOrEmpty(ubicacionPredio.localidad)) 
            {
                max = Buscar.numDireccion(new Utilidades().formarConsultaUbicacionXpredio(ubicacionPredio, Constantes.TOTALXPREDIO), ubicacionPredio.municipio);
                if (max > 0) 
                {
                    resultados = Buscar.obtenerResultadoUbicacionPredio(ubicacionPredio, pag);
                }                
            }
            String usuario = "";
            dao.guardarRegistro("", "", "Ubicacion Predio : ", Request != null ? Request.UserHostAddress : "0:0:0:0",
                Request != null ? Request.UserHostName : "ninguno", Request != null ? Request.Url.ToString().Substring(0, 60) : "///////", usuario, ubicacionPredio.asentamiento != "" ? ubicacionPredio.asentamiento : ubicacionPredio.calle, "", "");
            return Json(resultados);
        }

        // GET: RegistrosEncontrados/UbicacionPredio        
        public ActionResult UbicacionPredioGet(String cve_localidad, String nom_localidad, String cve_colonia,String colonia, String municipio, String calle, String numExt,  int pag)
        {
            UbicacionPredio ubicacionPredio = new UbicacionPredio 
            { 
                calle = calle != "0" ? calle : "",
                clave_localidad = cve_localidad != "0" ? cve_localidad : "",
                asentamiento = colonia != "0" ? colonia : "",
                cve_asentamiento = cve_colonia != "0" ? cve_colonia : "",
                localidad = nom_localidad != "0" ? nom_localidad : "",
                municipio = municipio != "0" ? municipio : "",
                numExt =  numExt != "0" ? numExt : "",
            };
            List<Resultados> resultados = new List<Resultados>();
            int max = 0;
            if (ubicacionPredio != null && !String.IsNullOrEmpty(ubicacionPredio.municipio) && !String.IsNullOrEmpty(ubicacionPredio.localidad))
            {
                max = Buscar.numDireccion(new Utilidades().formarConsultaUbicacionXpredio(ubicacionPredio, Constantes.TOTALXPREDIO), ubicacionPredio.municipio);
                if (max > 0)
                {
                    resultados = Buscar.obtenerResultadoUbicacionPredio(ubicacionPredio, pag);
                }
            }
            String usuario = "";
            dao.guardarRegistro("", "", "Ubicacion Predio", Request != null ? Request.UserHostAddress : "0:0:0:0",
                Request != null ? Request.UserHostName : "ninguno", Request != null ? Request.Url.ToString().Substring(0,60) : "///////", usuario, ubicacionPredio.asentamiento != "" ? ubicacionPredio.asentamiento : ubicacionPredio.calle, "", "");
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        // POST: RegistrosEncontrados/NombrePropietarioPersonaFisica
        [System.Web.Http.HttpPost]
        public ActionResult NombrePropietarioPersonaFisica([FromBody] PersonaFisica personaFisica, int pag)
        {
            List<Resultados> resultados =  new List<Resultados>();
            if (personaFisica != null) 
            {
                int max = Buscar.numPredioxPersonaFisica(personaFisica);
                if (max > 0) 
                {
                    resultados = Buscar.obtenerResultadoPersonaFisica(personaFisica, pag, max);
                }                
            }
            String usuario = "";
            dao.guardarRegistro("", "", "Nombre Propietario Persona Fisica", Request != null ? Request.UserHostAddress : "0:0:0:0",
                Request != null ? Request.UserHostName : "ninguno", Request != null ? Request.Url.ToString() : "///////", usuario , personaFisica.nombre, personaFisica.apaterno, personaFisica.amaterno);
            return Json(resultados);
        }

        // GET: RegistrosEncontrados/NombrePropietarioPersonaMoral
        public ActionResult NombrePropietarioPersonaMoral(String razonSocial, String municipio, int pag)
        {
            List<Resultados> resultados = new List<Resultados>();
            if (!String.IsNullOrEmpty(razonSocial) && !String.IsNullOrEmpty(municipio)) 
            {
                int max = Buscar.numPredioxPersonaMoral(razonSocial, municipio);
                if (max > 100) {
                    max = 50;
                }
                if (max > 0) {
                    resultados = Buscar.obtenerResultadoPersonaMoral(razonSocial, municipio, pag, max);
                }                
            }
            String usuario = "";
            dao.guardarRegistro("", "", "Nombre Propietario Persona Moral", Request != null ? Request.UserHostAddress : "0:0:0:0",
                Request != null ? Request.UserHostName : "ninguno", Request != null ? Request.Url.ToString() : "///////", usuario, razonSocial, "", "");
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
    }
}