using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VistaDelModelo;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebApplication1.Controllers
{
    public class NotificationsController : ApiController
    {
        private NotificationViewModel viewModel = new NotificationViewModel();

        public NotificationsController()
        {

        }

        [HttpGet]
        public IHttpActionResult ReadAllCustomer(string uid)
        {
            try
            {
                var result = this.viewModel.ReadAllNotifications(Modelo.Enums.NotificationTarget.Cliente, Guid.Parse(uid));
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}