using ServiciosMunicipio.Dao;
using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ServiciosMunicipio.Controllers
{
    public class BitacoraController : Controller
    {
        private BitacoraDao dao = new BitacoraDao();

        // GET: Bitacora/InsertarRegistroEnBitacora
        [System.Web.Http.HttpPost]
        public String InsertarRegistroEnBitacora([FromBody] BitacoraAccesoSistemas bitacora) {
            String respuesta = dao.insertarDatos(bitacora);
            return respuesta;
        }
    }
}