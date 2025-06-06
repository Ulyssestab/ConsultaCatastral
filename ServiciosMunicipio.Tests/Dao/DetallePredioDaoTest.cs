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
        private List<Tramite> tramite = new List<Tramite>();
        private String claveCatastral = "0100000147000102504300006000000";
        private String municipio = "001";

        [TestMethod]
        public void obtenerClavesEstandarTest()
        {
            DetallePredio mock = new DetallePredio();
            // Actuar
            resultado = dao.getDetallePredio(claveCatastral, municipio);
            
            mock.CVE_CAT_EST = claveCatastral;
            Assert.IsNotNull(resultado);
            Assert.AreEqual(mock.CVE_CAT_EST, resultado.CVE_CAT_EST);

            claveCatastral = "apeornok123";
            resultado = dao.getDetallePredio(claveCatastral, municipio);
            Assert.AreNotEqual(mock.CVE_CAT_EST, resultado.CVE_CAT_EST);
            Assert.IsNull(mock.CVE_CAT_ORI);
        }


        [TestMethod]
        public void obtenerTramitesTest()
        {
            claveCatastral = "0100000129000000000000002000000";
            List<Tramite> mock = new List<Tramite>();
            mock.Add(new Tramite {
                NumeroTramite = "1400/2024",
                estatus = "1",
                NombreTramite = "LEVANTAMIENTO TOPOGRAFICO"
            });
            mock.Add(new Tramite
            {
                NumeroTramite = "258/2025",
                estatus = "1",
                NombreTramite = "LEVANTAMIENTO TOPOGRAFICO"
            });
            tramite = dao.obtenerTramites(claveCatastral);
            Assert.AreEqual(mock.Count, tramite.Count);
            Assert.AreEqual(mock[0].NombreTramite, tramite[0].NombreTramite);
        }

        [TestMethod]
        public void obtenerDetalleTareasTramiteTest()
        {
            String tramite = "1400/2024";
            List<TareasTramite> lista = new List<TareasTramite>();
            List<TareasTramite> mock = new List<TareasTramite>();
            mock.Add(new TareasTramite
            {
                Tarea = "SOLICITUD DE LEVANTAMIENTO TOPOGRÁFICO",
                Orden = "1",
                estatus = "SE FINALIZO"
            });
            mock.Add(new TareasTramite
            {
                Tarea = "REVISION DE DOCUMENTOS",
                Orden = "2",
                estatus = "SE FINALIZO"
            });
            lista = dao.obtenerDetalleTareasTramite(tramite);
            Assert.AreEqual(13, lista.Count);
            Assert.AreEqual(mock[0].Tarea, lista[0].Tarea);
        }

    }
}
