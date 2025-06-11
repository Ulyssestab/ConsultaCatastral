using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiciosMunicipio.Controllers;
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
    public class DetallePredioControllerTest
    {
        private String claveCatastralEst = "0100000147000102504300006000000";
        private String municipio = "001";
        private JSONparser util = new JSONparser();
        [TestMethod]
        public void ResultadoTest() 
        {
            // Disponer
            DetallePredioController controller = new DetallePredioController();
            DetallePredio predio = new DetallePredio();
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.Resultado(claveCatastralEst, municipio);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);

            predio = util.parseJsonStringPredio(jsonString);
            Assert.IsNotNull(result);
            Assert.AreEqual("LA PRIMAVERA", predio.NOM_LOCALIDAD);
        }
        [TestMethod]
        public void TramitesTest()
        {
            // Disponer
            DetallePredioController controller = new DetallePredioController();
            Tramite tramite = new Tramite();
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.Tramites("0100000129000000000000002000000");
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);

            tramite = util.parseJsonStringTramite(jsonString);
            Assert.IsNotNull(result);
            Assert.AreEqual("258/2025", tramite.NumeroTramite);
        }
        [TestMethod]
        public void DetalleTareasTramitesTest()
        {
            // Disponer
            DetallePredioController controller = new DetallePredioController();
            List<TareasTramite> tramites = new List<TareasTramite>();
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.DetalleTareasTramites("258/2025");
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);

            tramites = util.parseJsonStringTareasTramite(jsonString);
            Assert.IsNotNull(result);
            Assert.IsTrue(tramites.Count > 2);
            Assert.AreEqual("SE FINALIZO", tramites[0].estatus);
        }
    }
}
