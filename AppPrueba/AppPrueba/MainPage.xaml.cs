using Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace AppPrueba
{
    public partial class MainPage : ContentPage
    {

        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://godeliverix.net") };

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            UpdateProductAsync();
        }

        public async void UpdateProductAsync()
        {

            HttpResponseMessage response = await client.GetAsync
                ("{api/Profile/Get?Usuario=" + "xxx" + "&Contrasena=" + "xxx");
            response.EnsureSuccessStatusCode();
            //txtIDUsuario.Text = await response.Content.ReadAsStringAsync();}
            //var objProducto = await response.Content.ReadAsAsync<Product>();

            //var response = await client.PostAsync("Token",
            //  new StringContent(
            //  "{ \"Usuario\":\"" + "xxx" + "\", \"Contrasena\":\"" + "xxx" + "\"}",
            //  Encoding.UTF8, "application/json"));
            //var resultJSON = await response.Content.ReadAsStringAsync();
            //var result = JsonConvert.DeserializeObject<string>(
            //  resultJSON);
            //return result;

        }

    }
}
