using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DBControl
{
    public class DbTelefono
    {
        #region Propiedades

        Conexion oConexion;
        #endregion

        #region Metodos
        public DataTable ObtenerElTipoTelefono(string UidTelefono)
        {
            oConexion = new Conexion();
            string sentencia = "select Nombre from TipoDeTelefono where UidTipoDeTelefono = '" + UidTelefono + "'";
            return oConexion.Consultas(sentencia);
        }
        public DataTable ObtenerTelefonosSucursal(string id)
        {
            oConexion = new Conexion();
            string sentencia = "select UidTelefono,UidTipoDeTelefono,Numero from Telefono where UidTelefono in (select UidTelefono from TelefonoSucursal where UidSucursal = '" + id + "')";
            return oConexion.Consultas(sentencia);
        }
        /// <summary>
        /// Recibe el uid de la empresa para ejecutar el query
        /// </summary>
        /// <param name="id">Uid de la empresa</param>
        public void EliminaTelefonoEmpresa(string id)
        {
            oConexion = new Conexion();
            string sentencia = "delete from Telefono where UidTelefono in (select IdTelefono from TelefonoEmpresa where IdEmpresa ='" + id + "'); delete from TelefonoEmpresa where IdEmpresa ='" + id + "'";
            oConexion.Consultas(sentencia);
        }
        public void EliminaTelefonoSucursal(string id)
        {
            oConexion = new Conexion();
            string sentencia = "delete from Telefono where UidTelefono in (select UidTelefono from TelefonoSucursal where Uidsucursal ='" + id + "'); delete from TelefonoSucursal where Uidsucursal ='" + id + "'";
            oConexion.Consultas(sentencia);
        }

        public void EliminarTelefonoUsuario(string uidTelefono)
        {
            oConexion = new Conexion();
            string sentencia = "delete from Telefono where  UidTelefono ='" + uidTelefono + "'; delete from TelefonoUsuario where UidTelefono ='" + uidTelefono + "'";
            oConexion.Consultas(sentencia);
        }
        public DataTable ObtenerTipoDeTelefono()
        {
            oConexion = new Conexion();
            string Sentencia = "select UidTipoDeTelefono,Nombre from TipoDeTelefono";
            return oConexion.Consultas(Sentencia);
        }

        public DataTable ObtenerTelefonosEmpresa(string id)
        {
            oConexion = new Conexion();
            string query = "select UidTelefono,UidTipoDeTelefono,Numero from Telefono where UidTelefono in (select IdTelefono from TelefonoEmpresa where IdEmpresa = '" + id + "') ";
            return oConexion.Consultas(query);
        }

        public void EliminaTelefonosUsuario(string UidUsuario)
        {
            oConexion = new Conexion();
            string query = "delete from Telefono where UidTelefono in (select UidTelefono from TelefonoUsuario where uidusuario = '" + UidUsuario + "')delete from TelefonoUsuario where uidusuario ='" + UidUsuario + "' ";
            oConexion.Consultas(query);
        }
        #endregion

    }
}
