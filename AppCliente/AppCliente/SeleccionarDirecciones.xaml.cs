using AppCliente.ViewModel;
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
        ListView myListProduct;
        Label lbCantidad;
        ListView MyListViewBusquedaEmpresas;
        int CantidadProductosMostrados;
        VMProductosYEmpresas oBusquedaProdutos = new VMProductosYEmpresas();
        public SeleccionarDirecciones(Button button,
            ListView myListProduct,
            int CantidadProductosMostrados,
            StackLayout PanelProductoNoEncontrados,
            ListView MyListViewBusquedaEmpresas,
            StackLayout ScrollView_Productos,
            StackLayout ScrollView_Empresas)
        {
            InitializeComponent();
            this.ScrollView_Empresas = ScrollView_Empresas;
            this.ScrollView_Productos = ScrollView_Productos;
            // App.MVDireccion.ObtenerDireccionesUsuario(App.Global1);
            this.Button = button;
            this.PanelProductoNoEncontrados = PanelProductoNoEncontrados;
            this.MyListViewBusquedaEmpresas = MyListViewBusquedaEmpresas;
            MyListViewDirecciones.ItemsSource = App.MVDireccion.ListaDIRECCIONES;
            var index = App.MVDireccion.ListaDIRECCIONES.Find(t => t.ID.ToString() == App.DireccionABuscar);
            this.myListProduct = myListProduct;
            //this.lbCantidad = lbCantidad;
            this.CantidadProductosMostrados = CantidadProductosMostrados;
            MyListViewDirecciones.SelectedItem = index;

        }


        private async void MyListViewDirecciones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            bool action;

            if (App.MVProducto.ListaDelCarrito.Count > 0)
            {
                action = await DisplayAlert("Aviso!", "Al cambiar de direccion se eliminara tu carrito", "Aceptar", "Cancelar");
            }
            else
            {
                action = true;
            }
            if (action)
            {
                if (tap)
                {
                    App.MVProducto.ListaDelCarrito = new List<VMProducto>();
                    App.MVProducto.ListaDelInformacionSucursales = new List<VMProducto>();
                    tap = false;
                    await PopupNavigation.Instance.PushAsync(new Popup.PopupLoanding());
                    var item = e;
                    VMDireccion ObjItem = (VMDireccion)item.Item;

                    try
                    {
                        this.Button.Text = "ENTREGAR EN " + ObjItem.IDENTIFICADOR + " >";
                        App.DireccionABuscar = ObjItem.ID.ToString();
                        App.UidColoniaABuscar = ObjItem.COLONIA.ToString();
                        App.UidEstadoABuscar = ObjItem.ESTADO.ToString();
                        //Pendiente a verificar
                        //BuscarProductos();
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
            string giro = App.giro;
            string categoria = App.categoria;
            string subcategoria = App.subcategoria;
            if (giro == "")
            {
                giro = AppCliente.App.MVGiro.LISTADEGIRO[0].UIDVM.ToString();
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
            Guid UidEstado;
            Guid UidColonia;
            Guid Direccion;
            if (Helpers.Settings.UidDireccion != string.Empty)
            {
                Direccion = App.MVDireccion.ListaDIRECCIONES[0].ID;
                UidEstado = new Guid(App.MVDireccion.ListaDIRECCIONES[0].ESTADO);
                UidColonia = new Guid(App.MVDireccion.ListaDIRECCIONES[0].COLONIA);
            }
            else
            {
                Direccion = new Guid(Helpers.Settings.UidDireccion);
                UidEstado = new Guid(Helpers.Settings.StrESTADO);
                UidColonia = new Guid(Helpers.Settings.StrCOLONIA);
            }
            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);


            //Busqueda por giro
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                //giro
                oBusquedaProdutos.BuscarProductos("Giro", Dia, UidEstado, UidColonia, new Guid(giro), Buscado);

                App.MVEmpresa.BuscarEmpresaBusquedaCliente("Giro", Dia, UidEstado, UidColonia, new Guid(giro), Buscado);
            }
            else
            //// Busqueda por categoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                oBusquedaProdutos.BuscarProductos("Categoria", Dia, UidEstado, UidColonia, new Guid(categoria), Buscado);

                App.MVEmpresa.BuscarEmpresaBusquedaCliente("Giro", Dia, UidEstado, UidColonia, new Guid(categoria), Buscado);
            }
            else
            //Busqueda por subcategoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
            {
                oBusquedaProdutos.BuscarProductos("Subcategoria", Dia, UidEstado, UidColonia, new Guid(subcategoria), Buscado);

                //App.MVProducto.buscarProductosEmpresaDesdeCliente("Subcategoria", Dia, Direccion, new Guid(subcategoria), Buscado);
                App.MVEmpresa.BuscarEmpresaBusquedaCliente("Giro", Dia, UidEstado, UidColonia, new Guid(subcategoria), Buscado);
            }

            if (App.buscarPor == "Productos")
            {

                foreach (VMProducto item in App.MVProducto.ListaDeProductos)
                {
                    if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                    {
                        App.MVProducto.BuscarProductoPorSucursal("Giro", Dia, UidColonia, UidEstado, new Guid(giro), item.UID);
                    }
                    else
                    if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
                    {
                        App.MVProducto.BuscarProductoPorSucursal("Categoria", Dia, UidColonia, UidEstado, new Guid(categoria), item.UID);
                    }
                    else
                    if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
                    {
                        App.MVProducto.BuscarProductoPorSucursal("Subcategoria", Dia, UidColonia, UidEstado, new Guid(subcategoria), item.UID);
                    }

                    item.StrCosto = App.MVProducto.ListaDePreciosSucursales[0].StrCosto;
                }
                myListProduct.ItemsSource = null;

                myListProduct.ItemsSource = App.MVProducto.ListaDeProductos;
                CantidadProductosMostrados = App.MVProducto.ListaDeProductos.Count;
                lbCantidad.Text = App.MVProducto.ListaDeProductos.Count + " Productos disponibles";

                MyListViewBusquedaEmpresas.ItemsSource = null;

                if (App.MVProducto.ListaDeProductos.Count == 0)
                {
                    lbCantidad.Text = "0-0/0";
                    PanelProductoNoEncontrados.IsVisible = true;
                }
                else
                {
                    PanelProductoNoEncontrados.IsVisible = false;
                }

            }
            //else if (App.buscarPor == "Empresas")
            //{
            //    int a = App.MVEmpresa.LISTADEEMPRESAS.Count;
            //    MyListViewBusquedaEmpresas.ItemsSource = null;
            //    myListProduct.ItemsSource = null;
            //    for (int i = 0; i < a; i++)
            //    {
            //        App.MVEmpresa.LISTADEEMPRESAS[i].StrRuta = "" + Helpers.Settings.sitio + "/vista" + App.MVEmpresa.LISTADEEMPRESAS[i].StrRuta.Substring(2);
            //    }


            //    MyListViewBusquedaEmpresas.ItemsSource = App.MVEmpresa.LISTADEEMPRESAS;
            //    ScrollView_Empresas.IsVisible = true;
            //    ScrollView_Productos.IsVisible = false;


            //    if (App.MVEmpresa.LISTADEEMPRESAS.Count == 0)
            //    {
            //        PanelProductoNoEncontrados.IsVisible = true;
            //        ScrollView_Empresas.IsVisible = false;
            //    }
            //    else
            //    {
            //        PanelProductoNoEncontrados.IsVisible = false;
            //        ScrollView_Empresas.IsVisible = true;
            //    }
            //}
        }
    }
}