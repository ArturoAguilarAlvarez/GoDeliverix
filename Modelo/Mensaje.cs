using DBControl;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class Mensaje
    {
        #region Propiedades

        Conexion Datos;
        private Guid _UidMensaje;

        public Guid Uid
        {
            get { return _UidMensaje; }
            set { _UidMensaje = value; }
        }
        private string _VchMensaje;

        public string StrMensaje
        {
            get { return _VchMensaje; }
            set { _VchMensaje = value; }
        }

        #endregion

        #region Metodos
        public bool Guardar()
        {
            bool resultado = false;
            Datos = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregarMensaje";

                Comando.Parameters.Add("@UidMensaje", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidMensaje"].Value = Uid;

                Comando.Parameters.Add("@VchMensaje", SqlDbType.NVarChar, 1000);
                Comando.Parameters["@VchMensaje"].Value = StrMensaje;


                resultado = Datos.ModificarDatos(Comando);
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
            Datos = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ActualizarMensaje";

                Comando.Parameters.Add("@UidMensaje", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidMensaje"].Value = Uid;

                Comando.Parameters.Add("@VchMensaje", SqlDbType.NVarChar, 1000);
                Comando.Parameters["@VchMensaje"].Value = StrMensaje;


                resultado = Datos.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool AsociarConSucursal(Guid UidSucursal)
        {
            bool resultado = false;
            Datos = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregarMensajeSucursal";

                Comando.Parameters.Add("@UidMensaje", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidMensaje"].Value = Uid;

                Comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSucursal"].Value = UidSucursal;


                resultado = Datos.ModificarDatos(Comando);
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
