using AllSuministradora.model;
using AllSuministradora.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using VistaDelModelo;

namespace AllSuministradora.VistasDelModelo
{
    public class VMOrdenes : NotifyBase
    {
        #region Propiedades


        #endregion
        #region Propiedades de los controles



        private Orden _oOrdenRepartidor;

        public Orden oOrdenRepartidor
        {
            get { return _oOrdenRepartidor; }
            set { _oOrdenRepartidor = value; OnpropertyChanged("oOrdenRepartidor"); }
        }

        private Orden _oOrdenParaEnvio;

        public Orden oOrdenParaEnvio
        {
            get { return _oOrdenParaEnvio; }
            set { _oOrdenParaEnvio = value; OnpropertyChanged("oOrdenParaEnvio"); }
        }

        private List<Orden> _ListaDeOrdenes;

        public List<Orden> ListaDeOrdenes
        {
            get { return _ListaDeOrdenes; }
            set { _ListaDeOrdenes = value; OnpropertyChanged("ListaDeOrdenes"); }
        }

        private int _intCantidadDeOrdenesAConfirmar;

        public int IntCantidadDeOrdenesAConfirmar
        {
            get { return _intCantidadDeOrdenesAConfirmar; }
            set { _intCantidadDeOrdenesAConfirmar = value; OnpropertyChanged("IntCantidadDeOrdenesAConfirmar"); }
        }
        private int _IntCantidadDeOrdenesAFinalizar;

        public int IntCantidadDeOrdenesAFinalizar
        {
            get { return _IntCantidadDeOrdenesAFinalizar; }
            set { _IntCantidadDeOrdenesAFinalizar = value; OnpropertyChanged("IntCantidadDeOrdenesAFinalizar"); }
        }


        private string _UidCodigoEntrega;

        public string UidCodigoEntrega
        {
            get { return _UidCodigoEntrega; }
            set { _UidCodigoEntrega = value; OnpropertyChanged("UidCodigoEntrega"); }
        }

        private string _BusquedaDeOrdenes;

        public string StrBusquedaDeOrdenes
        {
            get { return _BusquedaDeOrdenes; }
            set { _BusquedaDeOrdenes = value; OnpropertyChanged("StrBusquedaDeOrdenes"); }
        }
        private DispatcherTimer _Timer;

        public DispatcherTimer Timer
        {
            get { return _Timer; }
            set { _Timer = value; }
        }

        #endregion
        #region Comandos
        private ICommand _cmdMostrarOrden;

        public ICommand CmdMostrarOrden
        {
            get { return _cmdMostrarOrden; }
            set { _cmdMostrarOrden = value; }
        }

        #endregion
        #region Constructor
        public VMOrdenes()
        {
            StrBusquedaDeOrdenes = "Confirmar";
            CargaOrdenes();
            CmdMostrarOrden = new CommandBase(param => ObtenerOrden());
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(10);
            Timer.Tick += Reload;
        }

        private void Reload(object sender, EventArgs e)
        {
            CargaOrdenes();
        }
        #endregion
        #region Metodos

        public void CargaOrdenes()
        {
            VMOrden MVOrden = new VMOrden();
            ListaDeOrdenes = new List<Orden>();
            var instance = ControlGeneral.GetInstance();
            foreach (var item in instance.VMSucursalesLocal.ListaDeSucursales)
            {
                switch (StrBusquedaDeOrdenes)
                {
                    case "Confirmar":
                        MVOrden.BuscarOrdenes("Sucursal", UidLicencia: item.Licencia, EstatusSucursal: "Pendientes a confirmar", TipoDeSucursal: "S");
                        foreach (var it in MVOrden.ListaDeOrdenes)
                        {
                            if (!ListaDeOrdenes.Exists(x => x.UidOrden == it.Uidorden))
                            {
                                ListaDeOrdenes.Add(new Orden()
                                {
                                    UidOrden = it.Uidorden,
                                    UidSucursal = item.UidSucursal,
                                    DCLTotal = it.MTotal,
                                    LngFolio = it.LNGFolio,
                                    DtmFechaDeOrden = it.FechaDeOrden,
                                    StrIdentificadorSucursal = item.NombreSucursal,
                                    NombreComercialEmpresa = item.NombreEmpresa
                                });
                            }

                        }
                        IntCantidadDeOrdenesAConfirmar = ListaDeOrdenes.Count;
                        break;
                    case "Elaborar":
                        MVOrden.BuscarOrdenes("Sucursal", UidLicencia: item.Licencia, EstatusSucursal: "Pendiente para elaborar", TipoDeSucursal: "S");
                        foreach (var it in MVOrden.ListaDeOrdenes)
                        {
                            if (!ListaDeOrdenes.Exists(x => x.UidOrden == it.Uidorden))
                            {
                                ListaDeOrdenes.Add(new Orden()
                                {
                                    UidOrden = it.Uidorden,
                                    UidSucursal = item.UidSucursal,
                                    DCLTotal = it.MTotal,
                                    LngFolio = it.LNGFolio,
                                    DtmFechaDeOrden = it.FechaDeOrden,
                                    StrIdentificadorSucursal = item.NombreSucursal,
                                    NombreComercialEmpresa = item.NombreEmpresa
                                });
                            }
                        }
                        IntCantidadDeOrdenesAFinalizar = ListaDeOrdenes.Count;
                        break;
                    case "Recolectar":
                        MVOrden.BuscarOrdenes("Sucursal", UidLicencia: item.Licencia, EstatusSucursal: "Lista a enviar", TipoDeSucursal: "S");
                        foreach (var it in MVOrden.ListaDeOrdenes)
                        {
                            if (!ListaDeOrdenes.Exists(x => x.UidOrden == it.Uidorden))
                            {
                                ListaDeOrdenes.Add(new Orden()
                                {
                                    UidOrden = it.Uidorden,
                                    UidSucursal = item.UidSucursal,
                                    DCLTotal = it.MTotal,
                                    LngFolio = it.LNGFolio,
                                    DtmFechaDeOrden = it.FechaDeOrden,
                                    StrIdentificadorSucursal = item.NombreSucursal,
                                    NombreComercialEmpresa = item.NombreEmpresa
                                });
                            }
                        }
                        break;
                    case "Canceladas":
                        MVOrden.BuscarOrdenes("Sucursal", UidLicencia: item.Licencia, EstatusSucursal: "Canceladas", TipoDeSucursal: "S");
                        foreach (var it in MVOrden.ListaDeOrdenesCanceladas)
                        {
                            if (!ListaDeOrdenes.Exists(x => x.UidOrden == it.Uidorden))
                            {
                                ListaDeOrdenes.Add(new Orden()
                                {
                                    UidOrden = it.Uidorden,
                                    UidSucursal = item.UidSucursal,
                                    DCLTotal = it.MTotal,
                                    LngFolio = it.LNGFolio,
                                    DtmFechaDeOrden = it.FechaDeOrden,
                                    StrIdentificadorSucursal = item.NombreSucursal,
                                    NombreComercialEmpresa = item.NombreEmpresa
                                });
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        protected void ObtenerOrden()
        {
            if (!string.IsNullOrWhiteSpace(UidCodigoEntrega))
            {
                if (UidCodigoEntrega.Length == 36)
                {
                    VMOrden MVOrden = new VMOrden();
                    MVOrden.BuscarOrdenRepartidor(UidCodigoEntrega.Replace("'", "-"));
                    if (MVOrden.StrEstatusOrdenSucursal != null)
                    {
                        if (MVOrden.StrEstatusOrdenSucursal.ToString() == "C412D367-7D05-45D8-AECA-B8FABBF129D9".ToLower())
                        {
                            MessageBox.Show("Orden lista para ser enviada");
                            var instance = ControlGeneral.GetInstance();
                            Orden obj = instance.MVOrdenes.ListaDeOrdenes.Where(x => x.UidOrden == MVOrden.Uidorden).FirstOrDefault();
                            SucursalItem sucursal = instance.VMSucursalesLocal.ListaDeSucursales.Where(x => x.UidSucursal == obj.UidSucursal).FirstOrDefault();
                            string pago = "Pagada";
                            VMContrato objcontrato = new VMContrato();
                            if (objcontrato.VerificaPagoARecolectar(UidOrden: obj.UidOrden.ToString()))
                            {
                                pago = "Pago al recolectar";
                            }
                            MVOrden.ObtenerProductosDeOrden(MVOrden.Uidorden.ToString());
                            oOrdenRepartidor = new Orden() { UidOrden = MVOrden.Uidorden, UidSucursal = sucursal.UidSucursal, StrNombreRepartidor = MVOrden.StrNombreRepartidor, StrIdentificadorSucursal = sucursal.NombreSucursal, NombreComercialEmpresa = sucursal.NombreEmpresa, LngFolio = obj.LngFolio, DCLTotal = obj.DCLTotal, StrEstatusPagoOrden = pago };
                            oOrdenRepartidor.ListaDeProductos = new List<Producto>();

                            oOrdenRepartidor.VControlConfirmar = Visibility.Visible;
                            oOrdenRepartidor.VCancelarConfirmar = Visibility.Visible;
                            foreach (var item in MVOrden.ListaDeProductos)
                            {
                                oOrdenRepartidor.ListaDeProductos.Add(
                                    new Producto()
                                    {
                                        StrNombre = item.StrNombreProducto,
                                        IntCantidad = item.intCantidad,
                                        MTotalSucursal = item.MTotalSucursal
                                    });
                            }
                        }
                        else if (MVOrden.StrEstatusOrdenSucursal.ToString() == "B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7".ToLower())
                        {
                            MessageBox.Show("La orden ya ha sido enviada");
                        }
                        else
                        {
                            MessageBox.Show("La orden no esta lista para ser entregada al repartidor");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay coincidencia con el codigo");
                    }
                }
                else
                {
                    MessageBox.Show("Codigo invalido");
                }
            }
            else
            {
                MessageBox.Show("Ingrese un codigo");
            }

        }
        #endregion
    }
}
