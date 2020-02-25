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
            CargaDesdeMenu();
        }
        public async void CargaDesdeMenu()
        {
            try
            {
                SLCargando.IsVisible = true;
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
                        using (HttpClient _webApi = new HttpClient())
                        {
                            var content = "";
                            var _URL = "" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionConDatosDeGoogle?StrNombreCiudad=" + item.Locality + "&Latitud=" + item.Location.Latitude + "&Longitud=" + item.Location.Longitude + "";
                            content = await _webApi.GetStringAsync(_URL);
                            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                            oDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
                            if (oDireccion.ListaDIRECCIONES.Count == 1)
                            {
                                App.UidEstadoABuscar = oDireccion.ListaDIRECCIONES[0].ESTADO;
                                break;
                            }
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
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(300)), true);
                    odireccion = oDireccion;

                    SLCargando.IsVisible = false;
                    acLoading.IsRunning = false;
                    acLoading.IsVisible = false;
                }
            }
            catch (FeatureNotSupportedException e)
            {
                // Handle not supported on device exception
                await DisplayAlert("Aviso del sistema", "Los servicios de ubicacion no soportados por el dispositivo", "Aceptar");
            }
            catch (FeatureNotEnabledException e)
            {
                await DisplayAlert("Ubicacion no activa", "Activa el GPS para obtener tu ubicacion", "Aceptar");
            }
            catch (PermissionException e)
            {
                // Handle permission exception
                await DisplayAlert("Aviso", "Activa los permisos de ubicacion para continuar", "Aceptar");
            }
            catch (Exception e)
            {
                // Unable to get location
                await DisplayAlert("Aviso", "No se puede obtener la ubicacion, intenta otra", "Aceptar");
            }
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
                map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(500)));
                var geo = new Geocoder();
                var location = new Location();
                string content = string.Empty;
                IEnumerable<Placemark> placemarks = await Geocoding.GetPlacemarksAsync(Latitud, Longitud);
                IEnumerable<string> addresses = await geo.GetAddressesForPositionAsync(new Position(Latitud, Longitud));

                if (location != null)
                {
                    foreach (var item in placemarks)
                    {
                        using (HttpClient _webApi = new HttpClient())
                        {
                            string ciudad = string.Empty;
                            if (!string.IsNullOrEmpty(item.Locality))
                            {
                                ciudad = item.Locality;
                                string _URL = "" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionConDatosDeGoogle?StrNombreCiudad=" + ciudad + "&Latitud=" + item.Location.Latitude + "&Longitud=" + item.Location.Longitude + "";
                                await Task.Run(async () => { content = await _webApi.GetStringAsync(_URL); });
                                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                                var oDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
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
                                    break;
                                }
                            }
                            else
                            {
                                await DisplayAlert("Zona no disponible", "No existe servicio en el punto marcado", "Aceptar");
                            }
                        }
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

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            if (oPagina != null)
            {
                if (App.MVDireccionDemo == null && App.MVDireccion.ListaDIRECCIONES.Count == 0)
                {
                    await DisplayAlert("Colonia no seleccionada", "No se ha establecido una colonia para buscar", "Aceptar");
                }
                else if (App.MVDireccion.ListaDIRECCIONES.Count > 0 && App.MVDireccionDemo == null)
                {
                    App.MVDireccionDemo = App.MVDireccion;
                    await Navigation.PopAsync();
                }
            }
            else
            {
                Navigation.InsertPageBefore(new HomePage(), this);

            }
        }
    }
}