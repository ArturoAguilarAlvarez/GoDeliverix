using Repartidores_GoDeliverix.Views.Popup;
using System;
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

        private  void Button_Clicked(object sender, EventArgs e)
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
    }
}