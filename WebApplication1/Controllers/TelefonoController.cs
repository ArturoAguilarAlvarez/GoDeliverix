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
    public class TelefonoController : ApiController
    {
        VMTelefono MVTelefono;
        ResponseHelper Respuesta;
        public ResponseHelper GetGuardaTelefono(string uidUsuario, string Parametro)
        {
            Respuesta = new ResponseHelper();
            MVTelefono = new VMTelefono();

            MVTelefono.GuardaTelefono(new Guid(uidUsuario), Parametro);

            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        // GET: api/Profile/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Profile
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Profile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Profile/5
        public void Delete(int id)
        {
        }
    }
}
