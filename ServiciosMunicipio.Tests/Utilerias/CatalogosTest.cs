using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosMunicipio.Tests.Utilerias
{
    [TestClass]
    public class CatalogosTest
    {
        [TestMethod]
        public void formarCondicionTest() 
        {
            String condicion = Catalogos.formarCondicion("","","","",0);
            Assert.AreEqual("", condicion);

            condicion = Catalogos.formarCondicion(null, null,null,null, 0);
            Assert.AreEqual("", condicion);
        }

        [TestMethod]
        public void formarCondicionClaveTest()
        {
            String condicion = Catalogos.formarCondicion("Select * from db where ", "CVE_MUNICIPIO", "001", "", Constantes.COMILLAS);
            Assert.AreEqual("Select * from db where CVE_MUNICIPIO='001' ", condicion);

            condicion = Catalogos.formarCondicion("Select * from db where ", "CVE_MUNICIPIO", "001", "", Constantes.LIKE);
            Assert.AreEqual("Select * from db where CVE_MUNICIPIO like('%001%') ", condicion);

            condicion = Catalogos.formarCondicion("", "CVE_MUNICIPIO", "001", "", Constantes.IGUAL);
            Assert.AreEqual("CVE_MUNICIPIO=001 ", condicion);
        }
    }    
}
