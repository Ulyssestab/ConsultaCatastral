using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Utilerias
{
    public class Catalogos
    {
        internal static string formarCondicion(String condicion_anterior, String nombre_campo, String valor, String tipo_condicion, int tipo_campo)
        {
            String sql = "";
            //Si condicion anterior no es vacia concatenar
            if (!String.IsNullOrEmpty(condicion_anterior))
            {
                sql = condicion_anterior;
            }
            if (!String.IsNullOrEmpty(nombre_campo) && !String.IsNullOrEmpty(valor))
            {
                sql = sql + tipo_condicion;
                switch (tipo_campo)
                {
                    case 0:
                        nombre_campo = nombre_campo + " like('%" + valor + "%') ";
                        break;
                    case 1:
                        nombre_campo = nombre_campo + "=" + valor + " ";
                        break;
                    case 2:
                        nombre_campo = nombre_campo + "='" + valor + "' ";
                        break;
                }
                sql = sql + nombre_campo;
            }
            return sql;
        }
    }
}