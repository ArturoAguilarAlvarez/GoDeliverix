using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
namespace Repartidores_GoDeliverix.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home_Entregar : ContentPage
    {

        public Home_Entregar()
        {
            InitializeComponent();
        }

        private void BtnFinalizar_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PopAsync();
        }

        private void LblNumeroCliente_Clicked(object sender, EventArgs e)
        {
            try
            {
                ; PhoneDialer.Open(lblNumeroCliente.Text);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }

        }

        private void btnEscanearCodigo_Clicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    ECodigo.Text = result.Text;
                });
            };
        }
    }
}