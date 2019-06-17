using System;
using DBControl;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class Licencia
    {
        #region Propiedades
        private Guid _UidLicencia;

        public Guid UidLicencia
        {
            get { return _UidLicencia; }
            set { _UidLicencia = value; }
        }

        private bool _boolDisponibilidad;

        public bool BLUso
        {
            get { return _boolDisponibilidad; }
            set { _boolDisponibilidad = value; }
        }
        private string _VchIdentificador;

        public string StrIdentificador
        {
            get { return _VchIdentificador; }
            set { _VchIdentificador = value; }
        }

        public Sucursal oSucursal;
        public Estatus oEstatus;
        Conexion oConexion;

        #endregion

        #region Metodos
        public bool Guardar(string procedure, Guid uidUsuario)
        {
            bool resultado = false;
            oConexion = new Conexion();

            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = procedure;

                Comando.Parameters.Add("@UidPropietario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidPropietario"].Value = uidUsuario;

                Comando.Parameters.Add("@UidLicencia", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidLicencia"].Value = UidLicencia;

                Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                Comando.Parameters["@IntEstatus"].Value = oEstatus.ID;

                Comando.Parameters.Add("@VchIdentificador", SqlDbType.VarChar, 50);
                Comando.Parameters["@VchIdentificador"].Value = StrIdentificador;

                Comando.Parameters.Add("@BLUso", SqlDbType.Bit);
                Comando.Parameters["@BLUso"].Value = Convert.ToByte(BLUso);

                resultado = oConexion.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }



        public bool Actualizar(string procedure, Guid uidUsuario)
        {
            bool resultado = false;
            oConexion = new Conexion();

            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = procedure;

                Comando.Parameters.Add("@UidLicencia", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidLicencia"].Value = uidUsuario;

                if (oEstatus.ID != 0)
                {
                    Comando.Parameters.Add("@IntEstatus", SqlDbType.Int);
                    Comando.Parameters["@IntEstatus"].Value = oEstatus.ID;
                }
                if (!string.IsNullOrWhiteSpace(StrIdentificador))
                {
                    Comando.Parameters.Add("@VchIdentificador", SqlDbType.VarChar, 50);
                    Comando.Parameters["@VchIdentificador"].Value = StrIdentificador;
                }
                Comando.Parameters.Add("@BLUso", SqlDbType.Bit);
                Comando.Parameters["@BLUso"].Value = Convert.ToByte(BLUso);

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
