using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Models.Entidades
{
    public class ClaveCatastral
    {
        public String entidadCE { get; set; } //request.getParameter("campoEntidad");
        public String regionCE { get; set; } //request.getParameter("campoRegion");                                            
        public String municipioCE { get; set; }//request.getParameter("campoMunicipio");/
        public String zonaCE { get; set; } //request.getParameter("campoZona");                                         
        public String localidadCE { get; set; }//request.getParameter("campoLocalidad");                                                 
        public String sectorCE { get; set; } //request.getParameter("campoSector");
        public String manzanaCE { get; set; } //request.getParameter("campoManzana");
        public String predioCE { get; set; } //request.getParameter("campoPredio");                                            
        public String edificioCE { get; set; } //request.getParameter("campoEdificio");                                               
        public String unidadCE { get; set; }// request.getParameter("campoUnidad");                                            
    }
}