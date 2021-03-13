using Repartidores_GoDeliverix.Modelo;
using Repartidores_GoDeliverix.VM;
using System;
using Xamarin.Essentials;
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
        private void LblNumeroCliente_Clicked(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open(lblNumeroCliente.Text);
            }
            //catch (ArgumentNullException anEx)
            //{
            //    // Number was null or white space
            //}
            //catch (FeatureNotSupportedException ex)
            //{
            //    // Phone Dialer is not supported on this device.
            //}
            catch (Exception ex)
            {
                // Other error has occurred.
            }
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
            ImgQrCodigo.WidthRequest = 200;
            ImgQrCodigo.HeightRequest = 200;


            Label Mensaje = new Label() { Text = "Muestra el codigo para recibir la orden.", VerticalOptions = LayoutOptions.EndAndExpand, FontSize = 14, TextColor = Color.Black, HorizontalOptions = LayoutOptions.CenterAndExpand };
            pnlContenido.Children.Insert(0, Mensaje);
            pnlContenido.Children.Insert(1, ImgQrCodigo);
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var producto = e.Item as Productos;
            var viewmodel = new VMHomeOrden();
            await viewmodel.MuestraNota(producto.UidProducto);
            var nota = string.IsNullOrEmpty(viewmodel.StrNota) ? "No hay nota" : viewmodel.StrNota;            
            await DisplayAlert("Nota", nota, "Entendido");
        }
    }
}