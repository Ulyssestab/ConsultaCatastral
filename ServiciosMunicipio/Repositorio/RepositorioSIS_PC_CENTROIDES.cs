using ServiciosMunicipio.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Repositorio
{
    interface RepositorioSIS_PC_CENTROIDES
    {
        List<SIS_PC_CENTROIDES> ObtenerLista(String consulta, String municipio, String CVE_CAT_EST);
        SIS_PC_CENTROIDES ObtenerCoordenadas(String municipio, String CVE_CAT_EST);
    }
}