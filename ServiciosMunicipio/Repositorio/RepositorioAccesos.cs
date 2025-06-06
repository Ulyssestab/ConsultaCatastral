using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosMunicipio.Repositorio
{
    interface RepositorioAccesos
    {
        Usuario existeAccesoUsuario(String consulta);
    }
}
