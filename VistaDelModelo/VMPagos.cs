using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBControl;
namespace VistaDelModelo
{
    public class VMPagos
    {
        #region Propiedades

        Conexion oConexion;
        DBPago oDbPago;
        private Guid _UidPago;

        public Guid UidPago
        {
            get { return _UidPago; }
            set { _UidPago = value; }
        }

        private Guid _UidOrden;

        public Guid UidOrden
        {
            get { return _UidOrden; }
            set { _UidOrden = value; }
        }

        private decimal _DmlMMonto;

        public decimal MMonto
        {
            get { return _DmlMMonto; }
            set { _DmlMMonto = value; }
        }

        private string _StrEstatus;

        public string StrEstatus
        {
            get { return _StrEstatus; }
            set { _StrEstatus = value; }
        }

        private long _LngFolio;

        public long LngFolio
        {
            get { return _LngFolio; }
            set { _LngFolio = value; }
        }


        private Guid _UidOrdenFormaDeCobro;

        public Guid UidOrdenFormaDeCobro
        {
            get { return _UidOrdenFormaDeCobro; }
            set { _UidOrdenFormaDeCobro = value; }
        }

        private DateTime _dtmFechaRegistro;

        public DateTime DtmFechaRegistro
        {
            get { return _dtmFechaRegistro; }
            set { _dtmFechaRegistro = value; }
        }

        private Guid _UidEstatusDeCobro;

        public Guid UidEstatusDeCobro
        {
            get { return _UidEstatusDeCobro; }
            set { _UidEstatusDeCobro = value; }
        }


        #region Variables pago con tarjeta
        private Guid _UidPagoTarjeta;

        public Guid UidPagoTarjeta
        {
            get { return _UidPagoTarjeta; }
            set { _UidPagoTarjeta = value; }
        }



        private string _strTipoDeTarjeta;

        public string StrTipoDeTarjeta
        {
            get { return _strTipoDeTarjeta; }
            set { _strTipoDeTarjeta = value; }
        }

        private string _VchTipoDeOperacion;

        public string StrTipoDeOperacion
        {
            get { return _VchTipoDeOperacion; }
            set { _VchTipoDeOperacion = value; }
        }



        private string _strReferencia;



        public string StrReferencia
        {
            get { return _strReferencia; }
            set { _strReferencia = value; }
        }

        private string _VchEstatusPago;

        public string VchEstatusPago
        {
            get { return _VchEstatusPago; }
            set { _VchEstatusPago = value; }
        }

        private string _strEstatusPagoTarjeta;

        public string StrEstatusPagosTarjeta
        {
            get { return _strEstatusPagoTarjeta; }
            set { _strEstatusPagoTarjeta = value; }
        }

        private string _strFolioPagoConTarjeta;

        public string StrFolioPagoTarjeta
        {
            get { return _strFolioPagoConTarjeta; }
            set { _strFolioPagoConTarjeta = value; }
        }
        private bool _bUsabilidadDeFolioPagoTarjeta;

        public bool BUsoFolioPagoTarjeta
        {
            get { return _bUsabilidadDeFolioPagoTarjeta; }
            set { _bUsabilidadDeFolioPagoTarjeta = value; }
        }

        #endregion

        #endregion
        public VMPagos()
        {
        }


        /// <summary>
        /// Agrega el pago a la tabla general del control de cobros del cliente.
        /// </summary>
        /// <returns></returns>
        public bool IntegrarPago()
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ControlDePagos";

                Comando.Parameters.Add("@UidOrdenFormaDeCobro", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidOrdenFormaDeCobro"].Value = UidPago;

                Comando.Parameters.Add("@UidFormaDeCobro", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidFormaDeCobro"].Value = UidOrdenFormaDeCobro;

                Comando.Parameters.Add("@UidOrden", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidOrden"].Value = UidOrden;

                Comando.Parameters.Add("@MMonto", SqlDbType.Decimal);
                Comando.Parameters["@MMonto"].Value = MMonto;

                Comando.Parameters.Add("@UidEstatusDeCobro", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidEstatusDeCobro"].Value = UidEstatusDeCobro;

                oConexion = new Conexion();
                resultado = oConexion.ModificarDatos(Comando);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public void ObtenerFolioPagoTarjeta()
        {
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_ObtenerFolio";

                oConexion = new Conexion();
                foreach (DataRow item in oConexion.Busquedas(Comando).Rows)
                {
                    StrFolioPagoTarjeta = item["bifolio"].ToString();
                    BUsoFolioPagoTarjeta = bool.Parse(item["buso"].ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool BitacoraEstatusCobro(string uidOrden, string estatus)
        {
            bool resultado = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_bitacoraEstatusCobro";

                Comando.Parameters.Add("@UidOrden", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidOrden"].Value = new Guid(uidOrden);

                Comando.Parameters.Add("@UidEstatus", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidEstatus"].Value = new Guid(estatus);

                oConexion = new Conexion();
                resultado = oConexion.ModificarDatos(Comando);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;

        }
        public string ObtenerEstatusDeCobro()
        {
            oDbPago = new DBPago();
            string resultado = string.Empty;
            try
            {
                foreach (DataRow item in oDbPago.ObtenerEstatusPago(UidOrden).Rows)
                {
                    resultado = item["Estatus"].ToString();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return resultado;

        }
        /// <summary>
        /// Valida si el pago ya se ha efectuado.
        /// </summary>
        /// <param name="UidOrdenFormaDeCobro"></param>
        /// <returns></returns>
        public bool ValidaPagoTarjeta(string UidOrdenFormaDeCobro)
        {
            bool respuesta = false;
            try
            {
                oDbPago = new DBPago();
                if (oDbPago.ValidaPagoConTarjeta(UidFormaDeCobro: UidOrdenFormaDeCobro).Rows.Count == 1)
                {
                    respuesta = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return respuesta;
        }
        public bool AgregarInformacionTarjeta(string Autorizacion, string reference, DateTime HoraTransaccion, string response, string cc_type, string tp_operation, string nb_company, string nb_merchant, string id_url, string cd_error, string nb_error, string cc_number, string cc_mask,string FolioPago, string monto = "0.0")
        {
            SqlCommand Comando = new SqlCommand();
            bool resultado = false;
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregaInformacionPagoConTarjeta";

                Comando.Parameters.Add("@FolioPago", SqlDbType.VarChar, 50);
                Comando.Parameters["@FolioPago"].Value = FolioPago;
                
                Comando.Parameters.Add("@cc_number", SqlDbType.VarChar, 50);
                Comando.Parameters["@cc_number"].Value = cc_number;
                
                Comando.Parameters.Add("@cc_mask", SqlDbType.VarChar, 50);
                Comando.Parameters["@cc_mask"].Value = cc_mask;

                Comando.Parameters.Add("@nb_company", SqlDbType.VarChar, 50);
                Comando.Parameters["@nb_company"].Value = nb_company;

                Comando.Parameters.Add("@nb_merchant", SqlDbType.VarChar, 50);
                Comando.Parameters["@nb_merchant"].Value = nb_merchant;

                Comando.Parameters.Add("@id_url", SqlDbType.VarChar, 50);
                Comando.Parameters["@id_url"].Value = id_url;

                Comando.Parameters.Add("@cd_error", SqlDbType.VarChar, 200);
                Comando.Parameters["@cd_error"].Value = cd_error;

                Comando.Parameters.Add("@nb_error", SqlDbType.VarChar, 200);
                Comando.Parameters["@nb_error"].Value = nb_error;

                Comando.Parameters.Add("@UidOrdenFormaDeCobro", SqlDbType.VarChar, 50);
                Comando.Parameters["@UidOrdenFormaDeCobro"].Value = reference;

                Comando.Parameters.Add("@UidPago", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidPago"].Value = Guid.NewGuid();

                Comando.Parameters.Add("@FechaRegistro", SqlDbType.DateTime);
                Comando.Parameters["@FechaRegistro"].Value = HoraTransaccion;

                Comando.Parameters.Add("@VchReferencia", SqlDbType.Text);
                Comando.Parameters["@VchReferencia"].Value = reference;

                Comando.Parameters.Add("@VchEstatusPago", SqlDbType.VarChar, 50);
                Comando.Parameters["@VchEstatusPago"].Value = response;

                Comando.Parameters.Add("@VchTipoDeTarjeta", SqlDbType.VarChar, 200);
                Comando.Parameters["@VchTipoDeTarjeta"].Value = cc_type;

                Comando.Parameters.Add("@VchTipoDeOperacion", SqlDbType.VarChar, 100);
                Comando.Parameters["@VchTipoDeOperacion"].Value = tp_operation;

                Comando.Parameters.Add("@MMonto", SqlDbType.Money);
                Comando.Parameters["@MMonto"].Value = decimal.Parse(monto);

                Comando.Parameters.Add("@Autorizacion", SqlDbType.VarChar, 10);
                Comando.Parameters["@Autorizacion"].Value = Autorizacion;

                oConexion = new Conexion();
                resultado = oConexion.ModificarDatos(Comando);

            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        public void ObtenerPagosCliente()
        {

        }

        /// <summary>
        /// Retorna el estatus del registro del pago con tarjeta
        /// </summary>
        public void ObtenerEstatusPagoConTarjeta()
        {
            try
            {
                oDbPago = new DBPago();
                foreach (DataRow item in oDbPago.ObtenerPagoTarjeta(UidOrdenFormaDeCobro).Rows)
                {
                    StrEstatusPagosTarjeta = item["VchEstatus"].ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ProductoIncluyePagoConTarjeta(string UidEmpresa) 
        { 
        
        }

        /// <summary>
        /// Recupera el tipo de pago de una orden
        /// </summary>
        /// <param name="UidOrden"></param>
        public void ObtenerElTipoDePago(string UidOrden) { }
    }
}
