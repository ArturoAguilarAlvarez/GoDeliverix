﻿using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers
{
    public class DealersController : ApiController
    {
        private DealersViewModel DealerVm { get; }

        public DealersController()
        {
            this.DealerVm = new DealersViewModel();
        }

        [HttpPost]
        public IHttpActionResult Update([FromBody] RepartidorUpdate update)
        {
            try
            {
                bool result = this.DealerVm.Update(update);
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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult ReadAllPhones(Guid uidUser)
        {
            try
            {
                var result = this.DealerVm.ReadAllPhones(uidUser);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetLastWorkShiftSummary(Guid userUid)
        {
            try
            {
                var result = this.DealerVm.GetLastWorkShift(userUid);

                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetAssignedPurchaseShipmentSummary(Guid dealerWorkshiftUid)
        {
            try
            {
                var result = this.DealerVm.GetLastAssignedOrder(dealerWorkshiftUid);

                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult OpenWorkshift()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}