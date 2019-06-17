using DBControl;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class Oferta
    {
        #region Propiedades
        private Guid _uidOferta;

        public Guid UID
        {
            get { return _uidOferta; }
            set { _uidOferta = value; }
        }

        private string _vchNombre;

        public string STRNOMBRE
        {
            get { return _vchNombre; }
            set { _vchNombre = value; }
        }

        public Sucursal oSucursal;
        public Estatus oEstatus;

        Conexion Datos;

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
                Comando.CommandText = "asp_AgregaOferta";

                Comando.Parameters.Add("@UidOferta", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidOferta"].Value = UID;

                Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 100);
                Comando.Parameters["@VchNombre"].Value = STRNOMBRE;

                Comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSucursal"].Value = oSucursal.ID;

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = oEstatus.ID;


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
                Comando.CommandText = "asp_ActualizaOferta";

                Comando.Parameters.Add("@UidOferta", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidOferta"].Value = UID;

                Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 100);
                Comando.Parameters["@VchNombre"].Value = STRNOMBRE;

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = oEstatus.ID;


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
