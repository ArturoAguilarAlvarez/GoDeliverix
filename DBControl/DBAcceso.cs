using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DBControl
{
    public class DBAcceso
    {
        #region Propiedades
        Conexion oConexcion;
        #endregion
        #region Metodos
        public DataTable IngresoAlSitio(string USUARIO, string PASSWORD, string correoElectronico = "")
        {
            oConexcion = new Conexion();
            //Crea la variable de acceso que define el resultado del registro a buscar
            string query = $" select u.UidUsuario from Usuarios u inner join CorreoUsuario cu on cu.UidUsuario = u.UidUsuario " +
                $"inner join CorreoElectronico ce on ce.IdCorreo = cu.UidCorreo where('{USUARIO}' != '' and Usuario = '{USUARIO}' or('{USUARIO}' = '')) and" +
                $" (('{correoElectronico}' != '' and ce.Correo = '{correoElectronico}')or('{correoElectronico}' = '')) and Contrasena = '{PASSWORD}' and u.ESTATUS =1";
            return oConexcion.Consultas(query);
        }
        public DataTable EstatusUsuario(string UidUsuario)
        {
            oConexcion = new Conexion();
            string query = "  select UidUsuario from Usuarios where UidUsuario = '" + UidUsuario + "'and estatus = 1";
            return oConexcion.Consultas(query);
        }
        public DataTable NombreDeUsuario(Guid id)
        {
            oConexcion = new Conexion();
            string sentencia = "select Usuario from usuarios where UidUsuario = '" + id + "'";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable PerfilDeUsuario(Guid Id)
        {
            oConexcion = new Conexion();
            string sentencia = "select UidPerfil from usuarios where UidUsuario = '" + Id + "'";
            return oConexcion.Consultas(sentencia);
        }
        public DataTable RecuperarUsuarioPorEmail(string email)
        {
            oConexcion = new Conexion();
            string query = "select u.UidUsuario,u.Nombre, u.contrasena,u.Usuario,u.ApellidoPaterno,u.ApellidoMaterno,CE.Correo from CorreoElectronico CE inner join CorreoUsuario CU on CU.UidCorreo = CE.IdCorreo inner join Usuarios u on u.UidUsuario = CU.UidUsuario where CE.Correo ='" + email + "'";
            return oConexcion.Consultas(query);
        }

        public DataTable ObtenerUltimoEstatusRepartidor(Guid uidUsuario)
        {
            oConexcion = new Conexion();
            string query = "select top 1 uidestatusrepartidor from BitacoraEstatusRepartidor where uidusuario = '" + uidUsuario.ToString() + "' order by DtmFecha desc";
            return oConexcion.Consultas(query);
        }
        #endregion
    }
}
