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
    public class VMMonedero
    {
        #region Propiedades
        DBMonedero oDBMonedero;
        Conexion oConexion;
        private Guid _UidMonedero;

        public Guid UidMonedero
        {
            get { return _UidMonedero; }
            set { _UidMonedero = value; }
        }
        private Guid _UidPropietario;

        public Guid UidPropietario
        {
            get { return _UidPropietario; }
            set { _UidPropietario = value; }
        }
        private decimal _MMonto;

        public decimal MMonto
        {
            get { return _MMonto; }
            set { _MMonto = value; }
        }
        private DateTime _dtmFechaDeRegistro;

        public DateTime DtmFechaDeRegistro
        {
            get { return _dtmFechaDeRegistro; }
            set { _dtmFechaDeRegistro = value; }
        }

        private long _lngFolio;

        public long LngFolio
        {
            get { return _lngFolio; }
            set { _lngFolio = value; }
        }

        private List<VMMonedero> _ListaDeMovimientos;

        public List<VMMonedero> ListaDeMoviento
        {
            get { return _ListaDeMovimientos; }
            set { _ListaDeMovimientos = value; }
        }

        private string _strcolor;

        public string strcolor
        {
            get { return _strcolor; }
            set { _strcolor = value; }
        }


        #region Propiedades de movimientos
        private Guid _UidTipoDeMovimiento;

        public Guid UidTipoDeMovimiento
        {
            get { return _UidTipoDeMovimiento; }
            set { _UidTipoDeMovimiento = value; }
        }


        private Guid _UidConcepto;

        public Guid UidConcepto
        {
            get { return _UidConcepto; }
            set { _UidConcepto = value; }
        }


        private Guid _UidDireccionDeOperacion;

        public Guid UidDireccionDeOperacion
        {
            get { return _UidDireccionDeOperacion; }
            set { _UidDireccionDeOperacion = value; }
        }


        private string _strConcepto;

        public string StrConcepto
        {
            get { return _strConcepto; }
            set { _strConcepto = value; }
        }

        private string _strMovimiento;

        public string StrMovimiento
        {
            get { return _strMovimiento; }
            set { _strMovimiento = value; }
        }

        private Guid _UidOrenSucursal;

        public Guid uidOrdenSucursal
        {
            get { return _UidOrenSucursal; }
            set { _UidOrenSucursal = value; }
        }

        #endregion

        #endregion


        #region Metodos

        public void ObtenerMonedero(Guid UidUsuario)
        {
            oDBMonedero = new DBMonedero();
            foreach (DataRow item in oDBMonedero.ObtenerMonedero(UidUsuario).Rows)
            {
                UidMonedero = new Guid(item["UidMonedero"].ToString());
                UidUsuario = new Guid(item["UidUsuario"].ToString());
                MMonto = decimal.Parse(item["MMonto"].ToString());
                DtmFechaDeRegistro = DateTime.Parse(item["dtmfechadecreacion"].ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool MovimientoMonedero()
        {
            bool respuesta = false;
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_MovimientosMonedero";

                if (UidPropietario != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidUsuario"].Value = UidPropietario;
                }
                else
                {
                    Comando.Parameters.Add("@UidOrdenSucursal", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidOrdenSucursal"].Value = uidOrdenSucursal;
                }

                Comando.Parameters.Add("@MMonto", SqlDbType.Money);
                Comando.Parameters["@MMonto"].Value = MMonto;

                Comando.Parameters.Add("@UidTipoDeMovimiento", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidTipoDeMovimiento"].Value = UidTipoDeMovimiento;

                Comando.Parameters.Add("@UidConcepto", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidConcepto"].Value = UidConcepto;

                if (UidDireccionDeOperacion != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidDireccionDeOperacion", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidDireccionDeOperacion"].Value = UidDireccionDeOperacion;
                }


                oConexion = new Conexion();
                respuesta = oConexion.ModificarDatos(Comando);

            }
            catch (Exception)
            {

                throw;
            }
            return respuesta;
        }

        public void ObtenerMovimientosMonedero()
        {
            oDBMonedero = new DBMonedero();
            ListaDeMoviento = new List<VMMonedero>();
            foreach (DataRow item in oDBMonedero.ObtenerMovimientos(UidPropietario).Rows)
            {
                string Strcolor = string.Empty;
                switch (item["concepto"].ToString())
                {
                    case "Rembolso":
                        Strcolor = "#fafad2";
                        break;
                    case "Pago en GoDeliverix":
                        Strcolor = "#FFB6AE";
                        break;
                    case "Deposito bancario":
                        Strcolor = "#B0F2C2";
                        break;
                    case "Tarjeta prepago":
                        Strcolor = "#B0F2C2";
                        break;
                    default:
                        break;
                }
                ListaDeMoviento.Add(
                    new VMMonedero()
                    {
                        LngFolio = long.Parse(item["LngFolio"].ToString()),
                        StrConcepto = item["concepto"].ToString(),
                        StrMovimiento = item["tipoMovimiento"].ToString(),
                        strcolor = Strcolor,
                        DtmFechaDeRegistro = DateTime.Parse(item["DtmFechaRegistro"].ToString()),
                        MMonto = decimal.Parse(item["MMonto"].ToString())
                    });
            }
        }
        #endregion
    }
}
