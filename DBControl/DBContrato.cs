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
            string query = "select * from ContratoDeServicio where UidSucursalSuministradora = '" + UidSucursal + "' or UidSucursalDistribuidora = '" + UidSucursal + "'";
            return oConexion.Consultas(query);
        }
        public bool GuardaRelacionDeContrato(Guid UidContrato, Guid uidSucursalSumunistradora, Guid uidSucursalDistribuidora, Guid UidEstatus, bool confirmacionSuministradora, bool ConfirmacionDistribuidora)
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
    }
}
