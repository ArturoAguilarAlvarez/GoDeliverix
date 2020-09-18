using Modelo;
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
    public class CatalogosDeBusqueda : ApiController
    {
        public VMGiro MVGiro { get; set; }
        public VMCategoria MVCategoria { get; set; }
        public VMSubCategoria MVSubCategoria { get; set; }
        ResponseHelper Respuesta;
        // GET: api/Profile/5
        public ResponseHelper Get()
        {
            MVGiro = new VMGiro();
            MVGiro.ListaDeGiroConimagen();

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVGiro;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        #region Xamarin Movil
        public HttpResponseMessage GetGiroMovil()
        {
            MVGiro = new VMGiro();
            MVGiro.ListaDeGiroConimagen();
            return Request.CreateResponse(MVGiro.LISTADEGIRO);
        }
        public HttpResponseMessage GetCategoriaMovil(string UidGiro, string TipoDeRespuesta)
        {
            MVCategoria = new VMCategoria();
            MVCategoria.BuscarCategorias(UidGiro: UidGiro, tipo: TipoDeRespuesta); 
            return Request.CreateResponse(MVCategoria.LISTADECATEGORIAS);
        }
        public HttpResponseMessage GetSubCategoriaMovil(string uidCategoria, string TipoDeRespuesta)
        {
            MVSubCategoria = new VMSubCategoria();
            MVSubCategoria.BuscarSubCategoria(UidCategoria: uidCategoria, Tipo: TipoDeRespuesta); 
            return Request.CreateResponse(MVSubCategoria.LISTADESUBCATEGORIAS);
        }
        #endregion




        //// POST: api/Profile
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Profile/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Profile/5
        //public void Delete(int id)
        //{
        //}
    }
}
