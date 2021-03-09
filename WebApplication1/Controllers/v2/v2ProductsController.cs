using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers.v2
{
    [Route("api/v2/Products/{action}")]
    public class v2ProductsController : ApiController
    {
        private ProductViewModel ProductVm { get; }

        public v2ProductsController()
        {
            this.ProductVm = new ProductViewModel();
        }

        [HttpGet]
        public IHttpActionResult Search([FromUri] StoreSearchRequest request)
        {
            try
            {
                var result = this.ProductVm.ReadAllToStoreVersion3(request);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}