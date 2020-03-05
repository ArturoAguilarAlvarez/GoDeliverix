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
using System.Data;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeleccionaColonia : ContentPage
    {
        public List<VMDireccion> DireccionesListaColonia = new List<VMDireccion>();
        HomePage oPagina = new HomePage();
        VMDireccion odireccion = new VMDireccion();

        public SeleccionaColonia()
        {
            InitializeComponent();
            try
            {
                CargaDesdeMenu();
            }
            catch (Exception)
            {
                Device.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Alerta", "Te saliste sin que uno de los procesos haya terminado, por favor recarga la pagina", "ACEPTAR");
                });
                throw;
            }
        }
        public async void CargaDesdeMenu()
        {
            try
            {
                SLCargando.IsVisible = true;
                SLDatos.IsVisible = false;
                acLoading.IsRunning = true;
                acLoading.IsVisible = true;
                double Latitud;
                double Longitud;
                var location = new Location();
                location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    Latitud = location.Latitude;
                    Longitud = location.Longitude;
                    var geo = new Geocoder();
                    IEnumerable<Placemark> placemarks = null;

                    placemarks = await Geocoding.GetPlacemarksAsync(Latitud, Longitud);

                    var oDireccion = new VMDireccion();
                    foreach (var item in placemarks)
                    {
                        ApiService ApiService = new ApiService("/api/Direccion");
                        Dictionary<string, string> parameters = new Dictionary<string, string>();
                        parameters.Add("StrNombreCiudad", item.Locality);
                        parameters.Add("CodigoEstado", item.AdminArea);
                        parameters.Add("CodigoPais", item.CountryCode);
                        parameters.Add("Latitud", item.Location.Latitude.ToString());
                        parameters.Add("Longitud", item.Location.Longitude.ToString());
                        var result = await ApiService.GET<VMDireccion>(action: "GetObtenerDireccionConDatosDeGoogle", responseType: ApiService.ResponseType.Object, arguments: parameters);
                        var oReponse = result as ResponseHelper;
                        if (result != null && oReponse.Status != false)
                        {
                            oDireccion = oReponse.Data as VMDireccion;
                            if (oDireccion.ListaDIRECCIONES.Count == 1)
                            {
                                App.UidEstadoABuscar = oDireccion.ListaDIRECCIONES[0].ESTADO;
                                break;
                            }
                        }
                        else
                        {
                            var objeto = new MasterMenuMenuItem { Id = 1, Title = "Busqueda", TargetType = typeof(HomePage), UrlResource = "IconoHomeMenu" };
                            var Page = (Page)Activator.CreateInstance(objeto.TargetType);
                            App app = Application.Current as App;
                            App.Navegacion = Page.GetType().Name;
                            MasterDetailPage md = (MasterDetailPage)app.MainPage;
                            md.Detail = new NavigationPage(Page);
                        }
                    }
                    VMDireccion Colonias = new VMDireccion();
                    DataTable dt = Colonias.Colonias(new Guid(oDireccion.ListaDIRECCIONES[0].CIUDAD));
                    DireccionesListaColonia.Clear();
                    foreach (DataRow item in dt.Rows)
                    {
                        DireccionesListaColonia.Add(
                          new VMDireccion()
                          {
                              ID = new Guid(item["IdColonia"].ToString()),
                              COLONIA = item["Nombre"].ToString()
                          });
                        MypickerColonia.Items.Add(item["Nombre"].ToString());
                    }
                    Pin AquiEstoy = new Pin()
                    {
                        Type = PinType.Place,
                        Label = "Ubicación actual",
                        Position = new Position(Latitud, Longitud)
                    };
                    map.Pins.Clear();
                    map.Pins.Add(AquiEstoy);
                    var pos = new Position(Latitud, Longitud);
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMiles(0.05)), true);
                    odireccion = oDireccion;
                    SLColonias.IsVisible = true;
                }
            }
            catch (FeatureNotSupportedException e)
            {
                // Handle not supported on device exception
                lblMensaje.Text = "Servicios de ubicación no soportados, selecciona una ubicación en el mapa y descubre las colonias disponibles";
                SLMensaje.IsVisible = true;
                //await DisplayAlert("Aviso del sistema", "Los servicios de ubicacion no soportados por el dispositivo", "Aceptar");
            }
            catch (FeatureNotEnabledException e)
            {
                lblMensaje.Text = "Activa tu ubicación para mostrarte las colonias cercanas a ti, de lo contrario selecciona una ubicación en el mapa y descubre las colonias disponibles";
                SLMensaje.IsVisible = true;

                //await DisplayAlert("Ubicacion no activa", "Activa el GPS para obtener tu ubicacion", "Aceptar");
            }
            catch (PermissionException e)
            {
                // Handle permission exception
                lblMensaje.Text = "Activa los permisos de ubicación para poder mostrarte las colonias cercanas a ti, de lo contrario selecciona una ubicación en el mapa y descubre las colonias disponibles";
                SLMensaje.IsVisible = true;
                //await DisplayAlert("Aviso", "Activa los permisos de ubicacion para continuar", "Aceptar");
            }
            catch (Exception e)
            {
                // Unable to get location
                lblMensaje.Text = "Servicio no disponible en esta zona";
                SLMensaje.IsVisible = true;
                //await DisplayAlert("Aviso", "No se puede obtener la ubicacion, intenta otra", "Aceptar");
            }

            SLCargando.IsVisible = false;
            acLoading.IsRunning = false;
            acLoading.IsVisible = false;
            SLDatos.IsVisible = true;
        }



        private void MypickerColonia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void BtnConfirmarColonia_Clicked(object sender, EventArgs e)
        {
            if (MypickerColonia.SelectedItem != null || MypickerColonia.SelectedIndex > 0)
            {
                VMDireccion c = DireccionesListaColonia.Find(t => t.COLONIA == MypickerColonia.SelectedItem.ToString());
                App.UidColoniaABuscar = c.ID.ToString();
                App.MVDireccion = odireccion;
                App.MVDireccionDemo = App.MVDireccion;
                App.MVDireccion.ListaDIRECCIONES[0].NOMBRECOLONIA = c.COLONIA;
                App.MVDireccion.ListaDIRECCIONES[0].COLONIA = c.ID.ToString();
                Helpers.Settings.StrNombreColonia = c.COLONIA;
                Helpers.Settings.StrCOLONIA = c.ID.ToString();
                Helpers.Settings.StrESTADO = App.MVDireccion.ListaDIRECCIONES[0].ESTADO;

                App.MVProducto = new VMProducto();
                await Navigation.PopAsync();
                var objeto = new MasterMenuMenuItem { Id = 3, Title = "", TargetType = typeof(HomePage) };
                var Page = (Page)Activator.CreateInstance(objeto.TargetType);
                App app = Application.Current as App;
                App.Navegacion = Page.GetType().Name;
                MasterDetailPage md = (MasterDetailPage)app.MainPage;
                md.Detail = new NavigationPage(Page);
            }
            else
            {
                await DisplayAlert("Colonia no seleccionada", "Selecciona una colonia para continuar", "Aceptar");
            }
        }
        private async void Map_MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            try
            {
                SLColonias.IsVisible = false;
                SLMensaje.IsVisible = false;
                var Latitud = e.Point.Latitude;
                var Longitud = e.Point.Longitude;
                Pin AquiEstoy = new Pin()
                {
                    Type = PinType.Place,
                    Label = "Ubicación",
                    Position = new Position(Latitud, Longitud)
                };
                map.Pins.Clear();
                map.Pins.Add(AquiEstoy);
                var pos = new Position(Latitud, Longitud);

                var geo = new Geocoder();
                var location = new Location();
                string content = string.Empty;
                IEnumerable<Placemark> placemarks = await Geocoding.GetPlacemarksAsync(Latitud, Longitud);
                IEnumerable<string> addresses = await geo.GetAddressesForPositionAsync(new Position(Latitud, Longitud));

                if (location != null)
                {
                    var oDireccion = new VMDireccion();
                    foreach (var item in placemarks)
                    {
                        string ciudad = string.Empty;
                        if (!string.IsNullOrEmpty(item.Locality))
                        {
                            ApiService ApiService = new ApiService("/api/Direccion");
                            Dictionary<string, string> parameters = new Dictionary<string, string>();
                            parameters.Add("StrNombreCiudad", item.Locality);
                            parameters.Add("CodigoEstado", item.AdminArea);
                            parameters.Add("CodigoPais", item.CountryCode);
                            parameters.Add("Latitud", item.Location.Latitude.ToString());
                            parameters.Add("Longitud", item.Location.Longitude.ToString());
                            var result = await ApiService.GET<VMDireccion>(action: "GetObtenerDireccionConDatosDeGoogle", responseType: ApiService.ResponseType.Object, arguments: parameters);
                            var oReponse = result as ResponseHelper;
                            if (result != null && oReponse.Status != false)
                            {
                                oDireccion = oReponse.Data as VMDireccion;
                                if (oDireccion.ListaDIRECCIONES.Count == 1)
                                {
                                    VMDireccion Colonias = new VMDireccion();
                                    DataTable dt = Colonias.Colonias(new Guid(oDireccion.ListaDIRECCIONES[0].CIUDAD));
                                    DireccionesListaColonia.Clear();
                                    MypickerColonia.Items.Clear();
                                    foreach (DataRow items in dt.Rows)
                                    {
                                        DireccionesListaColonia.Add(
                                          new VMDireccion()
                                          {
                                              ID = new Guid(items["IdColonia"].ToString()),
                                              COLONIA = items["Nombre"].ToString()

                                          });
                                        MypickerColonia.Items.Add(items["Nombre"].ToString());
                                    }
                                    map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMiles(0.05)));
                                    SLColonias.IsVisible = true;
                                    odireccion = oDireccion;
                                    break;
                                }
                            }
                            else
                            {
                                lblMensaje.Text = "Sin servicio en esta ubicación, elige otra ";
                                SLMensaje.IsVisible = true;
                               // await DisplayAlert("Error extraño", "Ocurrio algo mal al recuperar la ubicación intenta de nuevo por favor", "aceptar");
                            }
                        }
                        else
                        {
                            lblMensaje.Text = "Sin servicio en esta ubicación, elige otra";
                            SLMensaje.IsVisible = true;
                            //await DisplayAlert("Zona no disponible", "No existe servicio en el punto marcado", "Aceptar");
                        }

                    }
                    
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
                lblMensaje.Text = "Servicios de ubicación no soportados, selecciona una ubicación en el mapa y descubre las colonias disponibles";
                SLMensaje.IsVisible = true;
                //await DisplayAlert("Aviso del sistema", "Los servicios de ubicacion no soportados por el dispositivo", "Aceptar");
            }
            catch (FeatureNotEnabledException)
            {
                lblMensaje.Text = "Activa tu ubicación para mostrarte las colonias cercanas a ti, de lo contrario selecciona una ubicación en el mapa y descubre las colonias disponibles";
                SLMensaje.IsVisible = true;

                //await DisplayAlert("Ubicacion no activa", "Activa el GPS para obtener tu ubicacion", "Aceptar");
            }
            catch (PermissionException)
            {
                // Handle permission exception
                lblMensaje.Text = "Activa los permisos de ubicación para poder mostrarte las colonias cercanas a ti, de lo contrario selecciona una ubicación en el mapa y descubre las colonias disponibles";
                SLMensaje.IsVisible = true;
                //await DisplayAlert("Aviso", "Activa los permisos de ubicacion para continuar", "Aceptar");
            }
            catch (Exception)
            {
                // Unable to get location
                lblMensaje.Text = "Servicio no disponible en esta zona";
                SLMensaje.IsVisible = true;
                //await DisplayAlert("Aviso", "No se puede obtener la ubicacion, intenta otra", "Aceptar");
            }

            SLCargando.IsVisible = false;
            acLoading.IsRunning = false;
            acLoading.IsVisible = false;
            SLDatos.IsVisible = true;

        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            if (App.MVDireccionDemo == null && App.MVDireccion.ListaDIRECCIONES.Count == 0)
            {
                await DisplayAlert("Colonia no seleccionada", "No se ha establecido una colonia para buscar", "Aceptar");
            }
            else
            {
                App.MVDireccionDemo = App.MVDireccion;
                await Navigation.PopAsync();
            }
        }
    }
}