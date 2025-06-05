using ServiciosMunicipio.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosMunicipio.Repositorio
{
    interface RepositorioDetallePredio
    {
        DetallePredio ObtenerElemento(String consulta, String municipio);
        int ObtenerTotal(String consulta, String municipio);
    }
}
