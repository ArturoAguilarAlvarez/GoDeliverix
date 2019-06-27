using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarritoPage : TabbedPage
    {
        decimal cantidad = 0;
        decimal subtotal = 0;
        decimal TotalEnvio = 0;
        decimal TotalPagar = 0;
        HttpClient _client = new HttpClient();

        public CarritoPage()
        {
            InitializeComponent();

            if (App.MVProducto.ListaDelCarrito.Count > 0)
            {
                MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDelCarrito;
                MyListViewCarritoEmpresa.ItemsSource = App.MVProducto.ListaDelInformacionSucursales;
                //for (int i = 0; i < App.MVProducto.ListaDelInformacionSucursales.Count; i++)
                //{

                //}
                for (int i = 0; i < App.MVProducto.ListaDelCarrito.Count; i++)
                {
                    cantidad = cantidad + App.MVProducto.ListaDelCarrito[i].Cantidad;
                    decimal a = decimal.Parse(App.MVProducto.ListaDelCarrito[i].StrCosto);
                }              
                for (int i = 0; i < App.MVProducto.ListaDelInformacionSucursales.Count; i++)
                {
                    TotalEnvio= TotalEnvio+ App.MVProducto.ListaDelInformacionSucursales[i].CostoEnvio;
                    TotalPagar = TotalPagar + App.MVProducto.ListaDelInformacionSucursales[i].Total;
                    subtotal = subtotal + App.MVProducto.ListaDelInformacionSucursales[i].Subtotal;
                }
                ViewListaProductoVacio.IsVisible = false;
                ScrollView_Productos.IsVisible = false;

                #region mostrar los datos al usuario
                txtTotalEnvio.Text ="Total de envio: " + TotalEnvio.ToString();
                txtCantidad.Text = "Total de articulos: " + cantidad;
                txtsubtotal.Text = "SubTotal: $" + subtotal;
                

                btnPagar.Text = "Pagar  $" + TotalPagar;
                btnPagar2.Text = "Pagar  $" + TotalPagar;
                //panelCarrito.Title = " Carrito (" + cantidad + ")";
                #endregion
            }
            else
            {
                ViewListaProductoVacio.IsVisible = true;
                ScrollView_Productos.IsVisible = false;
            }
        }

        private  void BtnPagar_Clicked(object sender, EventArgs e)
        {
             BtnPagar_ClickedAsync();
        }
            private async void BtnPagar_ClickedAsync()
        {


            Guid UidOrden = Guid.NewGuid();
            decimal total = TotalPagar;
            Guid UidUsuario =new Guid( App.Global1);
            Guid UidDireccion = new Guid(App.DireccionABuscar);


            if (App.MVProducto.ListaDelCarrito.Count > 0)
            {

                if (!App.MVProducto.ListaDelInformacionSucursales.Exists(t => t.UidTarifario == Guid.Empty))
                {                    //Guarda la orden con la sucursal
                    for (int i = 0; i < App.MVProducto.ListaDelCarrito.Count; i++)
                    {
                        VMProducto objeto = App.MVProducto.ListaDelInformacionSucursales.Find(Suc => Suc.UidSucursal == App.MVProducto.ListaDelCarrito[i].UidSucursal);
                        var objetos = App.MVProducto.ListaDelCarrito.FindAll(Suc => Suc.UidSucursal == App.MVProducto.ListaDelCarrito[i].UidSucursal);
                        decimal totalSucursal = 0.0m;
                        Guid UidOrdenSucursal = Guid.NewGuid();
                        foreach (var item in objetos)
                        {
                            totalSucursal = totalSucursal + item.Subtotal;
                            //Guarda la relacion con los productos
                            Guid Uidnota = new Guid();
                            string mensaje = "";
                            if (item.UidNota == null || item.UidNota == Guid.Empty)
                            {
                                Uidnota = Guid.Empty;
                            }
                            else
                            {
                                Uidnota = item.UidNota;
                            }
                            if (!string.IsNullOrEmpty(item.StrNota) && item.StrNota != null)
                            {
                                mensaje = item.StrNota;
                            }
                            else
                            {
                                mensaje = "sin nota";
                            }
                            //App.MVOrden.GuardaProducto(UidOrdenSucursal,
                            //    item.UidSeccionPoducto,
                            //    item.Cantidad,
                            //    item.StrCosto,
                            //    item.UidSucursal,
                            //    item.UidRegistroProductoEnCarrito,
                            //    Uidnota, mensaje);


                            string _Url = $"http://godeliverix.net/api/Orden/GetGuardarProductos?UIDORDEN={UidOrdenSucursal}&UIDSECCIONPRODUCTO={item.UidSeccionPoducto}&INTCANTIDAD={item.Cantidad}&STRCOSTO={item.StrCosto}&UidSucursal={item.UidSucursal}&UidRegistroEncarrito={item.UidRegistroProductoEnCarrito}&UidNota={Uidnota}&StrMensaje={mensaje}";
                            var content = await _client.GetAsync(_Url);

                        }
                        //Envia la orden a la sucursal suministradora
                        Random Codigo = new Random();
                        long CodigoDeEnrega = Codigo.Next(00001, 99999);
                        //App.MVOrden.GuardaOrden(
                        //    UidOrden,
                        //    total,
                        //    UidUsuario,
                        //    UidDireccion,
                        //    objeto.UidSucursal,
                        //    totalSucursal,
                        //    UidOrdenSucursal,
                        //    CodigoDeEnrega);

                        string _Url1 = $"http://godeliverix.net/api/Orden/GetGuardarOrden?UIDORDEN={UidOrden}&Total={total}&Uidusuario={UidUsuario}&UidDireccion={UidDireccion}&Uidsucursal={objeto.UidSucursal}&totalSucursal={totalSucursal}&UidRelacionOrdenSucursal={UidOrdenSucursal}&LngCodigoDeEntrega={CodigoDeEnrega}";
                        var content1 = await _client.GetAsync(_Url1);

                        // Envia la orden a la sucursal distribuidora
                        //App.MVTarifario.AgregarTarifarioOrden(UidOrden: UidOrdenSucursal, UidTarifario: objeto.UidTarifario);

                        string _Url2 = $@"http://godeliverix.net/api/Tarifario/GetGuardarTarifario?UidOrdenSucursal={UidOrdenSucursal}&UidTarifario={objeto.UidTarifario}";
                        var content2 = await _client.GetAsync(_Url2);

                        //Una vez que se haya guardado ella basededatosse le cambia el estatus a la orden
                        //App.MVOrden.AgregaEstatusALaOrden(new Guid("DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC"), UidOrden: UidOrdenSucursal, StrParametro: "U", UidSucursal: objeto.UidSucursal);

                        string _Url3 = $"http://godeliverix.net/api/Orden/GetAgregaEstatusALaOrden?UidEstatus=DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC&StrParametro=U&UidOrden={UidOrdenSucursal}&UidSucursal={objeto.UidSucursal}";
                        var content3 = await _client.GetAsync(_Url3);

                        App.MVProducto.ListaDelCarrito.RemoveAll(sucursal => sucursal.UidSucursal == objeto.UidSucursal);
                        i = i - 1;
                    }
                    LimpiarCarrito();
                    App.MVOrden.ObtenerInformacionDeLaUltimaOrden(UidUsuario);
                    await DisplayAlert("Que bien!", "Su orden esta en camino", "ok");
                }
                else
                {
                    await DisplayAlert("NO a escogido distribuidora","No se ha elegido una empresa distribuidora dentro de la orden", "ok");
                }
            }

        }

        private async void MyListViewCarritoEmpresa_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            var item = ((ItemTappedEventArgs)e);
            VMProducto ObjItem = (VMProducto)item.Item;
            MyListViewCarritoEmpresa.ItemsSource = App.MVProducto.ListaDelInformacionSucursales;
            await Navigation.PushAsync(new CarritoDetalleSucursal(ObjItem, txtCantidad, txtsubtotal, txtTotalEnvio,MyListViewCarritoEmpresa, btnPagar, btnPagar2));
            await PopupNavigation.Instance.PopAsync();
        }


        private void LimpiarCarrito()
        {

                App.MVProducto.ListaDelCarrito.Clear();
                App.MVProducto.ListaDelInformacionSucursales.Clear();
                MyListViewBusquedaProductos.ItemsSource = null;
                MyListViewCarritoEmpresa.ItemsSource = null;
                txtCantidad.Text = "Total de articulos: 0";
                txtsubtotal.Text = "SubTotal: $0.00";
                txtTotalEnvio.Text = "Total de envio : $0.00";
                btnPagar.Text = "pagar $0.00";
                btnPagar2.Text = "pagar $0.00";
            
        }

        private async void BtnLimpiarCarrito_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("Oooops!", "¿Desea eliminar el contenido del carrito?", "Si", "No");
            if (action)
            {
                LimpiarCarrito();
            }
        }

        private async void ButtonCambiarRepartidor_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            var item = sender as Button;
            var ObjItem = item.BindingContext as VMProducto;

            App.MVTarifario.BuscarTarifario("Cliente", ZonaEntrega: App.DireccionABuscar, uidSucursal: ObjItem.UidSucursal.ToString());
            await Navigation.PushAsync(new SeleccionarDistribuidoraCarrito(ObjItem,MyListViewCarritoEmpresa,txtTotalEnvio,btnPagar, btnPagar2));
            await PopupNavigation.Instance.PopAsync();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var item = sender as Button;
            var ObjItem = item.BindingContext as VMProducto;    
        }
    }
}