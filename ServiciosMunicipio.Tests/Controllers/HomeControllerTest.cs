using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiciosMunicipio;
using ServiciosMunicipio.Controllers;
using System.Web.Mvc;

namespace ServiciosMunicipio.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Disponer
            HomeController controller = new HomeController();

            // Actuar
            ViewResult result = controller.Index() as ViewResult;

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
