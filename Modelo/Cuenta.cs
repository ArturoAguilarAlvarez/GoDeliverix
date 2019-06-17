using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DBControl;
using Modelo.Usuario;
namespace Modelo
{
    public class Cuenta
    {
        #region Propiedades
        Conexion oConexion;
        private Guid _UidRegistro;

        public Guid UID
        {
            get { return _UidRegistro; }
            set { _UidRegistro = value; }
        }
        private Guid _UidCodigoActiviacion;

        public Guid CodigoActivacion
        {
            get { return _UidCodigoActiviacion; }
            set { _UidCodigoActiviacion = value; }
        }

        public Usuarios usuario;
        public Estatus estatus;

        #endregion

        #region Metodos
        public bool Guardar()
        {
            bool resultado = false;
            oConexion = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ActivarCuenta";

                Comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidUsuario"].Value = usuario.ID; ;

                Comando.Parameters.Add("@UidCodigo", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidCodigo"].Value = CodigoActivacion;

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = estatus.ID;

                resultado = oConexion.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public bool Actualizar()
        {
            bool resultado = false;
            oConexion = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ActualizarCuenta ";

                Comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidUsuario"].Value = usuario.ID; ;

                Comando.Parameters.Add("@UidCodigo", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidCodigo"].Value = CodigoActivacion;

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = estatus.ID;

                resultado = oConexion.ModificarDatos(Comando);
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
