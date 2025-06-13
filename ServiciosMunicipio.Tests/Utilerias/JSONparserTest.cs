using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiciosMunicipio.Models;
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
    public class JSONparserTest
    {
        private JSONparser parser = new JSONparser();
        [TestMethod]
        public void crearObjetoTest()
        {
            String json = "{\"OBJECTID\":1774,\"CVE_ENTIDAD\":\"01\",\"NOM_ENTIDAD\":null,\"CVE_MUNICIPIO\":\"001\",\"NOM_MUNICIPIO\":null,\"CVE_LOCALIDAD\":\"2373\",\"NOM_LOCALIDAD\":\"ADAPTACIONES Y PAILERIA CERVANTES\"}";
            CAT_LOCALIDAD localidad = parser.crearObjeto(json);
            Assert.IsNotNull(localidad);
            Assert.AreEqual("", localidad.NOM_MUNICIPIO);

            localidad = parser.crearObjeto(null);
            Assert.IsNotNull(localidad);
        }

        [TestMethod]
        public void crearObjetoAccesoTest()
        {
            String json = "{\"NombreUsuario\":1774,\"Nombre_Completo\":\"01\"}";
            Cat_Acceso acceso = parser.crearObjetoAcceso(json);
            Assert.IsNotNull(acceso);
            Assert.AreEqual("1774", acceso.NombreUsuario);

            acceso = parser.crearObjetoAcceso(null);
            Assert.IsNotNull(acceso);
        }

        [TestMethod]
        public void crearObjetoAsentamientoTest()
        {
            String json = "{\"NOMBRE_COMPLETO_ASENTAMIENTO\":1774,\"Nombre_Completo\":\"01\"}";
            SIS_ASENTAMIENTOS asentamiento = parser.crearObjetoAsentamiento(json);
            Assert.IsNotNull(asentamiento);
            Assert.AreEqual("1774", asentamiento.NOMBRE_COMPLETO_ASENTAMIENTO);

            asentamiento = parser.crearObjetoAsentamiento(null);
            Assert.IsNotNull(asentamiento);
        }

        //parseJsonStringUsuario
        [TestMethod]
        public void parseJsonStringUsuarioTest()
        {
            String json = "{\"ApellidoMaterno\":1774,\"Nombre_Completo\":\"01\"}";
            Usuario usuario = parser.parseJsonStringUsuario(json);
            Assert.IsNotNull(usuario);
            Assert.AreEqual("1774", usuario.ApellidoMaterno);

            usuario = parser.parseJsonStringUsuario(null);
            Assert.IsNotNull(usuario);
        }

        [TestMethod]
        public void parseJsonStringTest()
        {
            String json = "[{\"OBJECTID\":712,\"CLAVEUNICA\":712,\"STATUSREGISTROTABLA\":\"ACTIVO\",\"ALTAREGISTROTABLA\":\"\\/Date(1392703200000)\\/\",\"BAJAREGISTROTABLA\":\"\\/Date(-62135575200000)\\/\",\"HORAALTAREGISTROTABLA\":\"12:00:00\",\"HORABAJAREGISTROTABLA\":\"\",\"USUARIOALTA\":\"SIGSA\",\"USUARIOBAJA\":\"\",\"TERMINALALTA\":\"SIGSA\",\"TERMINALBAJA\":\"\",\"CVE_ENTIDAD\":\"01\",\"CVE_REGION_CATASTRAL\":\"000\",\"CVE_MUNICIPIO\":\"001\",\"CVE_ZONA_CATASTRAL\":\"47\",\"CVE_LOCALIDAD\":\"0001\",\"CVE_SECTOR_CATASTRAL\":\"001\",\"CVE_ASENTAMIENTO\":\"00010\",\"TIPO_ASENTAMIENTO\":\"COLONIA\",\"NOMBRE_ASENTAMIENTO\":\"PRIMAVERA\",\"CP\":\"20050\",\"NOMBRE_COMPLETO_ASENTAMIENTO\":\"COLONIA PRIMAVERA\",\"OBSERVACIONES\":\"\"},{\"OBJECTID\":4098,\"CLAVEUNICA\":0,\"STATUSREGISTROTABLA\":\"ACTIVO\",\"ALTAREGISTROTABLA\":\"\\/Date(1505973600000)\\/\",\"BAJAREGISTROTABLA\":\"\\/Date(-62135575200000)\\/\",\"HORAALTAREGISTROTABLA\":\"11:16:14 a.m.\",\"HORABAJAREGISTROTABLA\":\"\",\"USUARIOALTA\":\"CESAR HUMBERTO VAZQUEZ NOVELO \",\"USUARIOBAJA\":\"\",\"TERMINALALTA\":\"SEFI3090607D\",\"TERMINALBAJA\":\"\",\"CVE_ENTIDAD\":\"01\",\"CVE_REGION_CATASTRAL\":\"000\",\"CVE_MUNICIPIO\":\"001\",\"CVE_ZONA_CATASTRAL\":\"47\",\"CVE_LOCALIDAD\":\"0001\",\"CVE_SECTOR_CATASTRAL\":\"033\",\"CVE_ASENTAMIENTO\":\"00036\",\"TIPO_ASENTAMIENTO\":\"CONDOMINIO\",\"NOMBRE_ASENTAMIENTO\":\"PRIMAVERA ORQUIDEA\",\"CP\":\"20299\",\"NOMBRE_COMPLETO_ASENTAMIENTO\":\"CONDOMINIO PRIMAVERA ORQUIDEA\",\"OBSERVACIONES\":\"\"},{\"OBJECTID\":1134,\"CLAVEUNICA\":1134,\"STATUSREGISTROTABLA\":\"ACTIVO\",\"ALTAREGISTROTABLA\":\"\\/Date(1410933600000)\\/\",\"BAJAREGISTROTABLA\":\"\\/Date(-62135575200000)\\/\",\"HORAALTAREGISTROTABLA\":\"09:32:33 a.m.\",\"HORABAJAREGISTROTABLA\":\"\",\"USUARIOALTA\":\"CATASTRO\",\"USUARIOBAJA\":\"\",\"TERMINALALTA\":\"SEFI140158D\",\"TERMINALBAJA\":\"\",\"CVE_ENTIDAD\":\"01\",\"CVE_REGION_CATASTRAL\":\"000\",\"CVE_MUNICIPIO\":\"001\",\"CVE_ZONA_CATASTRAL\":\"47\",\"CVE_LOCALIDAD\":\"0001\",\"CVE_SECTOR_CATASTRAL\":\"025\",\"CVE_ASENTAMIENTO\":\"00006\",\"TIPO_ASENTAMIENTO\":\"POBLADO COMUNAL\",\"NOMBRE_ASENTAMIENTO\":\"LA PRIMAVERA\",\"CP\":\"20996\",\"NOMBRE_COMPLETO_ASENTAMIENTO\":\"POBLADO COMUNAL LA PRIMAVERA\",\"OBSERVACIONES\":\"\"}]";
            List<SIS_ASENTAMIENTOS> asentamientos = parser.parseJsonString(json);
            Assert.IsNotNull(asentamientos);
            Assert.AreEqual("COLONIA PRIMAVERA", asentamientos[0].NOMBRE_COMPLETO_ASENTAMIENTO);
            Assert.AreEqual(asentamientos.Count, 3);
            asentamientos = parser.parseJsonString(null);
            Assert.IsNotNull(asentamientos);
            Assert.AreEqual(asentamientos.Count,0);
        }

        
        [TestMethod]
        public void parseJsonStringPredioTest()
        {
            String json = "{\"NOMBRE_COMPLETO_ASENTAMIENTO\":1774,\"OBSERVACIONES\":\"01\"}";
            DetallePredio predio = parser.parseJsonStringPredio(json);
            Assert.IsNotNull(predio);
            Assert.AreEqual("1774", predio.NOMBRE_COMPLETO_ASENTAMIENTO);

            predio = parser.parseJsonStringPredio(null);
            Assert.IsNotNull(predio);
        }

        //crearObjetoMunicipio
        [TestMethod]
        public void crearObjetoMunicipioTest()
        {
            String json = "{\"Municipio\": \"EL LLANO\",\"OBSERVACIONES\":\"01\"},{\"Municipio\": \"EL LLANO\",\"OBSERVACIONES\":\"01\"}";
            List<Cat_Municipio> municipio = parser.crearObjetoMunicipio(json);
            Assert.IsNotNull(municipio);
            Assert.AreEqual("EL LLANO", municipio[0].Municipio);
            Assert.AreEqual(2, municipio.Count);
            municipio = parser.crearObjetoMunicipio(null);
            Assert.IsNotNull(municipio);
        }

        [TestMethod]
        public void crearObjetoResultadosTest() 
        {
            String json = "{\"NOM_LOCALIDAD\": \"EL LLANO\",\"NOMBRE_COMPLETO_ASENTAMIENTO\":\"01\"},{\"NOM_LOCALIDAD\": \"EL LLANO\",\"NOMBRE_COMPLETO_ASENTAMIENTO\":\"01\"}";
            List<Resultados> resultados = parser.crearObjetoResultados(json);
            Assert.IsNotNull(resultados);
            Assert.AreEqual("01", resultados[0].NOMBRE_COMPLETO_ASENTAMIENTO);
            Assert.AreEqual(2, resultados.Count);
            resultados = parser.crearObjetoResultados(null);
            Assert.IsNotNull(resultados);
        }
    }
}
