using Modelo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers.v2
{
    [Route("api/v2/Translations/{action}")]
    public class TranslationsController : ApiController
    {
        private TranslationsViewModel TranslationsVm { get; }

        public TranslationsController()
        {
            this.TranslationsVm = new TranslationsViewModel();
        }

        [HttpGet]
        public IHttpActionResult GetTranslations(EntityType? type = null, Guid? uid = null)
        {
            try
            {
                var result = this.TranslationsVm.ReadAllTranslations(type, uid);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}