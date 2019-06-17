using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;


namespace Repartidores_GoDeliverix.Views.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home_CodigoQR : ContentPage
    {
        public Home_CodigoQR()
        {
            InitializeComponent();
            var ImgQrCodigo = new ZXingBarcodeImageView
            {
                BarcodeFormat = BarcodeFormat.QR_CODE,
                BarcodeOptions = new QrCodeEncodingOptions
                {
                    Height = 250,
                    Width = 250
                },
                BarcodeValue = lblCodigo.Text,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            // Workaround for iOS
            ImgQrCodigo.WidthRequest = 250;
            ImgQrCodigo.HeightRequest = 250;


            pnlContenido.Children.Add(ImgQrCodigo);

        }

        private async void BtnDetalles_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new PopoLoading());
            await Navigation.PopAsync();
            await Navigation.PushAsync(new Home_DetallesSucursal());
        }
    }
}