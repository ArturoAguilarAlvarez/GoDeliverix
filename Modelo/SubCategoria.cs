using DBControl;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class SubCategoria
    {
        #region Propiedades
        Conexion Conn = new Conexion();
        private Guid UidSubcategoria;

        public Guid UID
        {
            get { return UidSubcategoria; }
            set { UidSubcategoria = value; }
        }
        private Guid _Uidategoria;

        public Guid UIDCATEGORIA
        {
            get { return _Uidategoria; }
            set { _Uidategoria = value; }
        }

        private string _StrNombre;

        public string STRNOMBRE
        {
            get { return _StrNombre; }
            set { _StrNombre = value; }
        }
        private string _descripcion;

        public string STRDESCRIPCION
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public Estatus oEstatus = new Estatus();
        #endregion

        #region Metodos
        public bool Guardar(SubCategoria Subcat)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregaSubCategoria";

                Comando.Parameters.Add("@UidSubcategoria", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSubcategoria"].Value = Subcat.UID;

                Comando.Parameters.Add("@UidCategoria", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidCategoria"].Value = Subcat.UIDCATEGORIA;

                Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 50);
                Comando.Parameters["@VchNombre"].Value = Subcat.STRNOMBRE;

                Comando.Parameters.Add("@VchDescripcion", SqlDbType.NVarChar, 100);
                Comando.Parameters["@VchDescripcion"].Value = Subcat.STRDESCRIPCION;

                Comando.Parameters.Add("@intEstatus", SqlDbType.Int);
                Comando.Parameters["@intEstatus"].Value = Subcat.oEstatus.ID;

                resultado = Conn.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool Actualizar(SubCategoria Objeto)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ActualizaSubcategoria";

                Comando.Parameters.Add("@UidSubcategoria", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSubcategoria"].Value = Objeto.UID;

                Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 50);
                Comando.Parameters["@VchNombre"].Value = Objeto.STRNOMBRE;

                Comando.Parameters.Add("@VchDescripcion", SqlDbType.NVarChar, 100);
                Comando.Parameters["@VchDescripcion"].Value = Objeto.STRDESCRIPCION;

                Comando.Parameters.Add("@intEstatus", SqlDbType.Int);
                Comando.Parameters["@intEstatus"].Value = Objeto.oEstatus.ID;

                resultado = Conn.ModificarDatos(Comando);
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
