using AppCliente.WebApi;
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

namespace AppCliente.Pagos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagoMonedero : ContentPage
    {
        VMMonedero MVMonedero;
        public PagoMonedero()
        {
            InitializeComponent();
            cargaMonedero();
            lvlCantidadAPagar.Text = "";
        }

        private async void cargaMonedero()
        {
            string _Url = $"" + Helpers.Settings.sitio + "/api/Monedero/Get?" +
                                $"id={App.Global1}";
            var content = "";
            using (HttpClient _client = new HttpClient())
            {
                content = await _client.GetStringAsync(_Url);
            }
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            MVMonedero = JsonConvert.DeserializeObject<VMMonedero>(obj);
            lblDineroMonedero.Text = "Cuentas con " + MVMonedero.MMonto.ToString("N2") + " OsbelCoins";
        }
    }
}