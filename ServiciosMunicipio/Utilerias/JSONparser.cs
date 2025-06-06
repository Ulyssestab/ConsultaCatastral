using Newtonsoft.Json.Linq;
using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Utilerias
{
    public class JSONparser
    {
        public CAT_LOCALIDAD crearObjeto(String json)
        {
            CAT_LOCALIDAD resultado = new CAT_LOCALIDAD();
            JObject r = JObject.Parse(json);
            foreach (var x in r)
            {
                if (x.Key == "OBJECTID")
                {
                    resultado.OBJECTID = Int32.Parse(x.Value +"");
                }
                if (x.Key == "CVE_ENTIDAD")
                {
                    resultado.CVE_ENTIDAD = x.Value + "";
                }
                if (x.Key == "NOM_ENTIDAD")
                {
                    resultado.NOM_ENTIDAD = x.Value + "";
                }
                if (x.Key == "CVE_MUNICIPIO")
                {
                    resultado.CVE_MUNICIPIO = x.Value + "";
                }
                if (x.Key == "NOM_MUNICIPIO")
                {
                    resultado.NOM_MUNICIPIO = x.Value + "";
                }
                if (x.Key == "CVE_LOCALIDAD")
                {
                    resultado.CVE_LOCALIDAD = x.Value + "";
                }
                if (x.Key == "NOM_LOCALIDAD")
                {
                    resultado.NOM_LOCALIDAD = x.Value + "";
                }
            }
            return resultado;
        }
    }

}
