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
    public class SeccionController : ApiController
    {
        VMSeccion MVSeccion;
        ResponseHelper Respuesta;
        // GET: api/Profile/5
        public ResponseHelper GetBuscarSeccion(string UIDSECCION = "", string UIDOFERTA = "", string NOMBRE = "", string HORAINICIO = "", string HORAFIN = "", string Estatus = "", string UidDirecccion = "")
        {
            MVSeccion = new VMSeccion();
            MVSeccion.Buscar(new Guid(UIDSECCION),new Guid(UIDOFERTA),NOMBRE,HORAINICIO,HORAFIN,Estatus,new Guid(UidDirecccion));

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVSeccion.ListaDeSeccion;
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
