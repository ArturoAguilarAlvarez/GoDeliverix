using Modelo.ApiResponse;
using Modelo.ApiResponse.Google;
using Modelo.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo.Services
{
    public class GoogleService
    {
        /// <summary>
        /// Http Client to make HTTP Requests
        /// </summary>
        protected HttpClient Client { get; }

        public GoogleService()
        {

            this.Client = new HttpClient();
            Client.BaseAddress = new Uri(ApplicationConstants.GoogleDirectionApiBaseURl);
        }

        public async Task<GoogleDirection> GetDirections(MapCoordinates origin, MapCoordinates Destination)
        {
            GoogleDirection googleDirection = new GoogleDirection();

            var response = await Client.GetAsync($"api/directions/json?mode=driving&transit_routing_preference=less_driving&origin={origin.Latitude},{origin.Longitude}&destination={Destination.Latitude},{Destination.Longitude}&key={ApplicationConstants.GoogleDirectionApiKey}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    googleDirection = await Task.Run(() =>
                       JsonConvert.DeserializeObject<GoogleDirection>(json)
                    ).ConfigureAwait(false);

                }

            }

            return googleDirection;
        }
    }
}
