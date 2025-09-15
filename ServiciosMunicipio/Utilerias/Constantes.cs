using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Utilerias
{
    public class Constantes
    {
        public const int LONG_MAX_LOC = 6;
        public const int LONG_MAX_NOM = 50;
        public const int LONG_MAX_NOM_USUARIO = 30;
        public const int LIKE = 0;
        public const int IGUAL = 1;
        public const int COMILLAS = 2;
        public const int RESULTADOS_POR_PAGINA = 10;

        public const int TOTALXPREDIO = 0;
        public const int XPREDIO = 1;

        //public const string UserID = "CatastroSA";
        //public const string Password = "CatAdmin#";
        //public const string DataSource = "10.1.2.126";
        //public const string InitialCatalogW = "WFTRAMITES";
        //public const string InitialCatalogM = "GDB01";

        //public const String HOST = "https//localhost:44330";
        //desarrollo

        //preProduccion
        public const string UserID = "usuCartografia01";
        public const string Password = "u5uC4rt0gr4";
        public const string DataSource = "10.1.111.210";
        public const string InitialCatalogW = "WFTRAMITES";
        public const string InitialCatalogM = "GDB01";
        public const String SERVICIOS_HOST = "https://geoportal.aguascalientes.gob.mx/ServiciosMunicipio";
        public const String HOST = "https://geoportal.aguascalientes.gob.mx/ConsultaCatastral";
    }
}