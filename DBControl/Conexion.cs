using System;
using System.Data;
using System.Data.SqlClient;

namespace DBControl
{
    public class Conexion
    {
        #region Propiedades
        //Conexion con el servidor
        //private SqlConnection cn = new SqlConnection(,);
        //Conexion Osbel
        //public SqlConnection cn = new SqlConnection("Data Source=HP-BLANCA;Initial Catalog=DELIVERIX;User ID=sa;Password=12345678");
        //Conexion para la GearHost
         private SqlConnection myVar = new SqlConnection("Data Source=den1.mssql5.gear.host;Initial Catalog=deliverix;Persist Security Info=True;User ID=deliverix;Password=Yj8q4DyP!d!o");
        //Conexion Toshiba
        //private SqlConnection myVar = new SqlConnection(connectionString: @"Data Source=192.168.100.50;Initial Catalog=Deliverix;User ID=sa;Password=12345678");

        public SqlConnection Cn
        {
            get { return myVar; }
        }

        //Conexion para alienware
        // SqlConnection cn = new SqlConnection("Data Source=DESKTOP-H7RK4ED\\SQLDELIVERIX;Initial Catalog=DELIVERIX;Integrated Security=True");
        #endregion


        #region Metodos
        public DataTable Consultas(string Consulta)
        {
            DataTable DT = new DataTable();
            Cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(Consulta, Cn);
                SqlDataAdapter a = new SqlDataAdapter(cmd);
                a.Fill(DT);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Cn.Close();
            }
            return DT;
        }
        public DataTable Busquedas(SqlCommand cmd)
        {
            DataTable DT = new DataTable();
            Cn.Open();
            try
            {
                cmd.Connection = Cn;
                SqlDataAdapter a = new SqlDataAdapter(cmd);
                a.Fill(DT);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Cn.Close();
            }
            return DT;
        }
        public bool ModificarDatos(SqlCommand cmd)
        {
            bool resultado = false;
            try
            {
                Cn.Open();
                cmd.Connection = Cn;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                resultado = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                Cn.Close();

            }
            return resultado;
        }
        #endregion
    }
}
