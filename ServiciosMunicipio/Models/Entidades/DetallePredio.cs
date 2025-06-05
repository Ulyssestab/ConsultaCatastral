using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Models.Entidades
{
    public class DetallePredio
    {
        public String CVE_CAT_EST { get; set; }
        public String CVE_CAT_ORI { get; set; }
        public String clavePredial { get; set; }
        public String NOMBRE_COMPLETO_VIALIDAD { get; set; }
        public String NUMERO_EXTERIOR { get; set; }
        public String NOMBRE_COMPLETO_ASENTAMIENTO { get; set; }
        public String NOM_LOCALIDAD { get; set; }
        public String NOM_MUNICIPIO { get; set; }
        public String NOMBRE_O_RAZON_SOCIAL { get; set; }
        public String APELLIDO_PATERNO { get; set; }
        public String APELLIDO_MATERNO { get; set; }
        public String VALOR_CATASTRAL { get; set; }
        public String SUP_TERRENO_ESCRITURAS { get; set; }
        public String SUP_CONSTRUCCION { get; set; }
        public String FOLIO_REAL { get; set; }
        public String VALOR_UNITARIO_TERRENO { get; set; }
        public String Domicilio_Notificaion { get; set; }
    }
}