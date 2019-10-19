using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeleccionarDirecciones : ContentPage
    {
        bool tap = true;
        Button Button;
        StackLayout PanelProductoNoEncontrados;
        StackLayout ScrollView_Empresas;
        StackLayout ScrollView_Productos;
        Label IDDireccionBusqueda;
        ListView myListProduct;
        Label lbCantidad;
        ListView MyListViewBusquedaEmpresas;
        int CantidadProductosMostrados;
        public SeleccionarDirecciones(Button button,
            Label IDDireccionBusqueda,
            ListView myListProduct,
            Label lbCantidad,
            int CantidadProductosMostrados,
            StackLayout PanelProductoNoEncontrados,
            ListView MyListViewBusquedaEmpresas,
            StackLayout ScrollView_Productos,
            StackLayout ScrollView_Empresas)
        {
            InitializeComponent();
            this.ScrollView_Empresas = ScrollView_Empresas;
            this.ScrollView_Productos = ScrollView_Productos;
            App.MVDireccion.ObtenerDireccionesUsuario(App.Global1);
            this.Button = button;
            this.IDDireccionBusqueda = IDDireccionBusqueda;
            this.PanelProductoNoEncontrados = PanelProductoNoEncontrados;
            this.MyListViewBusquedaEmpresas = MyListViewBusquedaEmpresas;
            MyListViewDirecciones.ItemsSource = AppCliente.App.MVDireccion.ListaDIRECCIONES;
            var index = AppCliente.App.MVDireccion.ListaDIRECCIONES.Find(t => t.ID.ToString() == IDDireccionBusqueda.Text);
            this.myListProduct = myListProduct;
            this.lbCantidad = lbCantidad;
            this.CantidadProductosMostrados = CantidadProductosMostrados;
            MyListViewDirecciones.SelectedItem = index;

            Inicializar(button, IDDireccionBusqueda, myListProduct, lbCantidad, CantidadProductosMostrados);
        }
        public async void Inicializar(Button button, Label IDDireccionBusqueda, ListView myListProduct, Label lbCantidad, int CantidadProductosMostrados)
        {
            await Task.Factory.StartNew(() =>
            {

            });
        }

        private async void MyListViewDirecciones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var action = false;

            if (App.MVProducto.ListaDelCarrito.Count > 0)
            {
                action = await DisplayAlert("Oooops!", "¿Al cambiar de direccion se eliminara tu carrito?", "Si", "No");
            }
            else
            {
                action = true;
            }
            if (action)
            {
                if (tap)
                {
                    App.MVProducto.ListaDelCarrito.Clear();
                    App.MVProducto.ListaDelInformacionSucursales.Clear();
                    tap = false;
                    await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
                    var item = ((ItemTappedEventArgs)e);
                    VMDireccion ObjItem = (VMDireccion)item.Item;

                    try
                    {
                        this.Button.Text = "ENTREGAR EN " + ObjItem.IDENTIFICADOR + " >";
                        this.IDDireccionBusqueda.Text = ObjItem.ID.ToString();
                        App.DireccionABuscar = ObjItem.ID.ToString();
                        BuscarProductos();
                    }
                    catch (Exception)
                    {
                        await Navigation.PopToRootAsync(false);
                        await PopupNavigation.Instance.PopAllAsync();
                    }
                    await Navigation.PopToRootAsync(false);
                    await PopupNavigation.Instance.PopAllAsync();
                }
            }
        }


        public void BuscarProductos()
        {

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

            string hora = Hora + ":" + DateTime.Now.Minute.ToString();


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


            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);


            //Busqueda por giro
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                //giro
                App.MVProducto.buscarProductosEmpresaDesdeCliente("Giro", Dia, Direccion, new Guid(giro), Buscado);
                App.MVEmpresa.BuscarEmpresaBusquedaCliente("Giro", Dia, Direccion, new Guid(giro), Buscado);
            }
            else
            //// Busqueda por categoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                App.MVProducto.buscarProductosEmpresaDesdeCliente("Categoria", Dia, Direccion, new Guid(categoria), Buscado);
                App.MVEmpresa.BuscarEmpresaBusquedaCliente("Giro", Dia, Direccion, new Guid(categoria), Buscado);
            }
            else
            //Busqueda por subcategoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
            {
                App.MVProducto.buscarProductosEmpresaDesdeCliente("Subcategoria", Dia, Direccion, new Guid(subcategoria), Buscado);
                App.MVEmpresa.BuscarEmpresaBusquedaCliente("Giro", Dia, Direccion, new Guid(subcategoria), Buscado);
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



                myListProduct.ItemsSource = null;

                myListProduct.ItemsSource = AppCliente.App.MVProducto.ListaDeProductos;
                CantidadProductosMostrados = AppCliente.App.MVProducto.ListaDeProductos.Count;
                lbCantidad.Text =  App.MVProducto.ListaDeProductos.Count + " Productos disponibles";

                MyListViewBusquedaEmpresas.ItemsSource = null;

                if (AppCliente.App.MVProducto.ListaDeProductos.Count == 0)
                {
                    lbCantidad.Text = "0-0/0";
                    PanelProductoNoEncontrados.IsVisible = true;
                }
                else
                {
                    PanelProductoNoEncontrados.IsVisible = false;
                }

            }
            else if (App.buscarPor == "Empresas")
            {
                int a = AppCliente.App.MVEmpresa.LISTADEEMPRESAS.Count;
                MyListViewBusquedaEmpresas.ItemsSource = null;
                myListProduct.ItemsSource = null;
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
        }
    }
}