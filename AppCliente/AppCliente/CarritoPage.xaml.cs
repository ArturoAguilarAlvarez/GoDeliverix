using AppCliente.WebApi;
using Newtonsoft.Json;
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
        decimal TotalPropina = 0;
        HttpClient _client = new HttpClient();

        public CarritoPage()
        {
            InitializeComponent();
            //using (var _webApi = new HttpClient())
            //{
            //    string url = "https://www.godeliverix.net/api/Direccion/GetBuscarDireccion?UidDireccion=" + App.DireccionABuscar + "";
            //    var content = _webApi.GetStringAsync(url);
            //    var obj = JsonConvert.DeserializeObject<ResponseHelper>(content.ToString()).Data.ToString();
            //    MDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
            //}

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

                    TotalEnvio = TotalEnvio + App.MVProducto.ListaDelInformacionSucursales[i].CostoEnvio;
                    TotalPagar = TotalPagar + App.MVProducto.ListaDelInformacionSucursales[i].Total;
                    subtotal = subtotal + App.MVProducto.ListaDelInformacionSucursales[i].Subtotal;
                    TotalPropina += App.MVProducto.ListaDelInformacionSucursales[i].DPropina;
                }
                ViewListaProductoVacio.IsVisible = false;
                ScrollView_Productos.IsVisible = false;

                #region mostrar los datos al usuario
                txtTotalEnvio.Text = "$" + TotalEnvio.ToString();
                txtCantidad.Text = cantidad.ToString();
                txtsubtotal.Text = "$" + subtotal.ToString();
                txtCantidadSucursales.Text = App.MVProducto.ListaDelInformacionSucursales.Count.ToString();
                txtPropina.Text = "$" + TotalPropina;
                btnPagar.Text = "Pagar  $" + TotalPagar;
                btnPagar2.Text = "Pagar  $" + TotalPagar;
                #endregion
            }
            else
            {
                #region mostrar los datos al usuario
                txtTotalEnvio.Text = "$0.00";
                txtCantidad.Text = "0";
                txtsubtotal.Text = "$0.00";
                txtCantidadSucursales.Text = "0";
                txtPropina.Text = "$0.00";
                btnPagar.Text = "Pagar  $0.00";
                btnPagar2.Text = "Pagar  $0.00";
                #endregion
                ViewListaProductoVacio.IsVisible = true;
                ScrollView_Productos.IsVisible = false;
            }

        }

        private void BtnPagar_Clicked(object sender, EventArgs e)
        {
            BtnPagar_ClickedAsync();
        }
        private async void BtnPagar_ClickedAsync()
        {


            Guid UidOrden = Guid.NewGuid();
            decimal total = TotalPagar;
            Guid UidUsuario = new Guid(App.Global1);
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
                            string _Url = $"http://godeliverix.net/api/Orden/GetGuardarProductos?" +
                                $"UIDORDEN={UidOrdenSucursal}" +
                                $"&UIDSECCIONPRODUCTO={item.UidSeccionPoducto}" +
                                $"&INTCANTIDAD={item.Cantidad}" +
                                $"&STRCOSTO={item.StrCosto}" +
                                $"&UidSucursal={item.UidSucursal}" +
                                $"&UidRegistroEncarrito={item.UidRegistroProductoEnCarrito}" +
                                $"&UidNota={Uidnota}" +
                                $"&StrMensaje={mensaje}";
                            var content = await _client.GetAsync(_Url);
                        }
                        //Envia la orden a la sucursal suministradora
                        Random Codigo = new Random();
                        long CodigoDeEnrega = Codigo.Next(00001, 99999);
                       

                        string _Url1 = $"http://godeliverix.net/api/Orden/GetGuardarOrden?" +
                            $"UIDORDEN={UidOrden}" +
                            $"&Total={total}" +
                            $"&Uidusuario={UidUsuario}" +
                            $"&UidDireccion={UidDireccion}" +
                            $"&Uidsucursal={objeto.UidSucursal}" +
                            $"&totalSucursal={totalSucursal}" +
                            $"&UidRelacionOrdenSucursal={UidOrdenSucursal}" +
                            $"&LngCodigoDeEntrega={CodigoDeEnrega}";
                        var content1 = await _client.GetAsync(_Url1);

                        // Envia la orden a la sucursal distribuidora
                        string _Url2 = $@"http://godeliverix.net/api/Tarifario/GetGuardarTarifario?" +
                            $"UidOrdenSucursal={UidOrdenSucursal}" +
                            $"&DPropina={objeto.DPropina}" +
                            $"&UidTarifario={objeto.UidTarifario}";
                        var content2 = await _client.GetAsync(_Url2);

                        //Una vez que se haya guardado ella basededatosse le cambia el estatus a la orden
                        string _Url3 = $"http://godeliverix.net/api/Orden/GetAgregaEstatusALaOrden?" +
                            $"UidEstatus=DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC" +
                            $"&StrParametro=U" +
                            $"&UidOrden={UidOrdenSucursal}" +
                            $"&UidSucursal={objeto.UidSucursal}";
                        var content3 = await _client.GetAsync(_Url3);

                        App.MVProducto.ListaDelCarrito.RemoveAll(sucursal => sucursal.UidSucursal == objeto.UidSucursal);
                        i = i - 1;
                    }
                    LimpiarCarrito();
                    App.MVOrden.ObtenerInformacionDeLaUltimaOrden(UidUsuario);
                    await DisplayAlert("Felicidades!", "Se ha enviado su orden", "OK");
                }
                else
                {
                    await DisplayAlert("NO a escogido distribuidora", "No se ha elegido una empresa distribuidora dentro de la orden", "ok");
                }
            }
        }

        private async void MyListViewCarritoEmpresa_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            var item = ((ItemTappedEventArgs)e);
            VMProducto ObjItem = (VMProducto)item.Item;
            MyListViewCarritoEmpresa.ItemsSource = App.MVProducto.ListaDelInformacionSucursales;
            await Navigation.PushAsync(new CarritoDetalleSucursal(ObjItem, txtCantidad, txtsubtotal, txtTotalEnvio, MyListViewCarritoEmpresa, btnPagar, btnPagar2));
            await PopupNavigation.Instance.PopAsync();
        }

        private void LimpiarCarrito()
        {
            App.MVProducto.ListaDelCarrito.Clear();
            App.MVProducto.ListaDelInformacionSucursales.Clear();
            MyListViewBusquedaProductos.ItemsSource = null;
            MyListViewCarritoEmpresa.ItemsSource = null;
            txtCantidad.Text = "0";
            txtPropina.Text = "$0.00";
            txtCantidadSucursales.Text = "0";
            txtsubtotal.Text = "$0.00";
            txtTotalEnvio.Text = "$0.00";
            btnPagar.Text = "pagar $0.00";
            btnPagar2.Text = "pagar $0.00";
        }

        private async void BtnLimpiarCarrito_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("Vaciar carrito", "¿Desea eliminar el contenido del carrito?", "Si", "No");
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
            await Navigation.PushAsync(new SeleccionarDistribuidoraCarrito(ObjItem, MyListViewCarritoEmpresa, txtTotalEnvio, btnPagar, btnPagar2));
            await PopupNavigation.Instance.PopAsync();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var item = sender as Button;
            var ObjItem = item.BindingContext as VMProducto;
        }

        private async void BtnAgregarPropina_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            var item = sender as Button;
            var ObjItem = item.BindingContext as VMProducto;
            await Navigation.PushAsync(new ModificarPropina(UidSucursal: ObjItem.UidSucursal, MyListViewCarritoEmpresa, btnPagar, btnPagar2, txtPropina));
            await PopupNavigation.Instance.PopAsync();

        }


    }
}