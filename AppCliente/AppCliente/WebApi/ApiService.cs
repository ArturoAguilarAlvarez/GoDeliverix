using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Connectivity;
namespace AppCliente.WebApi
{
    public class ApiService
    {

        string sitio = Helpers.Settings.sitio;
        public enum ResponseType
        {
            Object,
            Objects,
            List
        }

        public ApiService(string controller)
        {
            sitio += controller + "/";
        }

        public async Task<ResponseHelper> POST<T>(string action, ResponseType responseType, Dictionary<string, string> parameters = null)
        {
            try
            {
                var client = new HttpClient();

                client.BaseAddress = new Uri(sitio);

                string content = "";

                if (parameters != null)
                {
                    foreach (var item in parameters)
                    {
                        if (content == string.Empty)
                        {
                            content = "\"" + item.Key + "\":\"" + item.Value + "\"";
                        }
                        else
                        {
                            content += ",\"" + item.Key + "\":\"" + item.Value + "\"";
                        }
                    }
                }

                var response = await client.PostAsync(action,
                    new StringContent(
                    "{" + content + "}",
                    Encoding.UTF8, "application/json"));

                var jsonResult = await response.Content.ReadAsStringAsync();

                JObject jObject = JObject.Parse(jsonResult);
                string jResult = jObject.ToString();

                if (responseType == ResponseType.Object)
                {
                    var objectResult = JsonConvert.DeserializeObject<ResponseHelper>(jResult);
                    var resultObject = new object();
                    if (objectResult.Status != true)
                    {
                        resultObject = JsonConvert.DeserializeObject<T>(objectResult.Data.ToString());
                    }


                    if (objectResult.Status != false)
                    {
                        return new ResponseHelper
                        {
                            Status = objectResult.Status,
                            Message = objectResult.Message,
                            Data = null
                        };
                    }
                    else
                    {
                        return new ResponseHelper()
                        {
                            Status = objectResult.Status,
                            Message = objectResult.Message,
                            Data = resultObject
                        };
                    }


                }
                else if (responseType == ResponseType.Object)
                {
                    return new ResponseHelper()
                    {
                        Status = false,
                        Message = ""
                    };
                }
                else
                {
                    return new ResponseHelper()
                    {
                        Status = false,
                        Message = ""
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseHelper()
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }

      
        public async Task<ResponseHelper> GET<T>(string action, ResponseType responseType, Dictionary<string, string> arguments = null)
        {

            try
            {
                var client = new HttpClient();
                string urlArguments = string.Empty;
                if (arguments != null)
                {
                    foreach (var arg in arguments)
                    {
                        if (urlArguments == string.Empty)
                            urlArguments = arg.Value == string.Empty ? string.Empty : $"?{arg.Key}={arg.Value}";
                        else
                            urlArguments += arg.Value == string.Empty ? string.Empty : $"&{arg.Key}={arg.Value}";
                    }
                }
                string consulta = sitio + action + urlArguments;
                HttpResponseMessage response = new HttpResponseMessage();
                var conectivilidad = Connectivity.NetworkAccess;
                if (conectivilidad == NetworkAccess.Internet)
                {
                    
                        await Task.Run(async () => { response = await client.GetAsync(consulta); });
                   
                }
                else if (conectivilidad == NetworkAccess.None)
                {
                    GenerateMessage("Sin conexión", "No hay acceso a internet, revise su conexión.", "Aceptar");
                }

                Console.WriteLine(consulta);
                var jsonResult = await response.Content.ReadAsStringAsync();

                JObject jObject = JObject.Parse(jsonResult);

                string jResult = jObject.ToString();


                ResponseHelper result = new ResponseHelper()
                {
                    Status = false
                };

                if (responseType == ResponseType.Object)
                {
                    var objectResult = JsonConvert.DeserializeObject<ResponseHelper>(jResult);

                    result.Status = objectResult.Status;
                    result.Message = objectResult.Message;

                    if (objectResult.Status)
                        result.Data = JsonConvert.DeserializeObject<T>(objectResult.Data.ToString());
                }
                else
                {
                    var objectResult = JsonConvert.DeserializeObject<ResponseHelper>(jResult);

                    result.Status = objectResult.Status;
                    result.Message = objectResult.Message;

                    if (objectResult.Status)
                        result.Data = JsonConvert.DeserializeObject<List<T>>(objectResult.Data.ToString());
                }

                return result;



            }
            catch (Exception ex)
            {
                return new ResponseHelper()
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }
        protected async static void GenerateMessage(string Tittle, string Message, string TextOption)
        {
            await Application.Current.MainPage.DisplayAlert(
              Tittle,
              Message,
              TextOption);
        }
    }
}
