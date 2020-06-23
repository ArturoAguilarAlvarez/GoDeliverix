using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DBControl
{
    public class DBContrato
    {
        Conexion oConexion;

        public DataTable RecuperarRelacionContrato(string UidSucursal)
        {
            oConexion = new Conexion();
            string query = "select UidContrato, UidSucursalSuministradora, UidSucursalDistribuidora, DtmFechaDeCreacion, UidEstatusContrato, BlConfirmacionSuministradora, BlConfirmacionDistribuidora, BiPagaAlRecogerOrdenes, ComisionBackSite, DiferenciaComisionDistribuidora, ComisionDistribuidora from ContratoDeServicio where UidSucursalSuministradora = '" + UidSucursal + "' or UidSucursalDistribuidora = '" + UidSucursal + "'";
            return oConexion.Consultas(query);
        }
        public bool GuardaRelacionDeContrato(Guid UidContrato, Guid uidSucursalSumunistradora, Guid uidSucursalDistribuidora, Guid UidEstatus, bool confirmacionSuministradora, bool ConfirmacionDistribuidora, bool PagoAlRecolectar, int ComisionDeProducto)
        {
            oConexion = new Conexion();
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_GuardaRelacionContrato";
                //Dato1
                cmd.Parameters.Add("@UidSucursalSuministradora", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidSucursalSuministradora"].Value = uidSucursalSumunistradora;

                cmd.Parameters.Add("@UidContrato", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidContrato"].Value = UidContrato;

                cmd.Parameters.Add("@UidSucursalDistribuidora", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidSucursalDistribuidora"].Value = uidSucursalDistribuidora;

                cmd.Parameters.Add("@UidEstatus", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidEstatus"].Value = UidEstatus;

                cmd.Parameters.Add("@BlConfirmacionSuministradora", SqlDbType.Bit);
                cmd.Parameters["@BlConfirmacionSuministradora"].Value = confirmacionSuministradora;

                cmd.Parameters.Add("@BlConfirmacionDistribuidora", SqlDbType.Bit);
                cmd.Parameters["@BlConfirmacionDistribuidora"].Value = ConfirmacionDistribuidora;

                cmd.Parameters.Add("@BiPagaAlRecogerOrdenes", SqlDbType.Bit);
                cmd.Parameters["@BiPagaAlRecogerOrdenes"].Value = PagoAlRecolectar;

                cmd.Parameters.Add("@ComisionContrato", SqlDbType.Int);
                cmd.Parameters["@ComisionContrato"].Value = ComisionDeProducto;
                //Mandar comando a ejecución
                resultado = oConexion.ModificarDatos(cmd);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;

        }
        public void borrarRelacionDistribuidora(string uidSucursal)
        {
            oConexion = new Conexion();
            string query = "delete from contratodeservicio where UidSucursalDistribuidora = '" + uidSucursal + "'";
            oConexion.Consultas(query);
        }

        public void borrarRelacionSuministradora(string uidSucursal)
        {
            oConexion = new Conexion();
            string query = "delete from contratodeservicio where UidSucursalSuministradora = '" + uidSucursal + "'";
            oConexion.Consultas(query);
        }

        public DataTable PagaAlRecolectar(string uidSucursal, string licencia)
        {
            oConexion = new Conexion();
            string query = "select * from ContratoDeServicio where UidSucursalSuministradora = '" + uidSucursal.ToString() + "' and UidSucursalDistribuidora in (select s.UidSucursal from Sucursales s inner join SucursalLicencia sl on sl.UidSucursal = s.UidSucursal where sl.UidLicencia = '" + licencia + "') and BiPagaAlRecogerOrdenes = 1";
            return oConexion.Consultas(query);
        }

        public DataTable PagaAlRecolectarLaOrden(string uidOrden)
        {
            oConexion = new Conexion();
            string query = "select * from ContratoDeServicio where UidSucursalSuministradora = (select uidsucursal from OrdenSucursal where UidRelacionOrdenSucursal = '" + uidOrden + "') and UidSucursalDistribuidora in (select zr.UidSucursal from OrdenSucursal os inner join OrdenTarifario ot on ot.UidOrden = os.UidRelacionOrdenSucursal inner join Tarifario t on t.UidRegistroTarifario = ot.UidTarifario inner join ZonaDeRecoleccion zr on t.UidRelacionZonaRecolecta = zr.UidZonaDeRecolecta where os.UidRelacionOrdenSucursal = '" + uidOrden + "') and BiPagaAlRecogerOrdenes = 1";
            return oConexion.Consultas(query);
        }
    }
}
