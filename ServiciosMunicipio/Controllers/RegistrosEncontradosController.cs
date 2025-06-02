using InstitutoCatastralAGS.Models;
using ServiciosMunicipio.Dao;
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

        public ActionResult ClavePredial(String claveCuentaPredial, String numCuentaPredial, String municipioCE, int offset)
        {
            List<Resultados> resultados = Buscar.obtenerClavesPredial(claveCuentaPredial, numCuentaPredial, municipioCE, offset);
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult FolioReal(String folioReal, String municipioCE, int offset)
        {
            //Hago la consulta a la base de datos para ver si existen registros asociados a dicha clave                        
            List<Resultados> resultados = Buscar.obtenerResultadoFolioReal(folioReal,municipioCE, offset);
            return Json(resultados, JsonRequestBehavior.AllowGet); 
        }

        [System.Web.Http.HttpPost]
        public ActionResult UbicacionPredio([FromBody] UbicacionPredio ubicacionPredio, int pag) {
            List<Resultados> resultados = Buscar.obtenerResultadoUbicacionPredio(ubicacionPredio, pag);
            return Json(resultados);
        }
    }
}