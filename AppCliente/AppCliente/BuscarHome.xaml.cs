using AppCliente.WebApi;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class BuscarHome : ContentPage
    {
        int CantidadProductosMostrados = 0;
        public BuscarHome()
        {
            InitializeComponent();
            btnSeleccionarDireccion.IsVisible = false;
            MyListViewBusquedaEmpresas.ItemsSource = App.LISTADEEMPRESAS;
            MyListViewBusquedaProductosHome.ItemsSource = App.ListaDeProductos;
            CantidadProductosMostrados = App.ListaDeProductos.Count;
            lbCantidad.Text = App.ListaDeProductos.Count.ToString() + " Produtos disponibles";
        }

        private async void SearchFor_SearchButtonPressed(object sender, EventArgs e)
        {
            AILoading.IsRunning = true;
            AILoading.IsVisible = true;

            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            ScrollView_Productos.Orientation = 0;
            ScrollView_Empresas.IsVisible = false;
            ScrollView_Productos.IsVisible = false;
            string giro = App.giro;
            string categoria = App.categoria;
            string subcategoria = App.subcategoria;

            //if (giro == "")
            //{
            //    giro = App.MVGiro.LISTADEGIRO[0].UIDVM.ToString();
            //}
            //if (categoria == "")
            //{
            //    categoria = "00000000-0000-0000-0000-000000000000";
            //}
            //else
            //{
            //    categoria = App.categoria;
            //}
            //if (subcategoria == "")
            //{
            //    subcategoria = "00000000-0000-0000-0000-000000000000";
            //}
            //else
            //{
            //    subcategoria = App.subcategoria;
            //}

            //if (App.buscarPor == "")
            //{
            //    App.buscarPor = "Productos";
            //}
            //Guid uidcolonia = new Guid(App.UidColoniaABuscar);
            //Guid uidestado = new Guid(App.UidEstadoABuscar);
            //string _URL = "";
            //string UidParametroDeBusqueda = "";
            //string tipoDeBusqueda = "";
            //ApiService ApiService = new ApiService("");
            //Dictionary<string, string> parameters = new Dictionary<string, string>();
            //if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            //{
            //    UidParametroDeBusqueda = giro;
            //    tipoDeBusqueda = "Giro";
            //}
            //else if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            //{
            //    UidParametroDeBusqueda = categoria;
            //    tipoDeBusqueda = "Categoria";
            //}
            //else if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
            //{
            //    UidParametroDeBusqueda = subcategoria;
            //    tipoDeBusqueda = "Subcategoria";
            //}



            //if (App.buscarPor == "Productos")
            //{
            //    ApiService = new ApiService("/api/Producto");
            //    parameters = new Dictionary<string, string>();
            //    parameters.Add("StrParametroBusqueda", tipoDeBusqueda);
            //    parameters.Add("StrDia", Dia);
            //    parameters.Add("UidEstado", uidestado.ToString());
            //    parameters.Add("UidColonia", uidcolonia.ToString());
            //    parameters.Add("UidBusquedaCategorias", UidParametroDeBusqueda);
            //    parameters.Add("StrNombreEmpresa", searchFor.Text);
            //    var result = await ApiService.GET<VMProducto>(action: "GetBuscarProductosCliente", responseType: ApiService.ResponseType.Object, arguments: parameters);
            //    var oReponse = result as ResponseHelper;
            //    if (result != null && oReponse.Status != false)
            //    {
            //        var BusquedaProducto = oReponse.Data as VMProducto;
            //        App.ListaDeProductos = new List<VMProducto>();
            //        if (BusquedaProducto.ListaDeProductos != null)
            //        {
            //            foreach (VMProducto item in BusquedaProducto.ListaDeProductos)
            //            {
            //                App.ListaDeProductos.Add(item);
            //            }
            //            PanelProductoNoEncontrados.IsVisible = false;
            //            MyListViewBusquedaProductosHome.ItemsSource = App.ListaDeProductos;
            //            CantidadProductosMostrados = App.ListaDeProductos.Count;
            //            lbCantidad.Text = App.ListaDeProductos.Count + " Productos disponibles";
            //            ScrollView_Empresas.IsVisible = false;
            //            ScrollView_Productos.IsVisible = true;
            //            MyListViewBusquedaProductosHome.ItemsSource = null;
            //            MyListViewBusquedaProductosHome.ItemsSource = BusquedaProducto.ListaDeProductos;
            //            CantidadProductosMostrados = BusquedaProducto.ListaDeProductos.Count;
            //            MyListViewBusquedaEmpresas.ItemsSource = null;
            //            if (BusquedaProducto.ListaDeProductos.Count == 0)
            //            {
            //                PanelProductoNoEncontrados.IsVisible = true;
            //                ScrollView_Productos.IsVisible = false;
            //            }
            //            else
            //            {
            //                PanelProductoNoEncontrados.IsVisible = false;
            //                ScrollView_Productos.IsVisible = true;
            //            }
            //        }
            //    }

            //}
            //else
            if (App.buscarPor == "Empresas")
            {
                var ApiService = new ApiService("/api/Empresa");
                var parameters = new Dictionary<string, string>();
                parameters.Add("StrParametroBusqueda", "Giro");
                parameters.Add("StrDia", "martes");
                parameters.Add("UidEstado", "1fce366d-c225-47fd-b4bb-5ee4549fe913");
                parameters.Add("UidColonia", "f30f1394-c692-4766-a924-6d4a10209d80");
                parameters.Add("UidBusquedaCategorias", "efaedc66-7834-4066-a634-41244a171175");
                parameters.Add("StrNombreEmpresa", "");
                var result = await ApiService.GET<VMEmpresas>(action: "GetObtenerEmpresaCliente", responseType: ApiService.ResponseType.Object, arguments: parameters);
                var oReponse = result as ResponseHelper;
                if (result != null && oReponse.Status != false)
                {
                    var oEmpresa = oReponse.Data as VMEmpresas;
                    App.LISTADEEMPRESAS = new List<VMEmpresas>();
                    if (oEmpresa.LISTADEEMPRESAS != null)
                    {
                        foreach (var item in oEmpresa.LISTADEEMPRESAS)
                        {
                            item.StrRuta = "" + Helpers.Settings.sitio + "/vista/" + (item.StrRuta.Substring(3));
                        }
                        foreach (var item in oEmpresa.LISTADEEMPRESAS)
                        {
                            App.LISTADEEMPRESAS.Add(item);
                        }
                        MyListViewBusquedaEmpresas.ItemsSource = App.LISTADEEMPRESAS;
                        lbCantidad.Text = App.LISTADEEMPRESAS.Count + " Empresas disponibles";
                    }
                    ScrollView_Empresas.IsVisible = true;
                    ScrollView_Productos.IsVisible = false;
                    if (oEmpresa.LISTADEEMPRESAS == null)
                    {
                        PanelProductoNoEncontrados.IsVisible = true;
                        ScrollView_Empresas.IsVisible = false;
                    }
                    else
                    {
                        PanelProductoNoEncontrados.IsVisible = false;
                        ScrollView_Empresas.IsVisible = true;
                    }
                }

            }
            AILoading.IsRunning = false;
            AILoading.IsVisible = false;
        }

        private async void MyListViewBusquedaEmpresas_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            var item = e;
            VMEmpresas ObjItem = (VMEmpresas)item.Item;
            await Navigation.PushAsync(new ProductoDescripcionEmpresaPage(ObjItem));
            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void MyListViewBusquedaProductosHome_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            var item = e;
            VMProducto ObjItem = (VMProducto)item.Item;
            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            Guid UidColonia = new Guid(App.UidColoniaABuscar);
            Guid UidEstado = new Guid(App.UidEstadoABuscar);

            //App.MVProducto.BuscarProductoPorSucursal("Giro", Dia, UidColonia, UidEstado, new Guid(App.giro), ObjItem.UID);
            var _WebApi = new HttpClient();
            string _URL = "" + Helpers.Settings.sitio + "/api/Producto/GetObtenerInformacionDeProductoDeLaSucursal?StrParametroBusqueda=Giro&StrDia=" + Dia + "&UidEstado=" + UidEstado + "&UidColonia=" + UidColonia + "&UidBusquedaCategorias=" + App.giro + "&UidProducto=" + ObjItem.UID + "";
            var content = await _WebApi.GetStringAsync(_URL);
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            var VProducto = JsonConvert.DeserializeObject<VMProducto>(obj);


            await Navigation.PushAsync(new ProductoDescripcionPage(ObjItem, VProducto.ListaDePreciosSucursales));
            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void BtnSeleccionarDireccion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SeleccionarDirecciones(btnSeleccionarDireccion, MyListViewBusquedaProductosHome,  CantidadProductosMostrados, PanelProductoNoEncontrados, MyListViewBusquedaEmpresas, ScrollView_Productos, ScrollView_Empresas));
        }

        private void ButtonCambiarBusquedaProducto_Clicked(object sender, EventArgs e)
        {
            App.buscarPor = "Productos";
            ScrollView_Empresas.IsVisible = false;
            ScrollView_Productos.IsVisible = true;
            btnEmpresa.TextColor = Color.Black;
            btnProducto.TextColor = Color.Red;
            if (App.MVProducto.ListaDeProductos != null)
            {
                lbCantidad.Text = App.MVProducto.ListaDeProductos.Count.ToString() + " Produtos disponibles";
            }
        }

        private void ButtonCambiarBusqEmpresadaProducto_Clicked(object sender, EventArgs e)
        {
            App.buscarPor = "Empresas";
            ScrollView_Empresas.IsVisible = true;
            ScrollView_Productos.IsVisible = false;
            btnEmpresa.TextColor = Color.Red;
            btnProducto.TextColor = Color.Black;
            if (App.MVEmpresa.LISTADEEMPRESAS != null)
            {
                lbCantidad.Text = App.MVEmpresa.LISTADEEMPRESAS.Count.ToString() + " Empresas disponibles";
            }
        }

        private void BtnFitltrosBusquedas_Clicked(object sender, EventArgs e)
        {
            btnFitltrosBusquedas.IsEnabled = false;
            PopupNavigation.Instance.PushAsync(new Popup.PupupFiltroBusqueda(btnFitltrosBusquedas, ScrollView_Productos, ScrollView_Empresas, MyListViewBusquedaProductosHome, MyListViewBusquedaEmpresas, PanelProductoNoEncontrados));
        }


    }
}