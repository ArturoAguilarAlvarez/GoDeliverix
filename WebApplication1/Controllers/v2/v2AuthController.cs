using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers.v2
{
    [Route("api/v2/StoreAuth/{action}")]
    public class v2AuthController : ApiController
    {
        private AuthenticationViewModel AuthenticationVm { get; }

        public v2AuthController()
        {
            this.AuthenticationVm = new AuthenticationViewModel();
        }

        [HttpPost]
        public IHttpActionResult LoginStore([FromBody] StoreLoginRequest request)
        {
            try
            {
                if (request.Email.Trim().Length == 0 && request.Username.Trim().Length == 0)
                {
                    return BadRequest();
                }

                var login = this.AuthenticationVm.LoginStoreDapper(request.Password, request.Username, request.Email);

                if (login == null)
                    return BadRequest();

                return Json(login);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class StoreLoginRequest
    {
        public string Password { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
    }
}