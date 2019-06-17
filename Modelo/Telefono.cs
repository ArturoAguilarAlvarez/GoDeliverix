using DBControl;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class Telefono
    {
        #region Propiedades
        Conexion cn;
        private Guid UidTelefono;
        public Guid ID
        {
            get { return UidTelefono; }
            set { UidTelefono = value; }
        }
        private string StrNumero;
        public string NUMERO
        {
            get { return StrNumero; }
            set { StrNumero = value; }
        }
        private string _Tipo;

        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        public TipoDeTelefono TIPO;
        #endregion
        #region Constructores
        public Telefono()
        {

        }
        public Telefono(Guid Id, Guid Tipo = new Guid(), string tipo = "", string Numero = "")
        {
            this.ID = Id;
            this.Tipo = tipo;
            this.TIPO = new TipoDeTelefono(Tipo);
            this.NUMERO = Numero;
        }
        #endregion

        #region Metodos
        public bool GuardaTelefono(Guid idPropietario, string Parametro)
        {
            SqlCommand cmd = new SqlCommand();
            bool resultado = false;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "asp_AgregarTelefono";

                cmd.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidUsuario"].Value = idPropietario;

                cmd.Parameters.Add("@UidTelefono", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@UidTelefono"].Value = ID;

                cmd.Parameters.Add("@TipoDeTelefono", SqlDbType.UniqueIdentifier);
                cmd.Parameters["@TipoDeTelefono"].Value = new Guid(Tipo);

                cmd.Parameters.Add("@Numero", SqlDbType.NVarChar, 30);
                cmd.Parameters["@Numero"].Value = NUMERO;

                cmd.Parameters.Add("@ParametroDeAgregacion", SqlDbType.NVarChar, 30);
                cmd.Parameters["@ParametroDeAgregacion"].Value = Parametro;
                cn = new Conexion();
                resultado = cn.ModificarDatos(cmd);
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
