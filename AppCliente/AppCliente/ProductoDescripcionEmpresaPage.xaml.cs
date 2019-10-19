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
    public partial class ProductoDescripcionEmpresaPage : ContentPage
    {
        VMEmpresas ObjItem;
        Guid UidEmpresa;
        Guid UiSucursal;
        Guid UidDireccion;

        public ProductoDescripcionEmpresaPage()
        {
            InitializeComponent();
        }

        public ProductoDescripcionEmpresaPage(VMEmpresas ObjItem, Guid UidDireccion)
        {
            InitializeComponent();
            this.UidDireccion = UidDireccion;
            this.ObjItem = ObjItem;
            Title = ObjItem.NOMBRECOMERCIAL;
            //obtener el Dia del dispositivo
            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            txtNombreEmpresa.Text = ObjItem.NOMBRECOMERCIAL;
            UidEmpresa = ObjItem.UIDEMPRESA;

            App.MVSucursales.BuscarSucursalesCliente(ObjItem.UIDEMPRESA, Dia, UidDireccion);
            idSucursal.Text = App.MVSucursales.LISTADESUCURSALES[0].ID.ToString();

            App.MVSucursales.BuscarSucursalesCliente(UidEmpresa, Dia, UidDireccion);

            if (App.MVSucursales.LISTADESUCURSALES.Count > 1)
            {
                txtCantidadDeSucursales.Text = "Sucursales disponibles " + App.MVSucursales.LISTADESUCURSALES.Count.ToString();
            }
            else
            {
                txtCantidadDeSucursales.Text =  App.MVSucursales.LISTADESUCURSALES.Count.ToString() + " sucursal disponible";
            }

            #region foto de portada y foto de perfil
            //Imagen de portada 
            App.MVImagen.obtenerImagenDePortadaEmpresa(UidEmpresa.ToString());
            imgFotoPortada.Source = "http://www.godeliverix.net/vista/" + App.MVImagen.STRRUTA;
            //Imagen de perfil
            App.MVImagen.ObtenerImagenPerfilDeEmpresa(UidEmpresa.ToString());
            imgFotoPerfilEmpresa.Source = "http://www.godeliverix.net/vista/" + App.MVImagen.STRRUTA;
            #endregion
            Guid Colonia = new Guid(App.MVDireccion.ListaDIRECCIONES[0].COLONIA);

            App.MVProducto.BuscarProductoPorSucursal("Giro", Dia, Colonia, new Guid(App.giro), ObjItem.UIDEMPRESA);

            #region busqueda de ListaOferta
            var registro = App.MVSucursales.LISTADESUCURSALES[0];
            txtNombreSucursal.Text = registro.IDENTIFICADOR;
            App.MVOferta.Buscar(UIDSUCURSAL: registro.ID, ESTATUS: "1");
            MypickerMenu.ItemsSource = App.MVOferta.ListaDeOfertas;
            MypickerMenu.SelectedIndex = 0;
            #endregion


            #region Busqueda de Seccion4
            //VMOferta objMenu = MypickerMenu.SelectedItem as VMOferta;
            //App.MVSeccion.Buscar(UIDOFERTA: objMenu.UID, UidDirecccion: UidDireccion);
            // Carrusel.ItemsSource= App.MVSeccion.ListaDeSeccion;
            //MypickerSeccion.ItemsSource = App.MVSeccion.ListaDeSeccion;
            //MypickerSeccion.SelectedIndex = 0;
            #endregion




            #region buscar productos region
            //VMOferta ovjSeccion = MypickerMenu.SelectedItem as VMOferta;
            //App.MVProducto.BuscarProductosSeccion(App.MVSeccion.ListaDeSeccion[0].UID);
            //MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeProductos;
            #endregion
        }

        private void MypickerSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            VMSeccion ovjSeccion = MypickerSeccion.SelectedItem as VMSeccion;
            if (ovjSeccion != null)
            {

                App.MVProducto.BuscarProductosSeccion(ovjSeccion.UID);
                MyListViewBusquedaProductos.ItemsSource = null;
                MyListViewBusquedaProductos.ItemsSource = App.MVProducto.ListaDeProductos;
            }
            else
            {
                MyListViewBusquedaProductos.ItemsSource = null;
            }
        }

        private void MypickerMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                VMOferta objMenu = MypickerMenu.SelectedItem as VMOferta;
                // App.MVSeccion.Buscar(UIDOFERTA: objMenu.UID, UidDirecccion: UidDireccion);
                App.MVSeccion.Buscar(UIDOFERTA: objMenu.UID);
                MypickerSeccion.ItemsSource = null;
                MypickerSeccion.ItemsSource = App.MVSeccion.ListaDeSeccion;
                if (App.MVSeccion.ListaDeSeccion.Count > 0)
                {
                    MypickerSeccion.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {


            }

        }

        private void MyListViewBusquedaProductos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var item = ((ItemTappedEventArgs)e);
                VMProducto ObjItem = (VMProducto)item.Item;


                VMSeccion ovjSeccion = MypickerSeccion.SelectedItem as VMSeccion;

                //Navigation.PushAsync(new ProductoDescripcionPage(ObjItem, UiSucursal, ovjSeccion));
                Navigation.PushAsync(new ProductoDescripcionPage(ObjItem, new Guid(idSucursal.Text), ovjSeccion));

            }
            catch (Exception)
            {

            }
        }

        private void TxtCantidadDeSucursales_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SeleccionarSucursalEmpresa(MypickerMenu, App.MVSucursales.LISTADESUCURSALES, ObjItem, idSucursal, txtNombreSucursal));
        }

        private void BtnCarrito_Clicked(object sender, EventArgs e)
        {

        }
    }
}