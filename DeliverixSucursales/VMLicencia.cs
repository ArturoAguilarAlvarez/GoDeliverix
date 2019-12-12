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
            bool resultado = false;
            if (!string.IsNullOrEmpty(Registry.GetValue(@"HKEY_CURRENT_USER\GoDeliverixSuministradora", "Licencia", "").ToString()) && Registry.GetValue(@"HKEY_CURRENT_USER\GoDeliverixSuministradora", "Licencia", "").ToString() != Guid.Empty.ToString())
            {
                resultado = true;
            }
            return resultado;
        }
        public void RecuperaLicencia()
        {
            DataTable tabla = new DataTable();
            Licencia = string.Empty;
            string query = "select * from Licencia";
            tabla = oConexion.Consultas(query);
            if (!string.IsNullOrEmpty(Registry.GetValue(@"HKEY_CURRENT_USER\GoDeliverixSuministradora", "Licencia", "").ToString()) && Registry.GetValue(@"HKEY_CURRENT_USER\GoDeliverixSuministradora", "Licencia", "").ToString() != Guid.Empty.ToString())
            {
                Licencia = Registry.GetValue(@"HKEY_CURRENT_USER\GoDeliverixSuministradora", "Licencia", "").ToString();
            }
            else
            {
                Licencia = Guid.Empty.ToString();
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"GoDeliverixSuministradora",true);
                key.SetValue("Licencia", Licencia);
                key.Close();
            }
        }


        public void GuardarLicencia(string Licencia)
        {
            RegistryKey key;
            key = Registry.CurrentUser.OpenSubKey(@"GoDeliverixSuministradora",true);
            key.SetValue("Licencia", Licencia);
            key.Close();
        }

        internal void EliminarLicencia()
        {
            RegistryKey key;
            key = Registry.CurrentUser.OpenSubKey(@"GoDeliverixSuministradora",true);
            key.DeleteValue("Licencia");
            key.Close();
        }


    }
}
