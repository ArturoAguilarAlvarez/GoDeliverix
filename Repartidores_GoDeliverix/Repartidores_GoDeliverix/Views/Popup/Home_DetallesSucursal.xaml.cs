using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Repartidores_GoDeliverix.Views.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home_DetallesSucursal : ContentPage
    {
        public Home_DetallesSucursal()
        {
            InitializeComponent();
        }
        public async void CloseWindowsPopup(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void BtnVerMapa_Clicked(object sender, EventArgs e)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                Device.OpenUri(new Uri("http://maps.apple.com/?q=394+Pacific+Ave+San+Francisco+CA"));
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                // opens the Maps app directly
                Device.OpenUri(new Uri("geo:" + lblubicacionSucursal.Text + "?q=" + lblubicacionCliente.Text + ""));
            }
        }
    }
}