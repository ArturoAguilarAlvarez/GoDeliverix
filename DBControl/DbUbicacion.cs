using System;
using System.Data;

namespace DBControl
{
    public class DbUbicacion
    {
        #region Propiedades
        Conexion oConexion;
        #endregion
        #region Metodos
        public DataTable UbicacionSucursal(string UidSucursal)
        {
            oConexion = new Conexion();
            string query = "select UidUbicacion,VchLatitud, VchLongitud from Ubicacion where UidUbicacion in (select UidUbicacion from UbicacionSucursal where UidSucursal = '" + UidSucursal + "')";
            return oConexion.Consultas(query);
        }

        public void EliminarUbicacionSucursal(Guid uidSucursal)
        {
            oConexion = new Conexion();
            string query = "delete from Ubicacion where UidUbicacion in (select UidUbicacion from UbicacionSucursal where UidSucursal = '" + uidSucursal.ToString() + "');delete from ubicacionsucursal where uidsucursal ='" + uidSucursal.ToString() + "'";
            oConexion.Consultas(query);
        }

        public void EliminarUbicacionDireccion(Guid uidDireccion)
        {
            oConexion = new Conexion();
            string query = "delete from Ubicacion where UidUbicacion in (select UidUbicacion from DireccionUbicacion where UidDireccion = '" + uidDireccion.ToString() + "');delete from DireccionUbicacion where UidDireccion ='" + uidDireccion.ToString() + "'";
            oConexion.Consultas(query);
        }

        public DataTable UbicacionDireccion(string uidDireccion)
        {
            oConexion = new Conexion();
            string query = "select UidUbicacion,VchLatitud, VchLongitud from Ubicacion where UidUbicacion in (select UidUbicacion from DireccionUbicacion where UidDireccion = '" + uidDireccion + "')";
            return oConexion.Consultas(query);
        }
        #endregion
    }
}
