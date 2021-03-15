using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers.v2
{
    [Route("api/v2/Addresses/{action}")]
    public class v2AddressesController : ApiController
    {
        private AddressViewModel AddressVm { get; }

        public v2AddressesController()
        {
            this.AddressVm = new AddressViewModel();
        }

        [HttpGet]
        public IHttpActionResult GetAllByUser(Guid uid)
        {
            try
            {
                var result = this.AddressVm.GetAllByUserId(uid);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}