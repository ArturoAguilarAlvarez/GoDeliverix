using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DBControl
{
    public class DBLicencia
    {
        Conexion oConexion;
        public void EliminaLicenciaSucursal(string uidsucursal)
        {
            oConexion = new Conexion();
            string query = "delete from SucursalLicencia where UidSucursal = '" + uidsucursal + "'";
            oConexion.Consultas(query);
        }

        public void EliminaLicencia(Guid uidLicencia)
        {
            oConexion = new Conexion();
            string query = "delete from sucursallicencia where uidlicencia = '" + uidLicencia.ToString() + "'";
            oConexion.Consultas(query);
        }

        public DataTable VerificaLicenciaDeSucursal(string licencia)
        {
            oConexion = new Conexion();
            string query = "select * from SucursalLicencia where UidLicencia = '" + licencia + "'";
            return oConexion.Consultas(query);
        }

        public DataTable verificaEstatusLicenciaSucursal(string licencia)
        {
            oConexion = new Conexion();
            string query = "select * from SucursalLicencia where UidLicencia = '" + licencia + "' and IntEstatus = 1 ";
            return oConexion.Consultas(query);
        }

        public DataTable VerificaDisponibilidad(string licencia)
        {
            oConexion = new Conexion();
            string query = "select * from SucursalLicencia where UidLicencia = '" + licencia + "' and BLUso = 0";
            return oConexion.Consultas(query);
        }

        public void CambiaDisponibilidad(string licencia)
        {
            oConexion = new Conexion();
            string query = "update  SucursalLicencia set BLUso = 1 where UidLicencia = '" + licencia + "'";
            oConexion.Consultas(query);
        }
    }
}
