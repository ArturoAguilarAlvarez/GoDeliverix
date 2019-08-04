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
            
        }

        private async void BtnDetalles_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Home_DetallesSucursal());
        }

        private void LblCodigo_BindingContextChanged(object sender, System.EventArgs e)
        {
            var ImgQrCodigo = new ZXingBarcodeImageView
            {
                BarcodeFormat = BarcodeFormat.QR_CODE,
                BarcodeOptions = new QrCodeEncodingOptions
                {
                    Height = 250,
                    Width = 250
                },
                BarcodeValue = lblCodigo.Text,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            // Workaround for iOS
            ImgQrCodigo.WidthRequest = 250;
            ImgQrCodigo.HeightRequest = 250;


            Label Mensaje = new Label() { Text = "Muestra el codigo para recibir la orden.", VerticalOptions = LayoutOptions.EndAndExpand, FontSize = 24, TextColor = Color.Black, HorizontalOptions = LayoutOptions.CenterAndExpand };
            pnlContenido.Children.Insert(0, Mensaje);
            pnlContenido.Children.Insert(1, ImgQrCodigo);
        }
    }
}