using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiciosMunicipio.Controllers;
using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosMunicipio.Tests.Controllers
{
    [TestClass]
    public class BitacoraControllerTest
    {
        [TestMethod]
        public void InsertarRegistroNuloEnBitacoraTest()
        {
            BitacoraController controller = new BitacoraController();
            BitacoraAccesoSistemas bitacora = new BitacoraAccesoSistemas();
            // Actuar
            System.Web.Mvc.JsonResult resultado = (System.Web.Mvc.JsonResult)controller.InsertarRegistroEnBitacora(bitacora);

            String result = resultado.Data.ToString();

            // Declarar
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Equals("ERROR"));       
            
        }
        [TestMethod]
        public void InsertarRegistroEnBitacoraTest()
        {
            String result = "OK";
            BitacoraController controller = new BitacoraController();
           
            DateTime fecha = DateTime.ParseExact("2025-07-08 01:52:33", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);            

            BitacoraAccesoSistemas bitacora = new BitacoraAccesoSistemas() {
                ALTAREGISTROTABLA = fecha,
                APELLIDO_MATERNO = "",
                APELLIDO_PATERNO = "",
                APLICATIVO = "",
                CVE_CAT_EST = "000000000000000000",
                CVE_CAT_ORI = "",
                DESCRIPCION = "Consulta realizada por Clave Catastral",
                ID = 0,
                IP_ALTA = "129.999.2.1",
                NOMBRE_O_RAZON_SOCIAL = "",
                TERMINALALTA = "",
                URL = "https://localhost:44330/",
                USUARIOALTA = "Administrador"
            };
            // Actuar             
            System.Web.Mvc.JsonResult resultado = (System.Web.Mvc.JsonResult)controller.InsertarRegistroEnBitacora(bitacora);

            result = resultado.Data.ToString();

            // Declarar
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Equals("OK"));

        }
    }
}
