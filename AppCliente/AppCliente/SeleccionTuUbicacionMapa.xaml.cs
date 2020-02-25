using Xamarin.Forms.GoogleMaps;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using VistaDelModelo;
using System.Net.Http;
using Newtonsoft.Json;
using AppCliente.WebApi;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeleccionTuUbicacionMapa : ContentPage
    {
        Position MyPosicion = new Position(0, 0);
        VMDireccion objDireccion = null;
        ListView ListDirecciones;
        public SeleccionTuUbicacionMapa(ListView LVDirecciones)
        {
            InitializeComponent();
            ListDirecciones = LVDirecciones;
            btnContinuar.IsEnabled = false;
        }

        public SeleccionTuUbicacionMapa(VMDireccion objDireccion)
        {
            InitializeComponent();
            CargaUbicacion();
        }

        private void Map_MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            AICargando.IsVisible = true;
            AICargando.IsRunning = true;
            var lat = e.Point.Latitude.ToString("0.000");
            var lng = e.Point.Longitude.ToString("0.000");
            Pin AquiEstoy = new Pin()
            {
                Type = PinType.Place,
                Label = "Mi ubicacion =_=",
                Position = new Position(double.Parse(lat), double.Parse(lng))
            };
            map.Pins.Clear();
            map.Pins.Add(AquiEstoy);

            var pos = new Position(double.Parse(lat), double.Parse(lng));
            MyPosicion = pos;
            AICargando.IsVisible = false;
            AICargando.IsRunning = false;

            btnContinuar.IsEnabled = true;
        }

        private void Button_MiUbicacion(object sender, EventArgs e)
        {
            AICargando.IsVisible = true;
            AICargando.IsRunning = true;
            CargaUbicacion();
            AICargando.IsVisible = false;
            AICargando.IsRunning = false;
            btnContinuar.IsEnabled = true;
        }
        protected async void CargaUbicacion()
        {
            try
            {
                double Latitud;
                double Longitud;
                Position pos = new Position();
                if (string.IsNullOrEmpty(App.Global1))
                {


                    if (string.IsNullOrEmpty(Helpers.Settings.UidDireccion))
                    {
                        var location = await Geolocation.GetLocationAsync();
                        Latitud = location.Latitude;
                        Longitud = location.Longitude;

                        if (location != null)
                        {
                            var geo = new Geocoder();
                            var placemarks = await Geocoding.GetPlacemarksAsync(Latitud, Longitud);
                            //var addresses = await geo.GetAddressesForPositionAsync(new Position(Latitud, Longitud));
                            foreach (var item in placemarks)
                            {
                                using (HttpClient _webApi = new HttpClient())
                                {
                                    string _URL = "" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionConDatosDeGoogle?StrNombreCiudad=" + item.Locality + "&Latitud=" + item.Location.Latitude + "&Longitud=" + item.Location.Longitude + "";
                                    string content = await _webApi.GetStringAsync(_URL);
                                    string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                                    App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
                                    if (App.MVDireccion.ListaDIRECCIONES.Count == 1)
                                    {
                                        break;
                                    }
                                }
                            }
                            if (App.MVDireccion != null)
                            {
                                pos = new Position(Latitud, Longitud);
                                map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(500)));
                                MyPosicion = pos;
                            }

                        }
                        else
                        {
                            await DisplayAlert("No sé a podido encontrar su ubicación", "Seleccione el mapa", "Ok");
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Helpers.Settings.StrLatitud) && !string.IsNullOrEmpty(Helpers.Settings.StrLongitud))
                        {
                            pos = new Position(double.Parse(Helpers.Settings.StrLatitud), double.Parse(Helpers.Settings.StrLongitud));
                        }
                    }


                    Pin AquiEstoy = new Pin()
                    {
                        Type = PinType.Place,
                        Label = "Aqui estoy",
                        Position = pos
                    };
                    map.Pins.Clear();
                    map.Pins.Add(AquiEstoy);
                }
                else
                {
                    var location = await Geolocation.GetLocationAsync();
                    Latitud = location.Latitude;
                    Longitud = location.Longitude;

                    if (location != null)
                    {
                        var geo = new Geocoder();
                        var placemarks = await Geocoding.GetPlacemarksAsync(Latitud, Longitud);
                        //var addresses = await geo.GetAddressesForPositionAsync(new Position(Latitud, Longitud));
                        foreach (var item in placemarks)
                        {
                            using (HttpClient _webApi = new HttpClient())
                            {
                                string _URL = "" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionConDatosDeGoogle?StrNombreCiudad=" + item.Locality + "&Latitud=" + item.Location.Latitude + "&Longitud=" + item.Location.Longitude + "";
                                string content = await _webApi.GetStringAsync(_URL);
                                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                                App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
                                if (App.MVDireccion.ListaDIRECCIONES.Count == 1)
                                {
                                    break;
                                }
                            }
                        }
                        if (App.MVDireccion != null)
                        {
                            pos = new Position(Latitud, Longitud);
                            map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(500)));
                            MyPosicion = pos;
                        }
                        Pin AquiEstoy = new Pin()
                        {
                            Type = PinType.Place,
                            Label = "Aqui estoy",
                            Position = pos
                        };
                        map.Pins.Clear();
                        map.Pins.Add(AquiEstoy);
                    }
                    else
                    {
                        await DisplayAlert("No sé a podido encontrar su ubicación", "Seleccione el mapa", "Ok");
                    }
                }

            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
                await DisplayAlert("Aviso del sistema", "Los servicios de ubicacion no soportados por el dispositivo", "Aceptar");
            }
            catch (FeatureNotEnabledException)
            {
                await DisplayAlert("Ubicacion no activa", "Activa el GPS para obtener tu ubicacion", "Aceptar");
            }
            catch (PermissionException)
            {
                // Handle permission exception
                await DisplayAlert("Aviso", "Activa los permisos de ubicacion para continuar", "Aceptar");
            }
            catch (Exception)
            {
                // Unable to get location
                await DisplayAlert("Aviso", "No se puede obtener la ubicacion", "Aceptar");
            }
        }

        protected async override void OnDisappearing()
        {
            if (string.IsNullOrEmpty(App.Global1) && string.IsNullOrEmpty(Helpers.Settings.UidDireccion))
            {
                App.MVDireccion.ListaDIRECCIONES.Clear();
            }
            else
            {
                string _Url = ("" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + App.Global1);
                HttpClient _client = new HttpClient();
                string strDirecciones = await _client.GetStringAsync(_Url);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
                App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
            }
            base.OnDisappearing();
        }
        private void Button_ContinuarConElProcesoGuardado(object sender, EventArgs e)
        {
            if (MyPosicion.Latitude != 0)
            {
                if (objDireccion == null)
                {
                    Navigation.PushAsync(new DireccionModificar(MyPosicion, ListDirecciones));
                }
                else
                {
                    Navigation.PushAsync(new DireccionModificar(MyPosicion, objDireccion, ListDirecciones));
                }
            }
            else
            {
                DisplayAlert("", "Seleccione su ubicacion", "ok");
            }
        }
    }
}