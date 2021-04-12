using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers.v2
{
    [Route("api/v2/Codes/{action}")]
    public class v2CodesController : ApiController
    {
        private CodesViewModel VmCodes { get; }

        public v2CodesController()
        {
            this.VmCodes = new CodesViewModel();
        }

        [HttpGet]
        public IHttpActionResult VerifySignInCode(string code)
        {
            CodeResult result = new CodeResult();
            try
            {
                result.Code = VmCodes.VerifySignInCode(code);
                return Json(result);
            }
            catch (Exception ex)
            {
                result.Code = -5;
                return Json(result);
            }
        }

        [HttpGet]
        public IHttpActionResult GetByUser(Guid uid)
        {
            try
            {
                var result = this.VmCodes.GetSignInByUserUid(uid);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllParentChildRedeems(Guid UidCode)
        {
            try
            {
                var result = this.VmCodes.GetAllParentChildRedeems(UidCode);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetCodeConfig()
        {
            try
            {
                var result = this.VmCodes.GetSignInCodesConfig();
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class CodeResult
    {
        public int Code { get; set; }
    }
}