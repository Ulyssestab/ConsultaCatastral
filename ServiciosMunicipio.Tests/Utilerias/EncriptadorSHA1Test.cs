
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
    public class EncriptadorSHA1Test
    {
        private EncriptadorSHA1 encriptador = new EncriptadorSHA1();

        [TestMethod]
        public void getSha1Test() 
        {
            String pass = "Cat*---!!#";
            String r = encriptador.getSha1(pass);
            Assert.AreEqual("F4E18C7C7BA61C109DE864CB52E9FC78FC7AB013", r); 
        }

        [TestMethod]
        public void EnryptStringTest() 
        {
            String pass = "Cat*---!!##";            
            String r = encriptador.EnryptString(pass);
            Assert.AreEqual("Q2F0Ki0tLSEhIyM=", r);
        }

        [TestMethod]
        public void EncryptDataTest() {
            String pass = "Cat#122323";            
            String r = encriptador.EncryptData(pass);
            Assert.AreEqual("qGyRWbpEXP4N+De7PVDLRw==", r);
        }

        [TestMethod]
        public void DecryptStringTest()
        {
            String resultado = "?5?.??:?-t?1<????a=.?.?Mw"; 
            String pass = "F4E18C7C7BA61C109DE864CB52E9FC78FC7AB013";
            Assert.AreEqual(resultado, encriptador.DecryptString (pass));
        }

    }
}
