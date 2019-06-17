using DBControl;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class Vehiculo
    {
        #region Propiedades
        private Guid _UidVehiculo;
        /// <summary>
        /// Identificador de la clase
        /// </summary>
        public Guid UID
        {
            get { return _UidVehiculo; }
            set { _UidVehiculo = value; }
        }
        private Guid _UidEmpresa;
        /// <summary>
        /// Uid de la empresa pereneciente
        /// </summary>
        public Guid UIDEmpresa
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }
        private string _lngNumeroDeSerie;
        /// <summary>
        /// Numero de serie VIN
        /// </summary>
        public string LngNumeroDeSerie
        {
            get { return _lngNumeroDeSerie; }
            set { _lngNumeroDeSerie = value; }
        }
        private int _intCilindrada;
        /// <summary>
        /// Cilindrada del vehiculo
        /// </summary>
        public int IntCilindrada
        {
            get { return _intCilindrada; }
            set { _intCilindrada = value; }
        }
        private string _StrMarca;
        /// <summary>
        /// Marca del vehiculo
        /// </summary>
        public string StrMarca
        {
            get { return _StrMarca; }
            set { _StrMarca = value; }
        }
        private string _StrColor;
        /// <summary>
        /// Color del vehiculo
        /// </summary>
        public string StrColor
        {
            get { return _StrColor; }
            set { _StrColor = value; }
        }
        private string _StrAnio;
        /// <summary>
        /// Anio del vehiculo
        /// </summary>
        public string StrAnio
        {
            get { return _StrAnio; }
            set { _StrAnio = value; }
        }
        private string _StrNoDePlaca;
        /// <summary>
        /// Numero de placa
        /// </summary>
        public string StrNoDePLaca
        {
            get { return _StrNoDePlaca; }
            set { _StrNoDePlaca = value; }
        }
        private string _StrModelo;
        /// <summary>
        /// Modelo del vehiculo
        /// </summary>
        public string StrModelo
        {
            get { return _StrModelo; }
            set { _StrModelo = value; }
        }
        private Guid _UidTipoDeVehiculo;
        /// <summary>
        /// Tipo de vehiculo
        /// </summary>
        public Guid UidTipoDeVehiculo
        {
            get { return _UidTipoDeVehiculo; }
            set { _UidTipoDeVehiculo = value; }
        }
        /// <summary>
        /// Objeto sucursal del vehiculo
        /// </summary>
        public Sucursal oSucursal;
        Conexion oConexion;
        #endregion

        #region Metodos
        public bool Guardar()
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregaVehiculo";

                Comando.Parameters.Add("@UidVehiculo", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidVehiculo"].Value = UID;

                Comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidEmpresa"].Value = UIDEmpresa;

                Comando.Parameters.Add("@UidTipoDeVehiculo", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidTipoDeVehiculo"].Value = UidTipoDeVehiculo;

                Comando.Parameters.Add("@BINumeroDeSerie", SqlDbType.VarChar, 30);
                Comando.Parameters["@BINumeroDeSerie"].Value = LngNumeroDeSerie;

                Comando.Parameters.Add("@IntCilindradaDeMotor", SqlDbType.Int);
                Comando.Parameters["@IntCilindradaDeMotor"].Value = IntCilindrada;

                Comando.Parameters.Add("@VchMarca", SqlDbType.VarChar, 30);
                Comando.Parameters["@VchMarca"].Value = StrMarca;

                Comando.Parameters.Add("@VchColor", SqlDbType.VarChar, 30);
                Comando.Parameters["@VchColor"].Value = StrColor;

                Comando.Parameters.Add("@VchModelo", SqlDbType.VarChar, 10);
                Comando.Parameters["@VchModelo"].Value = StrModelo;

                Comando.Parameters.Add("@VchAnio", SqlDbType.VarChar, 10);
                Comando.Parameters["@VchAnio"].Value = StrAnio;

                Comando.Parameters.Add("@VchNoDePlaca", SqlDbType.VarChar, 20);
                Comando.Parameters["@VchNoDePlaca"].Value = StrNoDePLaca;
                oConexion = new Conexion();
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
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ActualizarVehiculo";

                Comando.Parameters.Add("@UidVehiculo", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidVehiculo"].Value = UID;

                Comando.Parameters.Add("@UidTipoDeVehiculo", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidTipoDeVehiculo"].Value = UidTipoDeVehiculo;

                Comando.Parameters.Add("@BINumeroDeSerie", SqlDbType.VarChar, 50);
                Comando.Parameters["@BINumeroDeSerie"].Value = LngNumeroDeSerie;

                Comando.Parameters.Add("@IntCilindradaDeMotor", SqlDbType.Int);
                Comando.Parameters["@IntCilindradaDeMotor"].Value = IntCilindrada;

                Comando.Parameters.Add("@VchMarca", SqlDbType.VarChar, 30);
                Comando.Parameters["@VchMarca"].Value = StrMarca;

                Comando.Parameters.Add("@VchColor", SqlDbType.VarChar, 30);
                Comando.Parameters["@VchColor"].Value = StrColor;

                Comando.Parameters.Add("@VchModelo", SqlDbType.VarChar, 10);
                Comando.Parameters["@VchModelo"].Value = StrModelo;

                Comando.Parameters.Add("@VchAnio", SqlDbType.VarChar, 10);
                Comando.Parameters["@VchAnio"].Value = StrAnio;

                Comando.Parameters.Add("@VchNoDePlaca", SqlDbType.VarChar, 20);
                Comando.Parameters["@VchNoDePlaca"].Value = StrNoDePLaca;
                oConexion = new Conexion();
                resultado = oConexion.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool AgragaVehiculoSucursal()
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregaVehiculoSucursal";

                Comando.Parameters.Add("@UidVehiculo", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidVehiculo"].Value = UID;

                Comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSucursal"].Value = oSucursal.ID;
                oConexion = new Conexion();
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
