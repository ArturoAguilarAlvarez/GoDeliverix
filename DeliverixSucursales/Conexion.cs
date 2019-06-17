using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Configuration;
using Microsoft.SqlServer;
using System.Diagnostics;


namespace DeliverixSucursales
{
    class Conexion
    {
        #region Atributos
        private string servidor;
        public string SERVIDOR
        {
            get { return servidor; }
            set { servidor = value; }
        }

        private string instancia;
        public string INSTANCIA
        {
            get { return instancia; }
            set { instancia = value; }
        }

        /* PC Arturo es choto */
        public SqlConnection _ConexionLocal = new SqlConnection();
        #endregion

        #region Metodos
        public DataTable Consultas(string Consulta)
        {
            DataTable DT = new DataTable();
            CrearCadenaConexion();
            _ConexionLocal.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(Consulta, _ConexionLocal);
                SqlDataAdapter a = new SqlDataAdapter(cmd);
                a.Fill(DT);
            }
            catch (Exception)
            {
                MessageBox.Show("No hay conexion a la base de datos");
            }
            finally
            {
                _ConexionLocal.Close();
            }
            return DT;
        }

        public void CrearCadenaConexion()
        {
            try
            {
                string source = Properties.Settings.Default["Source"].ToString();

                _ConexionLocal = new SqlConnection(@"Data Source=" + source + ";Initial Catalog=GoDeliverixSuministradora;Integrated Security=True");
            }
            catch (Exception e)
            {
                string ab = e.Message;
                _ConexionLocal = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=GoDeliverixSuministradora;Integrated Security=True");
            }
        }

        public bool ModificarDatos(SqlCommand cmd)
        {
            bool resultado = false;
            try
            {
                CrearCadenaConexion();
                _ConexionLocal.Open();
                cmd.Connection = _ConexionLocal;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                resultado = true;
            }
            catch (Exception)
            {
                MessageBox.Show("No hay conexion a la base de datos");
            }
            finally
            {

                _ConexionLocal.Close();

            }
            return resultado;
        }
        #endregion
    }
}
