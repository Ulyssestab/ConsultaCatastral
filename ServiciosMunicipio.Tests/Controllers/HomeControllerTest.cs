using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiciosMunicipio;
using ServiciosMunicipio.Controllers;
using ServiciosMunicipio.Models;
using ServiciosMunicipio.Utilerias;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System;

namespace ServiciosMunicipio.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private string nombre = "10du2";
        private string pass = "66FAEE583898CC373ED7EC34FC887DFF34809195";
        private JSONparser util = new JSONparser();

        [TestMethod]
        public void IndexTest()
        {
            // Disponer
            HomeController controller = new HomeController();

            // Actuar
            ViewResult result = controller.Index() as ViewResult;

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }

        [TestMethod]
        public void AccesoTest()
        {
            // Disponer
            HomeController controller = new HomeController();
            

            // Actuar
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.Acceso(nombre);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);
            
            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("true", jsonString);
        }


        [TestMethod]
        public void AccesoUsuarioPerfilTest()
        {
            // Disponer
            HomeController controller = new HomeController();
            Usuario variable = new Usuario();

            // Actuar
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.AccesoUsuarioPerfil(nombre);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);
            
            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("1", jsonString.Replace("\"", ""));
        }

        [TestMethod]
        public void ObtenerAccesoTest()
        {
            // Disponer
            HomeController controller = new HomeController();
            Usuario variable = new Usuario();
           
            // Actuar
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.ObtenerAcceso(nombre, pass);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);

            variable = util.parseJsonStringUsuario(jsonString);
            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("EL_USUARIO", variable.NombreUsuario);
        }
    }
}
