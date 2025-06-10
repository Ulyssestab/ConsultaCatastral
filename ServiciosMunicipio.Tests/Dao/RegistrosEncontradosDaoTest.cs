using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiciosMunicipio.Dao;
using ServiciosMunicipio.Models;
using ServiciosMunicipio.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosMunicipio.Tests.Controllers
{
    [TestClass]
    public class RegistrosEncontradosDaoTest
    {
        private String claveCatastralOriginal = "01001080237054000";
        private String clavePredialEstandar = "0100000147000100435300066000000";
        private String numCuentaPredial = "";
        private String municipio = "001";
        private String folioReal = "124631";
        private String personaFisicaStr = "GOBIERNO DEL ESTADO DE AGUASCALIENTES";
        private int resultado = 0;
        private List<Resultados> resultados = null;
        private ClaveCatastralOriginal claveCatastralOrig = new ClaveCatastralOriginal
        {
            campoMunicipioOri = "01", //01 001 08 0237 054 000
            campLocalidadOri = "001",
            campSectorOri = "08",
            campManzanaOri = "0237",
            campPredioOri = "054",
            campCondominioOri = "000"
        };
        private Resultados modelo = new Resultados
        {
            NUM = null,
            CVE_CAT_EST = "0100000147000102504300006000000",
            CVE_CAT_ORI = "01000990011514000",
            clavePredial = "R001040",
            NUMERO_EXTERIOR = "0",
            NOMBRE_COMPLETO_ASENTAMIENTO = "NINGUNO",
            NOM_LOCALIDAD = "LA PRIMAVERA",
            NOM_MUNICIPIO = null,
            NOMBRE_COMPLETO_VIALIDAD = "NINGUNO",
            NOMBRE_O_RAZON_SOCIAL = "COMISION FEDERAL DE ELECTRICIDAD",
            APELLIDO_PATERNO = "",
            APELLIDO_MATERNO = "",
            FOLIO_REAL = null
        };
        private UbicacionPredio predio = new UbicacionPredio
        {
            localidad = "",
            clave_localidad = "1055",
            cve_asentamiento = "",
            asentamiento = "",
            calle = "",
            numExt = "",
            municipio = "001"
        };

        private PersonaFisica personaFisica = new PersonaFisica
        {
            nombre = "Roberto",
            apaterno = "Solis",
            amaterno = "Montes",
            municipio = "001"
        };
        private RegistrosEncontradosDao dao = new RegistrosEncontradosDao();

        [TestMethod]
        public void numClavePredialTest()
        {
            // Actuar
            resultado = dao.numClavePredial(clavePredialEstandar, numCuentaPredial, municipio);
            // Declarar            
            Assert.AreEqual(resultado, 0);
        }
        [TestMethod]
        public void numClavesEstandarTest()
        {
            // Actuar
            resultado = dao.numClavesEstandar(clavePredialEstandar, municipio);
            // Declarar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, 1);
        }
        [TestMethod]
        public void numDireccionTest()
        {
            String condicion = " CVE_MUNICIPIO='001' and CVE_LOCALIDAD = '2253' and NOM_LOCALIDAD = 'LA PRIMAVERA'";
            //  Nombre y Clave
            resultado = dao.numDireccion(condicion, municipio);
            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, 0);

            /// Clave
            condicion = " CVE_MUNICIPIO='001' and CVE_LOCALIDAD = '1055'";
            resultado = dao.numDireccion(condicion, municipio);
            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, 1);

            /// Nombre
            condicion = " CVE_MUNICIPIO='001' and NOM_LOCALIDAD = 'PRIMAVERA'";
            resultado = dao.numDireccion(condicion, municipio);
            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, 0);

        }
        [TestMethod]
        public void numFolioRealTest()
        {
            // Actuar
            resultado = dao.numFolioreal(folioReal, municipio);
            // Declarar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, 1);
        }
        [TestMethod]
        public void obtenerClavesEstandarTest()
        {
            int pag = 0;

            // Actuar
            resultados = dao.obtenerClavesEstandar(clavePredialEstandar, municipio, pag);

            List<Resultados> mock = new List<Resultados>();
            mock.Add(modelo);
            // Declarar
            Assert.IsNotNull(resultados);
            Assert.AreEqual(mock.Count, resultados.Count);
        }

        [TestMethod]
        public void obtenerResultadoFolioRealTest()
        {
            int pag = 0;

            // Actuar
            resultados = dao.obtenerResultadoFolioReal(folioReal, municipio, pag);

            List<Resultados> mock = new List<Resultados>();
            mock.Add(modelo);
            // Declarar
            Assert.IsNotNull(resultados);
            Assert.AreEqual(mock.Count, resultados.Count);
        }

        [TestMethod]
        public void obtenerResultadoUbicacionPredioTest()
        {
            int pag = 0;

            // Actuar
            resultados = dao.obtenerResultadoUbicacionPredio(predio, pag);

            List<Resultados> mock = new List<Resultados>();
            mock.Add(modelo);
            // Declarar
            Assert.IsNotNull(resultados);
            Assert.AreEqual(mock.Count, resultados.Count);
        }

        [TestMethod]
        public void numClavesOriginalesTest()
        {
            int resultados = dao.numClavesOriginales(claveCatastralOriginal);

            // Declarar
            Assert.IsNotNull(resultados);
            Assert.AreEqual(1, resultados);
        }

        [TestMethod]
        public void obtenerClavesOriginalTest()
        {
            List<Resultados> mock = new List<Resultados>();
            mock.Add(modelo);
            List<Resultados> resultado = dao.obtenerClavesOriginal(claveCatastralOriginal,0,10);
            Assert.AreEqual(resultado.Count, mock.Count);
        }

        [TestMethod]
        public void obtenerClavesPredialTest()
        {
            ClavePredial clavePredial = new ClavePredial()
            {
                claveCuentaPredial = "",
                numCuentaPredial = "R001040",
                municipioCE = "001",
                offset = 0
            };
            List<Resultados> mock = new List<Resultados>();
            mock.Add(modelo);
            List<Resultados> resultado = dao.obtenerClavesPredial(clavePredial);
            Assert.AreEqual(resultado.Count, mock.Count);
        }

        [TestMethod]
        public void obtenerResultadoPersonaFisicaTest()
        {            
            List<Resultados> mock = new List<Resultados>();
            mock.Add(modelo);            
            List<Resultados> resultado = dao.obtenerResultadoPersonaFisica(personaFisica, 0, 10);
            Assert.AreEqual(resultado.Count, mock.Count);
        }

        [TestMethod]
        public void numPredioxPersonaFisicaTest()
        {
            List<Resultados> resultado = dao.obtenerResultadoPersonaFisica(personaFisica, 0, 10);
            Assert.AreEqual(resultado.Count, 2);
        }

        [TestMethod]
        public void obtenerResultadoPersonaMoralTest()
        {                                            
            List<Resultados> resultado = dao.obtenerResultadoPersonaMoral(personaFisicaStr, municipio, 0, 10);
            Assert.AreEqual(resultado.Count, 10);
        }
        
        [TestMethod]
        public void numPredioxPersonaMoralTest() 
        {
            List<Resultados> resultado = dao.obtenerResultadoPersonaMoral(personaFisicaStr, municipio, 0, 10);
            Assert.AreEqual(resultado.Count, 10);
        }

    }
}
