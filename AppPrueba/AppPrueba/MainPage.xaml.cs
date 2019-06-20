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
        //http://localhost:63509/api/Profile/Get?Usuario=xxx&Contrasena=xxxasdasd
        //static HttpClient client = new HttpClient();
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://godeliverix.net") };

        //var httpResponse = await client.GetAsync("api/users/" + "jesulink2514");
        string product;
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
                ("http://localhost:63509/api/Profile/Get?Usuario=xxx&Contrasena=xxxasdasd");

            var httpResponse = await client.GetAsync("api/Profile/" + "jesulink2514");

            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            string product = await response.Content.ReadAsStringAsync();
        }

    }
}
