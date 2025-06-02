using ServiciosMunicipio.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Utilerias
{
    public class Utilidades
    {
        public String completarCeros(int num, String cad) 
        {
            if (cad == null || "".Equals(cad))
            {
                return "";
            }
            else
            {
                int numero;
                try
                {
                    numero = Int32.Parse(cad);
                    if (num > cad.Length)
                    {
                        int n = (getMiles(num) + numero);
                        cad = n + "";
                        return cad.Substring(1, cad.Length);
                    }
                    else
                    {
                        return cad;
                    }
                }
                catch (NotFiniteNumberException e)
                {
                    return "";
                }
            }
        }

        public static int getMiles(int num)
        {
            switch (num)
            {
                case 1: return 10;
                case 2: return 100;
                case 3: return 1000;
                case 4: return 10000;
                case 5: return 100000;
                case 6: return 1000000;
                case 7: return 10000000;
            }
            return 1;
        }

        public String formarSQL(UbicacionPredio ubicacionPredio) 
        {                        
            String municipio = AntiInjectionSQL.quitarComillas(ubicacionPredio.municipio, Constantes.LONG_MAX_LOC);
            String sql = "";            
            int count_parameter = 0;
            String cond = "";
            Utilidades utilidades = new Utilidades();
            if (String.IsNullOrEmpty(municipio) == false)
            {
                sql = Catalogos.formarCondicion("", "CVE_MUNICIPIO", municipio, cond, 2);
                count_parameter++;
            }
            /*************************************CLAVE LOCALIDAD**************************************************/
            if (String.IsNullOrEmpty(ubicacionPredio.cve_asentamiento) == false)
            {
                String cve_localidad = utilidades.completarCeros(4, ubicacionPredio.clave_localidad);

                if (String.IsNullOrEmpty(sql))
                {
                    sql = (Catalogos.formarCondicion("", "CVE_LOCALIDAD", cve_localidad, cond, 2));
                }
                else
                {
                    sql = (Catalogos.formarCondicion((sql + " and "), "CVE_LOCALIDAD", cve_localidad, cond, 2));
                }
                count_parameter++;
            }
            /*************************************NOMBRE LOCALIDAD * *************************************************/
            if (String.IsNullOrEmpty(ubicacionPredio.localidad) == false)
            {
                if (String.IsNullOrEmpty(sql))
                {
                    sql = Catalogos.formarCondicion("", "NOM_LOCALIDAD", ubicacionPredio.localidad, cond, 2);
                }
                else
                {
                    sql = (Catalogos.formarCondicion(sql + " and ", "NOM_LOCALIDAD", ubicacionPredio.localidad, cond, 2));
                }
                count_parameter++;
            }
            /*************************************CLAVE ASENTAMIENTO**************************************************/
            if (String.IsNullOrEmpty(ubicacionPredio.cve_asentamiento) == false)
            {
                String cve_asentamiento = utilidades.completarCeros(5, ubicacionPredio.cve_asentamiento);
                if (String.IsNullOrEmpty(sql))
                {
                    sql = (Catalogos.formarCondicion("", "CVE_ASENTAMIENTO", cve_asentamiento, cond, 0));
                }
                else
                {
                    sql = (Catalogos.formarCondicion((sql + " and "), "CVE_ASENTAMIENTO", cve_asentamiento, cond, 0));
                }
                count_parameter++;
            }            
            /*************************************ASENTAMIENTO**************************************************/
            if (String.IsNullOrEmpty(ubicacionPredio.asentamiento) == false)
            {
                if (String.IsNullOrEmpty(sql))
                {
                    sql = Catalogos.formarCondicion("", "NOMBRE_ASENTAMIENTO", ubicacionPredio.asentamiento, cond, 0);
                }
                else
                {
                    sql = " and " + (Catalogos.formarCondicion(sql, "NOMBRE_ASENTAMIENTO", ubicacionPredio.asentamiento, cond, 0));
                }
                count_parameter++;
            }
            /******************************Numero Exterior ********/
            if (String.IsNullOrEmpty(ubicacionPredio.numExt) == false)
            {
                sql = Catalogos.formarCondicion(sql, " AND NUMERO_EXTERIOR", ubicacionPredio.numExt, cond, 2);
                count_parameter++;
            }
            if (String.IsNullOrEmpty(ubicacionPredio.calle) == false)
            {
                sql = Catalogos.formarCondicion(sql, " NOMBRE_COMPLETO_VIALIDAD", ubicacionPredio.calle.ToUpper(), " AND ", 0);
                count_parameter++;
            }            

            return sql;

        }

        public string formarSQLtotal(UbicacionPredio ubicacionPredio) 
        {
            String sql = "";
            String cond = "";
            Utilidades utilidades = new Utilidades();
            if (String.IsNullOrEmpty(ubicacionPredio.municipio) == false)
            {
                sql = Catalogos.formarCondicion("", "CVE_MUNICIPIO", ubicacionPredio.municipio, cond, 2);
            }

            /*************************************CLAVE LOCALIDAD**************************************************/
            if (String.IsNullOrEmpty(ubicacionPredio.clave_localidad) == false)
            {
                String cve_localidad = utilidades.completarCeros(4, ubicacionPredio.clave_localidad);

                if (String.IsNullOrEmpty(sql))
                {
                    sql = (Catalogos.formarCondicion("", "CVE_LOCALIDAD", cve_localidad, cond, 2));
                }
                else
                {
                    sql = (Catalogos.formarCondicion((sql + " and "), "CVE_LOCALIDAD", cve_localidad, cond, 2));
                }
            }
            /*************************************NOMBRE LOCALIDAD**************************************************/
            if (String.IsNullOrEmpty(ubicacionPredio.localidad)== false)
            {
                if (String.IsNullOrEmpty(sql))
                {
                    sql = Catalogos.formarCondicion("", "NOM_LOCALIDAD", ubicacionPredio.localidad, cond, 2);
                }
                else
                {
                    sql =  (Catalogos.formarCondicion(sql + " and ", "NOM_LOCALIDAD", ubicacionPredio.localidad, cond, 2));
                }
            }
            /*************************************CLAVE ASENTAMIENTO**************************************************/
            if (String.IsNullOrEmpty(ubicacionPredio.cve_asentamiento) == false)
            {
                String cve_asentamiento = utilidades.completarCeros(5, ubicacionPredio.cve_asentamiento);
                if (String.IsNullOrEmpty(sql))
                {
                    sql = (Catalogos.formarCondicion("", "CVE_ASENTAMIENTO", cve_asentamiento, cond, 2));
                }
                else
                {
                    sql = (Catalogos.formarCondicion((sql + " and "), "CVE_ASENTAMIENTO", cve_asentamiento, cond, 2));
                }
            }
            /*************************************ASENTAMIENTO**************************************************/
            if (String.IsNullOrEmpty(ubicacionPredio.asentamiento) == false)
            {
                if (String.IsNullOrEmpty(sql))
                {
                    sql = Catalogos.formarCondicion("", "NOMBRE_ASENTAMIENTO", ubicacionPredio.asentamiento, cond, 1);
                }
                else
                {
                    sql = " and " + (Catalogos.formarCondicion(sql, "NOMBRE_ASENTAMIENTO", ubicacionPredio.asentamiento, cond, 1));
                }
            }
            /******************************Numero Exterior **************************************************************/
            if (String.IsNullOrEmpty(ubicacionPredio.numExt) == false)
            {
                sql = Catalogos.formarCondicion(sql, " AND NUMERO_EXTERIOR", ubicacionPredio.numExt, cond, 2);
            }
            /************************************CALLE************************************************************************/
            if (String.IsNullOrEmpty(ubicacionPredio.calle) == false)
            {
                sql = Catalogos.formarCondicion(sql, " NOMBRE_COMPLETO_VIALIDAD", ubicacionPredio.calle.ToUpper(), " AND ", 0);

            }

            return sql;
        }
    }
}