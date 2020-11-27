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
    public class VersionController : ApiController
    {

        ResponseHelper respuesta;
        VMVersion MVersion;
        // GET: api/Version
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Version/5
        public ResponseHelper Get(string id)
        {
            respuesta = new ResponseHelper();
            MVersion = new VMVersion();
            MVersion.ObtenerVersion(UidAplicacion: new Guid(id));
            respuesta.Data = MVersion;
            respuesta.Status = true;
            respuesta.Message = "Version actual de la aplicacion";
            return respuesta;
        }
        public IHttpActionResult GetUltimaVersionMovil(string Uid)
        {
            MVersion = new VMVersion();
            MVersion.ObtenerVersion(UidAplicacion: new Guid(Uid));
            return Json(MVersion.StrVersion);
        }

        // POST: api/Version
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Version/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Version/5
        public void Delete(int id)
        {
        }
    }
}
