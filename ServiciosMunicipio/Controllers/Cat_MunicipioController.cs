using ServiciosMunicipio.Dao;
using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiciosMunicipio.Controllers
{
    public class Cat_MunicipioController : Controller
    {
        // GET: Cat_Municipio
        public ActionResult Index()
        {
            Cat_Municipio_Dao dao = new Cat_Municipio_Dao();
            List<Cat_Municipio> lista = dao.obtenerLista();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}