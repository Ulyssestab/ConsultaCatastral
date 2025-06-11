using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public Cat_Acceso crearObjetoAcceso(String json)
        {
            Cat_Acceso resultado = new Cat_Acceso();
            JObject r = JObject.Parse(json);
            foreach (var x in r)
            {
                if (x.Key == "NombreUsuario")
                {
                    resultado.NombreUsuario = x.Value + "";
                }
                if (x.Key == "Nombre_Completo")
                {
                    resultado.Nombre_Completo = x.Value + "";
                }             
            }
            return resultado;
        }

        public SIS_ASENTAMIENTOS crearObjetoAsentamiento(string v)
        {
            SIS_ASENTAMIENTOS resultado = new SIS_ASENTAMIENTOS();
            JObject r = JObject.Parse(v);
            foreach (var x in r)
            {
                if (x.Key == "OBJECTID")
                {
                    resultado.OBJECTID = x.Value + "" != "" ? Int32.Parse(x.Value + "") : 0;
                }
                if (x.Key == "CLAVEUNICA")
                {
                    resultado.CLAVEUNICA = x.Value + "" != "" ? Int32.Parse(x.Value + "") : 0;
                }
                if (x.Key == "STATUSREGISTROTABLA")
                {
                    resultado.STATUSREGISTROTABLA = x.Value + "";
                }
                if (x.Key == "ALTAREGISTROTABLA")
                {
                    resultado.ALTAREGISTROTABLA = DateTime.Parse(x.Value + "");
                }
                if (x.Key == "BAJAREGISTROTABLA")
                {
                    resultado.BAJAREGISTROTABLA = DateTime.Parse(x.Value + "");
                }
                if (x.Key == "USUARIOALTA")
                {
                    resultado.USUARIOALTA = x.Value + "";
                }
                if (x.Key == "USUARIOBAJA")
                {
                    resultado.USUARIOBAJA = x.Value + "";
                }
                if (x.Key == "TERMINALALTA")
                {
                    resultado.TERMINALALTA = x.Value + "";
                }
                if (x.Key == "TERMINALBAJA")
                {
                    resultado.TERMINALBAJA = x.Value + "";
                }
                if (x.Key == "CVE_ENTIDAD")
                {
                    resultado.CVE_ENTIDAD = x.Value + "";
                }
                if (x.Key == "CVE_REGION_CATASTRAL")
                {
                    resultado.CVE_REGION_CATASTRAL = x.Value + "";
                }
                if (x.Key == "CVE_MUNICIPIO")
                {
                    resultado.CVE_MUNICIPIO = x.Value + "";
                }
                if (x.Key == "CVE_ZONA_CATASTRAL")
                {
                    resultado.CVE_ZONA_CATASTRAL = x.Value + "";
                }
                if (x.Key == "CVE_LOCALIDAD")
                {
                    resultado.CVE_LOCALIDAD = x.Value + "";
                }
                if (x.Key == "CVE_SECTOR_CATASTRAL")
                {
                    resultado.CVE_SECTOR_CATASTRAL = x.Value + "";
                }
                if (x.Key == "CVE_ASENTAMIENTO")
                {
                    resultado.CVE_ASENTAMIENTO = x.Value + "";
                }
                if (x.Key == "TIPO_ASENTAMIENTO")
                {
                    resultado.TIPO_ASENTAMIENTO = x.Value + "";
                }
                if (x.Key == "NOMBRE_ASENTAMIENTO")
                {
                    resultado.NOMBRE_ASENTAMIENTO = x.Value + "";
                }
                if (x.Key == "CP")
                {
                    resultado.CP = x.Value + "";
                }
                if (x.Key == "NOMBRE_COMPLETO_ASENTAMIENTO")
                {
                    resultado.NOMBRE_COMPLETO_ASENTAMIENTO = x.Value + "";
                }
                if (x.Key == "OBSERVACIONES")
                {
                    resultado.OBSERVACIONES = x.Value + "";
                }
            }

            return resultado;
        }

        public Usuario crearObjetoUsuario(string json)
        {
            Usuario resultado = new Usuario();
            JObject r = JObject.Parse(json);
            foreach (var x in r)
            {
                if (x.Key == "Analista")
                {
                    resultado.Analista = x.Value + "" != "" ? false : true;
                }
                if (x.Key == "ApellidoMaterno")
                {
                    resultado.ApellidoMaterno = x.Value + "";
                }
                if (x.Key == "ApellidoPaterno")
                {
                    resultado.ApellidoPaterno = x.Value + "";
                }
                if (x.Key == "Celular")
                {
                    resultado.Celular = x.Value + "";
                }
                if (x.Key == "Contrasena")
                {
                    resultado.Contrasena = x.Value + "";
                }
                if (x.Key == "ContrasenaInicial")
                {
                    resultado.ContrasenaInicial = x.Value + "";
                }
                if (x.Key == "CorreoElectronico")
                {
                    resultado.CorreoElectronico = x.Value + "";
                }
                if (x.Key == "CP")
                {
                    resultado.Descripcion_Puesto = x.Value + "";
                }
                if (x.Key == "CP")
                {
                    resultado.Descripcion_Puesto = x.Value + "";
                }
                if (x.Key == "FechaActivacion")
                {
                    resultado.FechaActivacion = DateTime.Parse(x.Value +"");
                }
                if (x.Key == "FK_Cat_Municipio")
                {
                    resultado.FK_Cat_Municipio = x.Value + "" != "" ? Int32.Parse(x.Value + "") : 0;
                }
                if (x.Key == "FK_Coordinacion")
                {
                    resultado.FK_Coordinacion = x.Value + "" != "" ? Int32.Parse(x.Value + "") : 0;
                }
                if (x.Key == "FK_Cat_Perfil")
                {
                    resultado.FK_Cat_Perfil = x.Value + "" != "" ? Int32.Parse(x.Value + "") : 0;
                }
                if (x.Key == "FK_Puesto")
                {
                    resultado.FK_Puesto = x.Value + "" != "" ? Int32.Parse(x.Value + "") : 0;
                }
            }
            return resultado;
        }

        public Usuario parseJsonStringUsuario(String jsonString)
        {
            Usuario usuario = new Usuario();
            if (!String.IsNullOrEmpty(jsonString))
            {
                using (var stringReader = new StringReader(jsonString))
                using (var reader = new JsonTextReader(stringReader))
                {
                    reader.SupportMultipleContent = true;
                    var serializer1 = new JsonSerializer();
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            usuario = serializer1.Deserialize<Usuario>(reader);                            
                        }
                    }
                }
            }
            return usuario;

        }

        public List<SIS_ASENTAMIENTOS> parseJsonString(String jsonString)
        {
            List<SIS_ASENTAMIENTOS> objects = new List<SIS_ASENTAMIENTOS>();
            if (!String.IsNullOrEmpty(jsonString))
            {
                using (var stringReader = new StringReader(jsonString))
                using (var reader = new JsonTextReader(stringReader))
                {
                    reader.SupportMultipleContent = true;
                    var serializer1 = new JsonSerializer();
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            SIS_ASENTAMIENTOS obj = serializer1.Deserialize<SIS_ASENTAMIENTOS>(reader);
                            objects.Add(obj);
                        }
                    }
                }                
            }
            return objects;

        }
    }

}
