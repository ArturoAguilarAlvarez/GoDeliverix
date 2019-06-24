using System;
using System.Collections.Generic;
using System.Text;

namespace AppCliente.WebApi
{
    public static class RestService
    {
        public static string Servidor = "http://godeliverix.net/";
        public static string ContentType = "application/json";
        public static string ApiKey = "";

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
