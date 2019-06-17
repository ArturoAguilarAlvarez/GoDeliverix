using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DBControl;
namespace Modelo
{
    public class Categoria
    {
        #region Propiedades
        Conexion Conn = new Conexion();
        private Guid _UidCategoria;

        public Guid ID
        {
            get { return _UidCategoria; }
            set { _UidCategoria = value; }
        }

        private string _VchNombre;

        public string STRNOMBRE
        {
            get { return _VchNombre; }
            set { _VchNombre = value; }
        }

        private string _VchDescripcion;

        public string STRDESCRIPCION
        {
            get { return _VchDescripcion; }
            set { _VchDescripcion = value; }
        }

        public Giro OGiro = new Giro();

        public Estatus oEstatus = new Estatus();



        #endregion

        #region Metodos 
        public bool Guardar(Categoria Cat)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregaCategoria";

                Comando.Parameters.Add("@UidCategoria", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidCategoria"].Value = Cat.ID;

                Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 50);
                Comando.Parameters["@VchNombre"].Value = Cat.STRNOMBRE;

                Comando.Parameters.Add("@VchDescripcion", SqlDbType.NVarChar, 100);
                Comando.Parameters["@VchDescripcion"].Value = Cat.STRDESCRIPCION;

                Comando.Parameters.Add("@UidGiro", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidGiro"].Value = Cat.OGiro.UID;

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = Cat.oEstatus.ID;

                resultado = Conn.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool Actualizar(Categoria Cat)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ActualizaCategoria";

                Comando.Parameters.Add("@UidCategoria", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidCategoria"].Value = Cat.ID;

                Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 50);
                Comando.Parameters["@VchNombre"].Value = Cat.STRNOMBRE;

                Comando.Parameters.Add("@VchDescripcion", SqlDbType.NVarChar, 100);
                Comando.Parameters["@VchDescripcion"].Value = Cat.STRDESCRIPCION;

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = Cat.oEstatus.ID;

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
