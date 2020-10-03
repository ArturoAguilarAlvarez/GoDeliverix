using System.Data;

namespace DBControl
{
    public class DBEstatus
    {
        Conexion oConexion;

        public DataTable ObtenerEstatus()
        {
            oConexion = new Conexion();
            string Query = "(select -1 as IdEstatus, '-- Seleccionar --' as Nombre)union all(select IdEstatus,Nombre from Estatus)";
            return oConexion.Consultas(Query);
        }
        public DataTable ObtenerEstatusDeContrato()
        {
            oConexion = new Conexion();
            string query = "(select '00000000-0000-0000-0000-000000000000' as UidEstatus, '--- Seleccionar ---' as VchNombre)union all (select UidEstatus,VchNombre from EstatusDeContrato)";
            return oConexion.Consultas(query);
        }
        public DataTable ObtenerEstatusActivo()
        {
            oConexion = new Conexion();
            string Query = "select IdEstatus,Nombre from Estatus";
            return oConexion.Consultas(Query);
        }

        public DataTable ObtenerEstatusDeOrdenEnSucursal()
        {
            oConexion = new Conexion();
            string Query = "(select '00000000-0000-0000-0000-000000000000' as UidEstatus,'  ' as  VchNombre)Union all(select Uidestatus,VchNombre from EstatusOrdenSucursal)";
            return oConexion.Consultas(Query);
        }
        public DataTable ObtenerListaDeEstatusOrdenSucursal(string UidOrdenSucursal)
        {
            oConexion = new Conexion();
            string Query = "select BO.DtmFecha,EO.VchNombre from BitacoraOrdenEstatus BO inner join EstatusDeOrden EO on BO.UidEstatusDeOrden = EO.UidEstatus inner join OrdenSucursal OS on OS.UidRelacionOrdenSucursal = BO.UidOrden where OS.UidRelacionOrdenSucursal ='" + UidOrdenSucursal + "'";
            return oConexion.Consultas(Query);
        }
    }
}
