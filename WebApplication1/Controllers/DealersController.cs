using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers
{
    public class DealersController : ApiController
    {
        private DealersViewModel DealerVm { get; }

        public DealersController()
        {
            this.DealerVm = new DealersViewModel();
        }

        [HttpGet]
        public IHttpActionResult ReadAllPhones(Guid uidUser)
        {
            try
            {
                var result = this.DealerVm.ReadAllPhones(uidUser);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}