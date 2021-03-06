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
        VistaDelModelo.VMTurno MVTurno;
        VMSucursales MVSucursal = new VMSucursales();
        VMUbicacion MVUbicacion = new VMUbicacion();

        string url = "";
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

        private string _Texto;

        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
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
            set { SetValue(ref _blSinAsignar, value); }
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

        public ICommand ShowInfoOrder { get { return new RelayCommand(MapaEnEsperaAsync); } }

        public ICommand Entregar { get { return new RelayCommand(Entregarorden); } }

        public ICommand BtnMapaEspera { get { return new RelayCommand(MapaEnEsperaAsync); } }

        private async void Entregarorden()
        {
            IsLoading = true;
            IsEnable = false;

            var AppInstance = MainViewModel.GetInstance();

            AppInstance.MVHomeOrden = new VMHomeOrden
            {
                StrUidOrden = UidOrden.ToString(),
                UidDireccionDelCliente = UidDireccionCliente.ToString(),
                UidSucursal = UidSucursal,
                LngFolio = LngFolio,
                UidOrdenTarifario = UidOrdenTarifario,
                UidordenRepartidor = UidordenRepartidor,
                StrIdentificadorSucursal = MVSucursal.IDENTIFICADOR,
                StrCodigo = MVOrden.CodigoOrdenTarifario
            };
            AppInstance.MVHomeOrden.StrCodigo = string.Empty;
            AppInstance.MVHomeOrden.CargaOrden();


            await Application.Current.MainPage.Navigation.PushAsync(new Home_Entregar());
            IsLoading = false;
            IsEnable = true;
        }

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
            AppInstance.MVHomeOrden = new VMHomeOrden
            {
                StrUidOrden = UidOrden.ToString(),
                UidDireccionDelCliente = UidDireccionCliente.ToString(),
                UidSucursal = UidSucursal,
                UidOrdenTarifario = UidOrdenTarifario,
                UidordenRepartidor = UidordenRepartidor
            };
            AppInstance.MVHomeOrden.CargaOrden();
            AppInstance.MVHomeOrden.ShowInfoOrder.Execute(null);

        }

        private async void MostrarCodigoQR()
        {

            IsLoading = true;
            IsEnable = false;

            MVOrden = new VMOrden();
            using (var _WebApiGoDeliverix = new HttpClient())
            {
                url = "" + settings.Sitio + "api/Orden/GetObtenerCodigoOrdenTarifario?uidOrdenTarifario=" + UidOrdenTarifario + "";
                string content = await _WebApiGoDeliverix.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(obj);
                StrCodigo = MVOrden.CodigoOrdenTarifario;
            }

            using (var _WebApiGoDeliverix = new HttpClient())
            {
                url = string.Empty;
                url = "" + settings.Sitio + "api/Sucursales/GetBuscarSucursales?UidSucursal=" + UidSucursal + "";
                string content = await _WebApiGoDeliverix.GetStringAsync(url);
                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                MVSucursal = JsonConvert.DeserializeObject<VistaDelModelo.VMSucursales>(obj);
            }



            var AppInstance = MainViewModel.GetInstance();
            AppInstance.MVHomeOrden = new VMHomeOrden
            {
                StrUidOrden = UidOrden.ToString(),
                UidDireccionDelCliente = UidDireccionCliente.ToString(),
                UidSucursal = UidSucursal,
                LngFolio = LngFolio,
                UidOrdenTarifario = UidOrdenTarifario,
                UidordenRepartidor = UidordenRepartidor,
                StrIdentificadorSucursal = MVSucursal.IDENTIFICADOR,
                StrCodigo = MVOrden.CodigoOrdenTarifario
            };
            AppInstance.MVHomeOrden.CargaOrden();
            IsLoading = false;
            IsEnable = true;



            await Application.Current.MainPage.Navigation.PushAsync(new Home_CodigoQR());
        }

        private async void MostrarOrden()
        {
            IsLoading = true;
            IsEnable = false;
            var AppInstance = MainViewModel.GetInstance();
            AppInstance.MVHomeOrden = new VMHomeOrden
            {
                StrUidOrden = UidOrden.ToString(),
                UidDireccionDelCliente = UidDireccionCliente.ToString(),
                UidSucursal = UidSucursal,
                LngFolio = LngFolio,
                UidOrdenTarifario = UidOrdenTarifario,
                UidordenRepartidor = UidordenRepartidor,
                ListaProductos = new System.Collections.Generic.List<Modelo.Productos>()
            };
            AppInstance.MVHomeOrden.CargaOrden();
            IsLoading = false;
            IsEnable = true;
            await Application.Current.MainPage.Navigation.PushAsync(new Home_NuevaOrden());
        }
        #endregion
        public VMHome()
        {
            Timer tiempo = new Timer();
            tiempo.Interval = 10000;
            //enlazas un metodo al evento elapsed que es el que se ejecutara
            //cada vez que el intervalo de tiempo se cumpla
            tiempo.Elapsed += new ElapsedEventHandler(VerificaOrden);
            tiempo.Start();

        }
        private void VerificaOrden(object sender, ElapsedEventArgs e)
        {
            Verifica();
        }

        public void Verifica()
        {
            Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        var AppInstance = MainViewModel.GetInstance();
                        VMDireccion MVDireccion = new VMDireccion();
                        MVUbicacion = new VMUbicacion();
                        StrUbicacionCliente = string.Empty;
                        StrUbicacionSucursal = string.Empty;
                        string url = string.Empty;
                        IsLoading = true;
                        Guid uidUsuario = AppInstance.Session_.UidUsuario;
                        var Consulta = url = "" + settings.Sitio + "api/Profile/GetObtenerUltimoEstatusBitacoraRepartidor?UidUsuario=" + AppInstance.Session_.UidUsuario + "";
                        var Consulta2 = "" + settings.Sitio + "api/Turno/GetConsultaUltimoTurno?UidUsuario=" + uidUsuario + "";
                        string UltimoAcceso = string.Empty;
                        using (var _webApi = new HttpClient())
                        {
                            string content = await _webApi.GetStringAsync(Consulta);
                            UltimoAcceso = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        }

                        if (UltimoAcceso.ToUpper() == "AAD35D44-5E65-46B6-964F-CD2DF026ECB1")
                        {
                            Application.Current.MainPage = new NavigationPage(new Login());
                        }
                        else
                        {
                            using (var _webApi = new HttpClient())
                            {
                                string datos = await _webApi.GetStringAsync(Consulta2);
                                var obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                                MVTurno = JsonConvert.DeserializeObject<VistaDelModelo.VMTurno>(obj);
                            }

                            if (MVTurno.DtmHoraFin == DateTime.Parse("01/01/0001 12:00:00 a. m.") && MVTurno.DtmHoraInicio != DateTime.Parse("01/01/0001 12:00:00 a. m."))
                            {
                                using (var _webApi = new HttpClient())
                                {
                                    url = "" + settings.Sitio + "api/Orden/GetBuscarOrdenAsiganadaRepartidor?UidTurnoRepartidor=" + AppInstance.Session_.UidTurnoRepartidor + "";
                                    string content = await _webApi.GetStringAsync(url);
                                    string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                                    MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(obj);
                                }

                                Guid UidEstatusTurno = await ObtenUltimoEstatusDelTurno(uidUsuario);
                                if (UidEstatusTurno == new Guid("AE28F243-AA0D-43BD-BF10-124256B75B00"))
                                {
                                    Texto = "Debes liquidar para recibir ordenes";
                                    BlSinAsignar = true;
                                    BlNuevaOrden = false;
                                    BlRecolecta = false;
                                    BlEntrega = false;
                                }else if (UidEstatusTurno == new Guid("B03E3407-F76D-4DFA-8BF9-7F059DC76141"))
                                {
                                    Texto = "Debes recargar para recibir ordenes";
                                    BlSinAsignar = true;
                                    BlNuevaOrden = false;
                                    BlRecolecta = false;
                                    BlEntrega = false;
                                }
                                else
                                {
                                    if (MVOrden.Uidorden == Guid.Empty || MVOrden.StrEstatusOrdenRepartidor.ToUpper() == "7DA3A42F-2271-47B4-B9B8-EDD311F56864")
                                    {
                                        Texto = "Esperando orden";
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
                                            using (var _webApi = new HttpClient())
                                            {
                                                url = "" + settings.Sitio + "api/Ubicacion/GetRecuperaUbicacionSucursal?UidSucursal=" + UidSucursal + "";
                                                string content = await _webApi.GetStringAsync(url);
                                                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                                                MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);
                                            }


                                            StrUbicacionSucursal = MVUbicacion.VchLatitud + "," + MVUbicacion.VchLongitud;
                                            using (var _webApi = new HttpClient())
                                            {
                                                url = "" + settings.Sitio + "api/Ubicacion/GetRecuperaUbicacionDireccion?UidDireccion=" + UidDireccionCliente + "";
                                                string content = await _webApi.GetStringAsync(url);
                                                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                                                MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);
                                            }
                                            StrUbicacionCliente = MVUbicacion.VchLatitud + "," + MVUbicacion.VchLongitud;
                                        }
                                        else
                                        //Orden Confirmada
                                        if (UidEstatus.ToString().ToUpper() == "A42B2588-D650-4DD9-829D-5978C927E2ED")
                                        {
                                            if (MVOrden.StrEstatusOrdenGeneral.ToUpper() == "B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7")
                                            {
                                                using (var _webApi = new HttpClient())
                                                {
                                                    url = "" + settings.Sitio + "api/Profile/GetBitacoraRegistroRepartidores?StrParametro=O&UidUsuario=" + uidUsuario + "&UidEstatus=B6791F2C-FA16-40C6-B5F5-123232773612&UidOrdenRepartidor=" + UidordenRepartidor + "";
                                                    await _webApi.GetAsync(url);
                                                }

                                                BlSinAsignar = false;
                                                BlNuevaOrden = false;
                                                BlRecolecta = false;
                                                BlEntrega = true;
                                                using (var _webApi = new HttpClient())
                                                {
                                                    url = "" + settings.Sitio + "api/Ubicacion/GetRecuperaUbicacionDireccion?UidDireccion=" + UidDireccionCliente + "";
                                                    string content = await _webApi.GetStringAsync(url);
                                                    string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                                                    MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);
                                                }
                                                StrUbicacionCliente = MVUbicacion.VchLatitud + "," + MVUbicacion.VchLongitud;
                                            }
                                            else
                                            {
                                                BlSinAsignar = false;
                                                BlNuevaOrden = false;
                                                BlRecolecta = true;
                                                BlEntrega = false;
                                                using (var _webApi = new HttpClient())
                                                {
                                                    url = "" + settings.Sitio + "api/Ubicacion/GetRecuperaUbicacionSucursal?UidSucursal=" + UidSucursal + "";
                                                    string content = await _webApi.GetStringAsync(url);
                                                    string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                                                    MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);
                                                }

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
                                            using (var _webApi = new HttpClient())
                                            {
                                                url = "" + settings.Sitio + "api/Ubicacion/GetRecuperaUbicacionDireccion?UidDireccion=" + UidDireccionCliente + "";
                                                string content = await _webApi.GetStringAsync(url);
                                                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                                                MVUbicacion = JsonConvert.DeserializeObject<VistaDelModelo.VMUbicacion>(obj);
                                            }

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
                                }
                            }
                            else
                            {
                                Texto = "Inicia turno para recibir ordenes";
                                BlSinAsignar = true;
                                BlNuevaOrden = false;
                                BlRecolecta = false;
                                BlEntrega = false;
                            }

                            IsLoading = false;
                        }

                    }
                    catch (Exception)
                    {
                        GenerateMessage("Aviso de red", "Sin conexion a internet", "Aceptar");
                    }
                });

        }

        private async Task<Guid> ObtenUltimoEstatusDelTurno(Guid UidUsuario)
        {
            string obj = "";
            using (var _WebApi = new HttpClient())
            {
                string url = "" + settings.Sitio + "api/Turno/GetConsultaEstatusUltimoTurnoRepartidor?UidUsuario=" + UidUsuario + "";
                string datos = await _WebApi.GetStringAsync(url);
                obj = JsonConvert.DeserializeObject<ResponseHelper>(datos).Data.ToString();
                if (string.IsNullOrEmpty(obj))
                {
                    obj = Guid.Empty.ToString();
                }
            }
            return new Guid(obj);
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
