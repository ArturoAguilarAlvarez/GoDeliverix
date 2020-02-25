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
            foreach (VMOrden item in App.MVOrden.ListaDeProductos)
            {
                if (item.VisibilidadNota == "Visible")
                {
                    item.VisibilidadNota = "True";
                }
                else
                {
                    item.VisibilidadNota = "False";
                }
            }
            MyListviewOrdenesConfirmar.ItemsSource = App.MVOrden.ListaDeProductos;

            this.ObjItem = ObjItem;
            this.MyListviewOrdenesRecibidas = MyListviewOrdenesRecibidas;

        }

        private async void ButtonCancelar_Clicked(object sender, EventArgs e)
        {
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

            _url = (RestService.Servidor + "api/Orden/GetConfirmarOrden?Licencia=" + AppPrueba.Helpers.Settings.Licencia + "&Uidorden=" + ObjItem.Uidorden);
            var DatosObtenidos = await _client.GetStringAsync(_url);
            string _URL = (RestService.Servidor + "api/Orden/GetOrdenesSucursal?Licencia=" + AppPrueba.Helpers.Settings.Licencia +
            "&Estatus=Pendientesaconfirmar&tipoSucursal=s");
            var DatosObtenidos3 = await _client.GetAsync(_URL);
            string res = await DatosObtenidos3.Content.ReadAsStringAsync();
            var asd = JsonConvert.DeserializeObject<ResponseHelper>(res).Data.ToString();
            App.MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(asd);

            MyListviewOrdenesRecibidas.ItemsSource = null;
            MyListviewOrdenesRecibidas.ItemsSource = App.MVOrden.ListaDeOrdenes;

            await Navigation.PopToRootAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var item = sender as Button;
            var obj = item.BindingContext as VMOrden;
            await Navigation.PushAsync(new Notas(obj.UidProductoEnOrden));
        }
    }
}