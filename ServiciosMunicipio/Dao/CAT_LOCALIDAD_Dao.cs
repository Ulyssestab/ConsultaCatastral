using ServiciosMunicipio.Models;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Dao
{
    public class CAT_LOCALIDAD_Dao
    {

        private GDB01001Entities db = new GDB01001Entities();

        public List<CAT_LOCALIDAD> obtenerLocalidades(String id)
        {
            List<CAT_LOCALIDAD> localidades;
            id = AntiInjectionSQL.quitarComillas(id, Constantes.LONG_MAX_LOC);

            try
            {
                localidades = db.CAT_LOCALIDAD.SqlQuery("SELECT * "
                    + "FROM  sde.CAT_LOCALIDAD  where CVE_ENTIDAD = '01' "
                    + "and CVE_MUNICIPIO = '001' "
                    + "and NOM_LOCALIDAD like('%" + @id + "%') "
                    + "order by NOM_LOCALIDAD").ToList();
            }
            catch (System.InvalidOperationException e)
            {
                localidades = new List<CAT_LOCALIDAD>();
            }

            return localidades;
        }

        public List<CAT_LOCALIDAD> obtenerLocalidades(String clave_localidad, String nombre_localidad)
        {
            List<CAT_LOCALIDAD> localidades = new List<CAT_LOCALIDAD>();

            clave_localidad = AntiInjectionSQL.quitarComillas(clave_localidad, Constantes.LONG_MAX_LOC);            
            nombre_localidad = AntiInjectionSQL.quitarComillas(nombre_localidad, Constantes.LONG_MAX_NOM);            
            try
            {
                if (clave_localidad != null && clave_localidad != "")
                {
                    localidades = db.CAT_LOCALIDAD.SqlQuery("SELECT * "
                    + "FROM  sde.CAT_LOCALIDAD  " 
                    + "where CVE_ENTIDAD = '01' "
                    + "and CVE_MUNICIPIO = '001' "
                    + "and CVE_LOCALIDAD like('%" + @clave_localidad + "%') "
                    + "order by CVE_LOCALIDAD").ToList();
                }
                else if (nombre_localidad != null && nombre_localidad != "") 
                {
                    localidades = db.CAT_LOCALIDAD.SqlQuery("SELECT * "
                    + "FROM  sde.CAT_LOCALIDAD  where CVE_ENTIDAD = '01' "
                    + "and CVE_MUNICIPIO = '001' "
                    + "and NOM_LOCALIDAD like('%" + @nombre_localidad + "%') "
                    + "order by NOM_LOCALIDAD").ToList();
                }                
            }
            catch (System.InvalidOperationException e)
            {
                localidades = new List<CAT_LOCALIDAD>();
            }

            return localidades;
        }

        public CAT_LOCALIDAD obtenerCatalogoNombreLocalidad(String CVE_MUNICIPIO, String CVE_LOCALIDAD)
        {
            CAT_LOCALIDAD localidad = null;
            CVE_MUNICIPIO = AntiInjectionSQL.quitarComillas(CVE_MUNICIPIO, Constantes.LONG_MAX_LOC);
            CVE_LOCALIDAD = AntiInjectionSQL.quitarComillas(CVE_LOCALIDAD, Constantes.LONG_MAX_LOC);

            String consulta = "SELECT * " +
                "FROM sde.CAT_LOCALIDAD " +
                "where CVE_ENTIDAD='01' and CVE_MUNICIPIO='" + @CVE_MUNICIPIO + "' and CVE_LOCALIDAD='" + @CVE_LOCALIDAD + "';";
            try
            {
                localidad = db.CAT_LOCALIDAD.SqlQuery(consulta).First();
            }
            catch (System.InvalidOperationException e)
            {
                localidad = new CAT_LOCALIDAD();
            }
            return localidad;
        }

        public List<CAT_LOCALIDAD> catalogoLocalidad(String CVE_MUNICIPIO)
        {
            List<CAT_LOCALIDAD> catalogo = null;
            CVE_MUNICIPIO = AntiInjectionSQL.quitarComillas(CVE_MUNICIPIO, Constantes.LONG_MAX_LOC);
            String consulta = "SELECT * " +
                    "FROM sde.CAT_LOCALIDAD " +
                    "where CVE_ENTIDAD='01' and CVE_MUNICIPIO='" + @CVE_MUNICIPIO + "' order by NOM_LOCALIDAD;";
            try
            {
                catalogo = db.CAT_LOCALIDAD.SqlQuery(consulta).ToList();
            }
            catch (System.InvalidOperationException e)
            {
                catalogo = new List<CAT_LOCALIDAD>();
            }

            return catalogo;
        }

    }
}