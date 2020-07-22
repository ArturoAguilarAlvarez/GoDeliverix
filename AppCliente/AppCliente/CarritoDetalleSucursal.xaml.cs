using AppCliente.ViewModel;
using AppCliente.WebApi;
using CoreImage;
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
    public partial class CarritoDetalleSucursal : ContentPage
    {
        MVMProductos ObjItem;
        Label txtCantidad;
        Label txtsubtotal;
        Label txtTotalEnvio;
        Button btnPagar;
        Button btnPagar2;
        ListView MyListViewCarritoEmpresa;
        List<MVMProductos> listaDelCarrito = new List<MVMProductos>();

        decimal cantidad = 0;
        decimal subtotal = 0;
        decimal TotalEnvio = 0;
        decimal TotalPropina = 0;
        decimal TotalPagar = 0;

        public CarritoDetalleSucursal()
        {
            InitializeComponent();

        }

        //public CarritoDetalleSucursal(MVMProductos ObjItem, Label txtCantidad, Label txtSubtotal, Label txtTotalEnvio, ListView MyListViewCarritoEmpresa, Button btnPagar, Button btnPagar2)
        //{
        //    InitializeComponent();

        //    this.txtCantidad = txtCantidad;
        //    this.txtsubtotal = txtSubtotal;
        //    this.txtTotalEnvio = txtTotalEnvio;
        //    this.btnPagar = btnPagar;
        //    this.btnPagar2 = btnPagar2;

        //    this.MyListViewCarritoEmpresa = MyListViewCarritoEmpresa;
        //    App.MVProducto.ListaDeDetallesDeOrden.Clear();
        //    for (int i = 0; i < App.MVProducto.ListaDelCarrito.Count; i++)
        //    {
        //        App.MVProducto.ListaDelCarrito[i].IsVisible = false;
        //        if (ObjItem.UidSucursal == App.MVProducto.ListaDelCarrito[i].UidSucursal)
        //        {
        //            App.MVProducto.ListaDeDetallesDeOrden.Add(App.MVProducto.ListaDelCarrito[i]);
        //        }
        //        this.ObjItem = ObjItem;
        //    }
        //    MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;
        //}

        public CarritoDetalleSucursal(MVMProductos ObjItem, ListView MyListViewCarritoEmpresa, Button btnPagar)
        {
            InitializeComponent();
            this.btnPagar = btnPagar;
            this.MyListViewCarritoEmpresa = MyListViewCarritoEmpresa;
            this.ObjItem = ObjItem;
            CargaProductos();
        }
        protected async void CargaProductos()
        {
            App.MVProducto.ListaDeDetallesDeOrden.Clear();
            List<MVMProductos> ListaDeDetallesDeOrden = new List<MVMProductos>();
            cantidad = 0;
            subtotal = 0;
            TotalPropina = 0;
            TotalEnvio = 0;
            listaDelCarrito = new List<MVMProductos>();
            for (int i = 0; i < App.MVProducto.ListaDelCarrito.Count; i++)
            {
                HttpClient _WebApi = new HttpClient();
                string _URL = "" + Helpers.Settings.sitio + "/api/Seccion/GetBuscaSeccion?UIDSECCIONProducto=" + App.MVProducto.ListaDelCarrito[i].UidSeccionPoducto.ToString() + "";
                var content = await _WebApi.GetStringAsync(_URL);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                var oSeccion = JsonConvert.DeserializeObject<VMSeccion>(obj);

                _URL = "" + Helpers.Settings.sitio + "/api/Usuario/GetObtenerHora?UidEstado=" + App.UidEstadoABuscar + "";
                content = await _WebApi.GetStringAsync(_URL);
                obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                DateTime HoraActual = DateTime.Parse(obj);

                DateTime HoraSeccion = DateTime.Parse(oSeccion.StrHoraFin);
                TimeSpan TiempoRestante = new TimeSpan(0, 10, 0);
                TimeSpan Diferencia = new TimeSpan();
                Diferencia = (HoraSeccion - HoraActual);
                Color ocolor = new Color();
                if (HoraActual > HoraSeccion)
                {
                    ocolor = Color.Red;
                }
                else
                {
                    ocolor = Color.White;
                }
                TotalPropina = App.MVProducto.ListaDelCarrito[i].DPropina;
                TotalEnvio = App.MVProducto.ListaDelCarrito[i].CostoEnvio;
                cantidad = cantidad + App.MVProducto.ListaDelCarrito[i].Cantidad;
                decimal a = decimal.Parse(App.MVProducto.ListaDelCarrito[i].StrCosto);
                subtotal += (App.MVProducto.ListaDelCarrito[i].Cantidad * a);

                listaDelCarrito.Add(new MVMProductos()
                {
                    UidRegistroProductoEnCarrito = App.MVProducto.ListaDelCarrito[i].UidRegistroProductoEnCarrito,
                    UidSeccionPoducto = App.MVProducto.ListaDelCarrito[i].UidSeccionPoducto,
                    Total = App.MVProducto.ListaDelCarrito[i].Total,
                    Subtotal = App.MVProducto.ListaDelCarrito[i].Subtotal,
                    CostoEnvio = App.MVProducto.ListaDelCarrito[i].CostoEnvio,
                    DPropina = App.MVProducto.ListaDelCarrito[i].DPropina,
                    UID = App.MVProducto.ListaDelCarrito[i].UID,
                    UidSucursal = App.MVProducto.ListaDelCarrito[i].UidSucursal,
                    STRNOMBRE = App.MVProducto.ListaDelCarrito[i].STRNOMBRE,
                    StrCosto = App.MVProducto.ListaDelCarrito[i].StrCosto.ToString(),
                    Empresa = App.MVProducto.ListaDelCarrito[i].Empresa,
                    STRRUTA = App.MVProducto.ListaDelCarrito[i].STRRUTA,
                    Cantidad = App.MVProducto.ListaDelCarrito[i].Cantidad,
                    UidNota = App.MVProducto.ListaDelCarrito[i].UidNota,
                    StrNota = App.MVProducto.ListaDelCarrito[i].StrNota,
                    CColor = ocolor
                });
            }
            for (int i = 0; i < listaDelCarrito.Count; i++)
            {
                listaDelCarrito[i].IsVisible = false;
                if (ObjItem.UidSucursal == listaDelCarrito[i].UidSucursal)
                {
                    ListaDeDetallesDeOrden.Add(listaDelCarrito[i]);
                }
            }
            var b = listaDelCarrito[0];
            lblPropina.Text = "$" + TotalPropina.ToString("N2");
            lblTaria.Text = "$" + TotalEnvio.ToString("N2");
            lblTotal.Text = "$" + (TotalEnvio + TotalPropina).ToString("N2");
            lblResumenCantidad.Text = b.Cantidad.ToString();
            lblResumenSubtotal.Text = b.Subtotal.ToString("C2");
            MyListViewBusquedaProductos.ItemsSource = null;
            MyListViewBusquedaProductos.ItemsSource = ListaDeDetallesDeOrden;
        }
        private async void BtnDistribuidora_Clicked(object sender, EventArgs e)
        {

            using (HttpClient _WebApiGoDeliverix = new HttpClient())
            {
                string url = "" + Helpers.Settings.sitio + "/api/Tarifario/GetBuscarTarifario?TipoDeBusqueda=Cliente&ZonaEntrega=" + App.UidColoniaABuscar + "&uidSucursal=" + ObjItem.UidSucursal.ToString() + "";

                string content = await _WebApiGoDeliverix.GetStringAsync(url);
                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                App.MVTarifario = JsonConvert.DeserializeObject<VMTarifario>(obj);
            }

            //App.MVTarifario.BuscarTarifario("Cliente", ZonaEntrega: App.UidColoniaABuscar, uidSucursal: ObjItem.UidSucursal.ToString());
            //await Navigation.PushAsync(new SeleccionarDistribuidoraCarrito(ObjItem));
        }

        private void ButtonMenos_Clicked(object sender, EventArgs e)
        {
            var item = App.MVProducto.ListaDeDetallesDeOrden.Find(x => x.IsVisible == true);

            App.MVProducto.QuitarDelCarrito(item.UidRegistroProductoEnCarrito);
            MyListViewBusquedaProductos.ItemsSource = null;
            App.MVProducto.ListaDeDetallesDeOrden = null;
            App.MVProducto.ListaDeDetallesDeOrden = App.MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == ObjItem.UidSucursal).ToList();

            ActualizarCarrito();

            //MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;

        }

        private void ButtonMas_Clicked_1(object sender, EventArgs e)
        {
            var item = App.MVProducto.ListaDeDetallesDeOrden.Find(x => x.IsVisible == true);
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
            var item = App.MVProducto.ListaDeDetallesDeOrden.Find(x => x.IsVisible == true);

            App.MVProducto.EliminaProductoDelCarrito(item.UidRegistroProductoEnCarrito);

            MyListViewBusquedaProductos.ItemsSource = null;
            App.MVProducto.ListaDeDetallesDeOrden = null;
            App.MVProducto.ListaDeDetallesDeOrden = App.MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == ObjItem.UidSucursal).ToList();

            MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;

            ActualizarCarrito();
        }

        public void ActualizarCarrito()
        {
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
            CargaProductos();
            btnPagar.Text = "Pagar  $" + TotalPagar;
        }

        private async void ImageButtonEliminarProducto_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("", "Desea eliminar el productos del carrito", "Si", "No");
            if (action)
            {
                var item = sender as ImageButton;
                var ObjItem = item.BindingContext as MVMProductos;
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
            var ObjItem = item.BindingContext as MVMProductos;

            App.MVProducto.QuitarDelCarrito(ObjItem.UidRegistroProductoEnCarrito);
            //MyListViewBusquedaProductos.ItemsSource = null;
            //App.MVProducto.ListaDeDetallesDeOrden = null;
            //App.MVProducto.ListaDeDetallesDeOrden = App.MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == ObjItem.UidSucursal).ToList();

            ActualizarCarrito();

            //MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;
        }

        private void ButtonAgregarUnProducto_Clicked_1(object sender, EventArgs e)
        {
            var item = sender as Button;
            var ObjItem = item.BindingContext as MVMProductos;

            Guid seccion = new Guid();
            App.MVProducto.AgregaAlCarrito(ObjItem.UidRegistroProductoEnCarrito, ObjItem.UidSucursal, seccion, "1", RegistroProductoEnCarrito: ObjItem.UidRegistroProductoEnCarrito);

            //MyListViewBusquedaProductos.ItemsSource = null;
            //App.MVProducto.ListaDeDetallesDeOrden = null;
            //App.MVProducto.ListaDeDetallesDeOrden = App.MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == ObjItem.UidSucursal).ToList();

            ActualizarCarrito();
            //MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;
        }

        private void ENotas_TextChanged(object sender, TextChangedEventArgs e)
        {
            var item = sender as Entry;
            var ObjItem = item.BindingContext as MVMProductos;
            if (ObjItem != null)
            {
                var objeto = App.MVProducto.ListaDelCarrito.Find(s => s.UidRegistroProductoEnCarrito == ObjItem.UidRegistroProductoEnCarrito);
                if (ObjItem.UidNota == Guid.Empty || ObjItem.UidNota == null)
                {
                    ObjItem.UidNota = Guid.NewGuid();
                    objeto.StrNota = item.Text;
                }
                else
                {
                    objeto.StrNota = item.Text;
                }
                App.MVProducto.ListaDeDetallesDeOrden = App.MVProducto.ListaDelCarrito.Where(p => p.UidSucursal == ObjItem.UidSucursal).ToList();
                MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeDetallesDeOrden;
            }
        }

        private async void btnPropina_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            var item = sender as Button;
            var ObjItem = item.BindingContext as MVMProductos;
            await Navigation.PushAsync(new ModificarPropina(UidSucursal: listaDelCarrito[0].UidSucursal, MyListViewCarritoEmpresa, btnPagar));
            await PopupNavigation.Instance.PopAsync();
        }

        private async void btnTarifas_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            var item = sender as Button;
            using (HttpClient _WebApiGoDeliverix = new HttpClient())
            {
                string url = "" + Helpers.Settings.sitio + "/api/Tarifario/GetBuscarTarifario?TipoDeBusqueda=Cliente&ZonaEntrega=" + App.UidColoniaABuscar + "&uidSucursal=" + ObjItem.UidSucursal.ToString() + "";

                string content = await _WebApiGoDeliverix.GetStringAsync(url);
                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                App.MVTarifario = JsonConvert.DeserializeObject<VMTarifario>(obj);
            }
            await Navigation.PushAsync(new SeleccionarDistribuidoraCarrito(ObjItem, MyListViewCarritoEmpresa, btnPagar));
            await PopupNavigation.Instance.PopAsync();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargaProductos();
        }
    }
}