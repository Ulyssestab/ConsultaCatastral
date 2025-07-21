using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Utilerias
{
    public class AntiInjectionSQL
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AntiInjectionSQL));
        public static String quitarComillas(String cad, int longitud)
        {
             int LOC_MAX_LENGTH = longitud;            

            if (cad != null && cad.Length > 0)
            {
                cad = cad.Length > LOC_MAX_LENGTH ? cad.Substring(0, LOC_MAX_LENGTH) : cad;
                return cad.Replace("'", "");
            }
            return "";
        }
        public static Boolean esEntero(String num)
        {
            try
            {
                Int32.Parse(num);
                return true;
            }
            catch (Exception e)
            {
                log.Error("Error al guardar el registro: ", e);
                return false;
            }
        }
    }
}