using AppPrueba.WebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPrueba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdenesCanceladasPermanente : ContentPage
    {
        //bool Ordenamiento = false;
        //string escaneado;
        HttpClient _client = new HttpClient();
        string url = "";
        public OrdenesCanceladasPermanente()
        {
            InitializeComponent();
            Cargar();
        }

        public async void Cargar()
        {
            url = (RestService.Servidor + "api/Orden/GetOrdenesSucursal?Licencia=" + AppPrueba.Helpers.Settings.Licencia.ToString() + "&Estatus=Canceladas&tipoSucursal=s");
            string DatosObtenidos = await _client.GetStringAsync(url);
            var DatosGiros = JsonConvert.DeserializeObject<ResponseHelper>(DatosObtenidos).Data.ToString();
            App.MVOrden = JsonConvert.DeserializeObject<VistaDelModelo.VMOrden>(DatosGiros);
            MyListviewOrdenesCanceladas.ItemsSource = App.MVOrden.ListaDeOrdenesCanceladas;
        }
    }
}