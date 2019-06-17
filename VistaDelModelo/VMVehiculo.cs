using DBControl;
using Modelo.Usuario;
using Modelo;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace VistaDelModelo
{
    public class VMVehiculo
    {
        #region Propiedades
        Conexion oConexion = new Conexion();
        DbVehiculo oDBVehiculo = new DbVehiculo();
        Vehiculo oVehiculo = new Vehiculo();
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

        public List<VMVehiculo> ListaDeVehiculos = new List<VMVehiculo>();
        #endregion
        #region Metodos
        public bool GuardarVehiculo(Guid UidVehiculo, Guid UidEmpresa, string No_Serie, string Cilindrada, string Marca, string modelo, string color, string anio, string placa, Guid uidTipodeVehiculo)
        {
            bool resultado = false;
            try
            {
                oVehiculo = new Vehiculo();
                oVehiculo.UID = UidVehiculo;
                oVehiculo.IntCilindrada = int.Parse(Cilindrada);
                oVehiculo.LngNumeroDeSerie = No_Serie;
                oVehiculo.StrAnio = anio;
                oVehiculo.StrColor = color;
                oVehiculo.StrMarca = Marca;
                oVehiculo.StrModelo = modelo;
                oVehiculo.StrNoDePLaca = placa;
                oVehiculo.UidTipoDeVehiculo = uidTipodeVehiculo;
                oVehiculo.UIDEmpresa = UidEmpresa;
                resultado = oVehiculo.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public bool ActualizaVehiculo(Guid UidVehiculo, string No_Serie, string Cilindrada, string Marca, string modelo, string color, string anio, string placa, Guid uidTipodeVehiculo)
        {
            bool resultado = false;
            try
            {
                oVehiculo = new Vehiculo();
                oVehiculo.UID = UidVehiculo;
                oVehiculo.IntCilindrada = int.Parse(Cilindrada);
                oVehiculo.LngNumeroDeSerie = No_Serie;
                oVehiculo.StrAnio = anio;
                oVehiculo.StrColor = color;
                oVehiculo.StrMarca = Marca;
                oVehiculo.UidTipoDeVehiculo = uidTipodeVehiculo;
                oVehiculo.StrModelo = modelo;
                oVehiculo.StrNoDePLaca = placa;
                resultado = oVehiculo.Actualizar();
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        public void BuscarVehiculo(Guid UidVehiculo = new Guid(), Guid UidEmpresa = new Guid(), string No_Serie = "", string Cilindrada = "", string Marca = "", string modelo = "", string color = "", string anio = "", string placa = "", Guid uidTipodeVehiculo = new Guid())
        {
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_BuscarVehiculo";

                if (UidVehiculo != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidVehiculo", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidVehiculo"].Value = UidVehiculo;
                }
                if (UidEmpresa != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidEmpresa"].Value = UidEmpresa;
                }
                if (!string.IsNullOrEmpty(No_Serie))
                {
                    Comando.Parameters.Add("@BINumeroDeSerie", SqlDbType.VarChar, 50);
                    Comando.Parameters["@BINumeroDeSerie"].Value = No_Serie;
                }
                if (!string.IsNullOrEmpty(Cilindrada))
                {
                    Comando.Parameters.Add("@IntCilindradaDeMotor", SqlDbType.Int);
                    Comando.Parameters["@IntCilindradaDeMotor"].Value = Cilindrada;
                }
                if (!string.IsNullOrEmpty(Marca))
                {
                    Comando.Parameters.Add("@VchMarca", SqlDbType.VarChar, 30);
                    Comando.Parameters["@VchMarca"].Value = Marca;
                }
                if (!string.IsNullOrEmpty(color))
                {
                    Comando.Parameters.Add("@VchColor", SqlDbType.VarChar, 30);
                    Comando.Parameters["@VchColor"].Value = color;
                }
                if (!string.IsNullOrEmpty(modelo))
                {
                    Comando.Parameters.Add("@VchModelo", SqlDbType.VarChar, 10);
                    Comando.Parameters["@VchModelo"].Value = modelo;
                }
                if (!string.IsNullOrEmpty(anio))
                {
                    Comando.Parameters.Add("@VchAnio", SqlDbType.VarChar, 10);
                    Comando.Parameters["@VchAnio"].Value = anio;
                }
                if (!string.IsNullOrEmpty(placa))
                {
                    Comando.Parameters.Add("@VchNoDePlaca", SqlDbType.VarChar, 20);
                    Comando.Parameters["@VchNoDePlaca"].Value = placa;
                }
                if (uidTipodeVehiculo != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidTipoDeVehiculo", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidTipoDeVehiculo"].Value = uidTipodeVehiculo;
                }
                if (UidVehiculo != Guid.Empty)
                {
                    foreach (DataRow item in oConexion.Busquedas(Comando).Rows)
                    {
                        UID = new Guid(item["UidVehiculo"].ToString());
                        LngNumeroDeSerie = item["BINumeroDeSerie"].ToString().ToUpper();
                        IntCilindrada = int.Parse(item["IntCilindradaDeMotor"].ToString());
                        StrMarca = item["VchMarca"].ToString().ToUpper();
                        StrModelo = item["VchModelo"].ToString().ToUpper();
                        StrColor = item["VchColor"].ToString().ToUpper();
                        StrAnio = item["VchAnio"].ToString().ToUpper();
                        StrNoDePLaca = item["VchNoDePlaca"].ToString().ToUpper();
                        UidTipoDeVehiculo = new Guid(item["uidtipodevehiculo"].ToString());
                    }
                }
                else
                {
                    ListaDeVehiculos.Clear();
                    foreach (DataRow item in oConexion.Busquedas(Comando).Rows)
                    {
                        ListaDeVehiculos.Add(new VMVehiculo()
                        {
                            UID = new Guid(item["UidVehiculo"].ToString()),
                            LngNumeroDeSerie = item["BINumeroDeSerie"].ToString().ToUpper(),
                            IntCilindrada = int.Parse(item["IntCilindradaDeMotor"].ToString()),
                            StrMarca = item["VchMarca"].ToString().ToUpper(),
                            StrModelo = item["VchModelo"].ToString().ToUpper(),
                            StrColor = item["VchColor"].ToString().ToUpper(),
                            StrAnio = item["VchAnio"].ToString().ToUpper(),
                            StrNoDePLaca = item["VchNoDePlaca"].ToString().ToUpper()
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EliminarRelacionSucursal(Guid uid)
        {
            oDBVehiculo.EliminarRelacionSucursal(uid.ToString());
        }

        public DataTable ObtenerTipoDeVehiculo()
        {
            return oDBVehiculo.TiposDeVehiculos();
        }
        public DataTable ObtenerTipoDeVehiculoFitros()
        {
            return oDBVehiculo.TiposDeVehiculosBusqueda();
        }

        public bool RelacionConSucursal(Guid UidVehiculo, Guid UidSucursal)
        {
            bool resultado = false;
            try
            {
                oVehiculo = new Vehiculo();
                oVehiculo.UID = UidVehiculo;
                oVehiculo.oSucursal = new Sucursal() { ID = UidSucursal };
                resultado = oVehiculo.AgragaVehiculoSucursal();
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }


        #region Windows PresentationFoundation
        public void ObtenerVehiculosDeSucursal(string UidLicencia)
        {
            ListaDeVehiculos.Clear();
            oDBVehiculo = new DbVehiculo();
            foreach (DataRow item in oDBVehiculo.Obtener_VehiculosEnSucursal(UidLicencia).Rows)
            {
                ListaDeVehiculos.Add(new VMVehiculo()
                {
                    UID = new Guid(item["UidVehiculo"].ToString()),
                    LngNumeroDeSerie = item["BINumeroDeSerie"].ToString().ToUpper(),
                    IntCilindrada = int.Parse(item["IntCilindradaDeMotor"].ToString()),
                    StrMarca = item["VchMarca"].ToString().ToUpper(),
                    StrModelo = item["VchModelo"].ToString().ToUpper(),
                    StrColor = item["VchColor"].ToString().ToUpper(),
                    StrAnio = item["VchAnio"].ToString().ToUpper(),
                    StrNoDePLaca = item["VchNoDePlaca"].ToString().ToUpper()
                });
            }
        }
        public void EliminarDeLista(Guid UidVehiculo)
        {
            if (ListaDeVehiculos.Exists(V => V.UID == UidVehiculo))
            {
                var objeto = ListaDeVehiculos.Find(V => V.UID == UidVehiculo);
                ListaDeVehiculos.Remove(objeto);
            }
        }
        #endregion
        #endregion

    }
}
