using ServiciosMunicipio.Models;
using ServiciosMunicipio.Repositorio.Impl;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Dao
{
    public class SIS_ASENTAMIENTOS_Dao
    {
        private RepositorioSIS_ASENTAMIENTOImp db = new RepositorioSIS_ASENTAMIENTOImp();

        public List<SIS_ASENTAMIENTOS> obtenerAsentamientos(SIS_ASENTAMIENTOS asentamiento)
        {
            String CVE_MUNICIPIO = AntiInjectionSQL.quitarComillas(@asentamiento.CVE_MUNICIPIO, Constantes.LONG_MAX_LOC);
            String CVE_LOCALIDAD = AntiInjectionSQL.quitarComillas(@asentamiento.CVE_LOCALIDAD, Constantes.LONG_MAX_LOC);
            String NOMBRE_ASENTAMIENTO = AntiInjectionSQL.quitarComillas(@asentamiento.NOMBRE_ASENTAMIENTO, Constantes.LONG_MAX_NOM);

            String consulta = "SELECT * FROM sde.SIS_ASENTAMIENTOS " +
                    "where STATUSREGISTROTABLA='ACTIVO' " +
                    " and CVE_ENTIDAD='01' " +
                    " and CVE_MUNICIPIO=" + CVE_MUNICIPIO +
                    " and CVE_LOCALIDAD=" + CVE_LOCALIDAD +
                    " and NOMBRE_COMPLETO_ASENTAMIENTO " +
                    " like('%" + NOMBRE_ASENTAMIENTO + "%') " +
                    " or NOMBRE_COMPLETO_ASENTAMIENTO='" + NOMBRE_ASENTAMIENTO +
                    "' order by NOMBRE_COMPLETO_ASENTAMIENTO;";
            List<SIS_ASENTAMIENTOS> r = new List<SIS_ASENTAMIENTOS>();
            try
            {
                r = db.obtenerAsentamientos(consulta, CVE_MUNICIPIO);
            }
            catch (System.InvalidOperationException e)
            {
                r = new List<SIS_ASENTAMIENTOS>();
            }
            
            return r;
        }

        public SIS_ASENTAMIENTOS obtenerAsentamiento(int id, String municipio)
        {
            String consulta = "Select * from  sde.SIS_ASENTAMIENTOS  where OBJECTID =" + @id;
            return db.obtenerAsentamiento(consulta, municipio) != null ? db.obtenerAsentamiento(consulta, municipio) : new SIS_ASENTAMIENTOS();
        }

        public String obtenerCatalogoNombreAsentamiento(SIS_ASENTAMIENTOS asentamiento)
        {
            String nombre_Asentamiento = null;
            String CVE_ASENTAMIENTO = AntiInjectionSQL.quitarComillas(@asentamiento.CVE_ASENTAMIENTO, Constantes.LONG_MAX_NOM);
            String CVE_LOCALIDAD = AntiInjectionSQL.quitarComillas(@asentamiento.CVE_LOCALIDAD, Constantes.LONG_MAX_LOC);
            String CVE_MUNICIPIO = AntiInjectionSQL.quitarComillas(@asentamiento.CVE_MUNICIPIO, Constantes.LONG_MAX_LOC);

            String consulta = "SELECT * " +
                    "FROM sde.SIS_ASENTAMIENTOS " +
                    "where STATUSREGISTROTABLA='ACTIVO' " +
                    "and CVE_ENTIDAD='01' " +
                    "and CVE_MUNICIPIO='" + CVE_MUNICIPIO + "' " +
                    "and CVE_LOCALIDAD='" + CVE_LOCALIDAD
                    + "' and CVE_ASENTAMIENTO ='" + CVE_ASENTAMIENTO + "';";

            try
            {
                nombre_Asentamiento = db.obtenerCatalogoNombreAsentamiento(consulta, CVE_MUNICIPIO);
            }
            catch (System.InvalidOperationException e)
            {
                nombre_Asentamiento = "";
            }

            return nombre_Asentamiento;
        }

    }

}
