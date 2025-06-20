using ServiciosMunicipio.Conexion;
using ServiciosMunicipio.Models;
using ServiciosMunicipio.Models.Entidades;
using ServiciosMunicipio.Repositorio.Impl;
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
        public int numClavesEstandar(String clave, String municipio) //numClavesEstandar
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
        public List<Resultados> obtenerClavesEstandar(String clave, String municipio, int pag) //getClavesEstandar

        {
            int max = numClavesEstandar(clave, municipio);

            if (max == 0) {
                return new List<Resultados>();
            }
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
        public List<Resultados> obtenerClavesPredial(ClavePredial clavePredial) //getClavesPrediales
        {
            String clavePredialNum = clavePredial.claveCuentaPredial;
            String numCuentaPredial = clavePredial.numCuentaPredial;
            String municipioCE = clavePredial.municipioCE; 
            int pag = clavePredial.offset;

            int max = numClavePredial(clavePredialNum, numCuentaPredial, municipioCE);
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
                                + "where pp.cve_Predial like '" + @clavePredialNum + @numCuentaPredial + "%' and pp.STATUSREGISTROTABLA = 'ACTIVO') as a " 
                                + "WHERE NUM BETWEEN " + ((pag * max) + 1) + " AND " + (((pag + 1) * max)) + ";";
            return repositorio.ObtenerLista(consulta, municipioCE);
        }
        public int numFolioreal(string folioReal, string municipio) //numFolioReal
        {
            String consulta = "select count(*) total"
                + " from sde.SIS_PC_CLAVE_CATASTRAL"
                + " WHERE STATUSREGISTROTABLA = 'ACTIVO'"
                + " AND FOLIO_REAL = '" + @folioReal + "'";
            return repositorio.ObtenerTotal(consulta, municipio);
        }

        public List<Resultados> obtenerResultadoFolioReal(String folioReal, String municipio, int pag) //getFolioReal
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
        public List<Resultados> obtenerResultadoUbicacionPredio(UbicacionPredio ubicacionPredio, int pag) //getDirecciones
        {                                    
            Utilidades utilidades = new Utilidades();
            //String sql = utilidades.formarSQLtotal(ubicacionPredio);
            //int totalRegistros = numDireccion(sql, ubicacionPredio.municipio);
            String condicion = utilidades.formarConsultaUbicacionXpredio(ubicacionPredio, Constantes.XPREDIO);
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
                          + "from sde.sis_pc_clave_catastral pp left join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_EST = pp.CVE_CAT_EST left join sde.SIS_PC_PROPIETARIOS p on p.CVE_CAT_EST = pp.CVE_CAT_EST "
                          + "Where pp.STATUSREGISTROTABLA = 'ACTIVO' AND pu.STATUSREGISTROTABLA = 'ACTIVO' and P.STATUSREGISTROTABLA='ACTIVO' AND " + condicion + " "
                          + ") as a "
                          + "WHERE NUM BETWEEN " + ((pag * max) + 1) + " AND " + (((pag + 1) * max)) + "";
            }
            return repositorio.ObtenerLista(consulta, ubicacionPredio.municipio);
        }

        public int numDireccion(String sql, String municipio) //numDireccion
        {
            String consulta = "select  count(*) total "
                    + "from sde.sis_pc_clave_catastral pp inner join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_EST = pp.CVE_CAT_EST inner join sde.SIS_PC_PROPIETARIOS p on p.CVE_CAT_EST = pp.CVE_CAT_EST "
                    + " where pp.STATUSREGISTROTABLA = 'ACTIVO' and pu.STATUSREGISTROTABLA = 'ACTIVO' AND P.STATUSREGISTROTABLA='ACTIVO' AND " + sql;
            return repositorio.ObtenerTotal(consulta, municipio);
        }

        public int numClavesOriginales(String claveCatastralOriginal) //numClavesOriginales
        {            
            claveCatastralOriginal = AntiInjectionSQL.quitarComillas(claveCatastralOriginal, Constantes.LONG_MAX_NOM);
            String municipio = Utilidades.getMunicipioCve_Ori(claveCatastralOriginal);
                        
            //Defino la consulta a realizar
            String consulta = "select count(*) total "
                    + "from sde.sis_pc_clave_catastral pp "
                    + "left join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_ORI = pp.CVE_CAT_ORI and pu.STATUSREGISTROTABLA = 'ACTIVO' "
                    + "left join sde.SIS_PC_PROPIETARIOS p on p.CVE_CAT_ORI = pp.CVE_CAT_ORI " 
                    + "and p.STATUSREGISTROTABLA = 'ACTIVO' and p.TIPO_PROPIETARIO = 'PROPIETARIO' "
                    + "where pp.CVE_CAT_ORI like '" + claveCatastralOriginal + "%' and pp.STATUSREGISTROTABLA = 'ACTIVO'";            
            return repositorio.ObtenerTotal(consulta, municipio); 
        }

        public List<Resultados> obtenerClavesOriginal(String claveCatastralOriginal, int pag, int offset) //getClavesOriginales
        {
            int max = offset;
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
                + " from ("
                + " select pu.CVE_CAT_EST,"
                + " pu.CVE_CAT_ORI,"
                + " pp.cve_predial clavePredial,"
                + " p.NOMBRE_O_RAZON_SOCIAL,"
                + " p.APELLIDO_PATERNO,"
                + " p.APELLIDO_MATERNO,"
                + " pu.NOM_LOCALIDAD,"
                + " pu.NOMBRE_COMPLETO_ASENTAMIENTO,"
                + " pu.NOMBRE_COMPLETO_VIALIDAD,"
                + " pu.NUMERO_EXTERIOR,"
                + " ROW_NUMBER() OVER (ORDER BY pu.CVE_CAT_EST ) AS NUM "
                + " from sde.sis_pc_clave_catastral pp"
                + " left join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_ORI = pp.CVE_CAT_ORI and pu.STATUSREGISTROTABLA='ACTIVO'"
                + " left join sde.SIS_PC_PROPIETARIOS p on p.OBJECTID=(select max (OBJECTID)from sde.SIS_PC_PROPIETARIOS where STATUSREGISTROTABLA='ACTIVO'" 
                + " and CVE_CAT_ORI=pp.CVE_CAT_ORI)" 
                + "	where  pp.CVE_CAT_ORI like '" + claveCatastralOriginal + "%' and  pp.STATUSREGISTROTABLA = 'ACTIVO') as a" 
                + " WHERE NUM BETWEEN " + ((pag * max) + 1) + " AND " + (((pag + 1) * max)) + ";"; 
            String municipio = Utilidades.getMunicipioCve_Ori(claveCatastralOriginal);
            return repositorio.ObtenerLista(consulta, municipio);
        }

        public int numPredioxPersonaFisica(PersonaFisica personaFisica) //numPredioxPersonaFisica
        {
            String nombre = AntiInjectionSQL.quitarComillas(personaFisica.nombre.ToUpper(), Constantes.LONG_MAX_NOM);
            String apaterno = AntiInjectionSQL.quitarComillas(personaFisica.apaterno.ToUpper(), Constantes.LONG_MAX_NOM);
            String amaterno = AntiInjectionSQL.quitarComillas(personaFisica.amaterno.ToUpper(), Constantes.LONG_MAX_NOM);
            String municipio = AntiInjectionSQL.quitarComillas(personaFisica.municipio, Constantes.LONG_MAX_NOM);
            
            //Defino la consulta a realizar
            String consulta = "select count(*) total "
                    + "from sde.SIS_PC_PROPIETARIOS p "
                    + "left join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_ORI = p.CVE_CAT_ORI "
                    + "left join sde.sis_pc_clave_catastral pp on pp.CVE_CAT_ORI = p.CVE_CAT_ORI "
                    + "where pp.STATUSREGISTROTABLA = 'ACTIVO' and p.tipo_persona != 'moral' and p.STATUSREGISTROTABLA = 'ACTIVO' " 
                    + "AND pu.STATUSREGISTROTABLA = 'ACTIVO' " 
                    + "and p.NOMBRE_O_RAZON_SOCIAL like '%" + nombre + "%' and p.APELLIDO_PATERNO like '%" + apaterno + "%' and p.APELLIDO_MATERNO like '%" + amaterno + "%'";
            return repositorio.ObtenerTotal(consulta, municipio);
        }
            public List<Resultados> obtenerResultadoPersonaFisica(PersonaFisica personaFisica, int pag, int max) //getPersonasFisica
        {            
            String nombre = AntiInjectionSQL.quitarComillas(personaFisica.nombre.ToUpper(), Constantes.LONG_MAX_NOM);
            String apaterno = AntiInjectionSQL.quitarComillas(personaFisica.apaterno.ToUpper(), Constantes.LONG_MAX_NOM);
            String amaterno = AntiInjectionSQL.quitarComillas(personaFisica.amaterno.ToUpper(), Constantes.LONG_MAX_NOM);
            String municipio = AntiInjectionSQL.quitarComillas(personaFisica.municipio, Constantes.LONG_MAX_NOM);                        
            //Defino la consulta a realizar            
            String consulta = "select NUM, CVE_CAT_EST, CVE_CAT_ORI, clavePredial, NOMBRE_O_RAZON_SOCIAL, APELLIDO_PATERNO, APELLIDO_MATERNO, NOM_LOCALIDAD, NOMBRE_COMPLETO_ASENTAMIENTO, NOMBRE_COMPLETO_VIALIDAD, NUMERO_EXTERIOR \n" +
               "from (" 
               + "select pu.CVE_CAT_EST, pu.CVE_CAT_ORI, pp.cve_predial clavePredial, p.NOMBRE_O_RAZON_SOCIAL, p.APELLIDO_PATERNO, p.APELLIDO_MATERNO, pu.NOM_LOCALIDAD, pu.NOMBRE_COMPLETO_ASENTAMIENTO, pu.NOMBRE_COMPLETO_VIALIDAD, pu.NUMERO_EXTERIOR, ROW_NUMBER() OVER (ORDER BY pu.CVE_CAT_ORI ) AS NUM " 
               + "from sde.sis_pc_clave_catastral pp " 
               + "left join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_ORI = pp.CVE_CAT_ORI " 
               + "left join sde.SIS_PC_PROPIETARIOS p on p.CVE_CAT_ORI = pp.CVE_CAT_ORI " 
               + "where pp.STATUSREGISTROTABLA = 'ACTIVO' and p.tipo_persona = 'FISICA' and pu.STATUSREGISTROTABLA = 'ACTIVO' and p.STATUSREGISTROTABLA = 'ACTIVO' " 
               + "and RTRIM(LTRIM(ISNULL(p.NOMBRE_O_RAZON_SOCIAL,''))) + ' ' + RTRIM(LTRIM(ISNULL(p.APELLIDO_PATERNO ,''))) + ' ' + RTRIM(LTRIM(ISNULL(p.APELLIDO_MATERNO,''))) " 
               + "LIKE '%" + nombre + " " + apaterno + " " + amaterno + "%' ) as a " 
               + "WHERE NUM BETWEEN " + ((pag * max) + 1) + " AND " + (((pag + 1) * max)) + ";";

            return repositorio.ObtenerLista(consulta, municipio);
        }

        public int numPredioxPersonaMoral(String personaMoral, String municipio) //numPredioxPersonaMoral
        {
            String consulta = "select count(*) total "
                 + "from sde.SIS_PC_PROPIETARIOS p "
                 + "left join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_ORI = p.CVE_CAT_ORI "
                 + "left join sde.sis_pc_clave_catastral pp on pp.CVE_CAT_ORI = p.CVE_CAT_ORI "
                 + "where pp.STATUSREGISTROTABLA = 'ACTIVO' and p.tipo_persona != 'fisica' and p.STATUSREGISTROTABLA = 'ACTIVO' " 
                 + "AND pu.STATUSREGISTROTABLA = 'ACTIVO' and p.NOMBRE_O_RAZON_SOCIAL like '%" + personaMoral + "%' ;";
            return repositorio.ObtenerTotal(consulta, municipio);
        }

        public List<Resultados> obtenerResultadoPersonaMoral(String  razonSocial, String municipio, int pag, int max) //getPersonasMoral
        {            
            razonSocial = AntiInjectionSQL.quitarComillas(razonSocial.ToUpper(), Constantes.LONG_MAX_NOM);
            municipio = AntiInjectionSQL.quitarComillas(municipio, Constantes.LONG_MAX_NOM);
            String consulta = "select NUM, CVE_CAT_EST, CVE_CAT_ORI, clavePredial, NOMBRE_O_RAZON_SOCIAL, APELLIDO_PATERNO, APELLIDO_MATERNO, NOM_LOCALIDAD, NOMBRE_COMPLETO_ASENTAMIENTO, NOMBRE_COMPLETO_VIALIDAD, NUMERO_EXTERIOR " 
                + "from ( " 
                + "select pu.CVE_CAT_EST, pu.CVE_CAT_ORI, pp.cve_predial clavePredial, p.NOMBRE_O_RAZON_SOCIAL, p.APELLIDO_PATERNO, p.APELLIDO_MATERNO, pu.NOM_LOCALIDAD, pu.NOMBRE_COMPLETO_ASENTAMIENTO, pu.NOMBRE_COMPLETO_VIALIDAD, pu.NUMERO_EXTERIOR, ROW_NUMBER() OVER (ORDER BY pu.CVE_CAT_ORI ) AS NUM "
                + "from sde.sis_pc_clave_catastral pp " 
                + "left join sde.SIS_PC_UBICACION pu on pu.CVE_CAT_ORI = pp.CVE_CAT_ORI " 
                + "left join sde.SIS_PC_PROPIETARIOS p on p.CVE_CAT_ORI = pp.CVE_CAT_ORI " 
                + "where pp.STATUSREGISTROTABLA = 'ACTIVO' and p.tipo_persona = 'moral' and pu.STATUSREGISTROTABLA = 'ACTIVO' " 
                + "and p.STATUSREGISTROTABLA = 'ACTIVO' " 
                + "and p.NOMBRE_O_RAZON_SOCIAL like '%" + @razonSocial + "%') as a " 
                + "WHERE NUM BETWEEN " + ((pag * max) + 1) + " AND " + (((pag + 1) * max)) + ";";
            return repositorio.ObtenerLista(consulta, municipio);
        }


    }

}