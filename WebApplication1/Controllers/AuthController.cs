using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers
{
    public class AuthController : ApiController
    {
        private AuthenticationViewModel AuthenticationVm { get; }

        public AuthController()
        {
            this.AuthenticationVm = new AuthenticationViewModel();
        }

        /// <summary>
        /// Metodo para autenticación de los repartidores
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Login_DeliveryMan([FromBody] DeliveryManLogin login)
        {
            try
            {
                var result = this.AuthenticationVm.AuthenticateDeliveryMan(login.Username, login.Password, login.ProfileUid);

                if (result == null)
                {
                    return BadRequest("User does not exist");
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class DeliveryManLogin
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public Guid ProfileUid { get; set; }
    }
}