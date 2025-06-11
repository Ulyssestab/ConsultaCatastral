using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ServiciosMunicipio.Controllers;
using ServiciosMunicipio.Models;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.IO;
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
            CVE_LOCALIDAD = "0001",
            CVE_MUNICIPIO = "001",
            CVE_REGION_CATASTRAL = "",
            CVE_SECTOR_CATASTRAL = "",
            CVE_ZONA_CATASTRAL = "",
            HORAALTAREGISTROTABLA = "",
            HORABAJAREGISTROTABLA = "",
            NOMBRE_ASENTAMIENTO = "Primavera",
            NOMBRE_COMPLETO_ASENTAMIENTO = "CONDOMINIO CENTRO",
            OBJECTID = 3715,
            OBSERVACIONES = "",
            STATUSREGISTROTABLA = "",
            TERMINALALTA = "",
            TERMINALBAJA = "",
            TIPO_ASENTAMIENTO = "",
            USUARIOALTA = "",
            USUARIOBAJA = ""
        };

        [TestMethod]
        public void AsentamientoTest()
        {
            // Disponer
            SIS_ASENTAMIENTOSController controller = new SIS_ASENTAMIENTOSController();
            SIS_ASENTAMIENTOS asentamiento = new SIS_ASENTAMIENTOS();
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.Asentamiento(json.OBJECTID,json.CVE_MUNICIPIO);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);

            asentamiento = util.crearObjetoAsentamiento(jsonString);
            Assert.IsNotNull(result);
            Assert.AreEqual(json.OBJECTID, asentamiento.OBJECTID);
        }
        [TestMethod]
        public void AsentamientosTest()
        {
            // Disponer
            SIS_ASENTAMIENTOSController controller = new SIS_ASENTAMIENTOSController();

            List<SIS_ASENTAMIENTOS> resultado = new List<SIS_ASENTAMIENTOS>();
            // Actuar
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.Asentamientos(json);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(data);

            resultado = util.parseJsonString(jsonString);

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual(3, resultado.Count);
            Assert.AreEqual("COLONIA PRIMAVERA", resultado[0].NOMBRE_COMPLETO_ASENTAMIENTO);
        }

        [TestMethod]
        public void NombreAsentamientosTest()
        {
            // Disponer
            SIS_ASENTAMIENTOSController controller = new SIS_ASENTAMIENTOSController();

            List<SIS_ASENTAMIENTOS> resultado = new List<SIS_ASENTAMIENTOS>();
            // Actuar.

            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.NombreAsentamientos(json);
            var data = result.Data;
            var serializer = new JavaScriptSerializer();
            String jsonString = serializer.Serialize(data);

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("CONDOMINIO CENTRO COMERCIAL CHALET DOUGLAS", jsonString.Trim('"'));
        }

    }
}
