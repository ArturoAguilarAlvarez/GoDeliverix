using Modelo.ApiResponse.Firebase;
using Modelo.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo.Services
{
    public class FirebaseService
    {
        /// <summary>
        /// Http Client to make HTTP Requests
        /// </summary>
        protected HttpClient Client { get; }

        public FirebaseService()
        {
            this.Client = new HttpClient();
            Client.BaseAddress = new Uri(ApplicationConstants.FirebaseBaseUrl);
        }

        /// <summary>
        /// Leer todos los registros del seguimiento de la ubicacion del repartidor
        /// </summary>
        /// <param name="orderUid">Primary Key - Orden</param>
        /// <returns></returns>
        public async Task<IEnumerable<DeliveryDealerRouteLocation>> ReadAllDealerRouteLocations(Guid orderUid)
        {
            try
            {
                var response = await Client.GetAsync($"delivery_routes/{orderUid.GuidToString()}.json?auth={ApplicationConstants.FirebaseSecretKey}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    Stream stream = await response.Content.ReadAsStreamAsync();
                    StreamReader streamReader = new StreamReader(stream);
                    string contentResponse = streamReader.ReadToEnd();

                    if (string.IsNullOrEmpty(contentResponse))
                    {
                        return new HashSet<DeliveryDealerRouteLocation>();
                    }

                    var keyValues = JsonConvert.DeserializeObject<Dictionary<string, DeliveryDealerRouteLocation>>(contentResponse);

                    return keyValues.Select(k => k.Value);
                }

                return new HashSet<DeliveryDealerRouteLocation>();
            }
            catch (Exception ex)
            {
                return new HashSet<DeliveryDealerRouteLocation>();
            }
        }

        public async Task<FirebaseAddResponse> RegisterDealerRouteLocation(Guid orderUid, DeliveryDealerRouteLocation location)
        {

            try
            {
                string content = JsonConvert.SerializeObject(location);
                HttpContent stringContent = new StringContent(content,
                           UnicodeEncoding.UTF8,
                           "application/json");

                var response = await Client.PostAsync($"delivery_routes/{orderUid.GuidToString()}.json?auth={ApplicationConstants.FirebaseSecretKey}", stringContent).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    Stream stream = await response.Content.ReadAsStreamAsync();
                    StreamReader streamReader = new StreamReader(stream);
                    string contentResponse = streamReader.ReadToEnd();

                    if (string.IsNullOrEmpty(contentResponse))
                    {
                        return null;
                    }

                    return JsonConvert.DeserializeObject<FirebaseAddResponse>(contentResponse);
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public static class GuidExtension
    {
        public static string GuidToString(this Guid uid)
        {
            return uid.ToString().Replace("-", "").ToLower();
        }
    }
}
