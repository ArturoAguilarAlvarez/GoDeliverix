using DBControl;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class Producto
    {
        #region Propiedades
        Conexion CN = new Conexion();
        private Guid _Uidproducto;

        public Guid UID
        {
            get { return _Uidproducto; }
            set { _Uidproducto = value; }
        }

        private string _strNombre;

        public string STRNOMBRE
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }

        private string _strDescripcion;

        public string STRDESCRIPCION
        {
            get { return _strDescripcion; }
            set { _strDescripcion = value; }
        }

        public Estatus oEstatus;

        private Guid _UidEmpresa;

        public Guid UIDEMPRESA
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }

        private string _TiempoElaboracion;

        public string STRTiemporElaboracion
        {
            get { return _TiempoElaboracion; }
            set { _TiempoElaboracion = value; }
        }

        private string _mCosto;

        public string StrCosto
        {
            get { return _mCosto; }
            set { _mCosto = value; }
        }
        #endregion

        #region Metodos

        public bool Guardar(Producto Objeto)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregaProducto";

                Comando.Parameters.Add("@UidProducto", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidProducto"].Value = Objeto.UID;

                Comando.Parameters.Add("@VchNombre", SqlDbType.VarChar, 50);
                Comando.Parameters["@VchNombre"].Value = Objeto.STRNOMBRE;

                Comando.Parameters.Add("@VchDescripcion", SqlDbType.VarChar, 300);
                Comando.Parameters["@VchDescripcion"].Value = Objeto.STRDESCRIPCION;

                Comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidEmpresa"].Value = Objeto.UIDEMPRESA;

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = Objeto.oEstatus.ID;

                resultado = CN.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool Actualizar(Producto Objeto)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ActualizarProducto";

                Comando.Parameters.Add("@UidProducto", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidProducto"].Value = Objeto.UID;

                Comando.Parameters.Add("@VchNombre", SqlDbType.VarChar, 50);
                Comando.Parameters["@VchNombre"].Value = Objeto.STRNOMBRE;

                Comando.Parameters.Add("@VchDescripcion", SqlDbType.VarChar, 300);
                Comando.Parameters["@VchDescripcion"].Value = Objeto.STRDESCRIPCION;

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = Objeto.oEstatus.ID;

                resultado = CN.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool RELACIONGIRO(Guid giro, Guid uidProducto)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregaGiroProducto";

                Comando.Parameters.Add("@UidProducto", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidProducto"].Value = uidProducto;

                Comando.Parameters.Add("@UidGiro", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidGiro"].Value = giro;
                resultado = CN.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool RELACIONCATEGORIA(Guid categoria, Guid uidProducto)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregaCategoriaProducto";

                Comando.Parameters.Add("@UidProducto", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidProducto"].Value = uidProducto;

                Comando.Parameters.Add("@UidCategoria", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidCategoria"].Value = categoria;
                resultado = CN.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool RELACIONSUBCATEGORIA(Guid subcategoria, Guid uidProducto)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregaSubcategoriaProducto";

                Comando.Parameters.Add("@UidProducto", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidProducto"].Value = uidProducto;

                Comando.Parameters.Add("@UidSubcategoria", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSubcategoria"].Value = subcategoria;
                resultado = CN.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool ActualizaInformacionProductoSucursal(string UIDSECCION)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_actualizaSeccionProducto";

                Comando.Parameters.Add("@UidProducto", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidProducto"].Value = UID;

                Comando.Parameters.Add("@UidSeccion", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSeccion"].Value = new Guid(UIDSECCION);

                Comando.Parameters.Add("@MCosto", SqlDbType.VarChar, 20);
                Comando.Parameters["@MCosto"].Value = StrCosto;

                Comando.Parameters.Add("@VchTiempoElaboracion", SqlDbType.VarChar, 10);
                Comando.Parameters["@VchTiempoElaboracion"].Value = STRTiemporElaboracion;
                resultado = CN.ModificarDatos(Comando);
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
