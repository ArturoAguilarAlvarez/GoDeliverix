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
    public partial class OrdenDescripcionConfirmar : ContentPage
    {

        VMOrden ObjItem;
        ListView MyListviewOrdenesRecibidas;
        HttpClient _client = new HttpClient();
        string _url = "";

        public OrdenDescripcionConfirmar(VMOrden ObjItem, ListView MyListviewOrdenesRecibidas)
        {
            InitializeComponent();

            MyListviewOrdenesConfirmar.ItemsSource = App.MVOrden.ListaDeProductos;
            this.ObjItem = ObjItem;
            this.MyListviewOrdenesRecibidas = MyListviewOrdenesRecibidas;

        }

        private async void ButtonCancelar_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PopToRootAsync();
            await Navigation.PushAsync(new ConfirmarCancelacionOrden(ObjItem, MyListviewOrdenesRecibidas));
        }

        private async void ButtonConfirmar_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("", "¿Confirmar esta orden?", "Si", "No");
            if (action)
            {
                ConfirmarOrden();
            }
        }

        public async void ConfirmarOrden()
        {
            //App.MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("EC09BCDE-ADAC-441D-8CC1-798BC211E46E"), "S", AppPuestoTacos.Helpers.Settings.Licencia, UidOrden: ObjItem.Uidorden);
            //App.MVOrden.AgregaEstatusALaOrden(new Guid("2d2f38b8-7757-45fb-9ca6-6ecfe20356ed"), UidOrden: ObjItem.Uidorden, UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), StrParametro: "S");
            //App.MVTarifario.AgregarCodigoAOrdenTarifario(UidCodigo: Guid.NewGuid(), UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), uidorden: ObjItem.Uidorden);

            _url = (RestService.Servidor + "api/Orden/GetConfirmarOrden?Licencia=" + AppPrueba.Helpers.Settings.Licencia + "&Uidorden=" + ObjItem.Uidorden);
            var DatosObtenidos = await _client.GetStringAsync(_url);

            //App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPuestoTacos.Helpers.Settings.Licencia), EstatusSucursal: "Pendientes a confirmar", TipoDeSucursal: "S");

            string _URL = (RestService.Servidor + "api/Orden/GetOrdenesSucursal?Licencia=" + AppPrueba.Helpers.Settings.Licencia +
            "&Estatus=Pendientes%20a%20confirmar&tipoSucursal=s");
            var DatosObtenidos3 = await _client.GetAsync(_URL);
            string res = await DatosObtenidos3.Content.ReadAsStringAsync();
            var asd = JsonConvert.DeserializeObject<ResponseHelper>(res).Data.ToString();
            App.MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(asd);

            MyListviewOrdenesRecibidas.ItemsSource = null;
            MyListviewOrdenesRecibidas.ItemsSource = App.MVOrden.ListaDeOrdenesPorConfirmar;


            await Navigation.PopToRootAsync();
        }

    }
}