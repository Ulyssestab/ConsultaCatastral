using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Dao
{
    public class Cat_Municipio_Dao
    {
        private WFTRAMITESEntities db = new WFTRAMITESEntities();

        public List<Cat_Municipio> obtenerLista() 
        {
            return db.Cat_Municipio.ToList<Cat_Municipio>();
        }
    }
}