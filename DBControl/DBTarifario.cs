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
        /// <summary>
        /// Actualiza la propina del tarifario
        /// </summary>
        /// <param name="uidOrdenSucursal"></param>
        /// <param name="mPropina"></param>
        public void ActualizaTarifario(Guid uidOrdenSucursal, string mPropina)
        {
            oConexion = new Conexion();
            string query = "update OrdenTarifario set MPropina = " + mPropina + " where UidOrden = '" + uidOrdenSucursal + "'";
            oConexion.Consultas(query);
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
            string query = "select * from OrdenTarifario ot inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario inner join ZonaDeServicio zs on zs.UidRelacionZonaServicio = t.UidRelacionZonaEntrega inner join OrdenSucursal os on os.UidRelacionOrdenSucursal = ot.UidOrden inner join Sucursales s on s.UidSucursal = zs.UidSucursal inner join Empresa e on e.UidEmpresa = s.UidEmpresa where os.UidRelacionOrdenSucursal = '" + uidOrden.ToString() + "'";
            return oConexion.Consultas(query);
        }

        public DataTable ExportarExcel(string Uidempresa = "")
        {
            string Filtro = "";
            if (!string.IsNullOrEmpty(Uidempresa))
            {
                Filtro = "where Emp.UidEmpresa = '" + Uidempresa + "'";
            }
            string query = "select t.UidRegistroTarifario as ID, Emp.NombreComercial as Distribuidor,Col.Nombre as Recolecta, C.Nombre as Entrega, t.MCosto as Precio from Tarifario t inner join ZonaDeRecoleccion ZDR on ZDR.UidZonaDeRecolecta = t.UidRelacionZonaRecolecta inner join  ZonaDeServicio ZDS on ZDS.UidRelacionZonaServicio = t.UidRelacionZonaEntrega  inner join Sucursales suc on suc.UidSucursal = ZDR.UidSucursal inner join Sucursales S on s.UidSucursal = ZDS.UidSucursal inner join Colonia col on col.UidColonia = ZDR.UidColonia  inner join Colonia C on c.UidColonia = ZDS.UidColonia inner join empresa emp on emp.UidEmpresa = suc.UidEmpresa " + Filtro + " order by Col.Nombre,C.Nombre asc";
            oConexion = new Conexion();
            return oConexion.Consultas(query);
        }
        public void ActualizaTarifario(string UidTarifario, decimal DPrecio)
        {
            oConexion = new Conexion();
            string query = "update Tarifario set MCosto = " + DPrecio + " where UidRegistroTarifario = '" + UidTarifario + "'";
            oConexion.Consultas(query);
        }
    }
}
