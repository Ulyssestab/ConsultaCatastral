using InstitutoCatastralAGS.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiciosMunicipio.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosMunicipio.Tests.Controllers
{
    [TestClass]
    class RegistrosEncontradosDaoTest
    {
        private String clavePredial = "0100000147000102504300006000000";
        private String numCuentaPredial = "";
        private String municipio = "001";
        private int resultado = 0;
        private List<Resultados> resultados = null;
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
        private RegistrosEncontradosDao dao = new RegistrosEncontradosDao();

        [TestMethod]
        public void numClavePredialTest()
        {                 
            // Actuar
            resultado = dao.numClavePredial(clavePredial, numCuentaPredial, municipio);
            // Declarar            
            Assert.AreEqual(resultado, 1);
        }
        [TestMethod]
        public void numClavesEstandarTest()
        {
            // Actuar
            resultado = dao.numClavesEstandar(clavePredial, municipio);
            // Declarar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado,1);
        }
        [TestMethod]
        public void numDireccionTest()
        {
            // Actuar
            resultado = dao.numDireccion(clavePredial, municipio);
            // Declarar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, 1);
        }
        [TestMethod]
        public void numFolioRealTest()
        {
            // Actuar
            resultado = dao.numFolioreal(clavePredial, municipio);
            // Declarar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado, 1);
        }
        [TestMethod]
        public void obtenerClavesEstandarTest()
        {
            int pag = 0;            

            // Actuar
            resultados = dao.obtenerClavesEstandar(clavePredial, municipio, pag);

            List<Resultados> mock = new List<Resultados>();
            mock.Add(modelo);
            // Declarar
            Assert.IsNotNull(resultados);
            Assert.AreEqual(mock,resultados);
        }



    }
}
