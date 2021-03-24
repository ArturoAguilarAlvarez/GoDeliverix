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

        public DataTable ObtenerDireccionPorColoniaEstadoPais(string colonia, string codigopostal)
        {
            oConexion = new Conexion();
            string query = $"SELECT CO.[UidColonia], CO.[Nombre] AS Colonia, REPLACE(CO.[Nombre],' ',''), CI.[UidCiudad], CI.[Nombre] AS Ciudad, MU.[UidMunicipio], MU.[Nombre] AS Municipio, ES.[UidEstado], ES.[Nombre] AS Estado, P.[UidPais], P.[Nombre] AS Pais FROM [dbo].[Colonia] AS CO INNER JOIN [dbo].[Ciudades] AS CI ON CI.[UidCiudad] = CO.[UidCiudad] INNER JOIN [dbo].[Municipios] AS MU ON MU.[UidMunicipio] = CI.[UidMunicipio] INNER JOIN [dbo].[estados] AS ES ON ES.[UidEstado] = MU.[UidEstado] INNER JOIN [dbo].[Paises] AS P ON P.[UidPais] = ES.[UidPais] WHERE REPLACE(CO.[Nombre],' ','') LIKE '{colonia}'AND CO.[CodigoPostal] = '{codigopostal}' ORDER BY CO.Nombre DESC";
            return oConexion.Consultas(query);
        }
        #endregion
    }
}
