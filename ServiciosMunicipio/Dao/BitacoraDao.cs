using log4net;
using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Dao
{
    public class BitacoraDao
    {
        private WFTRAMITESEntities db = new WFTRAMITESEntities();
        private static readonly ILog log = LogManager.GetLogger(typeof(BitacoraDao));
        public string insertarDatos(BitacoraAccesoSistemas bitacora)
        {
            String respuesta = "OK";

            if (bitacora != null && bitacora.ID == 0 && bitacora.ALTAREGISTROTABLA != null && bitacora.DESCRIPCION != null )
            {
                bitacora.USUARIOALTA = bitacora.USUARIOALTA != null ? bitacora.USUARIOALTA : "";
                bitacora.CVE_CAT_EST = bitacora.CVE_CAT_EST != null ? bitacora.CVE_CAT_EST : "";
                bitacora.CVE_CAT_ORI = bitacora.CVE_CAT_ORI != null ? bitacora.CVE_CAT_ORI : "";
                bitacora.APELLIDO_MATERNO = bitacora.APELLIDO_MATERNO != null ? bitacora.APELLIDO_MATERNO : "";
                bitacora.APELLIDO_PATERNO = bitacora.APELLIDO_PATERNO != null ? bitacora.APELLIDO_PATERNO : "";
                bitacora.NOMBRE_O_RAZON_SOCIAL = bitacora.NOMBRE_O_RAZON_SOCIAL != null ? bitacora.NOMBRE_O_RAZON_SOCIAL : "";

                var bitacoraAcceso = db.Set<BitacoraAccesoSistemas>();
                try
                {
                    bitacoraAcceso.Add(bitacora);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    log.Error("Error al convertir", e);
                    return "ERROR";
                }

            }
            else 
            {
                respuesta = "ERROR";
            }
            return respuesta;
        }

        public String guardarRegistro(String claveCatastralEstandar, String claveCatastralOriginal, String tipo, String ip, String terminal, String url, String usuario , String nombre, String apP, String apM){
            DateTime d = DateTime.Now;
            DateTime fechaActual = DateTime.Parse(d.Year + "-" + d.Month + "-" + d.Day + " " + d.Hour + ":" + d.Minute + ":" + d.Second);
            BitacoraAccesoSistemas bitacora = new BitacoraAccesoSistemas()
            {
                ALTAREGISTROTABLA = fechaActual,
                APELLIDO_MATERNO = apM,
                APELLIDO_PATERNO = apP,
                APLICATIVO = "Consulta Catastral",
                CVE_CAT_EST = claveCatastralEstandar,
                CVE_CAT_ORI = claveCatastralOriginal,
                DESCRIPCION = "Realizo busqueda por " + tipo,
                ID = 0,
                IP_ALTA = ip,
                NOMBRE_O_RAZON_SOCIAL = nombre,
                TERMINALALTA = terminal,
                URL = url,
                USUARIOALTA = usuario
            };
            return insertarDatos(bitacora);
        }
    }
}