using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VistaDelModelo;
using WebApplication1.App_Start;
namespace WebApplication1.Controllers
{
    public class ContratoController : ApiController
    {
        VMContrato MvContrato = new VMContrato();
        ResponseHelper Respuesta = new ResponseHelper();
        // GET: api/Contrato
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Contrato/5
        public string Get(int id)
        {
            return "value";
        }

        public ResponseHelper GetPagaAlRecolectar(string UidOrdenSucursal)
        {
            MvContrato = new VMContrato();

            Respuesta = new ResponseHelper();
            Respuesta.Data = MvContrato.VerificaPagoARecolectar(UidOrden: UidOrdenSucursal);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        // POST: api/Contrato
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Contrato/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Contrato/5
        public void Delete(int id)
        {
        }
    }
}
