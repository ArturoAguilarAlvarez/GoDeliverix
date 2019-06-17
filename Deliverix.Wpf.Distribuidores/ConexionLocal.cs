using System;
using System.Data;
using System.Data.SqlClient;

namespace Conexiones
{
    public class ConexionLocal
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

        /* PC Arturo */
        public SqlConnection _ConexionLocal = new SqlConnection();
        #endregion

        #region Constructor

        public ConexionLocal()
        {

        }

        #endregion

        #region Metodos
        
        public void CrearCadenaConexion()
        {
            try
            {
                string source = Deliverix.Wpf.Distribuidores.Properties.Settings.Default["Source"].ToString();

                _ConexionLocal = new SqlConnection(@"Data Source=" + source + ";Initial Catalog=DeliverixDistribuidores;Integrated Security=True");
            }
            catch (Exception e)
            {
                string ab = e.Message;
                _ConexionLocal = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=DeliverixDistribuidores;Integrated Security=True");
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
