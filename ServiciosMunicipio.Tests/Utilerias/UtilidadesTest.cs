using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiciosMunicipio.Models.Entidades;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosMunicipio.Tests.Utilerias
{
    [TestClass]
    public class UtilidadesTest
    {
        Utilidades util = new Utilidades();
        private String claveCatastralOrig = "01001080237054000";
        [TestMethod]
        public void completarCerosTest()
        {
            String resultado = util.completarCeros(3, "");
            ClaveCatastralOriginal claveCatastralOriginal = new ClaveCatastralOriginal
            {
                campoMunicipioOri = "01",
                campLocalidadOri = "001",
                campSectorOri = "08",
                campManzanaOri = "0237",
                campPredioOri = "054",
                campCondominioOri = "000"
            };
            Assert.AreEqual("", resultado);
            resultado = claveCatastralOriginal.campoMunicipioOri
               + util.completarCeros(3, claveCatastralOriginal.campLocalidadOri)
               + util.completarCeros(2, claveCatastralOriginal.campSectorOri)
               + util.completarCeros(4, claveCatastralOriginal.campManzanaOri)
               + util.completarCeros(3, claveCatastralOriginal.campPredioOri)
               + util.completarCeros(3, claveCatastralOriginal.campCondominioOri);
            Assert.AreEqual(claveCatastralOrig, resultado);

        }
        [TestMethod]
        public void getMilesTest() 
        {
            int resultado = Utilidades.getMiles(3);
            Assert.AreEqual(1000, resultado);
            resultado = Utilidades.getMiles(0);
            Assert.AreEqual(1,resultado);
        }
        [TestMethod]
        public void formarConsultaUbicacionXpredioTest()
        {
            UbicacionPredio predio = new UbicacionPredio
            {
                localidad = "",
                clave_localidad = "1055",
                cve_asentamiento = "",
                asentamiento = "",
                calle = "",
                numExt = "",
                municipio = "001"
            };
            String consulta = util.formarConsultaUbicacionXpredio(predio, Constantes.XPREDIO);
            Assert.IsTrue(!String.IsNullOrEmpty(consulta));
            Assert.AreEqual("CVE_MUNICIPIO='001'  and CVE_LOCALIDAD='1055' ", consulta);

            predio.localidad = "PRIMAVERA";
            consulta = util.formarConsultaUbicacionXpredio(predio, Constantes.XPREDIO);
            Assert.AreEqual("CVE_MUNICIPIO='001'  and CVE_LOCALIDAD='1055'  and NOM_LOCALIDAD='PRIMAVERA' ", consulta);

            predio.localidad = "PRIMAVERA";            
            consulta = util.formarConsultaUbicacionXpredio(predio, Constantes.TOTALXPREDIO);
            Assert.AreEqual("CVE_MUNICIPIO='001'  and CVE_LOCALIDAD='1055'  and NOM_LOCALIDAD like('%PRIMAVERA%') ", consulta);
        }

        [TestMethod]
        public void getMunicipioCve_Ori() 
        {
            String clave = Utilidades.getMunicipioCve_Ori("");
            Assert.AreEqual("001", clave);

            clave = Utilidades.getMunicipioCve_Ori(null);
            Assert.AreEqual("001", clave);

            clave = Utilidades.getMunicipioCve_Ori(claveCatastralOrig);
            Assert.AreEqual("001", clave);
        }
    }
}
