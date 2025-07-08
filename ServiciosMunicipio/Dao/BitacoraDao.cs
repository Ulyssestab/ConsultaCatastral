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