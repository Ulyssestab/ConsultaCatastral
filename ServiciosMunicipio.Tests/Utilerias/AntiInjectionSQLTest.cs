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
    public class AntiInjectionSQLTest
    {
        [TestMethod]
        public void quitarComillasTest() 
        {
            String cadena = AntiInjectionSQL.quitarComillas("''023'",Constantes.LONG_MAX_LOC);
            Assert.IsNotNull(cadena);
            Assert.AreEqual(cadena, "023");
        }

        //
        [TestMethod]
        public void esEnteroTest()
        {                        
            Assert.IsTrue(AntiInjectionSQL.esEntero("4532"));
            Assert.IsFalse(AntiInjectionSQL.esEntero("dddd"));
        }
    }
}
