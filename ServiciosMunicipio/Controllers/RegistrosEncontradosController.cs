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
            List<Resultados> resultados = Buscar.obtenerClavesEstandar(claveCatastralEstandar,ClaveCatastral.municipioCE, offset);
            return Json(resultados);
        }

        // POST: RegistrosEncontrados/ClaveCatastralOriginal
        [System.Web.Http.HttpPost]
        public ActionResult ClaveCatastralOriginal([FromBody] ClaveCatastralOriginal ClaveCatastralOriginal)
        {
            //Formulario: Menu Tramite, clave catastral estandar                                    
            int offset = 0;
            int pag = 0;
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
                //Hago la consulta a la base de datos para ver si existen registros asociados a dicha clave                        
                resultados = Buscar.obtenerClavesOriginal(claveCatastralOriginal, offset, pag);
            }
            //Uno la clave catastral estandar enviada desde el formulario
            

            return Json(resultados);
        }

        // POST: RegistrosEncontrados/ClavePredial
        public ActionResult ClavePredial(ClavePredial clavePredial)
        {
            List<Resultados> resultados =  null;
            if (clavePredial != null && !String.IsNullOrEmpty(clavePredial.numCuentaPredial) && !String.IsNullOrEmpty(clavePredial.claveCuentaPredial)) {
                resultados = Buscar.obtenerClavesPredial(clavePredial);
            }
             
            return Json(resultados);
        }

        // GET: RegistrosEncontrados/FolioReal
        public ActionResult FolioReal(String folioReal, String municipioCE, int offset)
        {
            List<Resultados> resultados = new List<Resultados>();
            //Hago la consulta a la base de datos para ver si existen registros asociados a dicha clave                        
            if (!String.IsNullOrEmpty(folioReal)) {
                resultados = Buscar.obtenerResultadoFolioReal(folioReal, municipioCE, offset);
            } 
            return Json(resultados, JsonRequestBehavior.AllowGet); 
        }

        // POST: RegistrosEncontrados/UbicacionPredio
        [System.Web.Http.HttpPost]
        public ActionResult UbicacionPredio([FromBody] UbicacionPredio ubicacionPredio, int pag) 
        {
            List<Resultados> resultados = null;
            if (ubicacionPredio != null && !String.IsNullOrEmpty(ubicacionPredio.municipio) && !String.IsNullOrEmpty(ubicacionPredio.localidad)) 
            {
                resultados = Buscar.obtenerResultadoUbicacionPredio(ubicacionPredio, pag);
            }
            
            return Json(resultados);
        }

        // POST: RegistrosEncontrados/NombrePropietarioPersonaFisica
        [System.Web.Http.HttpPost]
        public ActionResult NombrePropietarioPersonaFisica([FromBody] PersonaFisica personaFisica, int pag)
        {
            List<Resultados> resultados =  null;
            if (personaFisica != null) 
            {
                int max = Buscar.numPredioxPersonaFisica(personaFisica);
                resultados = Buscar.obtenerResultadoPersonaFisica(personaFisica, pag, max);
            }
            
            return Json(resultados);
        }

        // GET: RegistrosEncontrados/NombrePropietarioPersonaMoral
        public ActionResult NombrePropietarioPersonaMoral(String razonSocial, String municipio, int pag)
        {
            List<Resultados> resultados = new List<Resultados>();
            if (!String.IsNullOrEmpty(razonSocial) && !String.IsNullOrEmpty(municipio)) 
            {
                int max = Buscar.numPredioxPersonaMoral(razonSocial, municipio);
                resultados = Buscar.obtenerResultadoPersonaMoral(razonSocial, municipio, pag, max);
            }            
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
    }
}