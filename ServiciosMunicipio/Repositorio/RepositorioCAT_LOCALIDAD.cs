using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosMunicipio.Repositorio
{
    interface RepositorioCAT_LOCALIDAD
    {
        List<CAT_LOCALIDAD> obtenerLocalidades(String id, String municipio);
        List<CAT_LOCALIDAD> obtenerLocalidades(String clave_localidad, String nombre_localidad, String municipio);
    }
}
