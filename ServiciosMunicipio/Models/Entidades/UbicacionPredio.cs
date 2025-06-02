using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Models.Entidades
{
    public class UbicacionPredio
    {
        public String localidad { get; set; }
        public String clave_localidad { get; set; }
        public String cve_asentamiento { get; set; }
        public String asentamiento { get; set; }
        public String calle { get; set; }
        public String numExt { get; set; }
        public String municipio { get; set; }
    }
}