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
    public class AccesosDaoTest
    {
        AccesosDao dao = new AccesosDao();
        String nombreUsuario = "Administrador";
        String pass = "66FAEE583898CC373ED7EC34FC887DFF34809195";
        [TestMethod]
        public void AccesoUsuarioTest() 
        {
            Boolean acceso = dao.AccesoUsuario(nombreUsuario);
            Assert.IsNotNull(acceso);
            Assert.AreEqual(true, acceso);
        }
        [TestMethod]
        public void AccesoUsuarioPerfilTest()
        {
            String perfil = dao.AccesoUsuarioPerfil(nombreUsuario);
            Assert.IsNotNull(perfil);
            Assert.AreEqual("1", perfil);
        }
        [TestMethod]
        public void existeAccesoUsuario() 
        {
            nombreUsuario = "10du2";
            Usuario usuario = dao.existeAccesoUsuario(nombreUsuario, pass);
            Assert.IsNotNull(usuario);
            Assert.AreEqual(usuario.NombreUsuario, "EL_USUARIO");
        }

        [TestMethod]
        public void getMunicipioUsuarioTest()
        {
            String municipio = dao.getMunicipioUsuario(nombreUsuario);
            Assert.IsNotNull(municipio);
            Assert.AreEqual("009", municipio);
        }
    }
}
