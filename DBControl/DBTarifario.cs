using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DBControl
{
    public class DBTarifario
    {
        #region Propiedades

        Conexion oConexion;
        #endregion
        public DataTable Busquedas(SqlCommand CMD)
        {
            oConexion = new Conexion();
            return oConexion.Busquedas(CMD);
        }

        public string ObtenerNombreColoniaRecolecta(string UidRelacionZR)
        {
            oConexion = new Conexion();
            string Nombre = string.Empty;
            string query = "select Nombre from Colonia where UidColonia in (select UidColonia from ZonaDeRecoleccion where UidZonaDeRecolecta ='" + UidRelacionZR + "')";
            foreach (DataRow item in oConexion.Consultas(query).Rows)
            {
                Nombre = item["Nombre"].ToString();
            };
            return Nombre;
        }

        public string ObtenerNombreColoniaEntrega(string UidRolacionZE)
        {
            oConexion = new Conexion();
            string Nombre = string.Empty;
            string query = "select Nombre from Colonia where UidColonia in (select UidColonia from ZonaDeServicio where UidRelacionZonaServicio ='" + UidRolacionZE + "')";
            foreach (DataRow item in oConexion.Consultas(query).Rows)
            {
                Nombre = item["Nombre"].ToString();
            };
            return Nombre;
        }

        public void EliminaTarifario(string uidsucursal)
        {
            oConexion = new Conexion();
            string query = "delete from Tarifario where UIdRelacionZonaRecolecta in (select UidZonaDeRecolecta from ZonaDeRecoleccion where uidSucursal = '" + uidsucursal + "')";
            oConexion.Consultas(query);
        }

        public DataTable RecuperaTarifario(Guid uidTarifario)
        {
            oConexion = new Conexion();
            string query = "select Uidregistrotarifario,UidRelacionZonaRecolecta,UidRelacionZonaEntrega, MCosto from Tarifario where UidRegistroTarifario = '" + uidTarifario.ToString() + "'";
            return oConexion.Consultas(query);
        }

        public void EliminaTarifarioDeSuministradora(string UidSucursal)
        {
            oConexion = new Conexion();
            string query = "delete from ZonaDeRepartoDeContrato where UidContrato in (select UidContrato from ContratoDeServicio where UidSucursalSuministradora = '" + UidSucursal + "')";
            oConexion.Consultas(query);
        }

        public DataTable ObtenerTarifarioDeOrden(Guid uidOrden)
        {
            oConexion = new Conexion();
            string query = "select * from OrdenTarifario ot inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario inner join OrdenSucursal os on os.UidRelacionOrdenSucursal = ot.UidOrden  where os.UidRelacionOrdenSucursal = '"+ uidOrden.ToString() + "'";
            return oConexion.Consultas(query);
        }
    }
}
