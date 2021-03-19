using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers.v2
{
    [Route("api/v2/Profiles/{action}")]
    public class v2ProfilesController : ApiController
    {
        private UserViewModel UserVm { get; }

        public v2ProfilesController()
        {
            this.UserVm = new UserViewModel();
        }

        [HttpGet]
        public IHttpActionResult Get(Guid uid)
        {
            try
            {
                var result = this.UserVm.GetAllByUserId(uid);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}