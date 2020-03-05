using Repartidores_GoDeliverix.Views.Popup;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Repartidores_GoDeliverix.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        static int ContentWidth = (int)(App.ScreenWidth / App.ScreenDensity);
        static int ContentHeight = (int)(App.ScreenHeight / App.ScreenDensity);
        static int ThresholdMax = Math.Max(ContentHeight, ContentWidth);
        static int ThresholdMin = Math.Min(ContentHeight, ContentWidth);
        Location location = new Location();
        public Home()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.Android)
            {
                SLHome.Padding = new Thickness(0, 0, 0, 0);
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                SLHome.Padding = new Thickness(0, 20, 0, 0);
            }
        }

        private async void BtnMapaEspera_ClickedAsync(object sender, EventArgs e)
        {
            location = await Geolocation.GetLocationAsync();
           await  Map.OpenAsync(location.Latitude, location.Longitude, new MapLaunchOptions { NavigationMode = NavigationMode.None });
            
        }

        private async void BtnMapaSucursalCliente_ClickedAsync(object sender, EventArgs e)
        {
            string[]  ubicacionSucursal=lblUbicacionSucursal.Text.Split(char.Parse(","));
            double longitudes = double.Parse(ubicacionSucursal[1]);
            double latitudes = double.Parse(ubicacionSucursal[0]);
            location = new Location(latitudes, longitudes);
            await Map.OpenAsync(location, new MapLaunchOptions { NavigationMode = NavigationMode.Driving });
            
        }

        private async void BtnMapaSucursal_ClickedAsync(object sender, EventArgs e)
        {
            string[] ubicacionSucursal = lblUbicacionSucursal.Text.Split(char.Parse(","));
            double longitudes = double.Parse(ubicacionSucursal[1]);
            double latitudes = double.Parse(ubicacionSucursal[0]);
            location = new Location(latitudes, longitudes);
            await Map.OpenAsync(location, new MapLaunchOptions { NavigationMode = NavigationMode.Driving });
            
        }

        private async void BtnMapaCliente_ClickedAsync(object sender, EventArgs e)
        {
            string[] ubicacionSucursal = lblUbicacionCliente.Text.Split(char.Parse(","));
            double longitudes = double.Parse(ubicacionSucursal[1]);
            double latitudes = double.Parse(ubicacionSucursal[0]);
            location = new Location(latitudes, longitudes);
            await Map.OpenAsync(location, new MapLaunchOptions { NavigationMode = NavigationMode.Driving });

        }
    }
}