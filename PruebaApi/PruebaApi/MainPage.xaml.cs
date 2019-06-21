using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PruebaApi.Modelo;
using PruebaApi.WebApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;  
using System.Text;
using System.Threading.Tasks;
using VistaDelModelo;
using Xamarin.Forms;

namespace PruebaApi
{
    public partial class MainPage : ContentPage
    {
        public List<VMGiro> LISTADEGIRO = new List<VMGiro>();

        private string _myMessage;

        public string MyMessage
        {
            get { return _myMessage; }
            set { _myMessage = value; }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            UpdateProductAsync();
        }

        public void GetGiros()
        {
            var cliente = HttpWebRequest.Create(RestService.Servidor + RestService.Methods.Giro + RestService.HTTPMethods.Get) as HttpWebRequest;
            cliente.Method = RestService.HTTPMethods.Get;
            //si agregamos api
            //request.Headers.Add("ApiKey", RestService.ApiKey);
            cliente.ContentType = RestService.ContentType;
            try
            {
                HttpWebResponse response = cliente.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string resp;
                resp = reader.ReadToEnd();
     
                var obj =  JsonConvert.DeserializeObject<ResponseHelper>(resp).Data.ToString();
                JArray blogPostArray = JArray.Parse(obj);
                IList<VMGiro> blogPosts = blogPostArray.Select(p => new VMGiro
                {
                    STRDESCRIPCION=(string)p["STRDESCRIPCION"],
                    RUTAIMAGEN = (string)p["RUTAIMAGEN"],
                    UIDVM = (Guid)p["UIDVM"],
                    STRNOMBRE = (string)p["STRNOMBRE"],
                    INTESTATUS = (int)p["INTESTATUS"]
                }).ToList();

            }
            catch (Exception)
            {
                txtIDUsuario.Text = "Error de conexion";
            }
        }

        public void Login()
        {
        }

        public async void UpdateProductAsync()
        {
            HttpClient _client= new HttpClient();
            string url = "http://godeliverix.net/api/Profile/GET?Usuario=xxx&Contrasena=xxx";
            string content= await _client.GetStringAsync(url);
            List<string> listaID = JsonConvert.DeserializeObject<List<string>>(content);


            //var tex= _client.GetStreamAsync(RestService.Servidor + RestService.Methods.Giro + RestService.HTTPMethods.Get);
            var tex=("http://godeliverix.net/api/Giro/get");
            string s = await _client.GetStringAsync(tex);
            var obj = JsonConvert.DeserializeObject<ResponseHelper>(s);
            //var cliente = HttpWebRequest.Create(RestService.Servidor + RestService.Methods.Profile + RestService.HTTPMethods.Get+ "?Usuario=xxx&Contrasena=xxx") as HttpWebRequest;
            //cliente.Method = RestService.HTTPMethods.Get;
            ////si agregamos api
            ////request.Headers.Add("ApiKey", RestService.ApiKey);
            //cliente.ContentType = RestService.ContentType;
            //HttpWebResponse response = cliente.GetResponse() as HttpWebResponse;
            //StreamReader reader = new StreamReader(response.GetResponseStream());
            //string resp;
            //resp = reader.ReadToEnd();

            //var result = JsonConvert.DeserializeObject<string>( resp);

            // Deserialize the updated product from the response body.
        }
    }
}
