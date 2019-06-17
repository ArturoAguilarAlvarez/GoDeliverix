using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DeliverixSucursales
{
    class VMLicencia
    {
        #region Propiedades
        Conexion oConexion = new Conexion();
        private string _UidLicencia;

        public string Licencia
        {
            get { return _UidLicencia; }
            set { _UidLicencia = value; }
        }

        #endregion
        public DataTable VerificaExistenciaDeLicenciaLocal()
        {
            string query = "select * from Licencia";
            return oConexion.Consultas(query);
        }
        public void RecuperaLicencia()
        {
            DataTable tabla = new DataTable();
            Licencia = string.Empty;
            string query = "select * from Licencia";
            tabla = oConexion.Consultas(query);

            foreach (DataRow item in tabla.Rows)
            {
                Licencia = item["UidLicencia"].ToString();
            }

        }

        
        public void GuardarLicencia(string Licencia)
        {
            string query = "insert into Licencia(UidLicencia)values('"+Licencia+"')";
            oConexion.Consultas(query);
        }

        internal void EliminarLicencia()
        {
            string query = "truncate table Licencia";
            oConexion.Consultas(query);
        }


    }
}
