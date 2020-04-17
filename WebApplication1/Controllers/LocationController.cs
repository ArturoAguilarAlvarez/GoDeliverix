using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class LocationController : ApiController
    {
        #region Properties
        protected readonly string GoogleGeodecodeApiKey = "AIzaSyADSN_aAoCuveZvoqYs40HltYoFP9gNis8";
        #endregion

        public LocationController()
        {

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAddressByGeolocation([FromUri]  UserGeolocation geolocation)
        {
            try
            {
                using (var _httpClient = new System.Net.Http.HttpClient())
                {
                    _httpClient.BaseAddress = new Uri("https://maps.googleapis.com/");
                    string urlParams = $"?latlng={geolocation.Latitude},{geolocation.Longitude}&key={this.GoogleGeodecodeApiKey}";

                    var response = await _httpClient.GetAsync("maps/api/geocode/json" + urlParams);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        GoogleGeolocationResponse result = JsonConvert.DeserializeObject<GoogleGeolocationResponse>(content);
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(response.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    #region Models
    public class UserGeolocation
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }

    public class GoogleGeolocationResponse
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
    #endregion
}