using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers
{
    public class WalletController : ApiController
    {

        private WalletViewModel WalletVm { get; }

        public WalletController()
        {
            this.WalletVm = new WalletViewModel();
        }

        [HttpGet]
        public IHttpActionResult GetBalance(Guid uidUser)
        {
            try
            {
                var result = this.WalletVm.GetUserWalletBalance(uidUser);

                if (result == null)
                {
                    return BadRequest("Does not exist");
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult ReadAllTransactions(Guid uidUser, Guid? uidConcept = null, Guid? uidType = null)
        {
            try
            {
                var result = this.WalletVm.ReadAllTransactions(uidUser, uidConcept, uidType);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}