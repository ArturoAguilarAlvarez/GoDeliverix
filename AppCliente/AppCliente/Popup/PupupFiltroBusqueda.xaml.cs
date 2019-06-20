using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rg.Plugins.Popup.Pages;

using Rg.Plugins.Popup.Services;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCliente.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PupupFiltroBusqueda : PopupPage
    {
        ImageButton MyBtnFiltroBusquedas;
        StackLayout ScrollView_Productos;
        StackLayout ScrollView_Empresas;
        StackLayout PanelProductoNoEncontrados;
        ListView MyListViewBusquedaProductosHome;
        ListView MyListViewBusquedaEmpresas;
        int CantidadProductosMostrados = 0;
        Label lbCantidad;

        public PupupFiltroBusqueda(ImageButton button,
            StackLayout ScrollView_Productos,
            StackLayout ScrollView_Empresas,
            ListView MyListViewBusquedaProductosHome,
            ListView MyListViewBusquedaEmpresas,
            StackLayout PanelProductoNoEncontrados,
            Label lbCantidad)
        {
            this.lbCantidad = lbCantidad;
            this.MyListViewBusquedaProductosHome= MyListViewBusquedaProductosHome;
            this.MyListViewBusquedaEmpresas = MyListViewBusquedaEmpresas;
             MyBtnFiltroBusquedas = button;
            InitializeComponent();
            this.ScrollView_Productos = ScrollView_Productos;
            this.ScrollView_Empresas = ScrollView_Empresas;
            this.PanelProductoNoEncontrados = PanelProductoNoEncontrados;
            MyPickerGiro.ItemsSource = null;
            MyPickerCategoria.ItemsSource = null;
            MyPickerSubCategoria.ItemsSource = null;

            AppCliente.App.MVGiro.LISTADEGIRO.Clear();
            AppCliente.App.MVGiro.ListaDeGiroConimagen();
            MyPickerGiro.ItemsSource = AppCliente.App.MVGiro.LISTADEGIRO;
            MyPickerGiro.SelectedIndex = 0;
            VMGiro objetoGiro = MyPickerGiro.SelectedItem as VMGiro;
            App.MVSubCategoria.BuscarSubCategoria(UidCategoria: Guid.Empty.ToString(), Tipo: "Seleccionar");
            App.MVCategoria.BuscarCategorias(UidGiro: objetoGiro.UIDVM.ToString(), tipo: "seleccion");
            MyPickerCategoria.ItemsSource = App.MVCategoria.LISTADECATEGORIAS;
            

            //if (App.buscarPor != "")
            //{
            //    MypickerEmpresaProducto.SelectedItem = App.buscarPor;
            //    if (App.giro != "")
            //    {
            //        MyPickerGiro.SelectedItem = App.MVGiro.LISTADEGIRO.Find(t => t.UIDVM == new Guid(App.giro));
            //        if (App.categoria != "")
            //        {
            //            MyPickerCategoria.SelectedItem = App.MVCategoria.LISTADECATEGORIAS.Find(t => t.UIDCATEGORIA == new Guid(App.categoria));
            //            if (App.subcategoria != "")
            //            {
            //                MyPickerCategoria.SelectedItem = App.MVCategoria.LISTADECATEGORIAS.Find(t => t.UIDCATEGORIA == new Guid(App.categoria));
            //                if (App.subcategoria != "")
            //                {
            //                    MyPickerSubCategoria.SelectedItem = App.MVSubCategoria.LISTADESUBCATEGORIAS.Find(t => t.UID == new Guid(App.subcategoria));
            //                }
            //                else
            //                {

            //                    MyPickerSubCategoria.SelectedIndex = 0;
            //                }
            //            }
            //            else
            //            {

            //                MypickerEmpresaProducto.SelectedIndex = 0;
            //            }
            //        }
            //        else
            //        {
            //            MyPickerSubCategoria.SelectedIndex = 0;
            //            MypickerEmpresaProducto.SelectedIndex = 0;
            //        }
            //    }
            //    else
            //    {
            //        MyPickerCategoria.SelectedIndex = 0;
            //        MyPickerSubCategoria.SelectedIndex = 0;
            //        MypickerEmpresaProducto.SelectedIndex = 0;
            //    }
            //}
            //else
            //{
                MyPickerCategoria.SelectedIndex = 0;
                MyPickerSubCategoria.SelectedIndex = 0;
                //MypickerEmpresaProducto.SelectedIndex = 0;
            //}
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            MyBtnFiltroBusquedas.IsEnabled = true;
            PopupNavigation.Instance.PopAsync();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            VMGiro objgiro = MyPickerGiro.SelectedItem as VMGiro;
            VMCategoria objCatergoria = MyPickerCategoria.SelectedItem as VMCategoria;
            VMSubCategoria objSubCategoria = MyPickerSubCategoria.SelectedItem as VMSubCategoria;

            //App.buscarPor = MypickerEmpresaProducto.SelectedItem.ToString();
            App.giro = objgiro.UIDVM.ToString();
            App.categoria = objCatergoria.UIDCATEGORIA.ToString();
            App.subcategoria = objSubCategoria.UID.ToString();

            SearchFor_SearchButtonPressed();




            MyBtnFiltroBusquedas.IsEnabled = true;
            PopupNavigation.Instance.PopAsync();
        }

        private void MyPickerGiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            VMGiro objetoGiro = MyPickerGiro.SelectedItem as VMGiro;

            App.MVCategoria.BuscarCategorias(UidGiro: objetoGiro.UIDVM.ToString(), tipo: "seleccion");


            App.MVSubCategoria.BuscarSubCategoria(UidCategoria: Guid.Empty.ToString(), Tipo: "Seleccionar");

            MyPickerCategoria.ItemsSource = App.MVCategoria.LISTADECATEGORIAS;

            MyPickerSubCategoria.ItemsSource = App.MVSubCategoria.LISTADESUBCATEGORIAS;
            MyPickerCategoria.SelectedIndex = 0;
            MyPickerSubCategoria.SelectedIndex = 0;
        }

        private void MyPickerCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            VMCategoria objetoCategoria = MyPickerCategoria.SelectedItem as VMCategoria;
            if (objetoCategoria != null)
            {
                App.MVSubCategoria.BuscarSubCategoria(UidCategoria: objetoCategoria.UIDCATEGORIA.ToString(), Tipo: "Seleccionar");
            }
            MyPickerSubCategoria.ItemsSource = App.MVSubCategoria.LISTADESUBCATEGORIAS;
            MyPickerSubCategoria.SelectedIndex = 0;
        }


        private void SearchFor_SearchButtonPressed()
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


            Guid Direccion = new Guid(App.DireccionABuscar);


            //Busqueda por giro
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                //giro
                App.MVProducto.buscarProductosEmpresaDesdeCliente("Giro", Dia, Direccion, new Guid(giro), "");
                App.MVEmpresa.BuscarEmpresaBusquedaCliente("Giro", Dia, Direccion, new Guid(giro), "");
            }
            else
            //// Busqueda por categoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || string.IsNullOrWhiteSpace(subcategoria)))
            {
                App.MVProducto.buscarProductosEmpresaDesdeCliente("Categoria", Dia, Direccion, new Guid(categoria), "");
                App.MVEmpresa.BuscarEmpresaBusquedaCliente("Categoria", Dia, Direccion, new Guid(categoria), "");
            }
            else
            //Busqueda por subcategoria
            if (giro != Guid.Empty.ToString() && (categoria == Guid.Empty.ToString() || !string.IsNullOrEmpty(categoria)) && (subcategoria == Guid.Empty.ToString() || !string.IsNullOrWhiteSpace(subcategoria)))
            {
                App.MVProducto.buscarProductosEmpresaDesdeCliente("Subcategoria", Dia, Direccion, new Guid(subcategoria), "");
                App.MVEmpresa.BuscarEmpresaBusquedaCliente("Subcategoria", Dia, Direccion, new Guid(subcategoria), "");
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


                //MyListViewBusquedaEmpresas.ItemsSource = AppCliente.App.MVEmpresa.LISTADEEMPRESAS;
                ScrollView_Empresas.IsVisible = true;
                ScrollView_Productos.IsVisible = false;

                if (AppCliente.App.MVEmpresa.LISTADEEMPRESAS.Count > 10)
                {
                    MyListViewBusquedaEmpresas.ItemsSource = AppCliente.App.MVEmpresa.LISTADEEMPRESAS.GetRange(0, 10);
                    CantidadProductosMostrados = 10;
                    lbCantidad.Text = "1-10/" + App.MVProducto.ListaDeProductos.Count;
                    //btnAtras.IsEnabled = false;
                }
                else
                {
                    MyListViewBusquedaEmpresas.ItemsSource = AppCliente.App.MVEmpresa.LISTADEEMPRESAS;
                    CantidadProductosMostrados = AppCliente.App.MVEmpresa.LISTADEEMPRESAS.Count;
                    lbCantidad.Text = "1-" + AppCliente.App.MVEmpresa.LISTADEEMPRESAS.Count + "/" + AppCliente.App.MVEmpresa.LISTADEEMPRESAS.Count;
                    //btnAtras.IsVisible = false;
                    //btnAdelante.IsVisible = false;
                }

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