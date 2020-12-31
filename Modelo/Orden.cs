using DBControl;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class Orden
    {
        #region Propiedades
        Conexion Datos = new Conexion();
        private Guid _UidOrden;

        public Guid Uidorden
        {
            get { return _UidOrden; }
            set { _UidOrden = value; }
        }
        private Guid _uidProducto;

        public Guid UidProducto
        {
            get { return _uidProducto; }
            set { _uidProducto = value; }
        }
        private decimal _dbTotal;

        public decimal MTotal
        {
            get { return _dbTotal; }
            set { _dbTotal = value; }
        }

        private long _LngCodigoDeEntrega;

        public long LngCodigoDeEntrega
        {
            get { return _LngCodigoDeEntrega; }
            set { _LngCodigoDeEntrega = value; }
        }

        public decimal TotalSucursal { get; set; }
        private Guid _Uidusuario;

        public Guid UidUsuario
        {
            get { return _Uidusuario; }
            set { _Uidusuario = value; }
        }
        private Guid _UidDireccion;

        public Guid UidDireccion
        {
            get { return _UidDireccion; }
            set { _UidDireccion = value; }
        }
        private Guid _Uidsucursal;

        public Guid UidSucursal
        {
            get { return _Uidsucursal; }
            set { _Uidsucursal = value; }
        }
        private long _lngFolio;

        public long LngFolio
        {
            get { return _lngFolio; }
            set { _lngFolio = value; }
        }
        public Guid UidRelacionOrdenSucursal { get; set; }
        public Guid Estatus { get; set; }
        public string cTipoDeSucursal { get; set; }

        public Guid uidLicencia { get; set; }



        public Mensaje oMensaje;


        #endregion

        #region Metodos
        public bool GuardarOrden(Guid UidTarifario)
        {
            bool resultado = false;
            Datos = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_Agrega_Orden";

                Comando.Parameters.Add("@UidOrden", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidOrden"].Value = Uidorden;

                Comando.Parameters.Add("@MTotal", SqlDbType.Money);
                Comando.Parameters["@MTotal"].Value = MTotal;

                Comando.Parameters.Add("@BiCodigoDeEntrega", SqlDbType.BigInt);
                Comando.Parameters["@BiCodigoDeEntrega"].Value = LngCodigoDeEntrega;

                Comando.Parameters.Add("@MTotalSucursal", SqlDbType.Money);
                Comando.Parameters["@MTotalSucursal"].Value = TotalSucursal;

                Comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidUsuario"].Value = UidUsuario;

                Comando.Parameters.Add("@UidDireccion", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidDireccion"].Value = UidDireccion;

                Comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSucursal"].Value = UidSucursal;

                Comando.Parameters.Add("@RelacionDeOrden", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@RelacionDeOrden"].Value = UidRelacionOrdenSucursal;

                Comando.Parameters.Add("@TarifarioDistribuidora", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@TarifarioDistribuidora"].Value = UidTarifario;

                resultado = Datos.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
        //Indica que producto se asigno a la orden
        public bool GuardarProductos(Guid UidOrden, Guid UidProducto, int cantidad, string STRCOSTO, Guid UidSucursal, Guid UidRegistroEncarrito, Guid UidNota, string strMensaje, string UidTarifario = "")
        {
            bool resultado = false;
            Datos = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "Agregar_SeccionProductoEnOrden";

                Comando.Parameters.Add("@UidOrden", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidOrden"].Value = UidOrden;

                Comando.Parameters.Add("@UidNota", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidNota"].Value = UidNota;

                Comando.Parameters.Add("@StrNota", SqlDbType.VarChar, 300);
                Comando.Parameters["@StrNota"].Value = strMensaje;

                Comando.Parameters.Add("@UidRegistroEnCarrito", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidRegistroEnCarrito"].Value = UidRegistroEncarrito;

                Comando.Parameters.Add("@UidProduto", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidProduto"].Value = UidProducto;

                Comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSucursal"].Value = UidSucursal;

                Comando.Parameters.Add("@IntCantidad", SqlDbType.Int);
                Comando.Parameters["@IntCantidad"].Value = cantidad;

                Comando.Parameters.Add("@MTotal", SqlDbType.Money);
                Comando.Parameters["@MTotal"].Value = double.Parse(STRCOSTO);

                Comando.Parameters.Add("@TarifarioDistribuidora", SqlDbType.VarChar, 40);
                Comando.Parameters["@TarifarioDistribuidora"].Value = UidTarifario;


                resultado = Datos.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool GuardaEstatus(Guid UidLicencia, string Parametro, Guid UidSucursal = new Guid())
        {
            bool resultado = false;
            Datos = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_BitacoraEstatusDeOrden";

                if (Uidorden != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidOrden", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidOrden"].Value = Uidorden;
                }

                if (oMensaje.Uid != Guid.Empty && oMensaje != null)
                {
                    Comando.Parameters.Add("@UidMensaje", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidMensaje"].Value = oMensaje.Uid;
                }

                if (LngFolio != 0)
                {
                    Comando.Parameters.Add("@IntFolioOrden", SqlDbType.BigInt);
                    Comando.Parameters["@IntFolioOrden"].Value = LngFolio;
                }
                if (UidSucursal != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidSucursal"].Value = UidSucursal;
                }
                if (UidLicencia != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidLicencia", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidLicencia"].Value = UidLicencia;
                }

                Comando.Parameters.Add("@UidEstatusEnOrden", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidEstatusEnOrden"].Value = Estatus;

                Comando.Parameters.Add("@Parametro", SqlDbType.Char, 1);
                Comando.Parameters["@Parametro"].Value = char.Parse(Parametro);

                resultado = Datos.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public DataTable InformacionDeOrdenesUltimoTurno(string UidSucursal)
        {
            Datos = new Conexion();
            string query = "select os.UidRelacionOrdenSucursal,os.IntFolio,os.MTotalSucursal,dbo.ObtenUltimoEstatusDeOrdenEnSucursal(os.UidRelacionOrdenSucursal,'s') as Estatus from OrdenSucursal os where  os.UidTurnoSuministradora in     (select top 1 UidTurnoSuministradora from TurnoSuministradora where UidSucursal = '" + UidSucursal + "' order by DtmHoraInicio desc)";
            return Datos.Consultas(query);
        }
        public DataTable InformacionDeOrdenesTurnoCallCenter(string v, Guid uidTurno)
        {
            Datos = new Conexion();
            string query = "select os.UidRelacionOrdenSucursal,os.IntFolio,os.MTotalSucursal,dbo.EstatusActualDeOrden(os.UidRelacionOrdenSucursal) as Estatus from OrdenSucursal os  where os.UidTurnoSuministradora in (select top 1 rt.UidTurnoSuministradora from RelacionTurnoSuministradoraTurnoCallcenter rt inner join TurnoSuministradora ts on ts.UidTurnoSuministradora = rt.UidTurnoSuministradora inner join TurnoCallCenter tc on tc.UidTunoCallCenter = rt.UidTurnoCallCenter where ts.UidSucursal = '" + v + "' and tc.UidTunoCallCenter = '" + uidTurno.ToString() + "' )";
            return Datos.Consultas(query);
        }
        public bool AsociarOrdenConDistribuidora(Guid uidorden, Guid iD, Guid Codigo, Guid uidLicencia, long LngFolio)
        {
            bool resultado = false;
            Datos = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "Asp_AgregaRelacionOrdenSucursalDistribuidora";

                Comando.Parameters.Add("@UidLicencia", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidLicencia"].Value = uidLicencia;

                if (uidorden != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidOrden", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidOrden"].Value = uidorden;
                }
                if (iD != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidSucursal"].Value = iD;
                }
                if (Codigo != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidCodigo", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidCodigo"].Value = Codigo;
                }

                if (LngFolio != 0)
                {
                    Comando.Parameters.Add("@BIFolio", SqlDbType.BigInt);
                    Comando.Parameters["@BIFolio"].Value = LngFolio;
                }
                resultado = Datos.ModificarDatos(Comando);
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public bool AgregaEstatusEnOrdenSucursal()
        {
            bool resultado = false;
            Datos = new Conexion();
            SqlCommand Comando = new SqlCommand();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_AgregarEstatusOrdenSucursal";

                if (Uidorden != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidOrden", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidOrden"].Value = Uidorden;
                }
                if (oMensaje.Uid != Guid.Empty && oMensaje != null)
                {
                    Comando.Parameters.Add("@UidMensaje", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidMensaje"].Value = oMensaje.Uid;
                }
                if (LngFolio != 0)
                {
                    Comando.Parameters.Add("@IntFolioOrden", SqlDbType.BigInt);
                    Comando.Parameters["@IntFolioOrden"].Value = LngFolio;
                }
                if (uidLicencia != Guid.Empty)
                {
                    Comando.Parameters.Add("@UidLicencia", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidLicencia"].Value = uidLicencia;
                }
                Comando.Parameters.Add("@UidEstatusEnOrden", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidEstatusEnOrden"].Value = Estatus;

                Comando.Parameters.Add("@TipoDeSucursal", SqlDbType.Char, 1);
                Comando.Parameters["@TipoDeSucursal"].Value = char.Parse(cTipoDeSucursal);

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
