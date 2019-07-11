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
    public class SubcategoriaController : ApiController
    {
        VMSubCategoria MVSubcategoria;
        ResponseHelper Respuesta;

        public ResponseHelper Get(string value)
        {
            MVSubcategoria = new VMSubCategoria();
            MVSubcategoria.SubcategoriaConImagen(value);

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVSubcategoria;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        // POST: api/Profile
        public ResponseHelper Post([FromBody]string value)
        {
            MVSubcategoria = new VMSubCategoria();
            MVSubcategoria.SubcategoriaConImagen(value);

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVSubcategoria.LISTADESUBCATEGORIAS;
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
