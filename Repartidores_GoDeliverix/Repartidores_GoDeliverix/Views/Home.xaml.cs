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
        public Home()
        {
            InitializeComponent();
        }
        
        private  void BtnMapaEspera_ClickedAsync(object sender, EventArgs e)
        {
            

            if (Device.RuntimePlatform == Device.iOS)
            {
                //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                Device.OpenUri(new Uri("comgooglemaps:center=0,0"));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                // opens the Maps app directly
                Device.OpenUri(new Uri("geo:0,0"));
            }
        }

        private async void BtnMapaSucursalCliente_ClickedAsync(object sender, EventArgs e)
        {
            var location = await Geolocation.GetLocationAsync();
            if (Device.RuntimePlatform == Device.iOS)
            {
                //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                Device.OpenUri(new Uri("comgooglemaps://? saddr="+location.Longitude+","+location.Latitude+"&daddr=" + lblUbicacionSucursal.Text + "&zoom=12"));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                Device.OpenUri(new Uri("geo:" + location.Longitude + "," + location.Latitude + "?q=" + lblUbicacionSucursal.Text+""));
                // opens the Maps app directly
                // Device.OpenUri(new Uri("geo:" + location.Latitude + "," + location.Longitude + ""));
            }
        }

        private async void BtnMapaSucursal_ClickedAsync(object sender, EventArgs e)
        {
            var location = await Geolocation.GetLocationAsync();
            if (Device.RuntimePlatform == Device.iOS)
            {
                //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                Device.OpenUri(new Uri("comgooglemaps://? saddr=" + location.Longitude + "," + location.Latitude + "&daddr=" + lblUbicacionSucursal.Text + "&zoom=12"));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                // opens the Maps app directly
                Device.OpenUri(new Uri("geo:" + location.Longitude + "," + location.Latitude + "?q=" + lblUbicacionSucursal.Text + ""));
            }
        }

        private async void BtnMapaCliente_ClickedAsync(object sender, EventArgs e)
        {
            var location = await Geolocation.GetLocationAsync();
            if (Device.RuntimePlatform == Device.iOS)
            {
                //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                Device.OpenUri(new Uri("comgooglemaps://? saddr=" + location.Longitude + "," + location.Latitude + "&daddr=" + lblUbicacionCliente.Text + "&zoom=12"));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                // opens the Maps app directly
                Device.OpenUri(new Uri("geo:" + location.Longitude + "," + location.Latitude + "?q=" + lblUbicacionCliente.Text + ""));
            }
        }
    }
}