using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiciosMunicipio.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult ObtenerUsuario()
        {
            return View();
        }
    }
}