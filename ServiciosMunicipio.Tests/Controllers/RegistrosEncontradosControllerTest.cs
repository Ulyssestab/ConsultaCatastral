using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiciosMunicipio.Controllers;
using ServiciosMunicipio.Models;
using ServiciosMunicipio.Models.Entidades;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ServiciosMunicipio.Tests.Controllers
{
    [TestClass]
    public class RegistrosEncontradosControllerTest
    {
        private ClaveCatastral claveCatastral = new ClaveCatastral { 
            edificioCE = "",
            localidadCE = "",
            entidadCE = "",
            manzanaCE = "",
            municipioCE = "001",
            predioCE = "",
            regionCE = "",
            sectorCE = "",
            unidadCE = "",
            zonaCE = ""            
        };
        private RegistrosEncontradosController controller = new RegistrosEncontradosController();
        private JSONparser util = new JSONparser();
        [TestMethod]
        public void ClaveCatastralTest() 
        {
            // Disponer            
            List<Resultados> resultado = new List<Resultados>();
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.ClaveCatastral(claveCatastral);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);

            resultado = jsonString != "[]" ? util.crearObjetoResultados(jsonString) : null;
            Assert.IsNotNull(resultado);
            Assert.AreEqual(1, resultado.Count);
            Assert.AreEqual("JAUREGUI", resultado[0].APELLIDO_PATERNO);
        }

        [TestMethod]
        public void ClaveCatastralOriginalTest()
        {
            ClaveCatastralOriginal claveCatastralOriginal = new ClaveCatastralOriginal
            {
                campoMunicipioOri = "01",
                campLocalidadOri= "001",
                campSectorOri = "08",
                campManzanaOri = "0237",
                campPredioOri = "054",
                campCondominioOri = "000"
            };
            // Disponer            
            List<Resultados> resultado = new List<Resultados>();
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.ClaveCatastralOriginal(claveCatastralOriginal);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);

            resultado = jsonString != "[]" ? util.crearObjetoResultados(jsonString) : null;
            Assert.IsNotNull(resultado);
            Assert.AreEqual(1, resultado.Count);
            Assert.AreEqual("LOPEZ", resultado[0].APELLIDO_PATERNO);

            result = (System.Web.Mvc.JsonResult)controller.ClaveCatastralOriginal(null);
            data = result.Data;
            serializer = new JavaScriptSerializer();
            jsonString = serializer.Serialize(data);
            resultado = jsonString != "[]" ? util.crearObjetoResultados(jsonString) : null;
            Assert.IsNull(resultado);
        }

        [TestMethod]
        public void ClavePredialTest() 
        {            
            List<Resultados> resultado = new List<Resultados>();
            ClavePredial clavePredial = new ClavePredial { 
                
                claveCuentaPredial = "",
                municipioCE = "001",
                numCuentaPredial = "",
                offset = 0
            };
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.ClavePredial(clavePredial);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);
            resultado = jsonString != "[]" ? util.crearObjetoResultados(jsonString) : null;
            Assert.IsNotNull(resultado);
            Assert.AreEqual(1, resultado.Count);
            Assert.AreEqual("LOPEZ", resultado[0].APELLIDO_PATERNO);
        }
    }
}
