using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Models.Entidades
{
    public class SIS_PC_CENTROIDES
    {
        public int OBJECTID { get; internal set; }
        public Decimal CENT_PRED_X { get; internal set; }
        public Decimal CENT_PRED_Y { get; internal set; }
    }
}