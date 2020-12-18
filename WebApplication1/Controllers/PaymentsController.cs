using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers
{
    public class PaymentsController : ApiController
    {
        private PaymentViewModel PaymentVm { get; }

        public PaymentsController()
        {
            this.PaymentVm = new PaymentViewModel();
        }

        [HttpPost]
        public IHttpActionResult Registry([FromBody] Payment payment)
        {
            try
            {
                bool result = this.PaymentVm.Add(payment);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message)
            }
        }
    }
}