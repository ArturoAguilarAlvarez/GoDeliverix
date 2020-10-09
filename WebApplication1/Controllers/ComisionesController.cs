using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Web.Http;
using VistaDelModelo;
using System.Collections;
using WebApplication1.App_Start;
using System.Net.Http;
using Modelo;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace WebApplication1.Controllers
{
    public class ComisionesController : ApiController
    {

        #region Xamarin movil
        public IHttpActionResult ObtenerComisionEmpresa(string UidEmpresa,string provedor= "MITec")
        {
            var viewmodelComisiones = new VMComision();
            viewmodelComisiones.ObtenerComisionPorEmpresa(new Guid(UidEmpresa));
            var bIncluyeComisionConTarjeta = viewmodelComisiones.BIncluyeComisionTarjeta;
            viewmodelComisiones.ObtenerComisionPasarelaDeCobro(provedor);
            var PorcentajeDeComision = viewmodelComisiones.FValor;
            var result = new { IncluyeComisionConTarjeta = bIncluyeComisionConTarjeta, ValorDeComision = PorcentajeDeComision };
            return Json(result);
        }
        #endregion
        // GET: api/Comisiones
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Comisiones/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Comisiones
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Comisiones/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Comisiones/5
        public void Delete(int id)
        {
        }
    }
}
