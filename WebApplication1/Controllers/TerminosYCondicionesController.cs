using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VistaDelModelo;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class TerminosYCondicionesController : ApiController
    {
        public VMTerminosYCondiciones oTYC { get; set; }

        public TerminosYCondicionesController()
        {
            oTYC = new VMTerminosYCondiciones();
        }
        [HttpGet]
        public IHttpActionResult CheckUserTermsAndConditions(string UidUser, string Languaje)
        {
            try
            {
                this.oTYC.CheckUserTermsAndConditions(UidUser, Languaje);
                var result = new { Uid = oTYC.Uid, url = oTYC.Url };
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult UserTermsAndConditionsAccepted(string uidUser, string uidTermsAndConditions)
        {
            try
            {
                this.oTYC.TerminosYCondicionesAceptadas(uidUser, uidTermsAndConditions);
                var result = new { Uid = oTYC.Uid, url = oTYC.Url };
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult SearchTermsAndConditions(string Languaje, string UidLada = "", string UidUsuario = "")
        {
            try
            {
                this.oTYC.BuscarTerminosYCondiciones(Languaje, UidLada, UidUsuario);
                var result = new { Uid = oTYC.Uid, url = oTYC.Url };
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
