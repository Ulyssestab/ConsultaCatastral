using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Dao
{
    public class Servicios_Consulta_Cat_Dao
    {
        private WFTRAMITESEntities db = new WFTRAMITESEntities();
        public List<Servicios_Consulta_Cat> obtenerLista() 
        {
            List<Servicios_Consulta_Cat> lista = new List<Servicios_Consulta_Cat>();
            lista = db.Servicios_Consulta_Cat.OrderBy(p => p.nombre).ToList<Servicios_Consulta_Cat>();
            return lista;
        }
    }
}