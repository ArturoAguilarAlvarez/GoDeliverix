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

            if (giro == "")
            {
                giro = App.MVGiro.LISTADEGIRO[0].UIDVM.ToString();
            }
            if (categoria == "")
            {
                categoria = "00000000-0000-0000-0000-000000000000";
            }
            else
            {
                categoria = App.categoria;
            }
            if (subcategoria == "")
            {
                subcategoria = "00000000-0000-0000-0000-000000000000";
            }
            else
            {
                subcategoria = App.subcategoria;
            }

            if (App.buscarPor == "")
            {
                App.buscarPor = "Productos";
            }
            Guid uidcolonia = new Guid(App.UidColoniaABuscar);
            Guid uidestado = new Guid(App.UidEstadoABuscar);
            string _URL = "";
            if (App.buscarPor == "Productos")
            {
                if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                {
                    _URL = "" + Helpers.Settings.sitio + "/api/Producto/GetBuscarProductosCliente?StrParametroBusqueda=Giro&StrDia=" + Dia + "&UidEstado=" + uidestado.ToString() + "&UidColonia=" + uidcolonia.ToString() + "&UidBusquedaCategorias=" + giro + "&StrNombreEmpresa=" + searchFor.Text + "";
                }
                else if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                {
                    _URL = "" + Helpers.Settings.sitio + "/api/Producto/GetBuscarProductosCliente?StrParametroBusqueda=Categoria&StrDia=" + Dia + "&UidEstado=" + uidestado.ToString() + "&UidColonia=" + uidcolonia.ToString() + "&UidBusquedaCategorias=" + categoria + "&StrNombreEmpresa=" + searchFor.Text + "";
                }
                else if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
                {
                    _URL = "" + Helpers.Settings.sitio + "/api/Producto/GetBuscarProductosCliente?StrParametroBusqueda=Subcategoria&StrDia=" + Dia + "&UidEstado=" + uidestado.ToString() + "&UidColonia=" + uidcolonia.ToString() + "&UidBusquedaCategorias=" + subcategoria + "&StrNombreEmpresa=" + searchFor.Text + "";
                }
                var content = "";
                using (HttpClient _WebApi = new HttpClient())
                {
                    content = await _WebApi.GetStringAsync(_URL);
                }
                var BusquedaProducto = new VMProducto();
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                BusquedaProducto = JsonConvert.DeserializeObject<VMProducto>(obj);
                App.ListaDeProductos = new List<VMProducto>();
                if (BusquedaProducto.ListaDeProductos != null)
                {
                    foreach (VMProducto item in BusquedaProducto.ListaDeProductos)
                    {
                        if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                        {
                            _URL = "" + Helpers.Settings.sitio + "/api/Producto/GetObtenerInformacionDeProductoDeLaSucursal?StrParametroBusqueda=Giro&StrDia=" + Dia + "&UidEstado=" + uidestado.ToString() + "&UidColonia=" + uidcolonia.ToString() + "&UidBusquedaCategorias=" + giro + "&UidProducto=" + item.UID + "";
                        }
                        else
                        if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                        {
                            _URL = "" + Helpers.Settings.sitio + "/api/Producto/GetObtenerInformacionDeProductoDeLaSucursal?StrParametroBusqueda=Categoria&StrDia=" + Dia + "&UidEstado=" + uidestado.ToString() + "&UidColonia=" + uidcolonia.ToString() + "&UidBusquedaCategorias=" + categoria + "&UidProducto=" + item.UID + "";
                        }
                        else
                        if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
                        {
                            _URL = "" + Helpers.Settings.sitio + "/api/Producto/GetObtenerInformacionDeProductoDeLaSucursal?StrParametroBusqueda=Subcategoria&StrDia=" + Dia + "&UidEstado=" + uidestado.ToString() + "&UidColonia=" + uidcolonia.ToString() + "&UidBusquedaCategorias=" + subcategoria + "&UidProducto=" + item.UID + "";
                        }
                        using (HttpClient _WebApi = new HttpClient())
                        {
                            content = await _WebApi.GetStringAsync(_URL);
                        }
                        obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        var VProducto = JsonConvert.DeserializeObject<VMProducto>(obj);
                        item.StrCosto = VProducto.ListaDePreciosSucursales[0].StrCosto;
                    }
                    for (int i = 0; i < BusquedaProducto.ListaDeProductos.Count; i++)
                    {
                        App.ListaDeProductos.Add(BusquedaProducto.ListaDeProductos[i]);
                    }
                    PanelProductoNoEncontrados.IsVisible = false;
                    MyListViewBusquedaProductosHome.ItemsSource = App.ListaDeProductos;
                    CantidadProductosMostrados = App.ListaDeProductos.Count;
                    lbCantidad.Text = App.ListaDeProductos.Count + " Productos disponibles";
                    ScrollView_Empresas.IsVisible = false;
                    ScrollView_Productos.IsVisible = true;
                    MyListViewBusquedaProductosHome.ItemsSource = null;
                    MyListViewBusquedaProductosHome.ItemsSource = BusquedaProducto.ListaDeProductos;
                    CantidadProductosMostrados = BusquedaProducto.ListaDeProductos.Count;
                    MyListViewBusquedaEmpresas.ItemsSource = null;
                    if (BusquedaProducto.ListaDeProductos.Count == 0)
                    {
                        PanelProductoNoEncontrados.IsVisible = true;
                        ScrollView_Productos.IsVisible = false;
                    }
                    else
                    {
                        PanelProductoNoEncontrados.IsVisible = false;
                        ScrollView_Productos.IsVisible = true;
                    }
                }
            }
            else if (App.buscarPor == "Empresas")
            {
                //Busqueda por giro
                if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                {
                    _URL = "" + Helpers.Settings.sitio + "/api/Empresa/GetObtenerEmpresaCliente?StrParametroBusqueda=Giro&StrDia=" + Dia + "&UidColonia=" + uidcolonia.ToString() + "&UidEstado=" + uidestado.ToString() + "&UidBusquedaCategorias=" + giro + "&StrNombreEmpresa=" + searchFor.Text + "";
                }
                else
                //// Busqueda por categoria
                if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                {
                    _URL = "" + Helpers.Settings.sitio + "/api/Empresa/GetObtenerEmpresaCliente?StrParametroBusqueda=Categoria&StrDia=" + Dia + "&UidColonia=" + uidcolonia.ToString() + "&UidEstado=" + uidestado.ToString() + "&UidBusquedaCategorias=" + categoria + "&StrNombreEmpresa=" + searchFor.Text + "";
                }
                else
                //Busqueda por subcategoria
                if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
                {
                    _URL = "" + Helpers.Settings.sitio + "/api/Empresa/GetObtenerEmpresaCliente?StrParametroBusqueda=Subcategoria&StrDia=" + Dia + "&UidColonia=" + uidcolonia.ToString() + "&UidEstado=" + uidestado.ToString() + "&UidBusquedaCategorias=" + subcategoria + "&StrNombreEmpresa=" + searchFor.Text + "";
                }
                string content;
                using (HttpClient _webApi = new HttpClient())
                {
                    content = await _webApi.GetStringAsync(_URL);
                }
                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                App.MVEmpresa = JsonConvert.DeserializeObject<VMEmpresas>(obj);
                App.LISTADEEMPRESAS = new List<VMEmpresas>();
                if (App.MVEmpresa.LISTADEEMPRESAS != null)
                {
                    foreach (var item in App.MVEmpresa.LISTADEEMPRESAS)
                    {
                        item.StrRuta = "" + Helpers.Settings.sitio + "/vista/" + (item.StrRuta.Substring(3));
                    }
                    for (int i = 0; i < App.MVEmpresa.LISTADEEMPRESAS.Count; i++)
                    {
                        App.LISTADEEMPRESAS.Add(App.MVEmpresa.LISTADEEMPRESAS[i]);
                    }
                    MyListViewBusquedaEmpresas.ItemsSource = App.MVEmpresa.LISTADEEMPRESAS;
                    lbCantidad.Text = App.MVEmpresa.LISTADEEMPRESAS.Count + " Empresas disponibles";
                }
                ScrollView_Empresas.IsVisible = true;
                ScrollView_Productos.IsVisible = false;
                if (App.MVEmpresa.LISTADEEMPRESAS == null)
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

            App.MVProducto.BuscarProductoPorSucursal("Giro", Dia, UidColonia, UidEstado, new Guid(App.giro), ObjItem.UID);

            await Navigation.PushAsync(new ProductoDescripcionPage(ObjItem, App.MVProducto.ListaDePreciosSucursales));
            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void BtnSeleccionarDireccion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SeleccionarDirecciones(btnSeleccionarDireccion,  MyListViewBusquedaProductosHome, lbCantidad, CantidadProductosMostrados, PanelProductoNoEncontrados, MyListViewBusquedaEmpresas, ScrollView_Productos, ScrollView_Empresas));
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
            PopupNavigation.Instance.PushAsync(new Popup.PupupFiltroBusqueda(btnFitltrosBusquedas, ScrollView_Productos, ScrollView_Empresas, MyListViewBusquedaProductosHome, MyListViewBusquedaEmpresas, PanelProductoNoEncontrados, lbCantidad));
        }


    }
}