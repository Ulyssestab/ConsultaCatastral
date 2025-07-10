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
                    return "ERROR";
                }

            }
            else 
            {
                respuesta = "ERROR";
            }
            return respuesta;
        }
    }
}