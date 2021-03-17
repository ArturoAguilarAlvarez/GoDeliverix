using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers.v2
{
    [Route("api/v2/Locations/{action}")]
    public class v2LocationsController : ApiController
    {
        private AddressViewModel AddressVm { get; }

        public v2LocationsController()
        {
            this.AddressVm = new AddressViewModel();
        }

        [HttpGet]
        public IHttpActionResult SearchNeighborhood(string filter)
        {
            try
            {
                var result = this.AddressVm.SearchNeighborhood(filter);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}