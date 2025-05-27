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
    }
}