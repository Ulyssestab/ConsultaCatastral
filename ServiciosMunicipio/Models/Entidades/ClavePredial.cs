using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Models.Entidades
{
    public class ClavePredial
    {
        public String claveCuentaPredial { get; set; }
        public String numCuentaPredial { get; set; }
        public String municipioCE { get; set; }
        public int offset { get; set; }
    }
}