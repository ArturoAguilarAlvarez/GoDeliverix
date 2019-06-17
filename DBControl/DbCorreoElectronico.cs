using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DBControl
{
    public class DbCorreoElectronico
    {
        Conexion oConexion;
        /// <summary>
        /// Elmina los correos asociados a la empresa
        /// </summary>
        /// <param name="uidempresa"></param>
        public void EliminaCorreosDeEmpresa(string uidempresa)
        {
            oConexion = new Conexion();
            string query = "delete from CorreoElectronico where idcorreo = (select idcorreo from CorreoEmpresa where idempresa = '" + uidempresa + "'); delete from correoempresa where idempresa = '" + uidempresa + "'";
            oConexion.Consultas(query);
        }
        /// <summary>
        /// Elimina los correos asociados al usuario
        /// </summary>
        /// <param name="uidUsuario"></param>
        public void EliminarCorreosUsuario(string uidUsuario)
        {
            oConexion = new Conexion();
            string query = "delete from CorreoElectronico where idcorreo = (select UidCorreo from Correousuario where UidUsuario = '" + uidUsuario + "'); delete from Correousuario where UidUsuario = '" + uidUsuario + "'";
            oConexion.Consultas(query);
        }
    }
}
