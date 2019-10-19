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
	public partial class CarritoDetalleSucursal : ContentPage
	{
        VMProducto ObjItem;
        Label txtCantidad;
        Label txtsubtotal;
        Label txtTotalEnvio;
        Button btnPagar;
        Button btnPagar2;
        ListView MyListViewCarritoEmpresa;

        decimal cantidad = 0;
        decimal subtotal = 0;
        decimal TotalEnvio = 0;
        decimal TotalPagar = 0;

        public CarritoDetalleSucursal ()
		{
			InitializeComponent ();
            
		}

        public CarritoDetalleSucursal(VMProducto ObjItem, Label txtCantidad, Label txtSubtotal, Label txtTotalEnvio, ListView MyListViewCarritoEmpresa, Button btnPagar, Button btnPagar2)
        {
            InitializeComponent();

            this.txtCantidad = txtCantidad;
            this.txtsubtotal = txtSubtotal;
            this.txtTotalEnvio = txtTotalEnvio;
            this.btnPagar = btnPagar;
            this.btnPagar2 = btnPagar2;


            this.MyListViewCarritoEmpresa = MyListViewCarritoEmpresa;
            App.MVProducto.ListaDeDetallesDeOrden.Clear();
            for (int i = 0; i < App.MVProducto.ListaDelCarrito.Count; i++)
            {
                App.MVProducto.ListaDelCarrito[i].IsVisible = false;
                if (ObjItem.UidSucursal == App.MVProducto.ListaDelCarrito[i].UidSucursal)
                {
                    App.MVProducto.ListaDeDetallesDeOrden.Add(App.MVProducto.ListaDelCarrito[i]);
                }
                this.ObjItem = ObjItem;
            }
            MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;

        }

        private void BtnDistribuidora_Clicked(object sender, EventArgs e)
        {

          App.MVTarifario.BuscarTarifario("Cliente", ZonaEntrega: App.DireccionABuscar, uidSucursal: ObjItem.UidSucursal.ToString());
            Navigation.PushAsync(new SeleccionarDistribuidoraCarrito(ObjItem));
        }

        private void ButtonMenos_Clicked(object sender, EventArgs e)
        {
            var item = AppCliente.App.MVProducto.ListaDeDetallesDeOrden.Find(x => x.IsVisible == true);

            App.MVProducto.QuitarDelCarrito(item.UidRegistroProductoEnCarrito);
            MyListViewBusquedaProductos.ItemsSource = null;
            App.MVProducto.ListaDeDetallesDeOrden = null;
            App.MVProducto.ListaDeDetallesDeOrden=App.MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == ObjItem.UidSucursal).ToList();


            ActualizarCarrito();

            MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;

        }

        private void ButtonMas_Clicked_1(object sender, EventArgs e)
        {

            var item = AppCliente.App.MVProducto.ListaDeDetallesDeOrden.Find(x => x.IsVisible == true);
            Guid seccion = new Guid();
            App.MVProducto.AgregaAlCarrito(item.UidRegistroProductoEnCarrito, item.UidSucursal, seccion, "1", RegistroProductoEnCarrito: item.UidRegistroProductoEnCarrito);

            MyListViewBusquedaProductos.ItemsSource = null;
            App.MVProducto.ListaDeDetallesDeOrden = null;
            App.MVProducto.ListaDeDetallesDeOrden = App.MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == ObjItem.UidSucursal).ToList();

            ActualizarCarrito();
            MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;
        }

        private void ButtonEliminar_Clicked(object sender, EventArgs e)
        {
            var item = AppCliente.App.MVProducto.ListaDeDetallesDeOrden.Find(x => x.IsVisible == true);

            App.MVProducto.EliminaProductoDelCarrito(item.UidRegistroProductoEnCarrito);

            MyListViewBusquedaProductos.ItemsSource = null;
            App.MVProducto.ListaDeDetallesDeOrden = null;
            App.MVProducto.ListaDeDetallesDeOrden = App.MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == ObjItem.UidSucursal).ToList();

            MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;

            ActualizarCarrito();

        }

        private void MyListViewBusquedaProductos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //var item = ((ItemTappedEventArgs)e);
            //VMProducto ObjItem = (VMProducto)item.Item;
            //if (ObjItem.IsVisible)
            //{
            //    ObjItem.IsVisible = false;
            //}
            //else
            //{
            //    for (int i = 0; i < App.MVProducto.ListaDeDetallesDeOrden.Count; i++)
            //    {
            //        App.MVProducto.ListaDeDetallesDeOrden[i].IsVisible = false;
            //    }
            //    ObjItem.IsVisible = true;
            //}
            //MyListViewBusquedaProductos.ItemsSource = null;
            //MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;
        }


        public void ActualizarCarrito()
        {
             cantidad = 0;
             subtotal = 0;
             TotalEnvio = 0;
             TotalPagar = 0;
            MyListViewBusquedaProductos.ItemsSource = null;
            MyListViewCarritoEmpresa.ItemsSource = null;
           MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDelCarrito;
            MyListViewCarritoEmpresa.ItemsSource = App.MVProducto.ListaDelInformacionSucursales;
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
            

            txtTotalEnvio.Text = "Total de envio: " + TotalEnvio.ToString();
            txtCantidad.Text = "Total de articulos: " + cantidad;
            txtsubtotal.Text = "SubTotal: $" + subtotal;


            btnPagar.Text = "Pagar  $" + TotalPagar;
            btnPagar2.Text = "Pagar  $" + TotalPagar;
        }

        private async void ImageButtonEliminarProducto_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("", "Desea eliminar el productos del carrito", "Si", "No");
            if (action)
            {
                var item = sender as ImageButton;
                var ObjItem = item.BindingContext as VMProducto;

                App.MVProducto.EliminaProductoDelCarrito(ObjItem.UidRegistroProductoEnCarrito);

                MyListViewBusquedaProductos.ItemsSource = null;
                App.MVProducto.ListaDeDetallesDeOrden = null;
                App.MVProducto.ListaDeDetallesDeOrden = App.MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == ObjItem.UidSucursal).ToList();

                MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;

                ActualizarCarrito();
            }
        }

        private void ButtonEliminarUnProducto_Clicked(object sender, EventArgs e)
        {
            var item = sender as Button;
            var ObjItem = item.BindingContext as VMProducto;


            App.MVProducto.QuitarDelCarrito(ObjItem.UidRegistroProductoEnCarrito);
            MyListViewBusquedaProductos.ItemsSource = null;
            App.MVProducto.ListaDeDetallesDeOrden = null;
            App.MVProducto.ListaDeDetallesDeOrden = App.MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == ObjItem.UidSucursal).ToList();


            ActualizarCarrito();

            MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;
        }

        private void ButtonAgregarUnProducto_Clicked_1(object sender, EventArgs e)
        {
            var item = sender as Button;
            var ObjItem = item.BindingContext as VMProducto;
            
            Guid seccion = new Guid();
            App.MVProducto.AgregaAlCarrito(ObjItem.UidRegistroProductoEnCarrito, ObjItem.UidSucursal, seccion, "1", RegistroProductoEnCarrito: ObjItem.UidRegistroProductoEnCarrito);

            MyListViewBusquedaProductos.ItemsSource = null;
            App.MVProducto.ListaDeDetallesDeOrden = null;
            App.MVProducto.ListaDeDetallesDeOrden = App.MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == ObjItem.UidSucursal).ToList();

            ActualizarCarrito();
            MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;
        }

        private void ENotas_TextChanged(object sender, TextChangedEventArgs e)
        {
            var item = sender as Entry;
            var ObjItem = item.BindingContext as VMProducto;

            var objeto = App.MVProducto.ListaDeDetallesDeOrden.Find(s => s.UidRegistroProductoEnCarrito == ObjItem.UidRegistroProductoEnCarrito);
            objeto.StrNota = item.Text;
        }
    }
}