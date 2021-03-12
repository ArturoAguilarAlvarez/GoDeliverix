using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers.v2
{
    [Route("api/v2/Categories/{action}")]
    public class v2CategoriesController : ApiController
    {
        private ProductViewModel ProductVm { get; }

        public v2CategoriesController()
        {
            this.ProductVm = new ProductViewModel();
        }

        [HttpGet]
        public IHttpActionResult GetTypesCbo()
        {
            try
            {
                var result = this.ProductVm.GetAllGiros();
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetCategoriesCbo([FromUri] Guid uid)
        {
            try
            {
                var result = this.ProductVm.GetCategorias(uid);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetSubcategoriesCbo([FromUri] Guid uid)
        {
            try
            {
                var result = this.ProductVm.GetSubcategorias(uid);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetBranchDeals(Guid uidSucursal, string dia)
        {
            try
            {
                var result = this.ProductVm.GetBranchDeals(uidSucursal, dia);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetDealSections(Guid uidOferta, Guid uidEstado)
        {
            try
            {
                var result = this.ProductVm.GetDealSections(uidOferta, uidEstado);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}