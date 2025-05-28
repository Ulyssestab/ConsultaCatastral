using InstitutoCatastralAGS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstitutoCatastralAGS.Repositorio
{
    interface RepositorioMunicipio
    {
        List<Resultados> ObtenerLista(String consulta, String municipio);
        int ObtenerTotal(String consulta, String municipio);
    }
}
