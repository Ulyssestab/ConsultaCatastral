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
        private System.Web.Mvc.JsonResult result;
        private List<Resultados> resultado = new List<Resultados>();

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
            result = (System.Web.Mvc.JsonResult)controller.ClaveCatastral(claveCatastral);
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
                campLocalidadOri = "001",
                campSectorOri = "08",
                campManzanaOri = "0237",
                campPredioOri = "054",
                campCondominioOri = "000"
            };
            // Disponer            

            result = (System.Web.Mvc.JsonResult)controller.ClaveCatastralOriginal(claveCatastralOriginal);
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
            ClavePredial clavePredial = new ClavePredial
            {

                claveCuentaPredial = "",
                municipioCE = "001",
                numCuentaPredial = "",
                offset = 0
            };
            result = (System.Web.Mvc.JsonResult)controller.ClavePredial(clavePredial);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);
            resultado = jsonString != "[]" ? util.crearObjetoResultados(jsonString) : null;
            Assert.IsNotNull(resultado);
            Assert.AreEqual(0, resultado.Count);
            clavePredial.claveCuentaPredial = "R001040";
            clavePredial.numCuentaPredial = "";
            result = (System.Web.Mvc.JsonResult)controller.ClavePredial(clavePredial);
            data = result.Data;
            serializer = new JavaScriptSerializer();
            jsonString = serializer.Serialize(data);
            resultado = jsonString != "[]" ? util.crearObjetoResultados(jsonString) : null;
            Assert.AreEqual("COMISION FEDERAL DE ELECTRICIDAD", resultado[0].NOMBRE_O_RAZON_SOCIAL);
        }

        [TestMethod]
        public void FolioRealTest()
        {
            result = (System.Web.Mvc.JsonResult)controller.FolioReal("", "001", 0);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);
            resultado = jsonString != "[]" ? util.crearObjetoResultados(jsonString) : null;
            Assert.IsNull(resultado);
            result = (System.Web.Mvc.JsonResult)controller.FolioReal("124631", "001", 0);
            data = result.Data;
            jsonString = serializer.Serialize(data);
            resultado = jsonString != "[]" ? util.crearObjetoResultados(jsonString) : null;
            Assert.IsNotNull(resultado);
            Assert.AreEqual("GUTIERREZ", resultado[0].APELLIDO_PATERNO);
        }

        [TestMethod]
        public void UbicacionPredioTest()
        {
            UbicacionPredio ubicacion = new UbicacionPredio
            {
                asentamiento = "",
                calle = "",
                clave_localidad = "",
                cve_asentamiento = "",
                localidad = "",
                municipio = "001",
                numExt = ""

            };
            result = (System.Web.Mvc.JsonResult)controller.UbicacionPredio(ubicacion, 0);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);
            resultado = jsonString != "[]" ? util.crearObjetoResultados(jsonString) : null;
            Assert.IsNotNull(resultado);
            ubicacion = new UbicacionPredio
            {
                asentamiento = "",
                calle = "",
                clave_localidad = "1055",
                cve_asentamiento = "",
                localidad = "LA PRIMAVERA",
                municipio = "001",
                numExt = ""

            };
            result = (System.Web.Mvc.JsonResult)controller.UbicacionPredio(ubicacion, 0);
            data = result.Data;
            jsonString = serializer.Serialize(data);
            resultado = jsonString != "[]" ? util.crearObjetoResultados(jsonString) : null;
            Assert.IsNotNull(resultado);
            Assert.AreEqual("R001040", resultado[0].clavePredial);
        }

        [TestMethod]
        public void NombrePropietarioPersonaFisicaTest()
        {
            PersonaFisica personaFisica = new PersonaFisica
            {
                amaterno = "SERNA",
                apaterno = "ALVARADO",
                municipio = "001",
                nombre = "MARGARITA"

            };
            result = (System.Web.Mvc.JsonResult)controller.NombrePropietarioPersonaFisica(personaFisica, 0);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);
            resultado = jsonString != "[]" ? util.crearObjetoResultados(jsonString) : null;
            Assert.IsNotNull(resultado);
            Assert.AreEqual("01001010044014000", resultado[0].CVE_CAT_ORI);
        }

        [TestMethod]
        public void NombrePropietarioPersonaMoralTest()
        {
            var serializer = new JavaScriptSerializer();
            result = (System.Web.Mvc.JsonResult)controller.NombrePropietarioPersonaMoral("","001",0);
            var data = result.Data;            
            var jsonString = serializer.Serialize(data);
            resultado = jsonString != "[]" ? util.crearObjetoResultados(jsonString) : null;
            Assert.IsNull(resultado);
            result = (System.Web.Mvc.JsonResult)controller.NombrePropietarioPersonaMoral("GOBIERNO DEL ESTADO DE ", "001", 0);
            data = result.Data;
            jsonString = serializer.Serialize(data);
            resultado = jsonString != "[]" ? util.crearObjetoResultados(jsonString) : null;
            Assert.IsNotNull(resultado);
            Assert.AreEqual("01000990000009000", resultado[0].CVE_CAT_ORI);
        }
    }
}
