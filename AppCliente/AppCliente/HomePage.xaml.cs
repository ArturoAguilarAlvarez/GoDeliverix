using AppCliente.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class HomePage : ContentPage
    {
    
        public List<VMProducto> ListaDeProductosHome = new List<VMProducto>();

        int CantidadProductosMostrados = 0;
        HttpClient _client = new HttpClient();

        public HomePage()
        {
            InitializeComponent();
            Iniciar();
        }

        private void MenuItem1_Activted(object sender, EventArgs e)
        {

            Navigation.PushAsync(new CarritoPage());
            //Navigation.PushAsync(new CarritoPage());
        }

        private void MenuBuscar_Activated(object sender, EventArgs e)
        {
            
            //ContenidoSearch.IsVisible = true;
        }

        private void MenuItemBuscar_Activted(object sender, EventArgs e)
        {
            //PanelNavegacionBuscar.IsVisible = false;
            //PanelNavegacionCarrito.IsVisible = true;
            Navigation.PushAsync(new BuscarHome());
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void ButtonFiltros_Clicked(object sender, EventArgs e)
        {
            btnFitltrosBusquedas.IsEnabled = false;
            PopupNavigation.Instance.PushAsync(new Popup.PupupFiltroBusqueda(btnFitltrosBusquedas, ScrollView_Productos, ScrollView_Empresas,MyListViewBusquedaProductosHome, MyListViewBusquedaEmpresas,PanelProductoNoEncontrados, lbCantidad));
        }

        private void SearchFor_SearchButtonPressed(object sender, EventArgs e)
        {
            //btnAdelante.IsEnabled = true;
            //btnAtras.IsEnabled = true;
            //btnAdelante.IsVisible = true;
            //btnAtras.IsVisible = true;

            string Buscado = "";

            string Hora = string.Empty;
            if (DateTime.Now.Hour < 10)
            {
                Hora = "0" + DateTime.Now.Hour.ToString();
            }
            else
            {
                Hora = DateTime.Now.Hour.ToString();
            }
            if (!string.IsNullOrEmpty(searchFor.Text))
            {
                Buscado = searchFor.Text;
            }

            string hora = Hora + ":" + DateTime.Now.Minute.ToString();

            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);

            ScrollView_Productos.Orientation = 0;

            //App.MVProducto.buscarProductosEmpresaDesdeCliente("Giro", hora, Dia, new Guid("d14a0380-e972-4013-a89b-586f54d4e381"), new Guid("63cd1aa3-74ef-4112-8835-fd4400706256"), Buscado);    
            string giro = App.giro;
            string categoria = App.categoria;
            string subcategoria = App.subcategoria;

            if (giro == "")
            {
                giro = AppCliente.App.MVGiro.LISTADEGIRO[0].UIDVM.ToString();
                //giro = "63cd1aa3-74ef-4112-8835-fd4400706256";
            }
            else
            {

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


            Guid Direccion = new Guid(IDDireccionBusqueda.Text);


            //Busqueda por giro
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                //giro
                App.MVProducto.buscarProductosEmpresaDesdeCliente("Giro", Dia, Direccion, new Guid(giro), searchFor.Text);
                App.MVEmpresa.BuscarEmpresaBusquedaCliente("Giro", Dia, Direccion, new Guid(giro), searchFor.Text);
            }
            else
            //// Busqueda por categoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                App.MVProducto.buscarProductosEmpresaDesdeCliente("Categoria", Dia, Direccion, new Guid(categoria), searchFor.Text);
                App.MVEmpresa.BuscarEmpresaBusquedaCliente("Giro", Dia, Direccion, new Guid(categoria), searchFor.Text);
            }
            else
            //Busqueda por subcategoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
            {
                App.MVProducto.buscarProductosEmpresaDesdeCliente("Subcategoria", Dia, Direccion, new Guid(subcategoria), searchFor.Text);
                App.MVEmpresa.BuscarEmpresaBusquedaCliente("Giro", Dia, Direccion, new Guid(subcategoria), searchFor.Text);
            }
            if (App.buscarPor == "Productos")
            {

                foreach (VMProducto item in AppCliente.App.MVProducto.ListaDeProductos)
                {
                    if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                    {
                        App.MVProducto.BuscarProductoPorSucursal("Giro", Dia, Direccion, new Guid(giro), item.UID);
                    }
                    else
                    if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                    {
                        App.MVProducto.BuscarProductoPorSucursal("Categoria", Dia, Direccion, new Guid(categoria), item.UID);
                    }
                    else
                    if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
                    {
                        App.MVProducto.BuscarProductoPorSucursal("Subcategoria", Dia, Direccion, new Guid(subcategoria), item.UID);
                    }
                    item.StrCosto = App.MVProducto.ListaDePreciosSucursales[0].StrCosto;
                }


                ScrollView_Empresas.IsVisible = false;
                ScrollView_Productos.IsVisible = true;

                MyListViewBusquedaProductosHome.ItemsSource = null;
                if (App.MVProducto.ListaDeProductos.Count > 10)
                {
                    MyListViewBusquedaProductosHome.ItemsSource = AppCliente.App.MVProducto.ListaDeProductos.GetRange(0, 10);
                    CantidadProductosMostrados = 10;
                    lbCantidad.Text = "1-10/" + App.MVProducto.ListaDeProductos.Count;
                    //btnAtras.IsEnabled = false;
                }
                else
                {
                    MyListViewBusquedaProductosHome.ItemsSource = AppCliente.App.MVProducto.ListaDeProductos;
                    CantidadProductosMostrados = AppCliente.App.MVProducto.ListaDeProductos.Count;
                    lbCantidad.Text = "1-" + App.MVProducto.ListaDeProductos.Count + "/" + App.MVProducto.ListaDeProductos.Count;
                    //btnAtras.IsVisible = false;
                    //btnAdelante.IsVisible = false;
                }

                MyListViewBusquedaEmpresas.ItemsSource = null;

                if (AppCliente.App.MVProducto.ListaDeProductos.Count == 0)
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
            else if (App.buscarPor == "Empresas")
            {
                int a = AppCliente.App.MVEmpresa.LISTADEEMPRESAS.Count;
                MyListViewBusquedaEmpresas.ItemsSource = null;
                MyListViewBusquedaProductosHome.ItemsSource = null;
                for (int i = 0; i < a; i++)
                {
                    AppCliente.App.MVEmpresa.LISTADEEMPRESAS[i].StrRuta = "http://www.godeliverix.net/vista" + AppCliente.App.MVEmpresa.LISTADEEMPRESAS[i].StrRuta.Substring(2);
                }


                MyListViewBusquedaEmpresas.ItemsSource = AppCliente.App.MVEmpresa.LISTADEEMPRESAS;
                ScrollView_Empresas.IsVisible = true;
                ScrollView_Productos.IsVisible = false;


                if (AppCliente.App.MVEmpresa.LISTADEEMPRESAS.Count == 0)
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

            PanelNavegacionCarrito.IsVisible = false;
            PanelNavegacionBuscar.IsVisible = true;
            txtBusquedaActual.Text = Buscado;
        }

        private async void BtnSeleccionarDireccion_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new PerfilDireccionesPage(btnSeleccionarDireccion, IDDireccionBusqueda));
            await Navigation.PushAsync(new SeleccionarDirecciones(btnSeleccionarDireccion, IDDireccionBusqueda,MyListViewBusquedaProductosHome, lbCantidad, CantidadProductosMostrados,PanelProductoNoEncontrados, MyListViewBusquedaEmpresas,ScrollView_Productos,ScrollView_Empresas));
        }

        private async void MyListViewBusquedaEmpresas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            var item = ((ItemTappedEventArgs)e);
            VMEmpresas ObjItem = (VMEmpresas)item.Item;

            await Navigation.PushAsync(new ProductoDescripcionEmpresaPage(ObjItem, new Guid(IDDireccionBusqueda.Text)));
            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void MyListViewBusquedaProductosHome_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
            var item = ((ItemTappedEventArgs)e);
            VMProducto ObjItem = (VMProducto)item.Item;

            string Hora = string.Empty;
            if (DateTime.Now.Hour < 10)
            {
                Hora = "0" + DateTime.Now.Hour.ToString();
            }
            else
            {
                Hora = DateTime.Now.Hour.ToString();
            }


            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);

            string hora = Hora + ":" + DateTime.Now.Minute.ToString();

            //Guid Direccion = App.MVDireccion.ListaDIRECCIONES[0].ID;
            Guid Direccion =new Guid( App.DireccionABuscar);

            App.MVProducto.BuscarProductoPorSucursal("Giro", Dia, Direccion, new Guid(App.giro), ObjItem.UID);
            //string StrParametroBusqueda, string StrDia, Guid UidDireccion, Guid UidBusquedaCategorias, object UidProducto
            string uiseccion = App.MVProducto.ListaDePreciosSucursales[0].UidSeccion.ToString();
            string asd = App.MVProducto.ListaDePreciosSucursales[0].UID.ToString();
            await Navigation.PushAsync(new ProductoDescripcionPage(ObjItem, App.MVProducto.ListaDePreciosSucursales));
            await PopupNavigation.Instance.PopAllAsync();
        }

        private void ButtonMasDeLaLista_Clicked(object sender, EventArgs e)
        {
            if (App.buscarPor == "Productos")
            {
                if (CantidadProductosMostrados < App.MVProducto.ListaDeProductos.Count)
                {
                    CantidadProductosMostrados = CantidadProductosMostrados + 1;
                    MyListViewBusquedaProductosHome.ItemsSource = null;
                    MyListViewBusquedaProductosHome.ItemsSource = AppCliente.App.MVProducto.ListaDeProductos.GetRange(0, CantidadProductosMostrados);
                }
                else
                {
                    DisplayAlert("!Ooooops", "No hay mas elementos para esta busqueda", "ok");
                }
            }
            else
            {
                DisplayAlert("!Ooooops", "No hay mas elementos para esta busqueda empresa", "ok");
            }

        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {

        }

        private void SearchFor_TextChanged(object sender, TextChangedEventArgs e)
        {

            txtBusquedaActual.Text = searchFor.Text;
        }

        //private void BtnAtras_Clicked(object sender, EventArgs e)
        //{
        //    if (CantidadProductosMostrados > 0)
        //    {
        //        int mod = CantidadProductosMostrados % 10;
        //        if (mod==0)
        //        {
        //            if (CantidadProductosMostrados>10)
        //            {
        //                lbCantidad.Text = ((CantidadProductosMostrados - (20)) + 1) + "-" + (CantidadProductosMostrados-10 ) + "/" + App.ListaDeProductos.Count;
        //                MyListViewBusquedaProductosHome.ItemsSource = null;
        //                MyListViewBusquedaProductosHome.ItemsSource = App.ListaDeProductos.GetRange((CantidadProductosMostrados - (20)), 10);
        //                CantidadProductosMostrados = CantidadProductosMostrados - (10);
        //            }
        //        }
        //        else
        //        {
        //            lbCantidad.Text = (CantidadProductosMostrados - (9 + mod )) + "-" + (CantidadProductosMostrados - mod)+"/"+ App.ListaDeProductos.Count;
        //            MyListViewBusquedaProductosHome.ItemsSource = null;
        //            MyListViewBusquedaProductosHome.ItemsSource = App.ListaDeProductos.GetRange((CantidadProductosMostrados - (10 + mod)), 10);
        //            CantidadProductosMostrados = CantidadProductosMostrados - (mod);
        //        }
        //    }
        //    else
        //    {

        //    }
        //}

        //private void BtnAdelante_Clicked(object sender, EventArgs e)
        //{
        //    if (AppCliente.App.ListaDeProductos.Count> CantidadProductosMostrados)
        //    {
        //        if (App.ListaDeProductos.Count>=(CantidadProductosMostrados+10))
        //        {
        //            lbCantidad.Text = (CantidadProductosMostrados+1) + "-" + (CantidadProductosMostrados + 10) + "/" + App.ListaDeProductos.Count;
        //            MyListViewBusquedaProductosHome.ItemsSource = null;
        //            MyListViewBusquedaProductosHome.ItemsSource = App.ListaDeProductos.GetRange(CantidadProductosMostrados,10);
        //            CantidadProductosMostrados = CantidadProductosMostrados + 10;
        //        }
        //        else
        //        {
        //            lbCantidad.Text = CantidadProductosMostrados+"-"+ App.ListaDeProductos.Count + "/" + App.ListaDeProductos.Count;
        //            MyListViewBusquedaProductosHome.ItemsSource = null;
        //            int posicion = CantidadProductosMostrados;
        //            int cantidad= App.ListaDeProductos.Count-CantidadProductosMostrados;
        //            MyListViewBusquedaProductosHome.ItemsSource = App.ListaDeProductos.GetRange(posicion,cantidad);
        //            CantidadProductosMostrados = App.ListaDeProductos.Count;
        //        }
        //    }
        //    else
        //    {

        //    }
        //}

        private void ImageButton_Clicked_2(object sender, EventArgs e)
        {

            //if (string.IsNullOrEmpty(searchFor.Text))
            //{
            //    btnAdelante.IsEnabled = true;
            //    btnAtras.IsEnabled = true;
            //    btnAdelante.IsVisible = true;
            //    btnAtras.IsVisible = true;

            //    string Buscado = "";

            //    string Hora = string.Empty;
            //    if (DateTime.Now.Hour < 10)
            //    {
            //        Hora = "0" + DateTime.Now.Hour.ToString();
            //    }
            //    else
            //    {
            //        Hora = DateTime.Now.Hour.ToString();
            //    }
            //    if (!string.IsNullOrEmpty(searchFor.Text))
            //    {
            //        Buscado = searchFor.Text;
            //    }

            //    string hora = Hora + ":" + DateTime.Now.Minute.ToString();

            //    CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            //    string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);

            //    ScrollView_Productos.Orientation = 0;

            //    //App.MVProducto.buscarProductosEmpresaDesdeCliente("Giro", hora, Dia, new Guid("d14a0380-e972-4013-a89b-586f54d4e381"), new Guid("63cd1aa3-74ef-4112-8835-fd4400706256"), Buscado);    
            //    string giro = App.giro;
            //    string categoria = App.categoria;
            //    string subcategoria = App.subcategoria;

            //    if (giro == "")
            //    {
            //        giro = AppCliente.App.MVGiro.LISTADEGIRO[0].UIDVM.ToString();
            //        //giro = "63cd1aa3-74ef-4112-8835-fd4400706256";
            //    }
            //    else
            //    {

            //    }
            //    if (categoria == "")
            //    {
            //        categoria = "00000000-0000-0000-0000-000000000000";
            //    }
            //    else
            //    {
            //        categoria = App.categoria;
            //    }
            //    if (subcategoria == "")
            //    {
            //        subcategoria = "00000000-0000-0000-0000-000000000000";
            //    }
            //    else
            //    {
            //        subcategoria = App.subcategoria;
            //    }

            //    if (App.buscarPor == "")
            //    {
            //        App.buscarPor = "Productos";
            //    }


            //    Guid Direccion = new Guid(IDDireccionBusqueda.Text);


            //    //Busqueda por giro
            //    if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            //    {
            //        //giro
            //        App.MVProducto.buscarProductosEmpresaDesdeCliente("Giro", Dia, Direccion, new Guid(giro), searchFor.Text);
            //        App.MVEmpresa.BuscarEmpresaBusquedaCliente("Giro", Dia, Direccion, new Guid(giro), searchFor.Text);
            //    }
            //    else
            //    //// Busqueda por categoria
            //    if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            //    {
            //        App.MVProducto.buscarProductosEmpresaDesdeCliente("Categoria", Dia, Direccion, new Guid(categoria), searchFor.Text);
            //        App.MVEmpresa.BuscarEmpresaBusquedaCliente("Giro", Dia, Direccion, new Guid(categoria), searchFor.Text);
            //    }
            //    else
            //    //Busqueda por subcategoria
            //    if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
            //    {
            //        App.MVProducto.buscarProductosEmpresaDesdeCliente("Subcategoria", Dia, Direccion, new Guid(subcategoria), searchFor.Text);
            //        App.MVEmpresa.BuscarEmpresaBusquedaCliente("Giro", Dia, Direccion, new Guid(subcategoria), searchFor.Text);
            //    }
            //    if (App.buscarPor == "Productos")
            //    {

            //        foreach (VMProducto item in AppCliente.App.MVProducto.ListaDeProductos)
            //        {
            //            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            //            {
            //                App.MVProducto.BuscarProductoPorSucursal("Giro", Dia, Direccion, new Guid(giro), item.UID);
            //            }
            //            else
            //            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            //            {
            //                App.MVProducto.BuscarProductoPorSucursal("Categoria", Dia, Direccion, new Guid(categoria), item.UID);
            //            }
            //            else
            //            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
            //            {
            //                App.MVProducto.BuscarProductoPorSucursal("Subcategoria", Dia, Direccion, new Guid(subcategoria), item.UID);
            //            }

            //            item.StrCosto = App.MVProducto.ListaDePreciosSucursales[0].StrCosto;
            //        }


            //        ScrollView_Empresas.IsVisible = false;
            //        ScrollView_Productos.IsVisible = true;

            //        MyListViewBusquedaProductosHome.ItemsSource = null;
            //        if (App.MVProducto.ListaDeProductos.Count > 10)
            //        {
            //            MyListViewBusquedaProductosHome.ItemsSource = AppCliente.App.MVProducto.ListaDeProductos.GetRange(0, 10);
            //            CantidadProductosMostrados = 10;
            //            lbCantidad.Text = "1-10/" + App.MVProducto.ListaDeProductos.Count;
            //            btnAtras.IsEnabled = false;
            //        }
            //        else
            //        {
            //            MyListViewBusquedaProductosHome.ItemsSource = AppCliente.App.MVProducto.ListaDeProductos;
            //            CantidadProductosMostrados = AppCliente.App.MVProducto.ListaDeProductos.Count;
            //            lbCantidad.Text = "1-" + App.MVProducto.ListaDeProductos.Count + "/" + App.MVProducto.ListaDeProductos.Count;
            //            btnAtras.IsVisible = false;
            //            btnAdelante.IsVisible = false;
            //        }

            //        MyListViewBusquedaEmpresas.ItemsSource = null;

            //        if (AppCliente.App.MVProducto.ListaDeProductos.Count == 0)
            //        {
            //            lbCantidad.Text = "0-0/0";
            //            PanelProductoNoEncontrados.IsVisible = true;
            //            ScrollView_Productos.IsVisible = false;
            //        }
            //        else
            //        {
            //            PanelProductoNoEncontrados.IsVisible = false;
            //            ScrollView_Productos.IsVisible = true;
            //        }

            //    }
            //    else if (App.buscarPor == "Empresas")
            //    {
            //        int a = AppCliente.App.MVEmpresa.LISTADEEMPRESAS.Count;
            //        MyListViewBusquedaEmpresas.ItemsSource = null;
            //        MyListViewBusquedaProductosHome.ItemsSource = null;
            //        for (int i = 0; i < a; i++)
            //        {
            //            AppCliente.App.MVEmpresa.LISTADEEMPRESAS[i].StrRuta = "http://www.godeliverix.net/vista" + AppCliente.App.MVEmpresa.LISTADEEMPRESAS[i].StrRuta.Substring(2);
            //        }


            //        MyListViewBusquedaEmpresas.ItemsSource = AppCliente.App.MVEmpresa.LISTADEEMPRESAS;
            //        ScrollView_Empresas.IsVisible = true;
            //        ScrollView_Productos.IsVisible = false;


            //        if (AppCliente.App.MVEmpresa.LISTADEEMPRESAS.Count == 0)
            //        {
            //            lbCantidad.Text = "0-0/0";
            //            PanelProductoNoEncontrados.IsVisible = true;
            //            ScrollView_Empresas.IsVisible = false;
            //        }
            //        else
            //        {
            //            PanelProductoNoEncontrados.IsVisible = false;
            //            ScrollView_Empresas.IsVisible = true;
            //        }

            //    }
            //}
            PanelNavegacionCarrito.IsVisible = false;
            PanelNavegacionBuscar.IsVisible = true;
        }


        private async void Iniciar()
        {
            var _URL = ("http://godeliverix.net/api/Giro/Get");
            string DatosObtenidos = await _client.GetStringAsync(_URL);
            var DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();

            JArray blogPostArray = JArray.Parse(DatosGiros.ToString());

            App.MVGiro.LISTADEGIRO = blogPostArray.Select(p => new VMGiro
            {
                UIDVM = (Guid)p["UIDVM"],
                STRNOMBRE = (string)p["STRNOMBRE"],
                STRDESCRIPCION = (string)p["RUTAIMAGEN"],
                RUTAIMAGEN = (string)p["REFERENCIA"]
            }).ToList();

            App.giro = AppCliente.App.MVGiro.LISTADEGIRO[0].UIDVM.ToString();


            string Hora = string.Empty;
            if (DateTime.Now.Hour < 10)
            {
                Hora = "0" + DateTime.Now.Hour.ToString();
            }
            else
            {
                Hora = DateTime.Now.Hour.ToString();
            }


            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);

            string hora = Hora + ":" + DateTime.Now.Minute.ToString();


            if (AppCliente.App.ListaDeProductos.Count == 0)
            {
                Guid Colonia = App.MVDireccion.ListaDIRECCIONES[0].ID;

                IDDireccionBusqueda.Text = Colonia.ToString();

                _URL = ("http://godeliverix.net/api/Categoria/Get?value="+ App.giro.ToString());
                DatosObtenidos = await _client.GetStringAsync(_URL);
                var DatosCategorias = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();

                blogPostArray = JArray.Parse(DatosCategorias.ToString());

                App.MVCategoria.LISTADECATEGORIAS = blogPostArray.Select(p => new VMCategoria
                {
                    UIDCATEGORIA = (Guid)p["UIDCATEGORIA"],
                    STRNOMBRE = (string)p["STRNOMBRE"],
                    STRDESCRIPCION = (string)p["STRDESCRIPCION"]
                }).ToList();


                Guid Direccion = App.MVDireccion.ListaDIRECCIONES[0].ID;
                App.DireccionABuscar = App.MVDireccion.ListaDIRECCIONES[0].ID.ToString();


                _URL = ("http://godeliverix.net/api/Producto/GetBuscarProductosCliente?StrParametroBusqueda="+"Giro"+"&StrDia="+Dia+"&UidDireccion="+Direccion+"&UidBusquedaCategorias="+App.giro+"&StrNombreEmpresa=");
                DatosObtenidos = await _client.GetStringAsync(_URL);
                var DatosProductos = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();

                var ArrayDatosProductos = JArray.Parse(DatosProductos.ToString());
                App.MVProducto.ListaDeProductos = ArrayDatosProductos.Select(p => new VMProducto
                {
                    UID = (Guid)p["UID"],
                    STRNOMBRE = (string)p["STRNOMBRE"],
                    Empresa = (string)p["Empresa"],
                    UIDEMPRESA = (Guid)p["UIDEMPRESA"],
                    STRDESCRIPCION = (string)p["STRDESCRIPCION"],
                    STRRUTA = (string)p["STRRUTA"]
                }).ToList();


                _URL = ("http://godeliverix.net/api/Empresa/GetObtenerEmpresaCliente?StrParametroBusqueda=Giro&StrDia="+Dia+"&UidDireccion="+Direccion+"&UidBusquedaCategorias="+App.giro);

                DatosObtenidos = await _client.GetStringAsync(_URL);
                var DatosEmpresa = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();


                ArrayDatosProductos = JArray.Parse(DatosEmpresa.ToString());
                App.MVEmpresa.LISTADEEMPRESAS = ArrayDatosProductos.Select(p => new VMEmpresas
                {
                    UIDEMPRESA = (Guid)p["UIDEMPRESA"],
                    NOMBRECOMERCIAL = (string)p["NOMBRECOMERCIAL"],
                    StrRuta= "http://godeliverix.net/vista/"+((string)p["StrRuta"].ToString().Substring(3))
                }).ToList();


                foreach (VMProducto item in AppCliente.App.MVProducto.ListaDeProductos)
                {
                    App.MVProducto.BuscarProductoPorSucursal("Giro", Dia, Colonia, new Guid(App.giro), item.UID);
                    item.StrCosto = App.MVProducto.ListaDePreciosSucursales[0].StrCosto;
                }
                for (int i = 0; i < AppCliente.App.MVProducto.ListaDeProductos.Count; i++)
                {
                    App.ListaDeProductos.Add(App.MVProducto.ListaDeProductos[i]);
                }
                for (int i = 0; i < App.MVEmpresa.LISTADEEMPRESAS.Count; i++)
                {
                    App.LISTADEEMPRESAS.Add(App.MVEmpresa.LISTADEEMPRESAS[i]);
                }
            }
            else
            {
                var Colonia = App.MVDireccion.ListaDIRECCIONES.Find(t => t.ID.ToString() == App.DireccionABuscar.ToString()); ;

                IDDireccionBusqueda.Text = Colonia.ID.ToString();
            }


            ListaDeProductosHome = AppCliente.App.ListaDeProductos;
            //if (App.ListaDeProductos.Count > 10)
            //{
            //    MyListViewBusquedaProductosHome.ItemsSource = AppCliente.App.ListaDeProductos.GetRange(0, 10);
            //    lbCantidad.Text = "1-10/" + App.ListaDeProductos.Count;
            //    CantidadProductosMostrados = 10;
            //}
            //else
            //{
                MyListViewBusquedaProductosHome.ItemsSource = AppCliente.App.ListaDeProductos;
                CantidadProductosMostrados = AppCliente.App.ListaDeProductos.Count;
                lbCantidad.Text = "1-" + App.ListaDeProductos.Count + "/" + App.ListaDeProductos.Count;
            //}
            if (App.DireccionABuscar != "")
            {
                btnSeleccionarDireccion.Text = "ENTREGAR EN " + AppCliente.App.MVDireccion.ListaDIRECCIONES.Find(x => x.ID == new Guid(App.DireccionABuscar)).IDENTIFICADOR + " >";
            }
            else
            {
                btnSeleccionarDireccion.Text = "ENTREGAR EN " + AppCliente.App.MVDireccion.ListaDIRECCIONES[0].IDENTIFICADOR + " >";
            }
            if (AppCliente.App.ListaDeProductos.Count == 0)
            {
                lbCantidad.Text = "0-0/0";
                PanelProductoNoEncontrados.IsVisible = true;
            }
        }
    }
}