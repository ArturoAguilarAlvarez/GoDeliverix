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
    public class GiroController : ApiController
    {
        VMGiro MVGiro;
        ResponseHelper Respuesta;
        // GET: api/Profile/5
        public ResponseHelper Get()
        {
            MVGiro = new VMGiro();
            MVGiro.ListaDeGiroConimagen();

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVGiro.LISTADEGIRO;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
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
