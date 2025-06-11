using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiciosMunicipio.Controllers;
using ServiciosMunicipio.Models;
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
    public class Cat_MunicipioControllerTest
    {
        private JSONparser util = new JSONparser();
        [TestMethod]
        public void IndexTest()
        {
            List<Cat_Municipio> variable = new List<Cat_Municipio>();
            // Disponer
            Cat_MunicipioController controller = new Cat_MunicipioController();

            // Actuar
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.Index();
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);

            variable = jsonString != "[]" ? util.crearObjetoMunicipio(jsonString) : variable;
            // Declarar
            Assert.IsNotNull(result);
            Assert.IsTrue(variable.Count == 11);
            Assert.AreEqual("AGUASCALIENTES", variable[0].Municipio);
        }
    }
}
