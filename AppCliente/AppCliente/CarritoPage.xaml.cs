using AppCliente.ViewModel;
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
    public partial class CarritoPage : ContentPage
    {
        decimal cantidad = 0;
        decimal subtotal = 0;
        decimal TotalEnvio = 0;
        decimal TotalPagar = 0;
        decimal TotalPropina = 0;
        List<MVMProductos> listainformacionsucursales = new List<MVMProductos>();

        public CarritoPage()
        {
            InitializeComponent();
        }

        protected async void CargaCarrito()
        {
            cantidad = 0;
            subtotal = 0;
            TotalEnvio = 0;
            TotalPagar = 0;
            TotalPropina = 0;
            btnPagar.IsEnabled = false;
            btnPagar.Text = "Cargando...";
            List<MVMProductos> listaDelCarrito = new List<MVMProductos>();
            listainformacionsucursales = new List<MVMProductos>();
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
                cantidad += App.MVProducto.ListaDelCarrito[i].Cantidad;
                decimal a = decimal.Parse(App.MVProducto.ListaDelCarrito[i].StrCosto);
                subtotal += a;
                listaDelCarrito.Add(new MVMProductos()
                {
                    UidRegistroProductoEnCarrito = App.MVProducto.ListaDelCarrito[i].UidRegistroProductoEnCarrito,
                    UidSeccionPoducto = App.MVProducto.ListaDelCarrito[i].UidSeccionPoducto,
                    Total = App.MVProducto.ListaDelCarrito[i].Total,
                    Subtotal = App.MVProducto.ListaDelCarrito[i].Subtotal,
                    CostoEnvio = App.MVProducto.ListaDelCarrito[i].CostoEnvio,
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
            for (int i = 0; i < App.MVProducto.ListaDelInformacionSucursales.Count; i++)
            {
                Color ocolor = new Color();
                TotalEnvio += App.MVProducto.ListaDelInformacionSucursales[i].CostoEnvio;
                TotalPagar += App.MVProducto.ListaDelInformacionSucursales[i].Total;
                //subtotal += App.MVProducto.ListaDelInformacionSucursales[i].Subtotal;
                TotalPropina += App.MVProducto.ListaDelInformacionSucursales[i].DPropina;
                if (listaDelCarrito.Exists(o => o.UidSucursal == App.MVProducto.ListaDelInformacionSucursales[i].UidSucursal && o.CColor == Color.Red))
                {
                    ocolor = Color.Red;
                }
                else
                {
                    ocolor = Color.White;
                }
                listainformacionsucursales.Add(new MVMProductos()
                {
                    UidTarifario = App.MVProducto.ListaDelInformacionSucursales[i].UidTarifario,
                    UidSucursal = App.MVProducto.ListaDelInformacionSucursales[i].UidSucursal,
                    Empresa = App.MVProducto.ListaDelInformacionSucursales[i].Empresa,
                    Total = App.MVProducto.ListaDelInformacionSucursales[i].Total + App.MVProducto.ListaDelInformacionSucursales[i].DPropina,
                    CostoEnvio = App.MVProducto.ListaDelInformacionSucursales[i].CostoEnvio,
                    Subtotal = App.MVProducto.ListaDelInformacionSucursales[i].Subtotal,
                    Cantidad = App.MVProducto.ListaDelInformacionSucursales[i].Cantidad,
                    DPropina = App.MVProducto.ListaDelInformacionSucursales[i].DPropina,
                    CColor = ocolor,
                    STRRUTA = App.MVProducto.ListaDelInformacionSucursales[i].STRRUTA
                });
            }
            int errores = 0;
            var productoserror = listaDelCarrito.FindAll(o => o.CColor == Color.Red);
            errores = productoserror.Count;
            int erroressucursales = 0;
            var sucursalerror = listainformacionsucursales.FindAll(o => o.CColor == Color.Red);
            erroressucursales = sucursalerror.Count;
            if (errores > 0)
            {
                await DisplayAlert("Producto no disponible", "Uno de los productos no esta disponible\nVerifica tu carrito", "Aceptar");
                btnPagar.IsEnabled = false;
            }
            else
            {
                btnPagar.IsEnabled = true;
            }
            lblResumenCantidad.Text = cantidad.ToString();
            lblResumenSubtotal.Text = "$" + subtotal.ToString("N2");
            lblResumenEnvio.Text = "$" + TotalEnvio.ToString("N2");
            lblResumenPropina.Text = "$" + TotalPropina.ToString("N2");
            MyListViewCarritoEmpresa.ItemsSource = null;
            MyListViewCarritoEmpresa.ItemsSource = listainformacionsucursales;
            ViewListaProductoVacio.IsVisible = false;
            #region mostrar los datos al usuario
            TotalPagar += TotalPropina;
            btnPagar.Text = "Pagar  $" + TotalPagar.ToString("N2");
            btnPagar.IsEnabled = true;
            #endregion

        }
        private async void BtnPagar_Clicked(object sender, EventArgs e)
        {
            if (App.Global1 == string.Empty)
            {
                await DisplayAlert("Inicio de sesion obligatorio", "Para continuar con el pedido inicia sesion o registrate", "Aceptar");
                var objeto = new MasterMenuMenuItem { Id = 3, Title = "Login", TargetType = typeof(Login) };
                var Page = (Page)Activator.CreateInstance(objeto.TargetType);
                App app = Application.Current as App;
                App.Navegacion = Page.GetType().Name;
                MasterDetailPage md = (MasterDetailPage)app.MainPage;
                md.Detail = new NavigationPage(Page);
            }
            else
            {
                await Navigation.PushAsync(new Pago(TotalPagar.ToString()));
            }
        }


        private async void MyListViewCarritoEmpresa_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            var item = ((ItemTappedEventArgs)e);
            MVMProductos ObjItem = (MVMProductos)item.Item;
            MyListViewCarritoEmpresa.ItemsSource = null;
            MyListViewCarritoEmpresa.ItemsSource = App.MVProducto.ListaDelInformacionSucursales;
            await Navigation.PushAsync(new CarritoDetalleSucursal(ObjItem, MyListViewCarritoEmpresa, btnPagar));
            await PopupNavigation.Instance.PopAsync();
        }

        private void LimpiarCarrito()
        {
            App.MVProducto.ListaDelCarrito.Clear();
            App.MVProducto.ListaDelInformacionSucursales.Clear();
            MyListViewCarritoEmpresa.ItemsSource = null;
            btnPagar.Text = "pagar $0.00";
            CargaCarrito();
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

            using (HttpClient _WebApiGoDeliverix = new HttpClient())
            {
                string url = "" + Helpers.Settings.sitio + "/api/Tarifario/GetBuscarTarifario?TipoDeBusqueda=Cliente&ZonaEntrega=" + App.UidColoniaABuscar + "&uidSucursal=" + ObjItem.UidSucursal.ToString() + "";

                string content = await _WebApiGoDeliverix.GetStringAsync(url);
                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                App.MVTarifario = JsonConvert.DeserializeObject<VMTarifario>(obj);
            }
            //await Navigation.PushAsync(new SeleccionarDistribuidoraCarrito(ObjItem, MyListViewCarritoEmpresa, btnPagar));
            await PopupNavigation.Instance.PopAsync();
        }



        private async void BtnAgregarPropina_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            var item = sender as Button;
            var ObjItem = item.BindingContext as MVMProductos;
            await Navigation.PushAsync(new ModificarPropina(UidSucursal: listainformacionsucursales[0].UidSucursal, MyListViewCarritoEmpresa, btnPagar));
            await PopupNavigation.Instance.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargaCarrito();
        }
    }
}