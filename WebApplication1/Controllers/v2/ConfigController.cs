using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers.v2
{
    [Route("api/v2/Config/{action}")]
    public class ConfigController : ApiController
    {
        private ConfigViewModel ConfigVm { get; }

        public ConfigController()
        {
            this.ConfigVm = new ConfigViewModel();
        }

        [HttpGet]
        public IHttpActionResult GetConfigByNames(string names)
        {
            try
            {
                var result = this.ConfigVm.GetConfig(names);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}