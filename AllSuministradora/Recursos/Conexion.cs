using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AllSuministradora.Recursos
{
    public class Conexion
    {
        #region Propiedades

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
                string source = AllSuministradora.Properties.Settings.Default["Source"].ToString();
                _ConexionLocal = new SqlConnection(@"Data Source=" + source + ";Initial Catalog=DeliverixMaster;Integrated Security=True");
            }
            catch (Exception e)
            {
                string ab = e.Message;
                _ConexionLocal = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=DeliverixMaster;Integrated Security=True");
            }
        }

        public string ConvertirConsultaACadena(SqlCommand SentenciaSQL)
        {
            CrearCadenaConexion();
            String Cadena = "";
            SentenciaSQL.Connection = _ConexionLocal;
            SqlDataAdapter da = new SqlDataAdapter(SentenciaSQL.CommandText, _ConexionLocal);
            da.SelectCommand.Connection.Open();
            Cadena = da.SelectCommand.ExecuteScalar().ToString();
            da.SelectCommand.Connection.Close();
            SentenciaSQL.Dispose();
            return Cadena;
        }
        #endregion
    }
}
