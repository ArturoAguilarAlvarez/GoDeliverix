using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
                string source = Deliverix.Wpf.Distribuidores.Properties.Settings.Default["Source"].ToString();

                _ConexionLocal = new SqlConnection(@"Data Source=" + source + ";Initial Catalog=DeliverixDistribuidores;Integrated Security=True");
            }
            catch (Exception e)
            {
                string ab = e.Message;
                _ConexionLocal = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=DeliverixDistribuidores;Integrated Security=True");
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
