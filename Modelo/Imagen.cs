using DBControl;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class Imagen
    {
        #region Propiedades
        Conexion Conn = new Conexion();
        private Guid _UidImagen;

        public Guid ID
        {
            get { return _UidImagen; }
            set { _UidImagen = value; }
        }

        private string _nVchRuta;
        public string STRRUTA
        {
            get { return _nVchRuta; }
            set { _nVchRuta = value; }
        }
        private string _NVchDescripcion;
        public string STRDESCRIPCION
        {
            get { return _NVchDescripcion; }
            set { _NVchDescripcion = value; }
        }
        #endregion

        #region Metodos
        public virtual bool GUARDARIMAGEN(Imagen img, Guid UidUsuario, string StoreProcedure)
        {
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = StoreProcedure;

                cmd.Parameters.Add("@UidImagen", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidImagen"].Value = img.ID;

                cmd.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidUsuario"].Value = UidUsuario;

                cmd.Parameters.Add("@NVchRuta", SqlDbType.NVarChar, 500);
                cmd.Parameters["@NVchRuta"].Value = img.STRRUTA;

                cmd.Parameters.Add("@NVchDescripcion", SqlDbType.NVarChar, 200);
                cmd.Parameters["@NVchDescripcion"].Value = img.STRDESCRIPCION;

                resultado = Conn.ModificarDatos(cmd);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public virtual bool ACTUALIZAIMAGEN(Imagen img,Guid uidempresa)
        {
            bool resultado = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "asp_ModificarImagenEmpresa";

                cmd.Parameters.Add("@UidImagen", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidImagen"].Value = img.ID;
                cmd.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidEmpresa"].Value = uidempresa;


                cmd.Parameters.Add("@NVchRuta", SqlDbType.NVarChar, 500);
                cmd.Parameters["@NVchRuta"].Value = img.STRRUTA;

                cmd.Parameters.Add("@NVchDescripcion", SqlDbType.NVarChar, 200);
                cmd.Parameters["@NVchDescripcion"].Value = img.STRDESCRIPCION;

                resultado = Conn.ModificarDatos(cmd);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        #endregion
    }
}
