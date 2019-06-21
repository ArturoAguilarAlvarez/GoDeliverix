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
using AppPrueba.WebApi;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AppPrueba.Modelo;


namespace AppPrueba
{
    public partial class MainPage : ContentPage
    {

        public List<VMGiro> LISTADEGIRO = new List<VMGiro>();

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            GetGiros();
        }

        public void GetGiros()
        {
            var cliente = HttpWebRequest.Create(RestService.Servidor + RestService.Methods.Giro + RestService.HTTPMethods.Get) as HttpWebRequest;
            cliente.Method = RestService.HTTPMethods.Get;
            //si agregamos api
            //request.Headers.Add("ApiKey", RestService.ApiKey);
            cliente.ContentType = RestService.ContentType;
            HttpWebResponse response = cliente.GetResponse() as HttpWebResponse;

            //StreamReader reader = new StreamReader(response.GetResponseStream());
            //string resp;
            //resp = reader.ReadToEnd();
            //var obj = JsonConvert.DeserializeObject<ResponseHelper>(resp);
            //string data = (string)obj;
            //JObject json2 = JObject.Parse(data);
            //var respuesta_data = json2;
            //string Response = respuesta_data.GetValue("Response").ToString();
        
            //LISTADEGIRO = (data as IEnumerable<VMGiro>).ToList();
            //JObject json2 = JObject.Parse(data);
            //var respuesta_data = json2;
            //string Response = respuesta_data.GetValue("Response").ToString();
        }
        //public async void UpdateProductAsync()
        //{
        //    HttpResponseMessage response = await client.GetAsync
        //        ("http://localhost:63509/api/Profile/Get?Usuario=xxx&Contrasena=xxxasdasd");

        //    var httpResponse = await client.GetAsync("api/Profile/" + "jesulink2514");

            //var response = await client.PostAsync("Token",
            //  new StringContent(
            //  "{ \"Usuario\":\"" + "xxx" + "\", \"Contrasena\":\"" + "xxx" + "\"}",
            //  Encoding.UTF8, "application/json"));
            //var resultJSON = await response.Content.ReadAsStringAsync();
            //var result = JsonConvert.DeserializeObject<string>(
            //  resultJSON);
            //return result;

        //    // Deserialize the updated product from the response body.
        //    string product = await response.Content.ReadAsStringAsync();
        //}

    }
}
