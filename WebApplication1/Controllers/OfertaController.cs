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
    public class OfertaController : ApiController
    {
        VMOferta MVOferta;
        ResponseHelper Respuesta;
        // GET: api/Profile/5
        public ResponseHelper GetBuscarOferta(string UIDOFERTA  = "", string UIDSUCURSAL = "", string NOMBRE = "", string ESTATUS = "", string UidEmpresa = "")
        {
            MVOferta = new VMOferta();
            MVOferta.Buscar(new Guid(UIDOFERTA), new Guid(UIDSUCURSAL), NOMBRE,ESTATUS,new Guid(UidEmpresa));

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOferta.ListaDeOfertas;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

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
