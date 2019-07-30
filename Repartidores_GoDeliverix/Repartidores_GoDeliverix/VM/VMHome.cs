﻿using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Repartidores_GoDeliverix.Helpers;
using Repartidores_GoDeliverix.Views;
using Repartidores_GoDeliverix.Views.Popup;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using VistaDelModelo;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Repartidores_GoDeliverix.VM
{
    public class VMHome : ControlsController
    {
        VMOrden MVOrden = new VMOrden();
        VMSucursales MVSucursal = new VMSucursales();
        VMEmpresas MVEmpresa = new VMEmpresas();
        VMAcceso MVAcceso = new VMAcceso();
        VMUbicacion MVUbicacion = new VMUbicacion();
        VMDireccion MVDireccion = new VMDireccion();


        string url = "";
        HttpClient _WebApiGoDeliverix = new HttpClient();
        private Guid _UidOrdenTarifario;

        #region Informacion de la sucursal de la orden
        private string _StrSucursal;

        public string StrSucursalIdentificador
        {
            get { return _StrSucursal; }
            set { SetValue(ref _StrSucursal, value); }
        }
        private string _StrEmpresa;

        public string StrEmpresaNombreComercial
        {
            get { return _StrEmpresa; }
            set { SetValue(ref _StrEmpresa, value); }
        }
        #endregion


        public Guid UidOrdenTarifario
        {
            get { return _UidOrdenTarifario; }
            set { SetValue(ref _UidOrdenTarifario, value); }
        }

        private Guid _UidOrdenSucursal;

        public Guid UidOrdenSucursal
        {
            get { return _UidOrdenSucursal; }
            set { SetValue(ref _UidOrdenSucursal, value); }
        }

        private Guid _UidordenRepartidor;

        public Guid UidordenRepartidor
        {
            get { return _UidordenRepartidor; }
            set { SetValue(ref _UidordenRepartidor, value); }
        }

        private string _StrCodigo;

        public string StrCodigo
        {
            get { return _StrCodigo; }
            set { SetValue(ref _StrCodigo, value); }
        }


        private bool _blEstatus;

        public bool BlEstatus
        {
            get { return _blEstatus; }
            set
            {

                try
                {
                    _WebApiGoDeliverix.BaseAddress = new Uri("http://www.godeliverix.net/api/");
                    url = "";
                    var AppInstance = MainViewModel.GetInstance();
                    SetValue(ref _blEstatus, value);

                    //if (BlEstatus)
                    //{

                    //    if (UidEstatus == Guid.Empty)
                    //    {
                    //        url = "Profile/GetBitacoraRegistroRepartidores?StrParametro=S&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=AAD35D44-5E65-46B6-964F-CD2DF026ECB1";
                    //        _WebApiGoDeliverix.GetAsync(url);
                    //        SetValue(ref _blEstatus, value);
                    //    }
                    //    else
                    //    {

                    //        //Orden pendiente
                    //        if (UidEstatus.ToString().ToUpper() == "6294DACE-C9D1-4F9F-A942-FF12B6E7E957")
                    //        {
                    //            GenerateMessage("Aviso", "No puedes cerrar session al tener una orden asignada", "OK");

                    //        }
                    //        else
                    //        //Orden Confirmada
                    //        if (UidEstatus.ToString().ToUpper() == "A42B2588-D650-4DD9-829D-5978C927E2ED")
                    //        {
                    //            GenerateMessage("Aviso", "No puedes cerrar session al haber confirmado la orden", "OK");

                    //        }
                    //        else
                    //        //Entrega
                    //        if (UidEstatus.ToString().ToUpper() == "B6791F2C-FA16-40C6-B5F5-123232773612")
                    //        {
                    //            GenerateMessage("Aviso", "No puedes cerrar session sin haber entregado la orden recolectada", "OK");

                    //        }
                    //        else
                    //        {
                    //            url = "Profile/GetBitacoraRegistroRepartidores?StrParametro=S&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=AAD35D44-5E65-46B6-964F-CD2DF026ECB1";
                    //            _WebApiGoDeliverix.GetAsync(url);
                    //            SetValue(ref _blEstatus, value);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    url = "Profile/GetBitacoraRegistroRepartidores?StrParametro=S&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=A298B40F-C495-4BD8-A357-4A3209FBC162";
                    //    _WebApiGoDeliverix.GetAsync(url);
                    //    SetValue(ref _blEstatus, value);
                    //}
                }
                catch (Exception)
                {
                    GenerateMessage("Alerta!!", "No hay internet", "Aceptar");
                }
            }
        }

        private bool _IsLoading;

        public bool IsLoading
        {
            get { return _IsLoading; }
            set { SetValue(ref _IsLoading, value); }
        }

        private bool _IsEnable;

        public bool IsEnable
        {
            get { return _IsEnable; }
            set { SetValue(ref _IsEnable, value); }
        }

        #region Propiedades de ubicacion
        private string _StrUbicacionSucursal;

        public string StrUbicacionSucursal
        {
            get { return _StrUbicacionSucursal; }
            set { SetValue(ref _StrUbicacionSucursal, value); }
        }
        private string _StrUbicacionCliente;

        public string StrUbicacionCliente
        {
            get { return _StrUbicacionCliente; }
            set { SetValue(ref _StrUbicacionCliente, value); }
        }

        #endregion

        #region Propiedades
        private Guid _UidOrden;

        public Guid UidOrden
        {
            get { return _UidOrden; }
            set { SetValue(ref _UidOrden, value); }
        }
        private Guid _UidDireccionSucursal;

        public Guid UidSucursal
        {
            get { return _UidDireccionSucursal; }
            set { SetValue(ref _UidDireccionSucursal, value); }
        }

        private Guid _UidDireccionCliente;

        public Guid UidDireccionCliente
        {
            get { return _UidDireccionCliente; }
            set { SetValue(ref _UidDireccionCliente, value); }
        }
        private Guid _UidEstatus;

        public Guid UidEstatus
        {
            get { return _UidEstatus; }
            set { SetValue(ref _UidEstatus, value); }
        }

        private string _StrIdentificador;

        public string StrIdentificador
        {
            get { return _StrIdentificador; }
            set { SetValue(ref _StrIdentificador, value); }
        }
        private long _LngFolio;

        public long LngFolio
        {
            get { return _LngFolio; }
            set { SetValue(ref _LngFolio, value); }
        }



        #endregion

        #region Propiedades de los controles
        private bool _blSinAsignar;

        public bool BlSinAsignar
        {
            get { return _blSinAsignar; }
            set { _blSinAsignar = value; }
        }

        private bool _blNuevaOrden;

        public bool BlNuevaOrden
        {
            get { return _blNuevaOrden; }
            set { SetValue(ref _blNuevaOrden, value); }
        }
        private bool _blRecolecta;

        public bool BlRecolecta
        {
            get { return _blRecolecta; }
            set { SetValue(ref _blRecolecta, value); }
        }
        private bool _blEntrega;

        public bool BlEntrega
        {
            get { return _blEntrega; }
            set { SetValue(ref _blEntrega, value); }
        }

        #endregion

        #region Comandos

        public ICommand ShowOrder { get { return new RelayCommand(MostrarOrden); } }
        public ICommand ShowCodeQr { get { return new RelayCommand(MostrarCodigoQR); } }
        public ICommand GetCode { get { return new RelayCommand(ObtenerCodigo); } }
        public ICommand GiveOrder { get { return new RelayCommand(EntregarOrden); } }
        public ICommand ShowInfoOrder { get { return new RelayCommand(MapaEnEsperaAsync); } }



        public ICommand BtnMapaEspera { get { return new RelayCommand(MapaEnEsperaAsync); } }






        private void MapaEnEsperaAsync()
        {
            //var location = await Geolocation.GetLastKnownLocationAsync();

            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
            //    Device.OpenUri(new Uri("http://maps.apple.com/?q=394+Pacific+Ave+San+Francisco+CA"));
            //}
            //else if (Device.RuntimePlatform == Device.Android)
            //{
            //    // opens the Maps app directly
            //    Device.OpenUri(new Uri("geo:" + location.Latitude + "," + location.Longitude + ""));
            //}

            var AppInstance = MainViewModel.GetInstance();
            AppInstance.MVHomeOrden = new VMHomeOrden();
            AppInstance.MVHomeOrden.StrUidOrden = UidOrden.ToString();
            AppInstance.MVHomeOrden.UidDireccionDelCliente = UidDireccionCliente.ToString();
            AppInstance.MVHomeOrden.UidSucursal = UidSucursal;
            AppInstance.MVHomeOrden.UidOrdenTarifario = UidOrdenTarifario;
            AppInstance.MVHomeOrden.UidordenRepartidor = UidordenRepartidor;
            AppInstance.MVHomeOrden.cargaOrden();
            AppInstance.MVHomeOrden.ShowInfoOrder.Execute(null);

        }

        private void EntregarOrden()
        {
            StrCodigo = string.Empty;
        }

        private async void ObtenerCodigo()
        {
            MVOrden = new VMOrden();
            var AppInstance = MainViewModel.GetInstance();
            _WebApiGoDeliverix.BaseAddress = new Uri("http://www.godeliverix.net/api/");

            url = "Orden/GetBuscarOrdenPorCodigoQR?strCodigo=" + StrCodigo + "&UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor.ToString() + "";
            var content = await _WebApiGoDeliverix.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            bool Respuesta = bool.Parse(obj.ToString());

            if (Respuesta)
            {
                url = "Profile/GetBitacoraRegistroRepartidores?StrParametro=O&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=7DA3A42F-2271-47B4-B9B8-EDD311F56864&UidOrdenRepartidor=" + UidordenRepartidor + "";
                await _WebApiGoDeliverix.GetAsync(url);

                //MVAcceso = new VMAcceso();
                //MVAcceso.BitacoraRegistroRepartidores(char.Parse("O"), AppInstance.Session_.UidUsuario, new Guid("7DA3A42F-2271-47B4-B9B8-EDD311F56864"), UidOrdenRepartidor: UidordenRepartidor);

                //Peticion de la api para el cambio del estatus de la orden
                url = "Orden/GetAgregaEstatusALaOrden?UidEstatus=2FDEE8E7-0D54-4616-B4C1-037F5A37409D&StrParametro=S&UidOrden=" + UidOrdenSucursal + "";
                await _WebApiGoDeliverix.GetAsync(url);

                AppInstance.MVTurno.CargarTurno();
                // MVOrden.AgregaEstatusALaOrden(new Guid("2FDEE8E7-0D54-4616-B4C1-037F5A37409D"), UidOrden: UidOrdenSucursal, StrParametro: "S");
                Verifica();
                GenerateMessage("Orden entregada", "Felicidades, entregaste la orden!!!", "Aceptar");
            }
            else
            {
                GenerateMessage("Mensaje del sistema", "El codigo no coincide con la orden", "Aceptar");
            }
        }

        private async void MostrarCodigoQR()
        {
            IsLoading = true;
            MVOrden = new VMOrden();
            url = "http://www.godeliverix.net/api/Orden/GetObtenerCodigoOrdenTarifario?uidOrdenTarifario=" + UidOrdenTarifario + "";
            string content = await _WebApiGoDeliverix.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(obj);
            StrCodigo = MVOrden.CodigoOrdenTarifario;

            url = string.Empty;
            url = "http://www.godeliverix.net/api/Sucursales/GetBuscarSucursales?UidSucursal=" + UidSucursal + "";
            content = await _WebApiGoDeliverix.GetStringAsync(url);
            obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            MVSucursal = JsonConvert.DeserializeObject<VistaDelModelo.VMSucursales>(obj);

            StrSucursalIdentificador = MVSucursal.IDENTIFICADOR;
            url = string.Empty;
            url = "http://www.godeliverix.net/api/Empresa/GetBuscarEmpresas?UidEmpresa=" + MVSucursal.UidEmpresa + "";
            content = await _WebApiGoDeliverix.GetStringAsync(url);
            obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            MVEmpresa = JsonConvert.DeserializeObject<VistaDelModelo.VMEmpresas>(obj);

            StrEmpresaNombreComercial = MVEmpresa.NOMBRECOMERCIAL;


            IsLoading = false;
            await Application.Current.MainPage.Navigation.PushAsync(new Home_CodigoQR());
        }

        private async void MostrarOrden()
        {
            IsLoading = true;
            IsEnable = false;
            var AppInstance = MainViewModel.GetInstance();
            AppInstance.MVHomeOrden = new VMHomeOrden();
            AppInstance.MVHomeOrden.StrUidOrden = UidOrden.ToString();
            AppInstance.MVHomeOrden.UidDireccionDelCliente = UidDireccionCliente.ToString();
            AppInstance.MVHomeOrden.UidSucursal = UidSucursal;
            AppInstance.MVHomeOrden.LngFolio = LngFolio;
            AppInstance.MVHomeOrden.UidOrdenTarifario = UidOrdenTarifario;
            AppInstance.MVHomeOrden.UidordenRepartidor = UidordenRepartidor;
            AppInstance.MVHomeOrden.cargaOrden();
            IsLoading = false;
            IsEnable = true;
            await Application.Current.MainPage.Navigation.PushAsync(new Home_NuevaOrden());
        }
        #endregion
        public VMHome()
        {
            //Verifica();
            Timer tiempo = new Timer();
            tiempo.Interval = 6000;
            //enlazas un metodo al evento elapsed que es el que se ejecutara
            //cada vez que el intervalo de tiempo se cumpla
            tiempo.Elapsed += new ElapsedEventHandler(VerificaOrden);
            tiempo.Start();
        }
        private void VerificaOrden(object sender, ElapsedEventArgs e)
        {
            Verifica();
        }

        protected async void Verifica()
        {
            try
            {

                _WebApiGoDeliverix.BaseAddress = new Uri("http://www.godeliverix.net/api/");
                var AppInstance = MainViewModel.GetInstance();
                MVDireccion = new VMDireccion();
                MVUbicacion = new VMUbicacion();
                StrUbicacionCliente = string.Empty;
                StrUbicacionSucursal = string.Empty;

                IsLoading = true;
                url = "Orden/GetBuscarOrdenAsiganadaRepartidor?UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor + "";
                string content = await _WebApiGoDeliverix.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(obj);
                //MVOrden.BuscarOrdenAsiganadaRepartidor(AppInstance.Session_.UidUsuario);
                url = "Profile/GetObtenerUltimoEstatusBitacoraRepartidor?UidUsuario=" + AppInstance.Session_.UidUsuario + "";

                content = await _WebApiGoDeliverix.GetStringAsync(url);
                obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                string UltimoAcceso = obj.ToString();
                if (UltimoAcceso.ToUpper() == "AAD35D44-5E65-46B6-964F-CD2DF026ECB1")
                {
                    Application.Current.MainPage = new NavigationPage(new Login());
                }
                else
                if (MVOrden.Uidorden == Guid.Empty)
                {
                    BlSinAsignar = true;
                    BlNuevaOrden = false;
                    BlRecolecta = false;
                    BlEntrega = false;
                }
                else
                {
                    UidOrden = MVOrden.Uidorden;
                    UidOrdenTarifario = MVOrden.UidOrdenTarifario;
                    UidordenRepartidor = MVOrden.UidordenRepartidor;
                    UidDireccionCliente = MVOrden.UidDireccionCliente;
                    UidSucursal = MVOrden.UidSucursal;
                    UidOrdenSucursal = MVOrden.UidOrdenSucursal;
                    LngFolio = MVOrden.LNGFolio;
                    StrIdentificador = MVOrden.StrNombreSucursal;

                    //Obtiene el estatus de al orden asignada al repartidor, aqui tambien se pueden controlar los demas tipos de estatus
                    UidEstatus = new Guid(MVOrden.StrEstatusOrdenRepartidor);
                    //Cancelado
                    if (UidEstatus.ToString().ToUpper() == "12748F8A-E746-427D-8836-B54432A38C07")
                    {
                        BlSinAsignar = true;
                        BlNuevaOrden = false;
                        BlRecolecta = false;
                        BlEntrega = false;

                    }
                    else//Orden pendiente
                    if (UidEstatus.ToString().ToUpper() == "6294DACE-C9D1-4F9F-A942-FF12B6E7E957")
                    {
                        BlSinAsignar = false;
                        BlNuevaOrden = true;
                        BlRecolecta = false;
                        BlEntrega = false;

                        url = "Ubicacion/GetRecuperaUbicacionSucursal?UidSucursal=" + UidSucursal + "";
                        content = await _WebApiGoDeliverix.GetStringAsync(url);
                        obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);

                        //MVUbicacion.RecuperaUbicacionSucursal(UidSucursal.ToString());
                        StrUbicacionSucursal = MVUbicacion.VchLatitud + "," + MVUbicacion.VchLongitud;

                        url = "Ubicacion/GetRecuperaUbicacionDireccion?UidDireccion=" + UidDireccionCliente + "";
                        content = await _WebApiGoDeliverix.GetStringAsync(url);
                        obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);

                        //MVUbicacion.RecuperaUbicacionDireccion(UidDireccionCliente.ToString());
                        StrUbicacionCliente = MVUbicacion.VchLatitud + "," + MVUbicacion.VchLongitud;

                    }
                    else
                    //Orden Confirmada
                    if (UidEstatus.ToString().ToUpper() == "A42B2588-D650-4DD9-829D-5978C927E2ED")
                    {
                        if (MVOrden.StrEstatusOrdenGeneral.ToUpper() == "B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7")
                        {
                            url = "Profile/GetBitacoraRegistroRepartidores?StrParametro=O&UidUsuario=" + AppInstance.Session_.UidUsuario + "&UidEstatus=B6791F2C-FA16-40C6-B5F5-123232773612&UidOrdenRepartidor=" + UidordenRepartidor + "";
                            await _WebApiGoDeliverix.GetAsync(url);

                            //MVAcceso = new VMAcceso();
                            //MVAcceso.BitacoraRegistroRepartidores(char.Parse("O"), AppInstance.Session_.UidUsuario, new Guid("B6791F2C-FA16-40C6-B5F5-123232773612"), UidOrdenRepartidor: UidordenRepartidor);
                            BlSinAsignar = false;
                            BlNuevaOrden = false;
                            BlRecolecta = false;
                            BlEntrega = true;

                            url = "Ubicacion/GetRecuperaUbicacionDireccion?UidDireccion=" + UidDireccionCliente + "";
                            content = await _WebApiGoDeliverix.GetStringAsync(url);
                            obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                            MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);

                            //MVUbicacion.RecuperaUbicacionDireccion(UidDireccionCliente.ToString());
                            StrUbicacionCliente = MVUbicacion.VchLatitud + "," + MVUbicacion.VchLongitud;
                        }
                        else
                        {
                            BlSinAsignar = false;
                            BlNuevaOrden = false;
                            BlRecolecta = true;
                            BlEntrega = false;

                            url = "Ubicacion/GetRecuperaUbicacionSucursal?UidSucursal=" + UidSucursal + "";
                            content = await _WebApiGoDeliverix.GetStringAsync(url);
                            obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                            MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);

                            // MVUbicacion.RecuperaUbicacionSucursal(UidSucursal.ToString());
                            StrUbicacionSucursal = MVUbicacion.VchLatitud + "," + MVUbicacion.VchLongitud;
                        }
                    }
                    else
                    //Entrega
                    if (UidEstatus.ToString().ToUpper() == "B6791F2C-FA16-40C6-B5F5-123232773612")
                    {
                        BlSinAsignar = false;
                        BlNuevaOrden = false;
                        BlRecolecta = false;
                        BlEntrega = true;

                        url = "Ubicacion/GetRecuperaUbicacionDireccion?UidDireccion=" + UidDireccionCliente + "";
                        content = await _WebApiGoDeliverix.GetStringAsync(url);
                        obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);

                        // MVUbicacion.RecuperaUbicacionDireccion(UidDireccionCliente.ToString());
                        StrUbicacionCliente = MVUbicacion.VchLatitud + "," + MVUbicacion.VchLongitud;
                    }
                    else
                    {
                        BlSinAsignar = true;
                        BlNuevaOrden = false;
                        BlRecolecta = false;
                        BlEntrega = false;
                    }
                }

                IsLoading = false;
            }
            catch (Exception)
            {
                GenerateMessage("Aviso de red", "Sin conexion a internet", "Aceptar");
            }
        }
        protected async void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }
    }
}
