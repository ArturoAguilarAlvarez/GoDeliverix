using AppPrueba.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdenesRecibidas : ContentPage
    {
        bool Ordenamiento = false;
        string TipoBusqueda = "";
        HttpClient _client = new HttpClient();
        string url = "";
        public OrdenesRecibidas()
        {
            InitializeComponent();
            Cargar();
        }



        public async void MetodoConsulta()//object sender, EventArgs e
        {
            try
            {
                if (txtBusqueda.Text == "true")
                {
                    await Task.Factory.StartNew(() =>
                    {
                        App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPrueba.Helpers.Settings.Licencia), EstatusSucursal: "Pendientes a confirmar", TipoDeSucursal: "S");
                        switch (TipoBusqueda)
                        {
                            case "Fecha":
                                if (Ordenamiento)
                                {
                                    App.MVOrden.ListaDeOrdenesPorConfirmar = App.MVOrden.ListaDeOrdenesPorConfirmar.OrderBy(x => x.FechaDeOrden).ToList();
                                }
                                else
                                {
                                    App.MVOrden.ListaDeOrdenesPorConfirmar = App.MVOrden.ListaDeOrdenesPorConfirmar.OrderByDescending(x => x.FechaDeOrden).ToList();
                                }
                                break;
                            case "NumeroOrden":
                                if (Ordenamiento)
                                {
                                    App.MVOrden.ListaDeOrdenesPorConfirmar = App.MVOrden.ListaDeOrdenesPorConfirmar.OrderBy(x => x.LNGFolio).ToList();
                                }
                                else
                                {
                                    App.MVOrden.ListaDeOrdenesPorConfirmar = App.MVOrden.ListaDeOrdenesPorConfirmar.OrderByDescending(x => x.LNGFolio).ToList();
                                }
                                break;
                            case "Total":
                                if (Ordenamiento)
                                {
                                    App.MVOrden.ListaDeOrdenesPorConfirmar = App.MVOrden.ListaDeOrdenesPorConfirmar.OrderBy(x => x.MTotal).ToList();
                                }
                                else
                                {
                                    App.MVOrden.ListaDeOrdenesPorConfirmar = App.MVOrden.ListaDeOrdenesPorConfirmar.OrderByDescending(x => x.MTotal).ToList();
                                }
                                break;
                            default:
                                break;
                        }
                    });
                    MyListviewOrdenesRecibidas.ItemsSource = null;
                    MyListviewOrdenesRecibidas.ItemsSource = App.MVOrden.ListaDeOrdenesPorConfirmar;
                }
            }
            catch (Exception)
            {
            }
        }

        private async void MyListviewOrdenesRecibidas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           // await PopupNavigation.Instance.PushAsync(new AppPuestoTacos.Popup.Loanding());
            var item = ((ItemTappedEventArgs)e);
            VMOrden ObjItem = (VMOrden)item.Item;

            //VMOrden fila = (VMOrden)DGOrdenesAConfirmar.SelectedItem;
            //MVLicencia.RecuperaLicencia();
            //string sucursal = App.MVSucursal.ObtenSucursalDeLicencia(AppPuestoTacos.Helpers.Settings.Licencia);
            //txtConfirmarUidOrden.Text = fila.Uidorden.ToString();
            //txtCNumeroOrden.Text = fila.LNGFolio.ToString();

            string _URL = (RestService.Servidor + "api/Orden/GetObtenerProductosDeOrden?UidOrden=" + ObjItem.Uidorden.ToString());
            string DatosObtenidos = await _client.GetStringAsync(_URL);
            var DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();

            JArray blogPostArray = JArray.Parse(DatosGiros.ToString());

            App.MVOrden.ListaDeProductos = blogPostArray.Select(p => new VMOrden
            {
                UidProducto = (Guid)p["UidProducto"],
                StrNombreSucursal = (string)p["Identificador"],
                StrNombreProducto = (string)p["StrNombreProducto"],
                Imagen = (string)p["Imagen"],
                intCantidad = (int)p["intCantidad"],
                UidSucursal = (Guid)p["UidSucursal"],
                MTotal = (int)p["MTotal"],
                MCostoTarifario = (int)p["MCostoTarifario"],
                VisibilidadNota = (string)p["StrNota"]
            }).ToList();

            //App.MVOrden.ObtenerProductosDeOrden(ObjItem.Uidorden.ToString());

            await Navigation.PushAsync(new OrdenDescripcionConfirmar(ObjItem, MyListviewOrdenesRecibidas));
            //await PopupNavigation.Instance.PopAllAsync();
            //GridViewDetalleOrdenConfirmar.ItemsSource = MVOrden.ListaDeProductos;
        }

        private async void ImageButtonBuscar_Clicked(object sender, EventArgs e)
        {
            //await PopupNavigation.Instance.PushAsync(new AppPuestoTacos.Popup.BuscarRecibidas(MyListviewOrdenesRecibidas, txtBusqueda));
        }

        private void ButtonOrdenamientoFecha_Clicked(object sender, EventArgs e)
        {
            if (Ordenamiento)
            {
                App.MVOrden.ListaDeOrdenesPorConfirmar = App.MVOrden.ListaDeOrdenesPorConfirmar.OrderBy(x => x.FechaDeOrden).ToList();
                MyListviewOrdenesRecibidas.ItemsSource = null;
                MyListviewOrdenesRecibidas.ItemsSource = App.MVOrden.ListaDeOrdenesPorConfirmar; Ordenamiento = false;
            }
            else
            {
                App.MVOrden.ListaDeOrdenesPorConfirmar = App.MVOrden.ListaDeOrdenesPorConfirmar.OrderByDescending(x => x.FechaDeOrden).ToList();
                MyListviewOrdenesRecibidas.ItemsSource = null;
                MyListviewOrdenesRecibidas.ItemsSource = App.MVOrden.ListaDeOrdenesPorConfirmar;
                Ordenamiento = true;
            }
            TipoBusqueda = "Fecha";
        }

        private void ButtonOrdenamientoNumeroOrden_Clicked(object sender, EventArgs e)
        {
            if (Ordenamiento)
            {
                App.MVOrden.ListaDeOrdenesPorConfirmar = App.MVOrden.ListaDeOrdenesPorConfirmar.OrderBy(x => x.LNGFolio).ToList();
                MyListviewOrdenesRecibidas.ItemsSource = null;
                MyListviewOrdenesRecibidas.ItemsSource = App.MVOrden.ListaDeOrdenesPorConfirmar; Ordenamiento = false;
            }
            else
            {
                App.MVOrden.ListaDeOrdenesPorConfirmar = App.MVOrden.ListaDeOrdenesPorConfirmar.OrderByDescending(x => x.LNGFolio).ToList();
                MyListviewOrdenesRecibidas.ItemsSource = null;
                MyListviewOrdenesRecibidas.ItemsSource = App.MVOrden.ListaDeOrdenesPorConfirmar;
                Ordenamiento = true;
            }
            TipoBusqueda = "NumeroOrden";
        }

        private void ButtonOrdenamientoTotal_Clicked(object sender, EventArgs e)
        {
            if (Ordenamiento)
            {
                App.MVOrden.ListaDeOrdenesPorConfirmar = App.MVOrden.ListaDeOrdenesPorConfirmar.OrderBy(x => x.MTotal).ToList();
                MyListviewOrdenesRecibidas.ItemsSource = null;
                MyListviewOrdenesRecibidas.ItemsSource = App.MVOrden.ListaDeOrdenesPorConfirmar; Ordenamiento = false;
            }
            else
            {
                App.MVOrden.ListaDeOrdenesPorConfirmar = App.MVOrden.ListaDeOrdenesPorConfirmar.OrderByDescending(x => x.MTotal).ToList();
                MyListviewOrdenesRecibidas.ItemsSource = null;
                MyListviewOrdenesRecibidas.ItemsSource = App.MVOrden.ListaDeOrdenesPorConfirmar;
                Ordenamiento = true;
            }
            TipoBusqueda = "Total";
        }

        private async void Cargar()
        {
            string _URL = (RestService.Servidor + "api/Orden/GetOrdenesSucursal?Licencia=" + AppPrueba.Helpers.Settings.Licencia +
                "&Estatus=Pendientes%20a%20confirmar&tipoSucursal=s");
            var DatosObtenidos = await _client.GetAsync(_URL);
            string res = await DatosObtenidos.Content.ReadAsStringAsync();
            var asd = JsonConvert.DeserializeObject<ResponseHelper>(res).Data.ToString();
            App.MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(asd);

            //App.MVOrden.BuscarOrdenesAppSucursal("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendientes a confirmar", TipoDeSucursal: "S");
            MyListviewOrdenesRecibidas.ItemsSource = App.MVOrden.ListaDeOrdenesPorConfirmar;
        }
    }
}