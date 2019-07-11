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
    public class CategoriaController : ApiController
    {

        VMCategoria MVCategoria;
        ResponseHelper Respuesta;

        public ResponseHelper Get(string value)
        {
            MVCategoria = new VMCategoria();
            MVCategoria.CategoriaConImagen(value);

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVCategoria;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        // POST: api/Profile
        public ResponseHelper Post([FromBody]string value)
        {
            MVCategoria = new VMCategoria();
            MVCategoria.CategoriaConImagen(value);

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVCategoria.LISTADECATEGORIAS;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
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
