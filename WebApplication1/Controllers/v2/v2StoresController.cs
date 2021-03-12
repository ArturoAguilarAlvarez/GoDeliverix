using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers.v2
{
    [Route("api/v2/Stores/{action}")]
    public class v2StoresController : ApiController
    {
        private ProductViewModel ProductVm { get; }

        public v2StoresController()
        {
            this.ProductVm = new ProductViewModel();
        }


        [HttpGet]
        public IHttpActionResult Search([FromUri] CompaniesSearchRequest request)
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

        [HttpGet]
        public IHttpActionResult GetCompanyDetail([FromUri] CompanyBranchRequest request)
        {
            try
            {
                var result = this.ProductVm.GetCompanyDetail(request.UidEmpresa, request.UidEstado, request.UidColonia);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public IHttpActionResult ReadAllCompanyBranches([FromUri] CompanyBranchRequest request)
        {
            try
            {
                var result = this.ProductVm.ReadAllCompanyBranches(request.UidEmpresa, request.UidEstado, request.UidColonia);
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}