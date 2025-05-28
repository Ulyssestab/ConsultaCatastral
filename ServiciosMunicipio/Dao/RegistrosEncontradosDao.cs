using ServiciosMunicipio.Conexion;
using ServiciosMunicipio.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Dao
{
    public class RegistrosEncontradosDao
    {
        public int numClavesEstandar(String clave, String municipio)
        {
            String consulta = "select count(*) total "
                                + "from sde.sis_pc_clave_catastral pp "
                                + "left join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_EST = pp.cve_cat_est "
                                + "and pu.STATUSREGISTROTABLA = 'ACTIVO' "
                                + "left join sde.SIS_PC_PROPIETARIOS p " 
                                + "on p.OBJECTID=(select max (OBJECTID)from sde.SIS_PC_PROPIETARIOS where STATUSREGISTROTABLA='ACTIVO' and CVE_CAT_ORI=pp.CVE_CAT_ORI) "
                                + "where pp.cve_cat_est like '" + @clave + "%' "
                                + "and pp.STATUSREGISTROTABLA = 'ACTIVO' ";
            ConexionAsentamientosBD con = new ConexionAsentamientosBD();
            
            return con.Conexion(consulta, municipio);
        }
    }
}