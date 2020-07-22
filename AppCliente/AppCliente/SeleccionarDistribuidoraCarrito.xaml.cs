using AppCliente.ViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeleccionarDistribuidoraCarrito : ContentPage
    {

        MVMProductos ObjItem;
        ListView listaEmpresa;
        Label txtTotalEnvio;
        Button btnPagar;
        Button btnPagar2;

        decimal cantidad = 0;
        decimal subtotal = 0;
        decimal TotalEnvio = 0;
        decimal TotalPagar = 0;

        public SeleccionarDistribuidoraCarrito(MVMProductos ObjItem)
        {
            InitializeComponent();
            this.ObjItem = ObjItem;
            MyListViewDistribuidora.ItemsSource = null;
            MyListViewDistribuidora.ItemsSource = App.MVTarifario.ListaDeTarifarios;
        }

        public SeleccionarDistribuidoraCarrito(MVMProductos ObjItem, ListView listaEmpresasDelCarrito, Label txtTotalEnvio, Button btnPagar, Button btnPagar2)
        {
            InitializeComponent();
            this.txtTotalEnvio = txtTotalEnvio;
            this.btnPagar = btnPagar;

            this.btnPagar2 = btnPagar2;
            listaEmpresa = listaEmpresasDelCarrito;
            this.ObjItem = ObjItem;
            MyListViewDistribuidora.ItemsSource = null;

            MyListViewDistribuidora.ItemsSource = App.MVTarifario.ListaDeTarifarios;
            var item = App.MVTarifario.ListaDeTarifarios.Find(t => t.UidTarifario == ObjItem.UidTarifario);
            MyListViewDistribuidora.SelectedItem = item;
        }


        public SeleccionarDistribuidoraCarrito(MVMProductos ObjItem, ListView listaEmpresasDelCarrito, Button btnPagar)
        {
            InitializeComponent();
            this.btnPagar = btnPagar;

            listaEmpresa = listaEmpresasDelCarrito;
            this.ObjItem = ObjItem;
            MyListViewDistribuidora.ItemsSource = null;

            MyListViewDistribuidora.ItemsSource = App.MVTarifario.ListaDeTarifarios;
            var item = App.MVTarifario.ListaDeTarifarios.Find(t => t.UidTarifario == ObjItem.UidTarifario);
            MyListViewDistribuidora.SelectedItem = item;
            var objeto = App.MVProducto.ListaDelCarrito.Find(o => o.UidSucursal == ObjItem.UidSucursal);
            lblPropina.Text = "$" + objeto.DPropina.ToString("N2");
        }

        private void MyListViewDistribuidora_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //var item = e;
            //listaEmpresa.ItemsSource = null;
            //listaEmpresa.ItemsSource = App.MVProducto.ListaDelInformacionSucursales;
            //VMTarifario registro = (VMTarifario)item.Item;
            //App.MVProducto.AgregaTarifarioOrden(ObjItem.UidSucursal, registro.UidTarifario, registro.DPrecio);

            //cantidad = 0;
            //subtotal = 0;
            //TotalEnvio = 0;
            //TotalPagar = 0;
            //for (int i = 0; i < App.MVProducto.ListaDelCarrito.Count; i++)
            //{
            //    cantidad = cantidad + App.MVProducto.ListaDelCarrito[i].Cantidad;
            //    decimal a = decimal.Parse(App.MVProducto.ListaDelCarrito[i].StrCosto);
            //}
            //for (int i = 0; i < App.MVProducto.ListaDelInformacionSucursales.Count; i++)
            //{
            //    TotalEnvio = TotalEnvio + App.MVProducto.ListaDelInformacionSucursales[i].CostoEnvio;
            //    TotalPagar = TotalPagar + App.MVProducto.ListaDelInformacionSucursales[i].Total;
            //    subtotal = subtotal + App.MVProducto.ListaDelInformacionSucursales[i].Subtotal;
            //}

            //txtTotalEnvio.Text = "Total de envio " + TotalEnvio.ToString();

            //btnPagar.Text = "Pagar  $" + TotalPagar;
            //btnPagar2.Text = "Pagar  $" + TotalPagar;

            //Navigation.PopAsync();
        }

        private async void btnPropina_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            var item = sender as Button;
            await Navigation.PushAsync(new ModificarPropina(UidSucursal: ObjItem.UidSucursal, listaEmpresa, btnPagar));
            await PopupNavigation.Instance.PopAsync();
        }

        private void BtnPanelPropina_Clicked(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            if (!pnlAgregarPropina.IsVisible)
            {
                BtnPanelPropina.Text = "\U000f0063";
                //BtnPanelPropina.BackgroundColor = Color.Red;
                BtnPanelPropina.HeightRequest = 60.00;
                BtnPanelPropina.WidthRequest = 60.00;
                pnlAgregarPropina.IsVisible = true;
            }
            else
            if (pnlAgregarPropina.IsVisible)
            {
                //PnlPropina.HeightRequest = 50.00;
                BtnPanelPropina.Text = "\U000f004b";
                //BtnPanelPropina.BackgroundColor = Color.Green;
                BtnPanelPropina.HeightRequest = 60.00;
                BtnPanelPropina.WidthRequest = 60.00;
                pnlAgregarPropina.IsVisible = false;

            }
        }
        private void AgregarPropinaBotones(object sender, EventArgs e)
        {
            Button propina = sender as Button;
            string valor = propina.Text.Replace("$", "");
            decimal cantidad = 0.0m;
            if (decimal.TryParse(valor, out cantidad))
            {
                var objeto = App.MVProducto.ListaDelCarrito.Find(o => o.UidSucursal == ObjItem.UidSucursal);
                var orden = App.MVProducto.ListaDelInformacionSucursales.Find(o => o.UidSucursal == ObjItem.UidSucursal);
                orden.DPropina = cantidad;
                objeto.DPropina = cantidad;
                lblPropina.Text = "$" + cantidad;
            }
            else
            {
                DisplayAlert("Cantidad no valida", "El valor ingresado no es valido", "ok");
            }
        }

        private void lblMontoPropina_TextChanged(object sender, TextChangedEventArgs e)
        {
            string valor = lblMontoPropina.Text;
            decimal cantidad = 0.0m;
            if (decimal.TryParse(valor, out cantidad))
            {
                var objeto = App.MVProducto.ListaDelCarrito.Find(o => o.UidSucursal == ObjItem.UidSucursal);
                var orden = App.MVProducto.ListaDelInformacionSucursales.Find(o => o.UidSucursal == ObjItem.UidSucursal);
                orden.DPropina = cantidad;
                objeto.DPropina = cantidad;
                lblPropina.Text = "$" + cantidad;
            }
            else
            {
                DisplayAlert("Cantidad no valida", "El valor ingresado no es valido", "ok");
            }
        }
    }
}