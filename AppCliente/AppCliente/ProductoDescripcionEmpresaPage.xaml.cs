using AppCliente.WebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
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
        public ProductoDescripcionEmpresaPage(VMEmpresas ObjItem)
        {
            InitializeComponent();
            this.ObjItem = ObjItem;
            Title = ObjItem.NOMBRECOMERCIAL;
            //obtener el Dia del dispositivo
            CultureInfo ConfiguracionDiaEspanol = new CultureInfo("Es-Es");
            string Dia = ConfiguracionDiaEspanol.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            txtNombreEmpresa.Text = ObjItem.NOMBRECOMERCIAL;
            UidEmpresa = ObjItem.UIDEMPRESA;
            CargaPerfilEmpresa(Dia);

        }
        protected async void CargaPerfilEmpresa(string Dia)
        {
            string uril = "" + Helpers.Settings.sitio + "/api/Sucursales/GetBuscarSucursalesDeUnProducto?uidEmpresa=" + ObjItem.UIDEMPRESA + "&day=" + Dia + "&UidEstado=" + App.UidEstadoABuscar + "&UidColonia=" + App.UidColoniaABuscar + "";
            string content = "";
            using (HttpClient _webClient = new HttpClient())
            {
                content = await _webClient.GetStringAsync(uril);
            }
            string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            App.MVSucursales = JsonConvert.DeserializeObject<VMSucursales>(obj);
            #region foto de portada y foto de perfil
            //Imagen de portada 
            App.MVImagen.obtenerImagenDePortadaEmpresa(UidEmpresa.ToString());
            imgFotoPortada.Source = "" + Helpers.Settings.sitio + "/vista/" + App.MVImagen.STRRUTA;
            //Imagen de perfil
            App.MVImagen.ObtenerImagenPerfilDeEmpresa(UidEmpresa.ToString());
            imgFotoPerfilEmpresa.Source = "" + Helpers.Settings.sitio + "/vista/" + App.MVImagen.STRRUTA;
            #endregion
            Guid UidColonia = new Guid(App.UidColoniaABuscar);
            Guid UidEstado = new Guid(App.UidEstadoABuscar);
            CantidadSucursales(Dia);
            #region busqueda de ListaOferta
            var registro = App.MVSucursales.LISTADESUCURSALES[0];
            txtNombreSucursal.Text = registro.IDENTIFICADOR;

            CargaOfertas();

            #endregion
        }
        protected async void CargaOfertas()
        {
            using (HttpClient _webClient = new HttpClient())
            {
                string uril = "" + Helpers.Settings.sitio + "/api/Oferta/GetBuscarOferta?UIDSUCURSAL=" + App.MVSucursales.LISTADESUCURSALES[0].ID + "&ESTATUS=1";
                string content = await _webClient.GetStringAsync(uril);
                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                App.MVOferta = JsonConvert.DeserializeObject<VMOferta>(obj);
            }
            idSucursal.Text = App.MVSucursales.LISTADESUCURSALES[0].ID.ToString();
            MypickerMenu.ItemsSource = App.MVOferta.ListaDeOfertas;
            MypickerMenu.SelectedIndex = 0;
        }
        private async void CantidadSucursales(string Dia)
        {
            using (HttpClient _webClient = new HttpClient())
            {
                string uril = "" + Helpers.Settings.sitio + "/api/Sucursales/GetBuscarSucursalesDeUnProducto?uidEmpresa=" + ObjItem.UIDEMPRESA + "&day=" + Dia + "&UidEstado=" + App.UidEstadoABuscar + "&UidColonia=" + App.UidColoniaABuscar + "";
                string content = await _webClient.GetStringAsync(uril);
                string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                App.MVSucursales = JsonConvert.DeserializeObject<VMSucursales>(obj);
            }
            idSucursal.Text = App.MVSucursales.LISTADESUCURSALES[0].ID.ToString();
            if (App.MVSucursales.LISTADESUCURSALES.Count > 1)
            {
                txtCantidadDeSucursales.Text = "Sucursales disponibles " + App.MVSucursales.LISTADESUCURSALES.Count.ToString();
            }
            else
            {
                txtCantidadDeSucursales.Text = App.MVSucursales.LISTADESUCURSALES.Count.ToString() + " sucursal disponible";
            }
        }

        private async void MypickerSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            VMSeccion ovjSeccion = MypickerSeccion.SelectedItem as VMSeccion;
            if (ovjSeccion != null)
            {
                VMProducto Busquedaproducto = new VMProducto();
                using (HttpClient _webClient = new HttpClient())
                {
                    string uril = "" + Helpers.Settings.sitio + "/api/Producto/GetObtenerProductosDeLaSeccion?UidSeccion=" + ovjSeccion.UID + "";
                    string content = await _webClient.GetStringAsync(uril);
                    string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    Busquedaproducto = JsonConvert.DeserializeObject<VMProducto>(obj);

                    MyListViewBusquedaProductos.ItemsSource = Busquedaproducto.ListaDeProductos;
                }
            }
            else
            {
                MyListViewBusquedaProductos.ItemsSource = null;
            }
        }

        private async void MypickerMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                VMOferta objMenu = MypickerMenu.SelectedItem as VMOferta;
                using (HttpClient _webClient = new HttpClient())
                {
                    string uril = "" + Helpers.Settings.sitio + "/api/Seccion/GetBuscarSeccion?UIDOFERTA=" + objMenu.UID.ToString() + "";
                    string content = await _webClient.GetStringAsync(uril);
                    string obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                    App.MVSeccion = JsonConvert.DeserializeObject<VMSeccion>(obj);
                    MypickerSeccion.ItemsSource = null;
                    MypickerSeccion.ItemsSource = App.MVSeccion.ListaDeSeccion;
                    if (App.MVSeccion.ListaDeSeccion.Count > 0)
                    {
                        MypickerSeccion.SelectedIndex = 0;
                    }
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
                var item = e;
                VMProducto ObjItem = (VMProducto)item.Item;
                VMSeccion ovjSeccion = MypickerSeccion.SelectedItem as VMSeccion;
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