using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AppCliente.WebApi
{
    public static class RestService
    {
        public static string Servidor = "http://godeliverix.net/";
        public static string ContentType = "application/json";
        public static string ApiKey = "";

        //public static async System.Threading.Tasks.Task<ResponseHelper> PeticionAsync(string _Url,ResponseHelper Datos)
        //{
        //    //HttpClient _client =new HttpClient();
        //    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    //_client.DefaultRequestHeaders.Add("Accept", ContentType);
        //    //_client.DefaultRequestHeaders.Add("Content-Type", ContentType);
        //    //var DatosObtenidos = await _client.GetAsync(_Url);
        //    //string Res = await DatosObtenidos.Content.ReadAsStringAsync();
        //    //var Data = JsonConvert.DeserializeObject<ResponseHelper>(Res);
        //    //return Data;
        //}

        public static class HTTPMethods
        {
            public static string Get = "GET";
            public static string Post = "POST";
            public static string Post_Moodify = "PUT";
            public static string Patch_Modify = "PATCH";
            public static string Delete = "DELETE";

        }

        public static class Methods
        {
            public static string Giro = "api/Giro/";
            public static string Profile = "api/Profile/";
        }
    }
}
