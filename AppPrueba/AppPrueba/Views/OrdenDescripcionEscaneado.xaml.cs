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
    public partial class OrdenDescripcionEscaneado : ContentPage
    {
        Guid Uidorden;
        HttpClient _WebApiGoDeliverix = new HttpClient();
        ListView MyListviewOrdenesPorEnviar;
        public OrdenDescripcionEscaneado(ListView MyListviewOrdenesPorEnviar)
        {
            InitializeComponent();
            //App.MVOrden.ObtenerProductosDeOrden(.ToString());

            CargaListaDeProductos(App.MVOrden.Uidorden, MyListviewOrdenesPorEnviar);
        }

        private async void CargaListaDeProductos(Guid uidorden, ListView MyListviewOrdenesPorEnviar)
        {

            string estatus = string.Empty;

            string url = RestService.Servidor + "api/Pagos/GetObtenerEstatusDeCobro?UidOrden=" + uidorden + "";
            string DatosObtenidos = await _WebApiGoDeliverix.GetStringAsync(url);
            estatus = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();


            if (estatus == "Pendiente")
            {
                estatus = "Pago en destino";
            }

            txtNumeroOrdenScaner.Text = App.MVOrden.LNGFolio.ToString();
            txtNombreEmpresaScaner.Text = App.MVOrden.StrNombreSucursal;
            lblEstatusDePago.Text = estatus;
            Uidorden = App.MVOrden.Uidorden;

            url = RestService.Servidor + "api/Orden/GetObtenerProductosDeOrden?UidOrden=" + uidorden + "";

            DatosObtenidos = await _WebApiGoDeliverix.GetStringAsync(url);
            var DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();
            App.MVOrden = JsonConvert.DeserializeObject<VMOrden>(DatosGiros);


            MyListviewOrdenes.ItemsSource = App.MVOrden.ListaDeProductos;
            decimal total = 0.0m;
            foreach (var item in App.MVOrden.ListaDeProductos)
            {
                total = total + decimal.Parse(item.MTotalSucursal);
            }
            lblTotalOrden.Text = "$" + total;

            App.MVOrden.Uidorden = new Guid();
            this.MyListviewOrdenesPorEnviar = MyListviewOrdenesPorEnviar;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("", "¿Entregar la orden " + txtNumeroOrdenScaner.Text + "?", "Si", "No");
            if (action)
            {
                string url = RestService.Servidor + "api/Orden/GetAgregaEstatusALaOrden?UidEstatus=B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7&StrParametro=S&UidOrden=" + Uidorden + "&UidLicencia=" + AppPrueba.Helpers.Settings.Licencia + "";
                await _WebApiGoDeliverix.GetAsync(url);
                url = string.Empty;
                url = RestService.Servidor + "api/Orden/GetAgregarEstatusOrdenEnSucursal?UidEstatus=E2BAD7D9-9CD0-4698-959D-0A211800545F&cTipoDeSucursal=S&UidOrden=" + Uidorden + "&UidLicencia=" + AppPrueba.Helpers.Settings.Licencia + "";
                await _WebApiGoDeliverix.GetAsync(url);

                url = string.Empty;
                url = (RestService.Servidor + "api/Orden/GetOrdenesSucursal?Licencia=" + AppPrueba.Helpers.Settings.Licencia.ToString() + "&Estatus=Listaaenviar&tipoSucursal=s");
                string DatosObtenidos = await _WebApiGoDeliverix.GetStringAsync(url);
                var DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();
                App.MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(DatosGiros);
                await DisplayAlert("Excelente", "La orden se ha entregado al repartidor", "Ok");


                this.MyListviewOrdenesPorEnviar.ItemsSource = App.MVOrden.ListaDeOrdenes;
                await Navigation.PopToRootAsync();
            }
        }
    }
}