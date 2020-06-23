using AppPrueba.WebApi;
using Newtonsoft.Json;
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
    public partial class OrdenesEnPreparacion : ContentPage
    {
        HttpClient _client = new HttpClient();
        string url = "";
        bool Ordenamiento = false;

        public OrdenesEnPreparacion()
        {
            try
            {
                InitializeComponent();
                Cargar();

            }
            catch (Exception)
            {
                DisplayAlert("Aviso", "Sin acceso a internet", "Ok");
            }
        }

        public async void Cargar()
        {
            url = (RestService.Servidor + "api/Orden/GetOrdenesSucursal?Licencia=" + AppPrueba.Helpers.Settings.Licencia.ToString() + "&Estatus=Pendienteparaelaborar&tipoSucursal=s");
            string DatosObtenidos = await _client.GetStringAsync(url);
            var DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();
            App.MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(DatosGiros);
            //App.MVOrden.BuscarOrdenesAppSucursal("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendiente para elaborar", TipoDeSucursal: "S");
            MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenes;
        }

        private async void ButtonFinalizarOrdenList_Clicked(object sender, EventArgs e)
        {
            var item = sender as Button;
            var ObjItem = item.BindingContext as VMOrden;
            var action = await DisplayAlert("Finalizar?", "¿A concluido el proceso de elaboracion de la orden " + ObjItem.LNGFolio + "?", "Si", "No");
            if (action)
            {
                url = (RestService.Servidor + "api/Orden/GetFinalizarOrden?Licencia="
                    + AppPrueba.Helpers.Settings.Licencia.ToString() +
                    "&Uidorden=" + ObjItem.Uidorden);

                var DatosObtenidos = await _client.GetAsync(url);

                //Crea el codigo para que el repartidor pueda recoger la orden                
                //url = (RestService.Servidor + "api/Orden/GetOrdenesSucursal?Licencia=" + AppPrueba.Helpers.Settings.Licencia.ToString() + "&Estatus=Pendiente%20para%20elaborar&tipoSucursal=s");
                //string DatosObtenidos2 = await _client.GetStringAsync(url);
                //var DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos2).Data.ToString();
                //App.MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(DatosGiros);

                //MyListviewOrdenesPorRealizar.ItemsSource = null;
                //MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenesPorElaborar;
                Cargar();
            }
        }

        private async void MyListviewOrdenesPorRealizar_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // desc await PopupNavigation.Instance.PushAsync(new AppPrueba.Popup.Loanding());
            var item = ((ItemTappedEventArgs)e);
            VMOrden ObjItem = (VMOrden)item.Item;
            await Navigation.PushAsync(new OrdenDescripcionPorElaborar(ObjItem, MyListviewOrdenesPorRealizar));
            //  descomentar await PopupNavigation.Instance.PopAllAsync();
        }

        private void ButtonBusquedaFecha_Clicked(object sender, EventArgs e)
        {
            if (Ordenamiento)
            {
                App.MVOrden.ListaDeOrdenesPorElaborar = App.MVOrden.ListaDeOrdenesPorElaborar.OrderBy(x => x.FechaDeOrden).ToList();
                MyListviewOrdenesPorRealizar.ItemsSource = null;
                MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenesPorElaborar;
                Ordenamiento = false;
            }
            else
            {
                App.MVOrden.ListaDeOrdenesPorElaborar = App.MVOrden.ListaDeOrdenesPorElaborar.OrderByDescending(x => x.FechaDeOrden).ToList();
                MyListviewOrdenesPorRealizar.ItemsSource = null;
                MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenesPorElaborar;
                Ordenamiento = true;
            }
        }

        private void ButtonBusquedaOrdenes_Clicked(object sender, EventArgs e)
        {
            if (Ordenamiento)
            {
                App.MVOrden.ListaDeOrdenesPorElaborar = App.MVOrden.ListaDeOrdenesPorElaborar.OrderBy(x => x.LNGFolio).ToList();
                MyListviewOrdenesPorRealizar.ItemsSource = null;
                MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenesPorElaborar;
                Ordenamiento = false;
            }
            else
            {
                App.MVOrden.ListaDeOrdenesPorElaborar = App.MVOrden.ListaDeOrdenesPorElaborar.OrderByDescending(x => x.LNGFolio).ToList();
                MyListviewOrdenesPorRealizar.ItemsSource = null;
                MyListviewOrdenesPorRealizar.ItemsSource = App.MVOrden.ListaDeOrdenesPorElaborar;
                Ordenamiento = true;
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {

        }

        private void ImageButtonBuscarIdOrden_Clicked(object sender, EventArgs e)
        {
            //await PopupNavigation.Instance.PushAsync(new AppPuestoTacos.Popup.BuscarRecibidas(MyListviewOrdenesPorRealizar, txtBusqueda));
        }
    }
}