using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.App_Start;
using VistaDelModelo;
namespace WebApplication1.Controllers
{
    public class DireccionController : ApiController
    {
        VMDireccion MVDireccion;
        ResponseHelper Respuesta;
        // GET: api/Profile/5
        public ResponseHelper GetObtenerDireccionCompletaDeSucursal(string UidSucursal)
        {
            MVDireccion = new VMDireccion();
            MVDireccion.ObtenerDireccionSucursal(UidSucursal);

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVDireccion;
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
