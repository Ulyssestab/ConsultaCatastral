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
    public class SIS_ASENTAMIENTOSControllerTest
    {
        private JSONparser util = new JSONparser();
        private SIS_ASENTAMIENTOS json = new SIS_ASENTAMIENTOS
        {
            ALTAREGISTROTABLA = new DateTime(),
            BAJAREGISTROTABLA = new DateTime(),
            CLAVEUNICA = 0,
            CP = "",
            CVE_ASENTAMIENTO = "00035",
            CVE_ENTIDAD = "",
            CVE_LOCALIDAD = "01",
            CVE_MUNICIPIO = "001",
            CVE_REGION_CATASTRAL = "",
            CVE_SECTOR_CATASTRAL = "",
            CVE_ZONA_CATASTRAL = "",
            HORAALTAREGISTROTABLA = "",
            HORABAJAREGISTROTABLA = "",
            NOMBRE_ASENTAMIENTO = "Primavera",
            NOMBRE_COMPLETO_ASENTAMIENTO = "",
            OBJECTID = 0,
            OBSERVACIONES = "",
            STATUSREGISTROTABLA = "",
            TERMINALALTA = "",
            TERMINALBAJA = "",
            TIPO_ASENTAMIENTO = "",
            USUARIOALTA = "",
            USUARIOBAJA = ""
        };
        [TestMethod]
        public void AsentamientosTest()
        {
            // Disponer
            SIS_ASENTAMIENTOSController controller = new SIS_ASENTAMIENTOSController();

            SIS_ASENTAMIENTOS resultado = new SIS_ASENTAMIENTOS();
            // Actuar
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.Asentamientos(json);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);

            resultado = jsonString != "[]" ? util.crearObjetoAsentamiento(jsonString.Replace("[", "").Replace("]", "")) : resultado;

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("true", jsonString);
        }
    }
}
