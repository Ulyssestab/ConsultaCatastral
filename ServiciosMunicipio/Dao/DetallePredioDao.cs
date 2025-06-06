using ServiciosMunicipio.Models.Entidades;
using ServiciosMunicipio.Repositorio.Impl;
using ServiciosMunicipio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosMunicipio.Dao
{
    public class DetallePredioDao
    {
        RepositorioDetallePredioImp repositorio = new RepositorioDetallePredioImp();
        public DetallePredio getDetallePredio(String clave, String municipio)
        {
            clave = Utilerias.AntiInjectionSQL.quitarComillas(clave, Constantes.LONG_MAX_NOM);
            String cv = "CVE_CAT_EST";
            String cv1 = "CVE_MAESTRO_EST";
            if (clave.Length < 31)
            {
                cv = "CVE_CAT_ORI";
                cv1 = "CVE_MAESTRO_ORI";
            }
            String consulta = "select distinct pu.CVE_CAT_EST, "
                + "pu.CVE_CAT_ORI, "
                + "p.cve_predial clavePredial, "
                + "pu.NOM_MUNICIPIO, "
                + "pu.NOM_LOCALIDAD, "
                + "Domicilio_Notificaion= CASE WHEN ISNULL(noti.DESCRIPCION_UBICACION,'')='' THEN"
                + "+isnull(noti.NOMBRE_COMPLETO_ASENTAMIENTO,'')+' '"
                + "+(case when isnull(noti.NUMERO_EXTERIOR,'0')='0' or noti.NUMERO_EXTERIOR='' then ' ' else ' # '+ noti.NUMERO_EXTERIOR end)"
                + "+(case when isnull(noti.NUMERO_INTERIOR,'0')='0' or noti.NUMERO_INTERIOR='' then ' ' else ' Int '+ noti.NUMERO_INTERIOR  end)"
                + "+(case when isnull(noti.MANZANA,'')='' then ' ' else ' MANZANA '+ noti.MANZANA + '' end)"
                + "+(case when isnull(noti.LOTE,'')='' then ' ' else ' LOTE  '+ noti.LOTE + ''  end)"
                + "+(case when isnull(noti.CP,'')='' then ' ' else ' CP '+noti.CP + '' end)+' '+"
                + "+isnull(noti.NOM_LOCALIDAD,'')+' '+isnull(noti.NOM_MUNICIPIO,'')+' '+isnull(noti.NOM_ENTIDAD,'') ELSE noti.DESCRIPCION_UBICACION END,"
                + "isnull(p.FOLIO_REAL,'No Existe Folio Real')as FOLIO_REAL, "
                + "pu.NOMBRE_COMPLETO_ASENTAMIENTO, "
                + "pu.NOMBRE_COMPLETO_VIALIDAD, "
                + "pu.NUMERO_EXTERIOR, "
                + "CONVERT(VARCHAR, CONVERT(money,se.VALOR_CATASTRAL), 5)  as VALOR_CATASTRAL, "
                + "CASE WHEN se.TIPO_ELEMENTO='UNIDAD CONDOMINAL' then papa.VALOR_UNITARIO_TERRENO else se.VALOR_UNITARIO_TERRENO end as Valor_Unitario_Terreno, "
                + "sp.NOMBRE_O_RAZON_SOCIAL, "
                + "sp.APELLIDO_PATERNO, "
                + "sp.APELLIDO_MATERNO, "
                + "convert(numeric(18,4),round(se.SUP_TERR_PRIV,4,1)) as SUP_TERRENO_ESCRITURAS, "
                + "convert(numeric(18,2),round(se.SUP_CONS_TOTAL,2,1)) as SUP_CONSTRUCCION "
                + "from sde.sis_pc_clave_catastral p "
                + "left join sde.SIS_PC_UBICACION pu on p." + cv + " = pu." + cv + " and pu.statusregistrotabla='ACTIVO' "
                + "left join sde.SIS_PC_SUPERFICIES2 se on se." + cv + " = p." + cv + " and se.statusregistrotabla='ACTIVO' "
                + "left join sde.SIS_PC_NOTIFICACION noti on noti." + cv + "=p." + cv + " and noti.STATUSREGISTROTABLA='ACTIVO' "
                + "left join sde.SIS_PC_PROPIETARIOS sp on sp.OBJECTID=(select max (OBJECTID)from sde.SIS_PC_PROPIETARIOS where STATUSREGISTROTABLA='ACTIVO' and CVE_CAT_ORI=p.CVE_CAT_ORI) "
                + "left outer join sde.SIS_PC_SUPERFICIES2 papa on papa." + cv + "=se." + cv1 + " and papa.STATUSREGISTROTABLA='ACTIVO' and papa.TIPO_ELEMENTO='BASE CONDOMINAL' "
                + "where p." + cv + " = '" + clave + "' and p.STATUSREGISTROTABLA='ACTIVO' ";
            return repositorio.ObtenerElemento(consulta, municipio);
        }

        public List<Tramite> obtenerTramites(String claveCatEst, String municipio) 
        {
            String consulta = "SELECT " 
                + "distinct " 
                + "t.NumeroTramite, " 
                + "t.Habilitado estatus," 
                + "ctpt.Nombre NombreTramite " 
                + "FROM dbo.Tramite t inner join dbo.SeguimientoTramite st " 
                + "on st.FK_NumeroTramite = t.NumeroTramite inner join dbo.Cat_EstatusTramite cet " 
                + "on cet.id = st.FK_Cat_EstatusTramite inner join dbo.Cat_TipodeProcesodeTramite ctpt " 
                + "on ctpt.id = t.FK_Cat_TipodeProcesoTramite " 
                + "where isnull(t.ClaveCatastralEstandar,'')!='' and t.ClaveCatastralEstandar = '" + claveCatEst + "' and t.Habilitado = 'true' " 
                + "order by t.NumeroTramite";
            return repositorio.obtenerTramites(consulta, municipio);
        }
    }
}
