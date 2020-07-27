using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppCliente.ViewModel;
using AppCliente.Pagos;
namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pago : ContentPage
    {
        decimal TotalAPagar = 0.0m;
        public Pago(string CantidadAPagar)
        {
            InitializeComponent();
            List<MVPago> tipodepago = new List<MVPago>()
            {
            new MVPago(){ NombreTipoDePago = "Monedero", IconoTipoDePago = "Monedero" },
            new MVPago(){ NombreTipoDePago = "Pago con tarjeta", IconoTipoDePago = "TarjetaCredito" },
            new MVPago(){ NombreTipoDePago = "Efectivo", IconoTipoDePago ="Efectivo" },
            };
            btnPagar.Text = "Pagaras $" + decimal.Parse(CantidadAPagar).ToString("N2");
            TotalAPagar = decimal.Parse(CantidadAPagar);
            LVMetodosDePago.ItemsSource = tipodepago;
        }

        private async void LVMetodosDePago_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MVPago objeto = e.SelectedItem as MVPago;
            switch (objeto.NombreTipoDePago)
            {
                case "Monedero":
                    Navigation.InsertPageBefore(new InformacionDeCompra("13DC10FE-FE47-48D6-A427-DD2F6DE0C564"), this);
                    await Navigation.PopAsync();
                    break;
                case "Pago con tarjeta":
                    if (TotalAPagar >= 100.00m)
                    {
                        Navigation.InsertPageBefore(new InformacionDeCompra("30545834-7FFE-4D1A-AA94-D6E569371C60"), this);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Campo invalido", "la cantidad a pagar con tarjeta debe ser igual o superior a $100 MXN", "Aceptar");
                    }
                    break;
                case "Efectivo":
                    if (TotalAPagar >= 50.00m)
                    {
                        Navigation.InsertPageBefore(new InformacionDeCompra("6518C044-CE40-41F4-9344-92F0C200A8C2"), this);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Campo invalido", "la cantidad a pagar con tarjeta debe ser igual o superior a $50 MXN", "Aceptar");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}