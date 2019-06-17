using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DBControl
{
    public class DbVehiculo
    {
        Conexion oConexcion;

        public DataTable TiposDeVehiculos()
        {
            oConexcion = new Conexion();
            string Query = "select * from tipodevehiculo";
            return oConexcion.Consultas(Query);
        }
        public DataTable TiposDeVehiculosBusqueda()
        {
            oConexcion = new Conexion();
            string Query = "(select '00000000-0000-0000-0000-000000000000' as UidTipoDeVehiculo, '-- Seleccionar --' as vchnombre) union all(select uidtipodevehiculo,vchnombre from tipodevehiculo)";
            return oConexcion.Consultas(Query);
        }

        public void EliminarRelacionSucursal(string UidVehiculo)
        {
            oConexcion = new Conexion();
            string query = "delete from vehiculoSucursal where UidVehiculo = '" + UidVehiculo + "'";
            oConexcion.Consultas(query);
        }

        public DataTable Obtener_VehiculosEnSucursal(string uidLicencia)
        {
            oConexcion = new Conexion();
            string query = "select * from vehiculo where uidvehiculo in (select Uidvehiculo from vehiculoSucursal where UidSucursal in(select UidSucursal from SucursalLicencia where UidLicencia = '" + uidLicencia + "'))";
            return oConexcion.Consultas(query);
        }
    }
}
