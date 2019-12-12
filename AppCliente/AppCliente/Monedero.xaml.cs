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
using AppCliente.ViewModel;
namespace AppCliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Monedero : ContentPage
    {
        VMMonedero MVMonedero;
        public Monedero()
        {
            InitializeComponent();
            cargaMonedero();
        }

        private async void cargaMonedero()
        {
            string _Url = $"https://godeliverix.net/api/Monedero/Get?" +
                                $"id={App.Global1}";
            var content = "";
            using (HttpClient _client = new HttpClient())
            {
                content = await _client.GetStringAsync(_Url);
            }
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            MVMonedero = JsonConvert.DeserializeObject<VMMonedero>(obj);
            lblDineroMonedero.Text = "Saldo actual $" + MVMonedero.MMonto.ToString("N2") + "";

            string _datosMovimientos = $"https://godeliverix.net/api/Monedero/GetObtenerMovimientos?" +
                                $"id={App.Global1}"; 

            using (HttpClient _client = new HttpClient())
            {
                content = await _client.GetStringAsync(_datosMovimientos);
            }
            obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
            VMMonedero VMovimientos = JsonConvert.DeserializeObject<VMMonedero>(obj);
            MVMovimientos oMovimientos = new MVMovimientos();
            oMovimientos.ListaDeMovimientos = new List<MVMovimientos>();
            foreach (var item in VMovimientos.ListaDeMoviento)
            {
                oMovimientos.ListaDeMovimientos.Add(new MVMovimientos() 
                {
                    LngFolio = item.LngFolio,
                    StrConcepto = item.StrConcepto,
                    StrMovimiento = item.StrMovimiento,
                    CColor = Color.FromHex(item.strcolor),
                    DtmFechaDeRegistro = item.DtmFechaDeRegistro,
                    MMonto = item.MMonto
                });
            }

            lvMovimientos.ItemsSource = oMovimientos.ListaDeMovimientos;
        }
    }
}