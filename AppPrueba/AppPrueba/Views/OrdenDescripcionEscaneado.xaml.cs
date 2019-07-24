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
            txtNumeroOrdenScaner.Text = App.MVOrden.LNGFolio.ToString();
            txtNombreEmpresaScaner.Text = App.MVOrden.StrNombreSucursal;
            Uidorden = App.MVOrden.Uidorden;

            string url = "http://www.godeliverix.net/api/Orden/GetObtenerProductosDeOrden?UidOrden=" + uidorden + "";

            string DatosObtenidos = await _WebApiGoDeliverix.GetStringAsync(url);
            var DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();
            App.MVOrden = JsonConvert.DeserializeObject<VMOrden>(DatosGiros);
            
           
            MyListviewOrdenes.ItemsSource = App.MVOrden.ListaDeProductos;
           
            App.MVOrden.Uidorden = new Guid();
            //this.MyListviewOrdenesPorEnviar = MyListviewOrdenesPorEnviar;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("", "¿Entregar la orden " + txtNumeroOrdenScaner.Text + "?", "Si", "No");
            if (action)
            {
                string url = "http://www.godeliverix.net/api/Orden/GetAgregaEstatusALaOrden?UidEstatus=2FDEE8E7-0D54-4616-B4C1-037F5A37409D&StrParametro=S&UidOrden=" + Uidorden + "&UidLicencia=" + AppPrueba.Helpers.Settings.Licencia + "";
                await _WebApiGoDeliverix.GetAsync(url);
                url = string.Empty;
                url = "http://www.godeliverix.net/api/Orden/GetAgregarEstatusOrdenEnSucursal?UidEstatus=2FDEE8E7-0D54-4616-B4C1-037F5A37409D&cTipoDeSucursal=S&UidOrden=" + Uidorden + "&UidLicencia=" + AppPrueba.Helpers.Settings.Licencia + "";
                await _WebApiGoDeliverix.GetAsync(url);

                //App.MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("0E08DE81-DED2-41BE-93A5-A3742C3C411F"), "S", AppPrueba.Helpers.Settings.Licencia, UidOrden: Uidorden);

                //App.MVOrden.AgregaEstatusALaOrden(new Guid("B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7"), UidOrden: Uidorden, UidLicencia: new Guid(AppPrueba.Helpers.Settings.Licencia), StrParametro: "S");

                //App.MVOrden.BuscarOrdenes("Sucursal", UidLicencia: new Guid(AppPrueba.Helpers.Settings.Licencia), EstatusSucursal: "Lista a enviar", TipoDeSucursal: "S");
                url = string.Empty;
                url = ("http://www.godeliverix.net/api/Orden/GetOrdenesSucursal?Licencia=" + AppPrueba.Helpers.Settings.Licencia.ToString() + "&Estatus=Lista%20a%20enviar&tipoSucursal=s");
                string DatosObtenidos = await _WebApiGoDeliverix.GetStringAsync(url);
                var DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();
                App.MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(DatosGiros);

                MyListviewOrdenesPorEnviar.ItemsSource = App.MVOrden.ListaDeOrdenes;
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }
    }
}