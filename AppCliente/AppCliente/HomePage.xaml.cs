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
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using AppCliente.ViewModel;

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

        private async void MenuItem1_Activted(object sender, EventArgs e)
        {
            NavigationPage navigationPage = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);

            foreach (var item in App.MVProducto.ListaDelInformacionSucursales)
            {
                string _URL = "" + Helpers.Settings.sitio + "/api/Sucursales/GetBuscarSucursales?UidSucursal=" + item.UidSucursal + "";
                string content = await _client.GetStringAsync(_URL);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                App.MVSucursales = JsonConvert.DeserializeObject<VMSucursales>(obj);

                _URL = "" + Helpers.Settings.sitio + "/api/Imagen/GetImagenDePerfilEmpresa?UidEmpresa=" + App.MVSucursales.UidEmpresa + "";
                content = await _client.GetStringAsync(_URL);
                obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                App.MVImagen = JsonConvert.DeserializeObject<VMImagen>(obj);
                item.STRRUTA = "" + Helpers.Settings.sitio + "/vista/" + App.MVImagen.STRRUTA;
            }
            await navigationPage.PushAsync(new CarritoPage());
            //Navigation.PushAsync(new CarritoPage());
        }

        private void MenuBuscar_Activated(object sender, EventArgs e)
        {
        }

        private void MenuItemBuscar_Activted(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BuscarHome());
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void ButtonFiltros_Clicked(object sender, EventArgs e)
        {
            btnFitltrosBusquedas.IsEnabled = false;
            PopupNavigation.Instance.PushAsync(new Popup.PupupFiltroBusqueda(btnFitltrosBusquedas, ScrollView_Productos, ScrollView_Empresas, MyListViewBusquedaProductosHome, MyListViewBusquedaEmpresas, PanelProductoNoEncontrados, lbCantidad));
        }

        private async void SearchFor_SearchButtonPressed(object sender, EventArgs e)
        {
            string Buscado = "";

            if (!string.IsNullOrEmpty(searchFor.Text))
            {
                Buscado = searchFor.Text;
            }
            Guid UidEstado;
            Guid UidColonia;

            if (Helpers.Settings.UidDireccion != string.Empty)
            {
                UidEstado = new Guid(App.MVDireccion.ListaDIRECCIONES[0].ESTADO);
                UidColonia = new Guid(App.MVDireccion.ListaDIRECCIONES[0].COLONIA);
            }
            else
            {
                UidEstado = new Guid(Helpers.Settings.StrESTADO);
                UidColonia = new Guid(Helpers.Settings.StrCOLONIA);
            }

            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);

            ScrollView_Productos.Orientation = 0;

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

            //Busqueda por giro
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                //giro
                BuscarProductos("Giro", Dia, UidEstado, UidColonia, new Guid(giro), searchFor.Text);
                BuscarEmpresas("Giro", Dia, UidEstado, UidColonia, new Guid(giro), searchFor.Text);
            }
            else
            //// Busqueda por categoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                BuscarProductos("Categoria", Dia, UidEstado, UidColonia, new Guid(categoria), searchFor.Text);
                BuscarEmpresas("Categoria", Dia, UidEstado, UidColonia, new Guid(giro), searchFor.Text);
            }
            else
            //Busqueda por subcategoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
            {
                BuscarProductos("Subcategoria", Dia, UidEstado, UidColonia, new Guid(subcategoria), searchFor.Text);
                BuscarEmpresas("Subcategoria", Dia, UidEstado, UidColonia, new Guid(giro), searchFor.Text);
            }
            if (App.buscarPor == "Productos")
            {
                foreach (VMProducto item in AppCliente.App.MVProducto.ListaDeProductos)
                {
                    if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                    {
                        using (HttpClient _WebApi = new HttpClient())
                        {
                            string _URL = "" + Helpers.Settings.sitio + "/api/Producto/GetObtenerInformacionDeProductoDeLaSucursal?StrParametroBusqueda=Giro&StrDia=" + Dia + "&UidEstado=" + UidEstado + "&UidColonia=" + UidColonia + "&UidBusquedaCategorias=" + giro + "&UidProducto=" + item.UID + "";
                            var content = await _WebApi.GetStringAsync(_URL);
                            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                            var VProducto = JsonConvert.DeserializeObject<VMProducto>(obj);
                            item.StrCosto = VProducto.ListaDePreciosSucursales[0].StrCosto;
                        }
                    }
                    else
                    if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                    {
                        using (HttpClient _WebApi = new HttpClient())
                        {
                            string _URL = "" + Helpers.Settings.sitio + "/api/Producto/GetObtenerInformacionDeProductoDeLaSucursal?StrParametroBusqueda=Categoria&StrDia=" + Dia + "&UidEstado=" + UidEstado + "&UidColonia=" + UidColonia + "&UidBusquedaCategorias=" + categoria + "&UidProducto=" + item.UID + "";
                            var content = await _WebApi.GetStringAsync(_URL);
                            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                            var VProducto = JsonConvert.DeserializeObject<VMProducto>(obj);
                            item.StrCosto = VProducto.ListaDePreciosSucursales[0].StrCosto;
                        }
                    }
                    else
                    if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
                    {
                        using (HttpClient _WebApi = new HttpClient())
                        {
                            string _URL = "" + Helpers.Settings.sitio + "/api/Producto/GetObtenerInformacionDeProductoDeLaSucursal?StrParametroBusqueda=Subcategoria&StrDia=" + Dia + "&UidEstado=" + UidEstado + "&UidColonia=" + UidColonia + "&UidBusquedaCategorias=" + subcategoria + "&UidProducto=" + item.UID + "";
                            var content = await _WebApi.GetStringAsync(_URL);
                            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                            var VProducto = JsonConvert.DeserializeObject<VMProducto>(obj);
                            item.StrCosto = VProducto.ListaDePreciosSucursales[0].StrCosto;
                        }
                    }
                }
                ScrollView_Empresas.IsVisible = false;
                ScrollView_Productos.IsVisible = true;

                MyListViewBusquedaProductosHome.ItemsSource = null;
                if (App.MVProducto.ListaDeProductos.Count > 10)
                {
                    MyListViewBusquedaProductosHome.ItemsSource = App.MVProducto.ListaDeProductos.GetRange(0, 10);
                    CantidadProductosMostrados = 10;
                    lbCantidad.Text = "1-10/" + App.MVProducto.ListaDeProductos.Count;
                }
                else
                {
                    MyListViewBusquedaProductosHome.ItemsSource = App.MVProducto.ListaDeProductos;
                    CantidadProductosMostrados = App.MVProducto.ListaDeProductos.Count;
                    lbCantidad.Text = "1-" + App.MVProducto.ListaDeProductos.Count + "/" + App.MVProducto.ListaDeProductos.Count;
                }

                MyListViewBusquedaEmpresas.ItemsSource = null;

                if (App.MVProducto.ListaDeProductos.Count == 0)
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
                int a = App.MVEmpresa.LISTADEEMPRESAS.Count;
                MyListViewBusquedaEmpresas.ItemsSource = null;
                MyListViewBusquedaProductosHome.ItemsSource = null;
                for (int i = 0; i < a; i++)
                {
                    App.MVEmpresa.LISTADEEMPRESAS[i].StrRuta = "" + Helpers.Settings.sitio + "/vista" + App.MVEmpresa.LISTADEEMPRESAS[i].StrRuta.Substring(2);
                }

                MyListViewBusquedaEmpresas.ItemsSource = App.MVEmpresa.LISTADEEMPRESAS;
                ScrollView_Empresas.IsVisible = true;
                ScrollView_Productos.IsVisible = false;

                if (App.MVEmpresa.LISTADEEMPRESAS.Count == 0)
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
            if (App.Global1 == string.Empty)
            {
                await DisplayAlert("Acceso denegado", "No puedes cambiar la direccion sin haber accesado", "Aceptar");
            }
            else
            {
                await Navigation.PushAsync(new SeleccionarDirecciones(btnSeleccionarDireccion, IDDireccionBusqueda, MyListViewBusquedaProductosHome, lbCantidad, CantidadProductosMostrados, PanelProductoNoEncontrados, MyListViewBusquedaEmpresas, ScrollView_Productos, ScrollView_Empresas));
            }

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
            if (ObjItem != null)
            {
                CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
                string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);

                //Guid Direccion = App.MVDireccion.ListaDIRECCIONES[0].ID;
                Guid UidColonia = new Guid(App.UidColoniaABuscar);
                Guid UidEstado = new Guid(App.UidEstadoABuscar);

                using (HttpClient _WebApi = new HttpClient())
                {
                    string _URL = "" + Helpers.Settings.sitio + "/api/Producto/GetObtenerInformacionDeProductoDeLaSucursal?StrParametroBusqueda=Giro&StrDia=" + Dia + "&UidEstado=" + UidEstado + "&UidColonia=" + UidColonia + "&UidBusquedaCategorias=" + App.giro + "&UidProducto=" + ObjItem.UID + "";
                    var content = await _WebApi.GetStringAsync(_URL);
                    var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    var VProducto = JsonConvert.DeserializeObject<VMProducto>(obj);
                    await Navigation.PushAsync(new ProductoDescripcionPage(ObjItem, VProducto.ListaDePreciosSucursales));
                    await PopupNavigation.Instance.PopAllAsync();
                }
            }
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


        private void SearchFor_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBusquedaActual.Text = searchFor.Text;
        }

        private void ImageButton_Clicked_2(object sender, EventArgs e)
        {
            PanelNavegacionCarrito.IsVisible = false;
            PanelNavegacionBuscar.IsVisible = true;
        }


        private async void Iniciar()
        {
        Iniciar:
            // Uid de la aplicacion 87b2dcfd-205a-4260-9092-1ce48b28aa4a
            string _URL = "" + Helpers.Settings.sitio + "/api/Version/Get?id=87b2dcfd-205a-4260-9092-1ce48b28aa4a";
            var content = await _client.GetStringAsync(_URL);
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            var oversion = JsonConvert.DeserializeObject<VMVersion>(obj);
            string version = VersionTracking.CurrentVersion;

            if (oversion.StrVersion == version)
            {
                acloading.IsVisible = true;
                acloading.IsRunning = true;
                bool direccionagregada = false;
                if (string.IsNullOrEmpty(App.Global1))
                {
                    btnAcceder.IsVisible = true;
                }
                else
                {
                    btnAcceder.IsVisible = false;
                }
                _URL = "" + Helpers.Settings.sitio + "/api/Giro/Get";
                content = await _client.GetStringAsync(_URL);
                obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                App.MVGiro = JsonConvert.DeserializeObject<VMGiro>(obj);

                App.giro = App.MVGiro.LISTADEGIRO[0].UIDVM.ToString();
                Guid UidEstado = new Guid();
                Guid UidColonia = new Guid();


                CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
                string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                //Busca si un usuario no esta registrado

                if (string.IsNullOrEmpty(App.Global1))
                {
                    if (Helpers.Settings.UidDireccion != string.Empty)
                    {
                        App.MVDireccion.ListaDIRECCIONES.Add(new VMDireccion()
                        {
                            ID = new Guid(Helpers.Settings.UidDireccion),
                            PAIS = Helpers.Settings.StrPAIS,
                            ESTADO = Helpers.Settings.StrESTADO,
                            MUNICIPIO = Helpers.Settings.StrMUNICIPIO,
                            CIUDAD = Helpers.Settings.StrCIUDAD,
                            COLONIA = Helpers.Settings.StrCOLONIA,
                            CALLE0 = Helpers.Settings.StrCALLE0,
                            CALLE1 = Helpers.Settings.StrCALLE1,
                            CALLE2 = Helpers.Settings.StrCALLE2,
                            MANZANA = Helpers.Settings.StrMANZANA,
                            LOTE = Helpers.Settings.StrLOTE,
                            CodigoPostal = Helpers.Settings.StrCodigoPostal,
                            REFERENCIA = Helpers.Settings.StrREFERENCIA,
                            IDENTIFICADOR = Helpers.Settings.StrIDENTIFICADOR,
                            NOMBRECIUDAD = Helpers.Settings.StrNombreCiudad,
                            NOMBRECOLONIA = Helpers.Settings.StrNombreColonia
                        });
                    }
                    else
                    {
                        App.MVDireccion.ListaDIRECCIONES.Clear();
                        PanelProductos.IsVisible = false;
                        PanelDireccionesVacias.IsVisible = true;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Helpers.Settings.UidDireccion))
                    {
                        var action = await DisplayAlert("Nueva direccion ", "Detectamos una nueva ubicacion en " + Helpers.Settings.StrNombreColonia + ", deseas agregarla a tu lista de direcciones?", "Si", "No");
                        if (action)
                        {
                            string _Url = "" + Helpers.Settings.sitio + "/api/Direccion/GetGuardarDireccion?" +
                                "UidUsuario=" + App.Global1 + "&UidPais=" + Helpers.Settings.StrPAIS + "&UidEstado=" + Helpers.Settings.StrESTADO + "&UidMunicipio=" + Helpers.Settings.StrMUNICIPIO + "&UidCiudad=" + Helpers.Settings.StrCIUDAD + "&UidColonia=" + Helpers.Settings.StrCOLONIA + "&CallePrincipal=" + Helpers.Settings.StrCALLE0 + "&CalleAux1=" + Helpers.Settings.StrCALLE1 + "&CalleAux2=" + Helpers.Settings.StrCALLE2 + "&Manzana=" + Helpers.Settings.StrMANZANA + "&Lote=" + Helpers.Settings.StrLOTE + "&CodigoPostal=" + Helpers.Settings.StrCodigoPostal + "&Referencia=" + Helpers.Settings.StrREFERENCIA + "&NOMBRECIUDAD=s&NOMBRECOLONIA=s&Identificador=" + Helpers.Settings.StrIDENTIFICADOR + "&Latitud=" + Helpers.Settings.StrLatitud + "&Longitud=" + Helpers.Settings.StrLongitud + "&UidDireccion=" + Helpers.Settings.UidDireccion + "";
                            await _client.GetAsync(_Url);

                            _Url = ("" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + App.Global1);
                            string strDirecciones = await _client.GetStringAsync(_Url);
                            obj = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
                            App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);

                            Helpers.Settings.StrPAIS = string.Empty;
                            Helpers.Settings.StrMUNICIPIO = string.Empty;
                            Helpers.Settings.StrCIUDAD = string.Empty;
                            Helpers.Settings.StrCALLE0 = string.Empty;
                            Helpers.Settings.StrCALLE1 = string.Empty;
                            Helpers.Settings.StrCALLE2 = string.Empty;
                            Helpers.Settings.StrMANZANA = string.Empty;
                            Helpers.Settings.StrLongitud = string.Empty;
                            Helpers.Settings.StrLatitud = string.Empty;
                            Helpers.Settings.StrLOTE = string.Empty;
                            Helpers.Settings.StrCodigoPostal = string.Empty;
                            Helpers.Settings.StrREFERENCIA = string.Empty;
                            Helpers.Settings.StrIDENTIFICADOR = string.Empty;
                            Helpers.Settings.StrNombreCiudad = string.Empty;
                            Helpers.Settings.StrNombreColonia = string.Empty;

                            await DisplayAlert("Direccion agregada", "Se ha agregado la direccion a tus direcciones", "Aceptar");
                            direccionagregada = true;
                        }
                        else
                        {
                            Helpers.Settings.UidDireccion = string.Empty;
                            Helpers.Settings.StrPAIS = string.Empty;
                            Helpers.Settings.StrESTADO = string.Empty;
                            Helpers.Settings.StrMUNICIPIO = string.Empty;
                            Helpers.Settings.StrCIUDAD = string.Empty;
                            Helpers.Settings.StrCOLONIA = string.Empty;
                            Helpers.Settings.StrCALLE0 = string.Empty;
                            Helpers.Settings.StrCALLE1 = string.Empty;
                            Helpers.Settings.StrCALLE2 = string.Empty;
                            Helpers.Settings.StrMANZANA = string.Empty;
                            Helpers.Settings.StrLongitud = string.Empty;
                            Helpers.Settings.StrLatitud = string.Empty;
                            Helpers.Settings.StrLOTE = string.Empty;
                            Helpers.Settings.StrCodigoPostal = string.Empty;
                            Helpers.Settings.StrREFERENCIA = string.Empty;
                            Helpers.Settings.StrIDENTIFICADOR = string.Empty;
                            Helpers.Settings.StrNombreCiudad = string.Empty;
                            Helpers.Settings.StrNombreColonia = string.Empty;

                            if (App.MVProducto.ListaDelCarrito.Count > 0)
                            {
                                App.MVProducto.ListaDelCarrito.Clear();
                                App.MVProducto.ListaDelInformacionSucursales.Clear();
                            }

                        }
                    }
                }
                if (App.MVDireccion.ListaDIRECCIONES.Count != 0)
                {
                    PanelProductos.IsVisible = true;
                    PanelDireccionesVacias.IsVisible = false;

                    if (!string.IsNullOrEmpty(App.UidColoniaABuscar) && !string.IsNullOrEmpty(App.UidEstadoABuscar))
                    {
                        UidEstado = new Guid(App.UidEstadoABuscar);
                        UidColonia = new Guid(App.UidColoniaABuscar);
                    }
                    else
                    {
                        UidEstado = new Guid(App.MVDireccion.ListaDIRECCIONES[0].ESTADO);
                        UidColonia = new Guid(App.MVDireccion.ListaDIRECCIONES[0].COLONIA);
                    }

                    _URL = "" + Helpers.Settings.sitio + "/api/Categoria/Get?value=" + App.giro.ToString();
                    content = await _client.GetStringAsync(_URL);
                    obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    App.MVCategoria = JsonConvert.DeserializeObject<VMCategoria>(obj);

                    if (direccionagregada)
                    {
                        App.UidColoniaABuscar = Helpers.Settings.StrCOLONIA;
                        App.UidEstadoABuscar = Helpers.Settings.StrESTADO;
                        App.DireccionABuscar = Helpers.Settings.UidDireccion;

                        Helpers.Settings.StrCOLONIA = string.Empty;
                        Helpers.Settings.StrESTADO = string.Empty;
                        Helpers.Settings.UidDireccion = string.Empty;
                    }
                    else
                    {
                        App.UidColoniaABuscar = App.MVDireccion.ListaDIRECCIONES[0].COLONIA;
                        App.UidEstadoABuscar = App.MVDireccion.ListaDIRECCIONES[0].ESTADO;
                        App.DireccionABuscar = App.MVDireccion.ListaDIRECCIONES[0].ID.ToString();
                    }
                    VMProducto oBusquedaproducto = new VMProducto();
                    using (HttpClient _WebApi = new HttpClient())
                    {
                        _URL = "" + Helpers.Settings.sitio + "/api/Producto/GetBuscarProductosCliente?StrParametroBusqueda=Giro&StrDia=" + Dia + "&UidEstado=" + UidEstado + "&UidColonia=" + UidColonia + "&UidBusquedaCategorias=" + App.giro + "&StrNombreEmpresa=" + txtBusquedaActual.Text + "";
                        content = await _WebApi.GetStringAsync(_URL);
                        obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        oBusquedaproducto = JsonConvert.DeserializeObject<VMProducto>(obj);
                    }
                    if (oBusquedaproducto.ListaDeProductos != null)
                    {
                        foreach (VMProducto item in oBusquedaproducto.ListaDeProductos)
                        {
                            using (HttpClient _WebApi = new HttpClient())
                            {
                                string Cadena = "" + Helpers.Settings.sitio + "/api/Producto/GetObtenerInformacionDeProductoDeLaSucursal?StrParametroBusqueda=Giro&StrDia=" + Dia + "&UidEstado=" + UidEstado + "&UidColonia=" + UidColonia + "&UidBusquedaCategorias=" + App.giro + "&UidProducto=" + item.UID + "";
                                content = await _WebApi.GetStringAsync(Cadena);
                                obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                                var VProducto = JsonConvert.DeserializeObject<VMProducto>(obj);
                                item.StrCosto = VProducto.ListaDePreciosSucursales[0].StrCosto;
                            }
                        }
                        for (int i = 0; i < oBusquedaproducto.ListaDeProductos.Count; i++)
                        {
                            App.ListaDeProductos.Add(oBusquedaproducto.ListaDeProductos[i]);
                        }
                        PanelProductoNoEncontrados.IsVisible = false;
                        ListaDeProductosHome = App.ListaDeProductos;

                        MyListViewBusquedaProductosHome.ItemsSource = App.ListaDeProductos;
                        CantidadProductosMostrados = App.ListaDeProductos.Count;
                        lbCantidad.Text = App.ListaDeProductos.Count + " Productos disponibles";
                    }
                    if (string.IsNullOrEmpty(App.Global1))
                    {

                        btnSeleccionarDireccion.Text = "ENTREGAR EN " + App.MVDireccion.ListaDIRECCIONES.Find(x => x.ID == new Guid(App.DireccionABuscar)).IDENTIFICADOR + " >";
                    }
                    else
                    if (App.DireccionABuscar != "")
                    {
                        btnSeleccionarDireccion.Text = "ENTREGAR A " + App.MVUsuarios.StrUsuario + " EN " + App.MVDireccion.ListaDIRECCIONES.Find(x => x.ID == new Guid(App.DireccionABuscar)).IDENTIFICADOR + " >";
                    }
                    else
                    {
                        btnSeleccionarDireccion.Text = "ENTREGAR A " + App.MVUsuarios.StrUsuario + " EN " + App.MVDireccion.ListaDIRECCIONES[0].IDENTIFICADOR + " >";
                    }
                    if (App.ListaDeProductos.Count == 0)
                    {
                        lbCantidad.Text = "0-0/0";
                        PanelProductoNoEncontrados.IsVisible = true;
                    }
                    acloading.IsRunning = false;
                    acloading.IsVisible = false;
                }
                else
                {
                    acloading.IsRunning = false;
                    acloading.IsVisible = false;

                    PanelProductos.IsVisible = false;
                    PanelDireccionesVacias.IsVisible = true;
                }
            }
            else
            {
                var action = await DisplayAlert("Actualizacion disponible", "Actualizar a la version " + oversion.StrVersion + "", "Aceptar", "Cancelar");
                if (action)
                {
                    var urlStore = "";
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        urlStore = "https://play.google.com/store/apps/details?id=com.CompuAndSoft.GDCliente";
                    }
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        urlStore = "";
                    }
                    Device.OpenUri(new Uri(urlStore));
                }
                else
                {
                    goto Iniciar;
                }
            }


        }

        private void BtnAgregarDireccion_Clicked(object sender, EventArgs e)
        {
            var objeto = new MasterMenuMenuItem { Id = 3, Title = "Direcciones", TargetType = typeof(UsuarioDirecciones) };
            var Page = (Page)Activator.CreateInstance(objeto.TargetType);
            App app = Application.Current as App;
            App.Navegacion = Page.GetType().Name;
            MasterDetailPage md = (MasterDetailPage)app.MainPage;
            md.Detail = new NavigationPage(Page);
        }

        protected async void BuscarProductos(string StrParametroBusqueda, string StrDia, Guid UidEstado, Guid UidColonia, Guid UidBusquedaCategorias, string StrNombreEmpresa)
        {
            acloading.IsRunning = true;
            acloading.IsVisible = true;
            using (HttpClient _WebApi = new HttpClient())
            {
                string _URL = "" + Helpers.Settings.sitio + "/api/Producto/GetBuscarProductosCliente?StrParametroBusqueda=" + StrParametroBusqueda + "&StrDia=" + StrDia + "&UidEstado=" + UidEstado + "&UidColonia=" + UidColonia + "&UidBusquedaCategorias=" + UidBusquedaCategorias + "&StrNombreEmpresa=" + StrNombreEmpresa + "";
                var content = await _WebApi.GetStringAsync(_URL);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                App.MVProducto = JsonConvert.DeserializeObject<VMProducto>(obj);
            }
            if (App.MVProducto.ListaDeProductos != null)
            {
                foreach (VMProducto item in App.MVProducto.ListaDeProductos)
                {
                    using (HttpClient _WebApi = new HttpClient())
                    {
                        string Cadena = "" + Helpers.Settings.sitio + "/api/Producto/GetObtenerInformacionDeProductoDeLaSucursal?StrParametroBusqueda=Giro&StrDia=" + StrDia + "&UidEstado=" + UidEstado + "&UidColonia=" + UidColonia + "&UidBusquedaCategorias=" + App.giro + "&UidProducto=" + item.UID + "";
                        var content = await _WebApi.GetStringAsync(Cadena);
                        var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                        var VProducto = JsonConvert.DeserializeObject<VMProducto>(obj);
                        item.StrCosto = VProducto.ListaDePreciosSucursales[0].StrCosto;
                    }
                }
                for (int i = 0; i < App.MVProducto.ListaDeProductos.Count; i++)
                {
                    App.ListaDeProductos.Add(App.MVProducto.ListaDeProductos[i]);
                }
                PanelProductoNoEncontrados.IsVisible = false;

                ListaDeProductosHome = App.ListaDeProductos;

                MyListViewBusquedaProductosHome.ItemsSource = App.ListaDeProductos;
                CantidadProductosMostrados = App.ListaDeProductos.Count;
                lbCantidad.Text = App.ListaDeProductos.Count + " Productos disponibles";
            }
            acloading.IsRunning = false;
            acloading.IsVisible = false;
        }
        protected async void BuscarEmpresas(string StrParametroBusqueda, string StrDia, Guid UidEstado, Guid UidColonia, Guid UidBusquedaCategorias, string StrNombreEmpresa)
        {
            string _URL = "" + Helpers.Settings.sitio + "/api/Empresa/GetObtenerEmpresaCliente?StrParametroBusqueda=" + StrParametroBusqueda + "&StrDia=" + StrDia + "&UidColonia=" + UidColonia + "&UidEstado=" + UidEstado + "&UidBusquedaCategorias=" + UidBusquedaCategorias + "&StrNombreEmpresa=" + StrNombreEmpresa + "";
            string content;
            using (HttpClient _webApi = new HttpClient())
            {
                content = await _webApi.GetStringAsync(_URL);
            }
            string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            App.MVEmpresa = JsonConvert.DeserializeObject<VMEmpresas>(obj);

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
            }
        }

        private void btnAcceder_Clicked(object sender, EventArgs e)
        {
            var objeto = new MasterMenuMenuItem { Id = 3, Title = "Login", TargetType = typeof(Login) };
            var Page = (Page)Activator.CreateInstance(objeto.TargetType);
            App app = Application.Current as App;
            App.Navegacion = Page.GetType().Name;
            MasterDetailPage md = (MasterDetailPage)app.MainPage;
            md.Detail = new NavigationPage(Page);
        }
    }
}