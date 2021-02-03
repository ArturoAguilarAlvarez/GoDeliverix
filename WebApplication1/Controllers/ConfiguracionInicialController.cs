using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers
{
    public class ConfiguracionInicialController : ApiController
    {
        private VMConfiguracionCliente ClienteVM { get; }

        public ConfiguracionInicialController()
        {
            ClienteVM = new VMConfiguracionCliente();
        }
        [HttpGet]
        public IHttpActionResult GetClientConfig()
        {
            try
            {
                this.ClienteVM.ObtenerConfiguracionCliente();
                var result = new { Nombre = ClienteVM.StrNombre, Valor = ClienteVM.StrValor };

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
    }
}
