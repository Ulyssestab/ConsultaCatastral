using ServiciosMunicipio.Models;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Dao
{
    public class SIS_ASENTAMIENTOS_Dao
    {
        private GDB01001Entities db = new GDB01001Entities();

        public List<SIS_ASENTAMIENTOS> obtenerAsentamientos(SIS_ASENTAMIENTOS asentamiento)
        {
            String CVE_MUNICIPIO = AntiInjectionSQL.quitarComillas(@asentamiento.CVE_MUNICIPIO, Constantes.LONG_MAX_LOC);
            String CVE_LOCALIDAD = AntiInjectionSQL.quitarComillas(@asentamiento.CVE_LOCALIDAD, Constantes.LONG_MAX_LOC);
            String NOMBRE_ASENTAMIENTO = AntiInjectionSQL.quitarComillas(@asentamiento.NOMBRE_ASENTAMIENTO, Constantes.LONG_MAX_NOM);

            var q = "SELECT * FROM sde.SIS_ASENTAMIENTOS " +
                    "where STATUSREGISTROTABLA='ACTIVO' " +
                    "and CVE_ENTIDAD='01' " +
                    "and CVE_MUNICIPIO=" + CVE_MUNICIPIO +
                    "and CVE_LOCALIDAD=" + CVE_LOCALIDAD +
                    "and NOMBRE_COMPLETO_ASENTAMIENTO " +
                    "like('%" + NOMBRE_ASENTAMIENTO + "%') " +
                    "or NOMBRE_COMPLETO_ASENTAMIENTO='" + NOMBRE_ASENTAMIENTO +
                    "' order by NOMBRE_COMPLETO_ASENTAMIENTO;";
            List<SIS_ASENTAMIENTOS> r = new List<SIS_ASENTAMIENTOS>();
            try
            {
                r = db.SIS_ASENTAMIENTOS.SqlQuery(q).ToList();
            }
            catch (System.InvalidOperationException e)
            {
                r = new List<SIS_ASENTAMIENTOS>();
            }
            
            return r;
        }

        public SIS_ASENTAMIENTOS obtenerAsentamiento(int id)
        {
            var consulta = from st in db.SIS_ASENTAMIENTOS
                           where st.OBJECTID == @id
                           select st;
            return consulta.First() != null ? consulta.First() : new SIS_ASENTAMIENTOS();
        }

        public String obtenerCatalogoNombreAsentamiento(SIS_ASENTAMIENTOS asentamiento)
        {
            String nombre_Asentamiento = null;
            String CVE_ASENTAMIENTO = AntiInjectionSQL.quitarComillas(@asentamiento.CVE_ASENTAMIENTO, Constantes.LONG_MAX_NOM);
            String CVE_LOCALIDAD = AntiInjectionSQL.quitarComillas(@asentamiento.CVE_LOCALIDAD, Constantes.LONG_MAX_LOC);
            String CVE_MUNICIPIO = AntiInjectionSQL.quitarComillas(@asentamiento.CVE_MUNICIPIO, Constantes.LONG_MAX_LOC);
            /* CVE_MUNICIPIO = String.format("%03d", Integer.parseInt(CVE_MUNICIPIO));
             CVE_LOCALIDAD = String.format("%04d", Integer.parseInt(CVE_LOCALIDAD));
             CVE_ASENTAMIENTO = String.format("%05d", Integer.parseInt(CVE_ASENTAMIENTO));*/
            String consulta = "SELECT * " +
                    "FROM sde.SIS_ASENTAMIENTOS " +
                    "where STATUSREGISTROTABLA='ACTIVO' " +
                    "and CVE_ENTIDAD='01' " +
                    "and CVE_MUNICIPIO='" + CVE_MUNICIPIO + "' " +
                    "and CVE_LOCALIDAD='" + CVE_LOCALIDAD
                    + "' and CVE_ASENTAMIENTO ='" + CVE_ASENTAMIENTO + "';";

            try
            {
                nombre_Asentamiento = db.SIS_ASENTAMIENTOS.SqlQuery(consulta).First().NOMBRE_COMPLETO_ASENTAMIENTO;
            }
            catch (System.InvalidOperationException e)
            {
                nombre_Asentamiento = "";
            }

            return nombre_Asentamiento;
        }

    }

}
