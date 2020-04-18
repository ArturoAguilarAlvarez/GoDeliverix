using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers
{
    public class LocationController : ApiController
    {
        #region Properties
        protected readonly string GoogleGeodecodeApiKey = "AIzaSyADSN_aAoCuveZvoqYs40HltYoFP9gNis8";
        private VMUbicacion vMUbicacion;
        #endregion

        public LocationController()
        {
            this.vMUbicacion = new VMUbicacion();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAddressByGeolocation(string latitude, string longitude)
        {
            try
            {
                using (var _httpClient = new System.Net.Http.HttpClient())
                {
                    _httpClient.BaseAddress = new Uri("https://maps.googleapis.com/");
                    string urlParams = $"?latlng={latitude},{longitude}&key={this.GoogleGeodecodeApiKey}";

                    var response = await _httpClient.GetAsync("maps/api/geocode/json" + urlParams);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        GoogleGeolocationResponse result = JsonConvert.DeserializeObject<GoogleGeolocationResponse>(content);

                        if (result.results.Count > 0)
                        {
                            string colonia = string.Empty;
                            string estado = string.Empty;
                            string pais = string.Empty;
                            string cp = string.Empty;

                            Ubicacion ubicacion = null;
                            foreach (var item in result.results)
                            {
                                if (item.types.Contains("route"))
                                {
                                    colonia = item.address_components.Where(a => a.types.Contains("sublocality_level_1")).FirstOrDefault().long_name;
                                    estado = item.address_components.Where(a => a.types.Contains("administrative_area_level_1")).FirstOrDefault().long_name;
                                    pais = item.address_components.Where(a => a.types.Contains("country")).FirstOrDefault().long_name;
                                    cp = item.address_components.Where(a => a.types.Contains("postal_code")).FirstOrDefault().long_name;

                                    DataTable rows = vMUbicacion.ObtenerDatosDireccion(colonia.Replace(" ", "").ToLower(), cp.Trim().ToLower());

                                    foreach (DataRow row in rows.Rows)
                                    {
                                        ubicacion = new Ubicacion()
                                        {
                                            UidColonia = (Guid)row["UidColonia"],
                                            Colonia = (string)row["Colonia"],
                                            UidCiudad = (Guid)row["UidCiudad"],
                                            Ciudad = (string)row["Ciudad"],
                                            UidEstado = (Guid)row["UidEstado"],
                                            Estado = (string)row["Estado"],
                                            UidMunicipio = (Guid)row["UidMunicipio"],
                                            Municipio = (string)row["Municipio"],
                                            UidPais = (Guid)row["UidPais"],
                                            Pais = (string)row["Pais"]
                                        };
                                    }

                                    break;
                                }
                            }

                            if (ubicacion == null)
                            {
                                return BadRequest();
                            }
                            else
                            {
                                return Ok(ubicacion);
                            }

                        }

                        return BadRequest();
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

        [HttpGet]
        public IHttpActionResult CheckAvailability()
        {
            return Ok();
        }

    }

    #region Models
    public class UserGeolocation
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class Ubicacion
    {
        public Guid UidColonia;
        public string Colonia;
        public Guid UidCiudad;
        public string Ciudad;
        public Guid UidMunicipio;
        public string Municipio;
        public Guid UidEstado;
        public string Estado;
        public Guid UidPais;
        public string Pais;
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