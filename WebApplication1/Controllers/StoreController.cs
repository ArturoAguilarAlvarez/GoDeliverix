using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers
{
    public class StoreController : ApiController
    {
        private ProductViewModel ProductVm { get; }

        public StoreController()
        {
            this.ProductVm = new ProductViewModel();
        }

        [HttpGet]
        public IHttpActionResult ReadAll([FromUri] StoreSearchRequest request)
        {
            try
            {
                var result = this.ProductVm.ReadAllToStore(request);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult ReadAllV2([FromUri] StoreSearchRequest request)
        {
            try
            {
                var result = this.ProductVm.ReadAllToStoreVersion2(request);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult ReadAllCompanies([FromUri] CompaniesSearchRequest request)
        {
            try
            {
                var result = this.ProductVm.ReadAllCompaniesToStore(request);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}