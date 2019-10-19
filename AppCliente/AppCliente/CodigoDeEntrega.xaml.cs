using Plugin.Clipboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CodigoDeEntrega : ContentPage
    {
        public CodigoDeEntrega(long CodigoDeEntrega)
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
                BarcodeValue = CodigoDeEntrega.ToString(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            // Workaround for iOS
            ImgQrCodigo.WidthRequest = 250;
            ImgQrCodigo.HeightRequest = 250;
            Label Mensaje = new Label() { Text = "Muestra el codigo para entregar orden" ,HorizontalOptions = LayoutOptions.CenterAndExpand,VerticalOptions=LayoutOptions.Start,FontSize = 24};
            
            Label Mensaje2 = new Label() { Text = "Dale click para copiar el codigo" ,HorizontalOptions = LayoutOptions.CenterAndExpand,VerticalOptions=LayoutOptions.Start,FontSize = 24};
            lblCodigoDeEntrega.Text = CodigoDeEntrega.ToString();
            PanelContenido.Children.Insert(0, Mensaje);
            PanelContenido.Children.Insert(1, ImgQrCodigo);
            PanelContenido.Children.Insert(2, Mensaje2);
        }

        private async void lblCodigoDeEntrega_Clicked(object sender, EventArgs e)
        {
            CrossClipboard.Current.SetText(lblCodigoDeEntrega.Text);
            await DisplayAlert("!", "Codigo copiado", "Ok");
        }
    }
}