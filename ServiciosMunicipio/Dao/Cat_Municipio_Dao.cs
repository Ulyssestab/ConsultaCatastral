using ServiciosMunicipio.Models;
using ServiciosMunicipio.Models.Entidades;
using ServiciosMunicipio.Repositorio.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Dao
{
    public class Cat_Municipio_Dao
    {
        private WFTRAMITESEntities db = new WFTRAMITESEntities();
        private RepositorioSIS_PC_CENTROIDESImpl repo = new RepositorioSIS_PC_CENTROIDESImpl();

        public List<Cat_Municipio> obtenerLista() 
        {
            return db.Cat_Municipio.ToList<Cat_Municipio>();
        }

        public SIS_PC_CENTROIDES obtenerCoordenadas(string municipio, String clave)
        {
            return repo.ObtenerCoordenadas(municipio, clave);
        }
    }
}