using Repartidores_GoDeliverix.Modelo;
using Repartidores_GoDeliverix.Views.Popup;
using Repartidores_GoDeliverix.VM;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Repartidores_GoDeliverix.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home_NuevaOrden : ContentPage
    {
        public Home_NuevaOrden()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new PopoLoading());
            //await Navigation.PopAsync();
            //await Navigation.PushAsync(new Home_DetallesSucursal());
        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
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