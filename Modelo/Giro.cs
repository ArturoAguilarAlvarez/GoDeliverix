using DBControl;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class Giro
    {
        #region Propiedades
        Conexion Con = new Conexion();
        private Guid _UidGiro;
        /// <summary>
        /// Guid del giro
        /// </summary>
        public Guid UID
        {
            get { return _UidGiro; }
            set { _UidGiro = value; }
        }
        private string _vchNombre;

        public string STRNOMBRE
        {
            get { return _vchNombre; }
            set { _vchNombre = value; }
        }

        private int _IdEstatus;

        public int ESTATUS
        {
            get { return _IdEstatus; }
            set { _IdEstatus = value; }
        }
        private string _Descripcion;

        public string STRDESCRIPCION
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }


        #endregion

        #region Metodos
        public bool Guardar(Giro Objeto)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregaGiro";

                Comando.Parameters.Add("@UidGiro", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidGiro"].Value = Objeto.UID;

                Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 100);
                Comando.Parameters["@VchNombre"].Value = Objeto.STRNOMBRE;

                Comando.Parameters.Add("@VchDescripcion", SqlDbType.NVarChar, 100);
                Comando.Parameters["@VchDescripcion"].Value = Objeto.STRDESCRIPCION;

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = Objeto.ESTATUS;


                resultado = Con.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool Actualizar(Giro Objeto)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ActualizaGiro";

                Comando.Parameters.Add("@UidGiro", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidGiro"].Value = Objeto.UID;

                Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 100);
                Comando.Parameters["@VchNombre"].Value = Objeto.STRNOMBRE;

                Comando.Parameters.Add("@VchDescripcion", SqlDbType.NVarChar, 100);
                Comando.Parameters["@VchDescripcion"].Value = Objeto.STRDESCRIPCION;

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = Objeto.ESTATUS;

                resultado = Con.ModificarDatos(Comando);
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
