using InstitutoCatastralAGS.Models;
using InstitutoCatastralAGS.Repositorio.Impl;
using ServiciosMunicipio.Conexion;
using ServiciosMunicipio.Models.Entidades;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Dao
{
    public class RegistrosEncontradosDao
    {
        RepositorioMunicipioImp repositorio = new RepositorioMunicipioImp();
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


            return repositorio.ObtenerTotal(consulta, municipio);
        }
        public List<Resultados> obtenerClavesEstandar(String clave, String municipio, int pag)

        {
            int max = numClavesEstandar(clave, municipio);

            String consulta = "select NUM,"
                    + " CVE_CAT_EST,"
                    + " CVE_CAT_ORI,"
                    + " clavePredial,"
                    + " NOMBRE_O_RAZON_SOCIAL,"
                    + " APELLIDO_PATERNO,"
                    + " APELLIDO_MATERNO,"
                    + " NOM_LOCALIDAD,"
                    + " NOMBRE_COMPLETO_ASENTAMIENTO,"
                    + " NOMBRE_COMPLETO_VIALIDAD,"
                    + " NUMERO_EXTERIOR "
                    + "from "
                    + "("
                    + " select pu.CVE_CAT_EST,"
                    + " pu.CVE_CAT_ORI,"
                    + " pp.cve_predial clavePredial,"
                    + " p.NOMBRE_O_RAZON_SOCIAL,"
                    + " p.APELLIDO_PATERNO,"
                    + " p.APELLIDO_MATERNO,"
                    + " pu.NOM_LOCALIDAD,"
                    + " pu.NOMBRE_COMPLETO_ASENTAMIENTO,"
                    + " pu.NOMBRE_COMPLETO_VIALIDAD,"
                    + " pu.NUMERO_EXTERIOR ,"
                    + " ROW_NUMBER() OVER (ORDER BY pu.CVE_CAT_EST ) AS NUM "
                    + " from sde.sis_pc_clave_catastral pp "
                    + " left join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_EST = pp.cve_cat_est and pu.STATUSREGISTROTABLA = 'ACTIVO'"
                    + " left join sde.SIS_PC_PROPIETARIOS p on p.OBJECTID=(select max (OBJECTID)from sde.SIS_PC_PROPIETARIOS where STATUSREGISTROTABLA='ACTIVO' and CVE_CAT_ORI=pp.CVE_CAT_ORI)"
                    + " where pp.cve_cat_est like '" + @clave + "%' "
                    + "and pp.STATUSREGISTROTABLA = 'ACTIVO') as a"
                    + " WHERE NUM BETWEEN " + ((@pag * @max) + 1) + " AND " + (((@pag + 1) * @max)) + ";";
            return repositorio.ObtenerLista(consulta, municipio);
        }

        public int numClavePredial(string clavePredial, String numCuentaPredial, string municipio)
        {
            String consulta = "select count(*) total "
                + "from sde.sis_pc_clave_catastral pp "
                + "left join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_EST = pp.CVE_CAT_EST and pu.STATUSREGISTROTABLA = 'ACTIVO' "
                + "left join sde.SIS_PC_PROPIETARIOS p on p.OBJECTID=(select max (OBJECTID)from sde.SIS_PC_PROPIETARIOS where STATUSREGISTROTABLA='ACTIVO' and CVE_CAT_ORI=pp.CVE_CAT_ORI) "
                + "where pp.cve_predial like '" + clavePredial + numCuentaPredial + "%' and pp.STATUSREGISTROTABLA = 'ACTIVO' ";
            return repositorio.ObtenerTotal(consulta, municipio);
        }
        internal List<Resultados> obtenerClavesPredial(String clavePredial, String numCuentaPredial,  String municipioCE, int pag)
        {
            int max = numClavePredial(clavePredial, numCuentaPredial, municipioCE);
            String consulta = "select NUM,"
                                + " CVE_CAT_EST,"
                                + " CVE_CAT_ORI,"
                                + " clavePredial,"
                                + " NOMBRE_O_RAZON_SOCIAL,"
                                + " APELLIDO_PATERNO,"
                                + " APELLIDO_MATERNO, "
                                + "NOM_LOCALIDAD, "
                                + "NOMBRE_COMPLETO_ASENTAMIENTO, NOMBRE_COMPLETO_VIALIDAD, NUMERO_EXTERIOR "
                                + "from ( "
                                + "select pu.CVE_CAT_EST, pu.CVE_CAT_ORI, pp.cve_predial clavePredial, p.NOMBRE_O_RAZON_SOCIAL, p.APELLIDO_PATERNO, p.APELLIDO_MATERNO, pu.NOM_LOCALIDAD, pu.NOMBRE_COMPLETO_ASENTAMIENTO, pu.NOMBRE_COMPLETO_VIALIDAD, pu.NUMERO_EXTERIOR, ROW_NUMBER() OVER (ORDER BY pu.CVE_CAT_EST ) AS NUM "
                                + "from sde.sis_pc_clave_catastral pp "
                                + "left join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_ORI = pp.CVE_CAT_ORI and pu.STATUSREGISTROTABLA = 'ACTIVO' "
                                + "left join sde.SIS_PC_PROPIETARIOS p on p.OBJECTID=(select max (OBJECTID)from sde.SIS_PC_PROPIETARIOS where STATUSREGISTROTABLA='ACTIVO' and CVE_CAT_ORI=pp.CVE_CAT_ORI) "
                                + "where pp.cve_Predial like '" + @clavePredial + @numCuentaPredial + "%' and pp.STATUSREGISTROTABLA = 'ACTIVO') as a " 
                                + "WHERE NUM BETWEEN " + ((pag * max) + 1) + " AND " + (((pag + 1) * max)) + ";";
            return repositorio.ObtenerLista(consulta, municipioCE);
        }

        public int numFolioreal(string folioReal, string municipio)
        {
            String consulta = "select count(*) total"
                + " from sde.SIS_PC_CLAVE_CATASTRAL"
                + " WHERE STATUSREGISTROTABLA = 'ACTIVO'"
                + " AND FOLIO_REAL = '" + @folioReal + "'";
            return repositorio.ObtenerTotal(consulta, municipio);
        }

        public List<Resultados> obtenerResultadoFolioReal(String folioReal, String municipio, int pag)
        {
            int max = numFolioreal(folioReal, municipio);
            String consulta = "select NUM,"
               + " CVE_CAT_EST,"
               + " CVE_CAT_ORI,"
               + " clavePredial,"
               + " NOMBRE_O_RAZON_SOCIAL,"
               + " APELLIDO_PATERNO,"
               + " APELLIDO_MATERNO,"
               + " NOM_LOCALIDAD,"
               + " NOMBRE_COMPLETO_ASENTAMIENTO,"
               + " NOMBRE_COMPLETO_VIALIDAD,"
               + " NUMERO_EXTERIOR "
               + " FROM "
               + "(select pu.CVE_CAT_EST,"
               + " pu.CVE_CAT_ORI,"
               + " pp.cve_predial clavePredial,"
               + " p.NOMBRE_O_RAZON_SOCIAL,"
               + " p.APELLIDO_PATERNO,"
               + " p.APELLIDO_MATERNO,"
               + " pu.NOM_LOCALIDAD,"
               + " pu.NOMBRE_COMPLETO_ASENTAMIENTO,"
               + " pu.NOMBRE_COMPLETO_VIALIDAD,"
               + " pu.NUMERO_EXTERIOR ,"
               + " ROW_NUMBER() OVER (ORDER BY pu.CVE_CAT_EST ) AS NUM "
               + " from sde.sis_pc_clave_catastral pp "
               + " left join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_EST = pp.cve_cat_est and pu.STATUSREGISTROTABLA = 'ACTIVO'"
               + " left join sde.SIS_PC_PROPIETARIOS p on p.OBJECTID=(select max (OBJECTID)from sde.SIS_PC_PROPIETARIOS where STATUSREGISTROTABLA='ACTIVO' and CVE_CAT_ORI=pp.CVE_CAT_ORI)"
               + " where pp.FOLIO_REAL='" + @folioReal + "' and pp.STATUSREGISTROTABLA = 'ACTIVO') as a"
               + " WHERE NUM BETWEEN " + ((pag * max) + 1) + " AND " + (((pag + 1) * max)) + ";";

            return repositorio.ObtenerLista(consulta, municipio);
        }
        public List<Resultados> obtenerResultadoUbicacionPredio(UbicacionPredio ubicacionPredio, int pag)
        {                                    
            Utilidades utilidades = new Utilidades();
            String sql = utilidades.formarSQLtotal(ubicacionPredio);
            //int totalRegistros = numDireccion(sql, ubicacionPredio.municipio);
            String condicion = utilidades.formarSQL(ubicacionPredio);
            String consulta = "";
            int max = 10;
            
            if (String.IsNullOrEmpty(condicion) == false)
            {
                consulta = "select NUM,"
                          + " CVE_CAT_EST, "
                          + "CVE_CAT_ORI,"
                          + " clavePredial,"
                          + " NOMBRE_O_RAZON_SOCIAL, "
                          + "APELLIDO_PATERNO,"
                          + " APELLIDO_MATERNO, "
                          + "NOM_LOCALIDAD, "
                          + "NOMBRE_COMPLETO_ASENTAMIENTO, "
                          + "NOMBRE_COMPLETO_VIALIDAD, "
                          + "NUMERO_EXTERIOR "
                          + "from ("
                          + "select pu.CVE_CAT_EST, pu.CVE_CAT_ORI, pp.cve_predial clavePredial, p.NOMBRE_O_RAZON_SOCIAL, p.APELLIDO_PATERNO, p.APELLIDO_MATERNO, pu.NOM_LOCALIDAD, pu.NOMBRE_COMPLETO_ASENTAMIENTO, pu.NOMBRE_COMPLETO_VIALIDAD, pu.NUMERO_EXTERIOR, ROW_NUMBER() OVER (ORDER BY pu.CVE_CAT_EST ) AS NUM "
                          + "	from sde.sis_pc_clave_catastral pp left join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_EST = pp.CVE_CAT_EST left join sde.SIS_PC_PROPIETARIOS p on p.CVE_CAT_EST = pp.CVE_CAT_EST "
                          + "	Where pp.STATUSREGISTROTABLA = 'ACTIVO' AND pu.STATUSREGISTROTABLA = 'ACTIVO' and P.STATUSREGISTROTABLA='ACTIVO' AND " + condicion + " "
                          + ") as a "
                          + "WHERE NUM BETWEEN " + ((pag * max) + 1) + " AND " + (((pag + 1) * max)) + "";
            }
            return repositorio.ObtenerLista(consulta, ubicacionPredio.municipio);
        }

        public int numDireccion(String sql, String municipio ) 
        {
            String consulta = "select  count(*) total "
                    + "from sde.sis_pc_clave_catastral pp inner join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_EST = pp.CVE_CAT_EST inner join sde.SIS_PC_PROPIETARIOS p on p.CVE_CAT_EST = pp.CVE_CAT_EST "
                    + " where pp.STATUSREGISTROTABLA = 'ACTIVO' and pu.STATUSREGISTROTABLA = 'ACTIVO' AND P.STATUSREGISTROTABLA='ACTIVO' AND " + sql;
            return repositorio.ObtenerTotal(consulta, municipio);
        }

    }

}