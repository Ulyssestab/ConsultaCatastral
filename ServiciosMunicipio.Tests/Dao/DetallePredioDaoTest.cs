using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiciosMunicipio.Models.Entidades;
using ServiciosMunicipio.Repositorio.Impl;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiciosMunicipio.Dao;

namespace ServiciosMunicipio.Dao
{
    [TestClass]
    public class DetallePredioDaoTest
    {
        private DetallePredioDao dao = new DetallePredioDao();
        private DetallePredio resultado = new DetallePredio();
        private String claveCatastral = "0100000147000102504300006000000";
        private String municipio = "001";

        [TestMethod]
        public void obtenerClavesEstandarTest()
        {             
            // Actuar
            resultado = dao.getDetallePredio(claveCatastral, municipio);

            DetallePredio mock = new DetallePredio();
            mock.CVE_CAT_EST = claveCatastral;
            Assert.IsNotNull(resultado);
            Assert.AreEqual(mock.CVE_CAT_EST, resultado.CVE_CAT_EST);
            claveCatastral = "apeornok123";
            resultado = dao.getDetallePredio(claveCatastral, municipio);
            Assert.AreNotEqual(mock.CVE_CAT_EST, resultado.CVE_CAT_EST);
            Assert.IsNull(mock.CVE_CAT_ORI);
        }

    }
}
