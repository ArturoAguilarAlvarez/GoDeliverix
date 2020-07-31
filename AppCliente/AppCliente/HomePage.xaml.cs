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
using System.Threading;
using Plugin.Connectivity;
using System.Data;

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
            CargaInicial();
        }
        protected async void CargaInicial()
        {
            //if (await IsRunningGoDeliverixServicesAsync())
            //{
            string versionApp = "";
            // Uid de la aplicacion 87b2dcfd-205a-4260-9092-1ce48b28aa4a
            if (Device.RuntimePlatform == Device.Android)
            {
                versionApp = "87b2dcfd-205a-4260-9092-1ce48b28aa4a";
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                versionApp = "310cba91-57a5-4699-91fe-3677c2718907";
            }
            Iniciar:
            ApiService ApiService = new ApiService("/api/Version");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", versionApp);
            var result = await ApiService.GET<VMVersion>(action: "Get", responseType: ApiService.ResponseType.Object, arguments: parameters);
            var oReponse = result as ResponseHelper;

            if (result != null && oReponse.Status != false)
            {
                var oversion = oReponse.Data as VMVersion;
                string version = VersionTracking.CurrentVersion;
                if (oversion.StrVersion == version)
                {
                    if (string.IsNullOrEmpty(App.Global1))
                    {
                        btnAcceder.Text = "Entrar";
                        btnAcceder.IsEnabled = true;
                    }
                    else
                    {
                        btnAcceder.Text = string.Empty;
                        btnAcceder.IsEnabled = false;
                    }
                    ApiService = new ApiService("/api/Giro");
                    parameters = new Dictionary<string, string>();
                    result = await ApiService.GET<VMGiro>(action: "Get", responseType: ApiService.ResponseType.Object, arguments: parameters);
                    oReponse = result as ResponseHelper;
                    if (result != null && oReponse.Status != false)
                    {
                        App.MVGiro = oReponse.Data as VMGiro;
                        App.giro = App.MVGiro.LISTADEGIRO[0].UIDVM.ToString();
                        ApiService = new ApiService("/api/Categoria");
                        parameters = new Dictionary<string, string>();
                        parameters.Add("value", App.giro.ToString());
                        result = await ApiService.GET<VMCategoria>(action: "Get", responseType: ApiService.ResponseType.Object, arguments: parameters);
                        oReponse = result as ResponseHelper;
                        if (result != null && oReponse.Status != false)
                        {
                            App.MVCategoria = oReponse.Data as VMCategoria;

                            if (!string.IsNullOrEmpty(App.Global1))
                            {
                                Iniciar();
                            }
                            else
                        if (!string.IsNullOrEmpty(Helpers.Settings.StrCOLONIA))
                            {

                                Iniciar();
                            }
                            else
                            {
                                try
                                {
                                    await Navigation.PushAsync(new SeleccionaColonia());
                                    PanelUbicacionNoEstablecida.IsVisible = true;
                                    PanelProductoNoEncontrados.IsVisible = false;
                                    ScrollView_Productos.IsVisible = false;
                                    //lbCantidad.Text = "No hay resultados";
                                    btnSeleccionarDireccion.Text = "No hay ubicación";
                                }
                                catch (FeatureNotSupportedException)
                                {
                                    // Handle not supported on device exception
                                    await DisplayAlert("Aviso del sistema", "Los servicios de ubicacion no soportados por el dispositivo", "Aceptar");
                                }
                                catch (FeatureNotEnabledException)
                                {
                                    await DisplayAlert("Ubicacion no activa", "Activa el GPS para obtener tu ubicacion", "Aceptar");
                                }
                                catch (PermissionException)
                                {
                                    // Handle permission exception
                                    await DisplayAlert("Aviso", "Activa los permisos de ubicacion para continuar", "Aceptar");
                                }
                                catch (Exception)
                                {
                                    // Unable to get location
                                    await DisplayAlert("Aviso", "No se puede obtener la ubicacion", "Aceptar");
                                }
                            }
                        }
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
                        await Launcher.OpenAsync(new Uri(urlStore));
                    }
                    else
                    {
                        goto Iniciar;
                    }
                }
            }
            //}
            //else
            //{
            //    Application.Current.MainPage = new NavigationPage(new SitioEnMantenimiento());
            //}
            //}
            //else
            //{
            //    GenerateMessage("Sin internet", "El dispositivo no esta conectado a internet, verifique su conexión.", "Aceptar");
            //}
        }
        public async Task<bool> IsRunningGoDeliverixServicesAsync()
        {
            var conectivilidad = Connectivity.NetworkAccess;
            bool resultado = false;
            if (conectivilidad == NetworkAccess.Internet)
            {
                resultado = await CrossConnectivity.Current.IsReachable("www.google.com", 5000);
            }
            else if (conectivilidad == NetworkAccess.Local)
            {
                resultado = await CrossConnectivity.Current.IsReachable(Helpers.Settings.sitio, 5000);

            }
            return resultado;
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
            PopupNavigation.Instance.PushAsync(new Popup.PupupFiltroBusqueda(btnFitltrosBusquedas, ScrollView_Productos, ScrollView_Empresas, MyListViewBusquedaProductosHome, MyListViewBusquedaEmpresas, PanelProductoNoEncontrados));
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
                    //lbCantidad.Text = "1-10/" + App.MVProducto.ListaDeProductos.Count;
                }
                else
                {
                    MyListViewBusquedaProductosHome.ItemsSource = App.MVProducto.ListaDeProductos;
                    CantidadProductosMostrados = App.MVProducto.ListaDeProductos.Count;
                    // lbCantidad.Text = "1-" + App.MVProducto.ListaDeProductos.Count + "/" + App.MVProducto.ListaDeProductos.Count;
                }

                MyListViewBusquedaEmpresas.ItemsSource = null;
                if (!string.IsNullOrEmpty(App.UidColoniaABuscar))
                {
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
                else
                {
                    PanelUbicacionNoEstablecida.IsVisible = true;
                    PanelProductoNoEncontrados.IsVisible = false;
                    ScrollView_Productos.IsVisible = false;
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
            //txtBusquedaActual.Text = Buscado;
        }

        private async void BtnSeleccionarDireccion_Clicked(object sender, EventArgs e)
        {
            if (App.Global1 == string.Empty)
            {
                App.MVDireccionDemo = null;
                NavigationPage NPScannerCompanyPage = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);

                await NPScannerCompanyPage.PushAsync(new SeleccionaColonia());

            }
            else
            {
                await Navigation.PushAsync(new SeleccionarDirecciones(btnSeleccionarDireccion, MyListViewBusquedaProductosHome, CantidadProductosMostrados, PanelProductoNoEncontrados, MyListViewBusquedaEmpresas, ScrollView_Productos, ScrollView_Empresas));
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
            //txtBusquedaActual.Text = searchFor.Text;
        }

        private void ImageButton_Clicked_2(object sender, EventArgs e)
        {
            PanelNavegacionCarrito.IsVisible = false;
            PanelNavegacionBuscar.IsVisible = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        public async void Iniciar()
        {
            acloading.IsVisible = true;
            acloading.IsRunning = true;
            string obj = "";
            //lbCantidad.Text = "Cargando productos";

            if (string.IsNullOrEmpty(App.Global1))
            {
                App.MVDireccion.ListaDIRECCIONES = new List<VMDireccion>();
                App.MVDireccion.ListaDIRECCIONES.Add(new VMDireccion()
                {
                    ESTADO = Helpers.Settings.StrESTADO,
                    COLONIA = Helpers.Settings.StrCOLONIA,
                    NOMBRECOLONIA = Helpers.Settings.StrNombreColonia
                });
                App.UidEstadoABuscar = Helpers.Settings.StrESTADO;

                btnSeleccionarDireccion.Text = "ENTREGAR EN " + App.MVDireccion.ListaDIRECCIONES[0].NOMBRECOLONIA.ToUpper() + " >";

                btnAcceder.Text = "Entrar";
                btnAcceder.IsEnabled = true;
            }
            else
            {
                if (App.MVDireccion.ListaDIRECCIONES.Count == 0)
                {
                    string strDirecciones = string.Empty;
                    App.MVDireccion = new VMDireccion();
                    using (HttpClient _client = new HttpClient())
                    {
                        var tex = "" + Helpers.Settings.sitio + "/api/Direccion/GetObtenerDireccionUsuario?UidUsuario=" + App.Global1;
                        strDirecciones = await _client.GetStringAsync(tex);
                        obj = JsonConvert.DeserializeObject<ResponseHelper>(strDirecciones).Data.ToString();
                        App.MVDireccion = JsonConvert.DeserializeObject<VMDireccion>(obj);
                    }
                }
                btnAcceder.Text = string.Empty;
                btnAcceder.IsEnabled = false;
            }

            if (App.MVDireccion != null)
            {
                acloading.IsVisible = true;
                acloading.IsRunning = true;

                #region Busqueda

                Guid UidEstado = new Guid();
                Guid UidColonia = new Guid();

                CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
                string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                PanelRefrescar.IsVisible = false;
                if (App.MVDireccion.ListaDIRECCIONES.Count != 0)
                {
                    PanelProductos.IsVisible = true;
                    App.UidColoniaABuscar = App.MVDireccion.ListaDIRECCIONES[0].COLONIA;
                    App.UidEstadoABuscar = App.MVDireccion.ListaDIRECCIONES[0].ESTADO;

                    if (!string.IsNullOrEmpty(App.Global1))
                    {
                        App.DireccionABuscar = App.MVDireccion.ListaDIRECCIONES[0].ID.ToString();
                    }
                    if (!string.IsNullOrEmpty(App.UidColoniaABuscar) && !string.IsNullOrEmpty(App.UidEstadoABuscar))
                    {
                        UidEstado = new Guid(App.UidEstadoABuscar);
                        UidColonia = new Guid(App.UidColoniaABuscar);
                        VMProducto oBusquedaproducto = new VMProducto();
                        App.ListaDeProductos = new List<VMProducto>();
                        if (App.DireccionABuscar != "" && !string.IsNullOrEmpty(App.Global1))
                        {
                            btnSeleccionarDireccion.Text = "ENTREGAR A " + App.MVUsuarios.StrUsuario + " EN " + App.MVDireccion.ListaDIRECCIONES.Find(x => x.ESTADO == App.UidEstadoABuscar).IDENTIFICADOR + " >";
                        }
                        ApiService ApiService = new ApiService("/api/Producto");
                        Dictionary<string, string> parameters = new Dictionary<string, string>();
                        parameters.Add("StrParametroBusqueda", "Giro");
                        parameters.Add("StrDia", Dia);
                        parameters.Add("UidEstado", UidEstado.ToString());
                        parameters.Add("UidColonia", UidColonia.ToString());
                        parameters.Add("UidBusquedaCategorias", App.giro);
                        //parameters.Add("StrNombreEmpresa", txtBusquedaActual.Text);
                        var result = await ApiService.GET<VMProducto>(action: "GetBuscarProductosCliente", responseType: ApiService.ResponseType.Object, arguments: parameters);
                        var oReponse = result as ResponseHelper;
                        if (result != null && oReponse.Status != false)
                        {
                            oBusquedaproducto = oReponse.Data as VMProducto;
                            if (oBusquedaproducto.ListaDeProductos != null && oBusquedaproducto.ListaDeProductos.Count > 0)
                            {
                                foreach (VMProducto item in oBusquedaproducto.ListaDeProductos)
                                {
                                    if (App.MVProducto.ListaDelCarrito.Exists(o => o.UID == item.UID))
                                    {
                                        item.IsSelected = true;
                                    }
                                    //parameters = new Dictionary<string, string>();
                                    //parameters.Add("StrParametroBusqueda", "Giro");
                                    //parameters.Add("StrDia", Dia);
                                    //parameters.Add("UidEstado", UidEstado.ToString());
                                    //parameters.Add("UidColonia", UidColonia.ToString());
                                    //parameters.Add("UidBusquedaCategorias", App.giro);
                                    //parameters.Add("UidProducto", item.UID.ToString());
                                    //result = await ApiService.GET<VMProducto>(action: "GetObtenerInformacionDeProductoDeLaSucursal", responseType: ApiService.ResponseType.Object, arguments: parameters);

                                    //oReponse = result as ResponseHelper;
                                    //if (result != null && oReponse.Status != false)
                                    //{
                                    //    var VProducto = oReponse.Data as VMProducto;
                                    //    if (VProducto.ListaDePreciosSucursales.Count > 0)
                                    //    {
                                    //        item.StrCosto = VProducto.ListaDePreciosSucursales[0].StrCosto;
                                    App.ListaDeProductos.Add(item);
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    var objeto = new MasterMenuMenuItem { Id = 1, Title = "Busqueda", TargetType = typeof(HomePage), UrlResource = "IconoHomeMenu" };
                                    //    var Page = (Page)Activator.CreateInstance(objeto.TargetType);
                                    //    App app = Application.Current as App;
                                    //    App.Navegacion = Page.GetType().Name;
                                    //    MasterDetailPage md = (MasterDetailPage)app.MainPage;
                                    //    md.Detail = new NavigationPage(Page);
                                    //}
                                }
                                PanelProductoNoEncontrados.IsVisible = false;
                                ListaDeProductosHome = App.ListaDeProductos;

                                MyListViewBusquedaProductosHome.ItemsSource = App.ListaDeProductos;
                                MyListViewBusquedaProductosHome.HeightRequest = 550;
                                CantidadProductosMostrados = App.ListaDeProductos.Count;
                                //lbCantidad.Text = App.ListaDeProductos.Count + " Productos disponibles";
                            }
                            else
                            {
                                //lbCantidad.Text = "No hay productos disponibles";
                                PanelProductoNoEncontrados.IsVisible = true;
                            }
                        }
                        else
                        {
                            var objeto = new MasterMenuMenuItem { Id = 1, Title = "Busqueda", TargetType = typeof(HomePage), UrlResource = "IconoHomeMenu" };
                            var Page = (Page)Activator.CreateInstance(objeto.TargetType);
                            App app = Application.Current as App;
                            App.Navegacion = Page.GetType().Name;
                            MasterDetailPage md = (MasterDetailPage)app.MainPage;
                            md.Detail = new NavigationPage(Page);
                        }
                    }
                    acloading.IsRunning = false;
                    acloading.IsVisible = false;
                }
                else
                {
                    acloading.IsRunning = false;
                    acloading.IsVisible = false;

                    PanelProductos.IsVisible = false;
                    if (!string.IsNullOrEmpty(App.Global1))
                    {
                        PanelDireccionesVacias.IsVisible = true;
                    }
                    else
                    {
                        PanelRefrescar.IsVisible = true;



                    }
                }
                #endregion
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
                //lbCantidad.Text = App.ListaDeProductos.Count + " Productos disponibles";
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

        private void btnRefrescar_Clicked(object sender, EventArgs e)
        {
            //Iniciar();
        }
        protected async void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }

        private void MyListViewBusquedaProductosHome_Refreshing(object sender, EventArgs e)
        {
            //Iniciar();
            MyListViewBusquedaProductosHome.IsRefreshing = false;
        }
    }
}