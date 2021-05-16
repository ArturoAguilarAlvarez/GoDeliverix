using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers
{
    public class OrdersController : ApiController
    {
        private OrderViewModel OrderVm { get; }

        public OrdersController()
        {
            this.OrderVm = new OrderViewModel();
        }

        [HttpPost]
        public IHttpActionResult Cancel([FromBody] CancelOrderParams parameters)
        {
            try
            {
                bool result = this.OrderVm.CancelOrder(parameters.UidOrdenSucursal, parameters.UidUsuario, parameters.UidDireccion);
                if (result)
                    return Ok();
                else
                    return BadRequest("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult ReadAllUserPurchases([FromUri] Guid uid)
        {
            try
            {
                var result = this.OrderVm.ReadAllUserPurchases(uid);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetPurchaseOrder([FromUri] Guid uid)
        {
            try
            {
                var result = this.OrderVm.GetOrderPurchase(uid);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class CancelOrderParams
    {
        public Guid UidOrdenSucursal { get; set; }
        public Guid UidUsuario { get; set; }
        public Guid UidDireccion { get; set; }
    }
}