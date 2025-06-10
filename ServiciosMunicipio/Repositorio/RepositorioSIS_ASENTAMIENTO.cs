using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Repositorio
{
    public interface RepositorioSIS_ASENTAMIENTO
    {
        List<SIS_ASENTAMIENTOS> obtenerAsentamientos(String consulta, String municipio);
        SIS_ASENTAMIENTOS obtenerAsentamiento(String consulta, string municipio);
        String obtenerCatalogoNombreAsentamiento(String consulta, string municipio);

    }
}