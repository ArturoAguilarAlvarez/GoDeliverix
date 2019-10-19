using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarPropina : ContentPage
    {
        Guid UidSucursal;
        ListView Carrito;
        Button btnresumen;
        Button btndetalle;
        Label LblPropina;
        public ModificarPropina(Guid UidSucursal, ListView LVCarrito, Button btnPagarResumen, Button btnPagarDetalle, Label lblPropina)
        {
            this.UidSucursal = UidSucursal;
            Carrito = LVCarrito;
            btnresumen = btnPagarResumen;
            btndetalle = btnPagarDetalle;
            LblPropina = lblPropina;
            InitializeComponent();
            var sucursal = App.MVProducto.ListaDelInformacionSucursales.Find(s => s.UidSucursal == UidSucursal);
            lblpropina.Text = sucursal.DPropina.ToString();
            PCantidadDePropina.SelectedIndex = 0;
        }

        private async void BtnAgregarPropina_Clicked(object sender, EventArgs e)
        {
            Carrito.ItemsSource = null;
            Carrito.ItemsSource = App.MVProducto.ListaDelInformacionSucursales;
            var sucursal = App.MVProducto.ListaDelInformacionSucursales.Find(s => s.UidSucursal == UidSucursal);
            decimal propina = new decimal();
            if (PCantidadDePropina.SelectedIndex != 0)
            {
                propina = decimal.Parse(PCantidadDePropina.SelectedItem.ToString().Substring(1, 2));
                sucursal.Total -= sucursal.DPropina;
                sucursal.DPropina = propina;
                decimal subtotal = 0;
                decimal TotalEnvio = 0;
                decimal TotalPagar = 0;
                decimal TotalPropina = 0;
                sucursal.Total += propina;

                for (int i = 0; i < App.MVProducto.ListaDelInformacionSucursales.Count; i++)
                {
                    TotalEnvio += App.MVProducto.ListaDelInformacionSucursales[i].CostoEnvio;
                    TotalPagar += App.MVProducto.ListaDelInformacionSucursales[i].Total;
                    subtotal += App.MVProducto.ListaDelInformacionSucursales[i].Subtotal;
                    TotalPropina += App.MVProducto.ListaDelInformacionSucursales[i].DPropina;
                }
                
                LblPropina.Text = "$" + TotalPropina;
                btnresumen.Text = "Pagar  $" + TotalPagar;
                btndetalle.Text = "Pagar  $" + TotalPagar;

                await Navigation.PopAsync();
            }
            else
            {
                decimal resultado = 0.0m;
                if (decimal.TryParse(lblMontoPropina.Text, out resultado))
                {
                    propina = decimal.Parse(lblMontoPropina.Text);
                    sucursal.Total -= sucursal.DPropina;
                    sucursal.DPropina = propina;
                    decimal subtotal = 0;
                    decimal TotalEnvio = 0;
                    decimal TotalPagar = 0;
                    decimal TotalPropina = 0;
                    sucursal.Total += propina;
                    for (int i = 0; i < App.MVProducto.ListaDelInformacionSucursales.Count; i++)
                    {
                        TotalEnvio += App.MVProducto.ListaDelInformacionSucursales[i].CostoEnvio;
                        TotalPagar += App.MVProducto.ListaDelInformacionSucursales[i].Total;
                        subtotal += App.MVProducto.ListaDelInformacionSucursales[i].Subtotal;
                        TotalPropina += App.MVProducto.ListaDelInformacionSucursales[i].DPropina;
                    }
                    
                    LblPropina.Text = TotalPropina.ToString();
                    btnresumen.Text = "Pagar  $" + TotalPagar;
                    btndetalle.Text = "Pagar  $" + TotalPagar;

                    await Navigation.PopAsync();

                }
                else
                {
                    GenerateMessage("Numero invalido", "El formato de la propina no es correcto", "Ok");
                }
            }


        }

        protected async void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }
    }
}