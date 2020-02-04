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

        VMProducto ObjItem;
        ListView listaEmpresa;
        Label txtTotalEnvio;
        Button btnPagar;
        Button btnPagar2;

        decimal cantidad = 0;
        decimal subtotal = 0;
        decimal TotalEnvio = 0;
        decimal TotalPagar = 0;

        public SeleccionarDistribuidoraCarrito(VMProducto ObjItem)
        {
            InitializeComponent();
            this.ObjItem = ObjItem;
            MyListViewDistribuidora.ItemsSource = null;
            MyListViewDistribuidora.ItemsSource = App.MVTarifario.ListaDeTarifarios;
        }

        public SeleccionarDistribuidoraCarrito(VMProducto ObjItem, ListView listaEmpresasDelCarrito, Label txtTotalEnvio, Button btnPagar, Button btnPagar2)
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
        
        
        public SeleccionarDistribuidoraCarrito(VMProducto ObjItem, ListView listaEmpresasDelCarrito,  Button btnPagar)
        {
            InitializeComponent();
            this.btnPagar = btnPagar;

            listaEmpresa = listaEmpresasDelCarrito;
            this.ObjItem = ObjItem;
            MyListViewDistribuidora.ItemsSource = null;

            MyListViewDistribuidora.ItemsSource = App.MVTarifario.ListaDeTarifarios;
            var item = App.MVTarifario.ListaDeTarifarios.Find(t => t.UidTarifario == ObjItem.UidTarifario);
            MyListViewDistribuidora.SelectedItem = item;
        }

        private void MyListViewDistribuidora_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e;
            listaEmpresa.ItemsSource = null;
            listaEmpresa.ItemsSource = App.MVProducto.ListaDelInformacionSucursales;
            VMTarifario registro = (VMTarifario)item.Item;
            App.MVProducto.AgregaTarifarioOrden(ObjItem.UidSucursal, registro.UidTarifario, registro.DPrecio);

            cantidad = 0;
            subtotal = 0;
            TotalEnvio = 0;
            TotalPagar = 0;
            for (int i = 0; i < App.MVProducto.ListaDelCarrito.Count; i++)
            {
                cantidad = cantidad + App.MVProducto.ListaDelCarrito[i].Cantidad;
                decimal a = decimal.Parse(App.MVProducto.ListaDelCarrito[i].StrCosto);
            }
            for (int i = 0; i < App.MVProducto.ListaDelInformacionSucursales.Count; i++)
            {
                TotalEnvio = TotalEnvio + App.MVProducto.ListaDelInformacionSucursales[i].CostoEnvio;
                TotalPagar = TotalPagar + App.MVProducto.ListaDelInformacionSucursales[i].Total;
                subtotal = subtotal + App.MVProducto.ListaDelInformacionSucursales[i].Subtotal;
            }

            txtTotalEnvio.Text = "Total de envio " + TotalEnvio.ToString();

            btnPagar.Text = "Pagar  $" + TotalPagar;
            btnPagar2.Text = "Pagar  $" + TotalPagar;

            Navigation.PopAsync();
        }
    }
}