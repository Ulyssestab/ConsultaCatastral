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
    public class Cat_Municipio_DaoTest
    {
        Cat_Municipio_Dao dao = new Cat_Municipio_Dao();
       [TestMethod]
        public void listaDeMunicipiosTest()
        {
            // Actuar
            List<Cat_Municipio> resultado = dao.obtenerLista();
            // Declarar            
            Assert.AreEqual(resultado.Count, 11);
        }
    }
}
