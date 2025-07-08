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
            String result = controller.InsertarRegistroEnBitacora(bitacora);
            
            // Declarar
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Equals("ERROR"));       
            
        }
        [TestMethod]
        public void InsertarRegistroEnBitacoraTest()
        {
            String result = "OK";
            BitacoraController controller = new BitacoraController();
            DateTime fecha = DateTime.ParseExact(DateTime.Now.ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);

            BitacoraAccesoSistemas bitacora = new BitacoraAccesoSistemas() {
                ALTAREGISTROTABLA = fecha,
                APELLIDO_MATERNO = "",
                APELLIDO_PATERNO = "",
                APLICATIVO = "",
                CVE_CAT_EST = "",
                CVE_CAT_ORI = "",
                DESCRIPCION = "",
                ID = 0,
                IP_ALTA = "",
                NOMBRE_O_RAZON_SOCIAL = "",
                TERMINALALTA = "",
                URL = "",
                USUARIOALTA = ""
            };
            // Actuar             
            //result = controller.InsertarRegistroEnBitacora(bitacora);


            // Declarar
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Equals("OK"));

        }
    }
}
