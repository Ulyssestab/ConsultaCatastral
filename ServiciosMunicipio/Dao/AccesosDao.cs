using ServiciosMunicipio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosMunicipio.Dao
{
    public class AccesosDao
    {
        private WFTRAMITESEntities db = new WFTRAMITESEntities();
        public Boolean AccesoUsuario(String nombreUsuario) 
        {                        
            return db.Cat_Acceso.SqlQuery("SELECT * FROM dbo.Cat_Acceso where NombreUsuario = '" +  @nombreUsuario + "'").First<Cat_Acceso>().NombreUsuario != null;
        }

        public String AccesoUsuarioPerfil(String nombreUsuario)
        {            
            return db.Usuario.SqlQuery("SELECT * FROM dbo.Usuario " +
                    "where NombreUsuario = '" + @nombreUsuario + "' and Habilitado = 'true'").First<Usuario>().FK_Cat_Perfil +"";
        }
        
    }
}