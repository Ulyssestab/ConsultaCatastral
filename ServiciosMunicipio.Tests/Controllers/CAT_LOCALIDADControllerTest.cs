using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ServiciosMunicipio.Controllers;
using ServiciosMunicipio.Models;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ServiciosMunicipio.Tests.Controllers
{

    [TestClass]
    public class CAT_LOCALIDADControllerTest
    {
        private String clave_localidad = "";
        private String nombre_localidad = "ADAPTACIONES Y PAILERIA CERVANTES";
        private CAT_LOCALIDAD localidad = new CAT_LOCALIDAD
        {
            OBJECTID = 1774,
            CVE_ENTIDAD = "01",
            NOM_ENTIDAD = null,
            CVE_MUNICIPIO = "001",
            NOM_MUNICIPIO = null,
            CVE_LOCALIDAD = "2373",
            NOM_LOCALIDAD = "ADAPTACIONES Y PAILERIA CERVANTES"
        };

        private JSONparser util = new JSONparser();
        [TestMethod]
        public void LocalidadesTest()
        {
            CAT_LOCALIDAD variable = new CAT_LOCALIDAD();
            // Disponer
            CAT_LOCALIDADController controller = new CAT_LOCALIDADController();

            // Actuar
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult) controller.Localidades(clave_localidad, nombre_localidad);

            String resultado  = JsonConvert.SerializeObject(result.Data);
            variable = resultado != "[]" ? util.crearObjeto(resultado) : variable;
            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual(localidad.NOM_LOCALIDAD,variable.NOM_LOCALIDAD);
        }
        [TestMethod]
        public void NombreLocalidadTest()
        {
            // Disponer
            CAT_LOCALIDADController controller = new CAT_LOCALIDADController();
            String resultados;
            // Actuar
            System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)controller.NombreLocalidad(localidad.CVE_LOCALIDAD, localidad.CVE_MUNICIPIO);
            resultados = JsonConvert.SerializeObject(result.Data);
            //resultados = resultados != null ? resultados.TrimStart('[').TrimEnd(']') : "";
                
            // Declarar
            Assert.IsNotNull(result);
            //Assert.AreEqual(localidad, util.crearObjeto(resultados));
        }
    }
}
