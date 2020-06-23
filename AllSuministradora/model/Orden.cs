using AllSuministradora.Recursos;
using AllSuministradora.VistasDelModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using VistaDelModelo;

namespace AllSuministradora.model
{
    public class Orden : NotifyBase
    {
        #region Propiedades
        private Guid _UidOrden;
        public Guid UidOrden
        {
            get { return _UidOrden; }
            set { _UidOrden = value; OnpropertyChanged("UidOrden"); }
        }
        private Guid _UidSucursal;

        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; OnpropertyChanged("UidSucursal"); }
        }


        private string _strNombreComercialEmpresa;
        public string NombreComercialEmpresa
        {
            get { return _strNombreComercialEmpresa; }
            set { _strNombreComercialEmpresa = value; OnpropertyChanged("NombreComercialEmpresa"); }
        }

        private string _strIdentificadorSucursal;
        public string StrIdentificadorSucursal
        {
            get { return _strIdentificadorSucursal; }
            set { _strIdentificadorSucursal = value; OnpropertyChanged("StrIdentificadorSucursal"); }
        }

        private decimal _dclTotal;
        public decimal DCLTotal
        {
            get { return _dclTotal; }
            set { _dclTotal = value; OnpropertyChanged("DCLTotal"); }
        }

        private decimal _TotalDeEnvio;
        public decimal TotalDeEnvio
        {
            get { return _TotalDeEnvio; }
            set { _TotalDeEnvio = value; OnpropertyChanged("TotalDeEnvio"); }
        }

        private string _StrTipoDePago;
        public string StrTipoDePago
        {
            get { return _StrTipoDePago; }
            set { _StrTipoDePago = value; OnpropertyChanged("StrTipoDePago"); }
        }

        private string _NombreRepartidor;
        public string StrNombreRepartidor
        {
            get { return _NombreRepartidor; }
            set { _NombreRepartidor = value; OnpropertyChanged("StrNombreRepartidor"); }
        }

        private string _dtmFechaDeOrden;
        public string DtmFechaDeOrden
        {
            get { return _dtmFechaDeOrden; }
            set { _dtmFechaDeOrden = value; OnpropertyChanged("DtmFechaDeOrden"); }
        }
        private string _strEstatusPagoOrden;

        public string StrEstatusPagoOrden
        {
            get { return _strEstatusPagoOrden; }
            set { _strEstatusPagoOrden = value; OnpropertyChanged("StrEstatusPagoOrden"); }
        }

        private long _lbgFolio;

        public long LngFolio
        {
            get { return _lbgFolio; }
            set { _lbgFolio = value; OnpropertyChanged("LngFolio"); }
        }

        private List<Producto> _listaDeProductos;

        public List<Producto> ListaDeProductos
        {
            get { return _listaDeProductos; }
            set { _listaDeProductos = value; OnpropertyChanged("ListaDeProductos"); }
        }

        private List<Mensaje> _ListaDeMensajes;

        public List<Mensaje> ListaDeMensajes
        {
            get { return _ListaDeMensajes; }
            set { _ListaDeMensajes = value; OnpropertyChanged("ListaDeMensajes"); }
        }
        private Mensaje _SMensaje;

        public Mensaje SMensaje
        {
            get { return _SMensaje; }
            set { _SMensaje = value; OnpropertyChanged("SMensaje"); }
        }

        #endregion

        #region Controles
        private Visibility _VControlConfirmar;

        public Visibility VControlConfirmar
        {
            get { return _VControlConfirmar; }
            set { _VControlConfirmar = value; OnpropertyChanged("VControlConfirmar"); }
        }
        private Visibility _VCancelarConfirmar;

        public Visibility VCancelarConfirmar
        {
            get { return _VCancelarConfirmar; }
            set { _VCancelarConfirmar = value; }
        }

        #endregion
        #region Comandos
        private ICommand _CmdCancelar;

        public ICommand CmdCancelar
        {
            get { return _CmdCancelar; }
            set { _CmdCancelar = value; }
        }
        private ICommand _cmdConfirmarOrden;

        public ICommand CmdConfirmarOrden
        {
            get { return _cmdConfirmarOrden; }
            set { _cmdConfirmarOrden = value; }
        }
        private ICommand _cmdTerminar;

        public ICommand CMdTerminarOrden
        {
            get { return _cmdTerminar; }
            set { _cmdTerminar = value; }
        }
        private ICommand _CmdEntregarOrden;

        public ICommand CMDEntregarOrden
        {
            get { return _CmdEntregarOrden; }
            set { _CmdEntregarOrden = value; OnpropertyChanged("CMDEntregarOrden"); }
        }

        private ICommand _cmdConfirmarCancelacion;

        public ICommand CmdConfirmarCancelacion
        {
            get { return _cmdConfirmarCancelacion; }
            set { _cmdConfirmarCancelacion = value; OnpropertyChanged("CmdConfirmarCancelacion"); }
        }

        #endregion
        #region Constructor
        public Orden()
        {
            VCancelarConfirmar = Visibility.Hidden;
            VControlConfirmar = Visibility.Hidden;

            CmdCancelar = new CommandBase(param => Cancelar());
            CmdConfirmarOrden = new CommandBase(param => ConfirmarOrden());
            CMdTerminarOrden = new CommandBase(param => TerminarOrden());
            CMDEntregarOrden = new CommandBase(param => EntregarOrden());
            CmdConfirmarCancelacion = new CommandBase(param => ConfirmarCancelacion());
        }
        #endregion
        #region 
        protected void ConfirmarCancelacion()
        {
            if (!string.IsNullOrEmpty(SMensaje.StrMensaje))
            {
                var instance = ControlGeneral.GetInstance();
                var sucursal = instance.VMSucursalesLocal.ListaDeSucursales.Where(x => x.UidSucursal == UidSucursal).FirstOrDefault();
                var MVOrden = new VMOrden();
                MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("EAE7A7E6-3F19-405E-87A9-3162D36CE21B"), "S", sucursal.Licencia.ToString(), UidOrden: UidOrden, UidMensaje: SMensaje.UidMensaje);
                VMMonedero obj = new VMMonedero();
                obj.uidOrdenSucursal = UidOrden;
                obj.UidTipoDeMovimiento = new Guid("E85F0486-1FBE-494C-86A2-BFDDC733CA5D");
                obj.UidConcepto = new Guid("2AABDF7F-EDCE-455F-B775-6283654D7DA0");
                obj.MMonto = DCLTotal;
                obj.MovimientoMonedero();
                instance.Principal.VisibilidadVentanaCancelar = false;
                instance.MVOrdenes.CargaOrdenes();
                MessageBox.Show("Orden cancelada");
            }
            else
            {
                MessageBox.Show("Selecciona un mensaje para cancelar la orden");
            }

        }
        protected void EntregarOrden()
        {
            VMOrden MVOrden = new VMOrden();
            var instance = ControlGeneral.GetInstance();
            Orden obj = instance.MVOrdenes.ListaDeOrdenes.Where(x => x.UidOrden == UidOrden).FirstOrDefault();

            var sucursal = instance.VMSucursalesLocal.ListaDeSucursales.Where(x => x.UidSucursal == UidSucursal).FirstOrDefault();
            MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("E2BAD7D9-9CD0-4698-959D-0A211800545F"), "S", sucursal.Licencia.ToString(), UidOrden: UidOrden);
            MVOrden.AgregaEstatusALaOrden(new Guid("B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7"), UidOrden: UidOrden, UidLicencia: sucursal.Licencia, StrParametro: "S");
            instance.MVOrdenes.StrBusquedaDeOrdenes = "Recolectar";
            instance.MVOrdenes.CargaOrdenes();
            instance.MVOrdenes.oOrdenRepartidor = new Orden();
            instance.MVOrdenes.UidCodigoEntrega = string.Empty;
            MessageBox.Show("Orden entregada");
        }
        protected void TerminarOrden()
        {
            VMOrden MVOrden = new VMOrden();
            var instance = ControlGeneral.GetInstance();
            SucursalItem sucursal = instance.VMSucursalesLocal.ListaDeSucursales.Where(x => x.UidSucursal == UidSucursal).FirstOrDefault();
            MVOrden.AgregaEstatusALaOrden(new Guid("c412d367-7d05-45d8-aeca-b8fabbf129d9"), UidOrden: UidOrden, UidLicencia: sucursal.Licencia, StrParametro: "S");
            instance.Principal.oSeleccionElaboracion = new Orden();
            MessageBox.Show("Orden finalizada");
            instance.MVOrdenes.StrBusquedaDeOrdenes = "Elaborar";
            instance.MVOrdenes.CargaOrdenes();
            instance.Principal.VisibilidadVentnaFinalizar = false;
        }
        protected void Cancelar()
        {
            var instance = ControlGeneral.GetInstance();
            instance.Principal.oOrdenCancelada = new Orden() { UidOrden = UidOrden, UidSucursal = UidSucursal, LngFolio = LngFolio, DCLTotal = DCLTotal };
            var sucursal = instance.VMSucursalesLocal.ListaDeSucursales.Where(x => x.UidSucursal == UidSucursal).FirstOrDefault();
            var MVMensaje = new VMMensaje();
            MVMensaje.Buscar(strLicencia: sucursal.Licencia.ToString());
            instance.Principal.oOrdenCancelada.ListaDeMensajes = new List<Mensaje>();
            foreach (var item in MVMensaje.ListaDeMensajes)
            {
                instance.Principal.oOrdenCancelada.ListaDeMensajes.Add(new Mensaje() { UidMensaje = item.Uid, StrMensaje = item.StrMensaje });
            }
            ;
            instance.Principal.VisibilidadVentanaCancelar = true;
            //instance.MVOrdenes.oSeleccionado = new Orden();
            //instance.MVOrdenes.oSeleccionElaboracion = new Orden();
        }

        protected void ConfirmarOrden()
        {
            VMOrden MVOrden = new VMOrden();
            VMTarifario MVTarifario = new VMTarifario();
            var instance = ControlGeneral.GetInstance();
            SucursalItem sucursal = instance.VMSucursalesLocal.ListaDeSucursales.Where(x => x.UidSucursal == UidSucursal).FirstOrDefault();
            MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("EC09BCDE-ADAC-441D-8CC1-798BC211E46E"), "S", sucursal.Licencia.ToString(), UidOrden: UidOrden);
            MVOrden.AgregaEstatusALaOrden(new Guid("2d2f38b8-7757-45fb-9ca6-6ecfe20356ed"), UidOrden: UidOrden, UidLicencia: sucursal.Licencia, StrParametro: "S");
            MVTarifario.AgregarCodigoAOrdenTarifario(UidCodigo: Guid.NewGuid(), UidLicencia: sucursal.Licencia, uidorden: UidOrden);
            instance.Principal.oSeleccionado = new Orden();
            instance.Principal.oSeleccionElaboracion = new Orden();
            MessageBox.Show("Orden confirmada");
            instance.MVOrdenes.StrBusquedaDeOrdenes = "Confirmar";
            instance.Principal.VisibilidadVentnaConfirmar = false;
            instance.MVOrdenes.CargaOrdenes();
        }
        #endregion
    }
}
