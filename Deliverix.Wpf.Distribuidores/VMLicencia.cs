using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;

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
        public bool VerificaExistenciaDeLicenciaLocal()
        {
            //string query = "select * from Licencia";
            //return oConexion.Consultas(query);
            bool resultado = false;
            if (!string.IsNullOrEmpty(Registry.GetValue(@"HKEY_CURRENT_USER\GoDeliverixDistribuidores", "Licencia", "").ToString()) && Registry.GetValue(@"HKEY_CURRENT_USER\GoDeliverixDistribuidores", "Licencia", "").ToString() != Guid.Empty.ToString())
            {
                resultado = true;
            }
            return resultado;
        }
        public void RecuperaLicencia()
        {
            DataTable tabla = new DataTable();
            Licencia = Guid.Empty.ToString();
            //string query = "select * from Licencia";
            //tabla = oConexion.Consultas(query);

            //foreach (DataRow item in tabla.Rows)
            //{
            //    Licencia = item["UidLicencia"].ToString();
            //}


            if (!string.IsNullOrEmpty(Registry.GetValue(@"HKEY_CURRENT_USER\GoDeliverixDistribuidores", "Licencia", "").ToString()) && Registry.GetValue(@"HKEY_CURRENT_USER\GoDeliverixDistribuidores", "Licencia", "").ToString() != Guid.Empty.ToString())
            {
                Licencia = Registry.GetValue(@"HKEY_CURRENT_USER\GoDeliverixDistribuidores", "Licencia", "").ToString();
            }
            else
            {
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"GoDeliverixDistribuidores", true);
                key.SetValue("Licencia", Licencia);
                key.Close();
            }



        }

        
        public void GuardarLicencia(string Licencia)
        {
            RegistryKey key;
            key = Registry.CurrentUser.OpenSubKey(@"GoDeliverixDistribuidores", true);
            key.SetValue("Licencia", Licencia);
            key.Close();

            //string query = "insert into Licencia(UidLicencia)values('"+Licencia+"')";
            //oConexion.Consultas(query);
            //Licencia = string.Empty;
        }

        internal void EliminarLicencia()
        {
            RegistryKey key;
            key = Registry.CurrentUser.OpenSubKey(@"GoDeliverixDistribuidores", true);
            key.DeleteValue("Licencia");
            key.Close();

            //string query = "truncate table Licencia";
            //oConexion.Consultas(query);
        }
    }
}
