using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiciosMunicipio.Dao;
using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosMunicipio.Tests.Dao
{
    [TestClass]
    public class SIS_ASENTAMIENTO_DaoTest
    {
        private SIS_ASENTAMIENTOS_Dao dao = new SIS_ASENTAMIENTOS_Dao();
        private SIS_ASENTAMIENTOS json = new SIS_ASENTAMIENTOS
        {
            ALTAREGISTROTABLA = new DateTime(),
            BAJAREGISTROTABLA = new DateTime(),
            CLAVEUNICA = 0,
            CP = "",
            CVE_ASENTAMIENTO = "00035",
            CVE_ENTIDAD = "",
            CVE_LOCALIDAD = "0001" ,
            CVE_MUNICIPIO = "001",
            CVE_REGION_CATASTRAL = "",
            CVE_SECTOR_CATASTRAL = "",
            CVE_ZONA_CATASTRAL = "",
            HORAALTAREGISTROTABLA = "",
            HORABAJAREGISTROTABLA = "",
            NOMBRE_ASENTAMIENTO = "Calvi",
            NOMBRE_COMPLETO_ASENTAMIENTO = "EJIDO CALVILLITO",
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
        public void obtenerAsentamientosTest()
        {
            List<SIS_ASENTAMIENTOS> lista = dao.obtenerAsentamientos(json);
            Assert.IsNotNull(lista);
            Assert.AreEqual("CALVILLITO", lista[0].NOMBRE_ASENTAMIENTO);
        }

        [TestMethod]
        public void obtenerAsentamientoTest()
        {
            SIS_ASENTAMIENTOS asentamiento = dao.obtenerAsentamiento(3715,"001");
            Assert.IsNotNull(asentamiento);
            Assert.AreEqual("CALVILLITO", asentamiento.NOMBRE_ASENTAMIENTO);
        }

        [TestMethod]
        public void obtenerCatalogoNombreAsentamientoTest() 
        {           
            String nombre = dao.obtenerCatalogoNombreAsentamiento(json);
            Assert.IsNotNull(nombre);
            Assert.AreEqual("EJIDO CALVILLITO", nombre);
        }
    }
}
