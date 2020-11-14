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
        public ResponseHelper GetBuscarSeccion(string UIDSECCION = "", string UIDOFERTA = "", string NOMBRE = "", string HORAINICIO = "", string HORAFIN = "", string Estatus = "", string UidDirecccion = "", string UidEstado = "", string UidColonia = "")
        {
            MVSeccion = new VMSeccion();
            if (string.IsNullOrEmpty(UidDirecccion))
            {
                UidDirecccion = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UIDOFERTA))
            {
                UIDOFERTA = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UIDSECCION))
            {
                UIDSECCION = Guid.Empty.ToString();
            }
            MVSeccion.Buscar(new Guid(UIDSECCION), new Guid(UIDOFERTA), NOMBRE, HORAINICIO, HORAFIN, Estatus, new Guid(UidDirecccion), UidEstado, UidColonia);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVSeccion;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }
        public ResponseHelper GetBuscaSeccion(string UIDSECCIONProducto)
        {
            MVSeccion = new VMSeccion();
            MVSeccion.BuscarSeccion(UIDSECCIONProducto);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVSeccion;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        #region Xamarin Api


        public IHttpActionResult GetBusquedaDeSecciones(string UIDSECCION = "", string UIDOFERTA = "", string NOMBRE = "", string HORAINICIO = "", string HORAFIN = "",  string UidDirecccion = "", string UidEstado = "", string UidColonia = "")
        {
            MVSeccion = new VMSeccion();
            if (string.IsNullOrEmpty(UidDirecccion))
            {
                UidDirecccion = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UIDOFERTA))
            {
                UIDOFERTA = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UIDSECCION))
            {
                UIDSECCION = Guid.Empty.ToString();
            }
            MVSeccion.Buscar(new Guid(UIDSECCION), new Guid(UIDOFERTA), NOMBRE, HORAINICIO, HORAFIN, "1", new Guid(UidDirecccion), UidEstado, UidColonia);
            var result = new
            {
                listaDeSecciones = MVSeccion.ListaDeSeccion.Select(s => new
                {
                    Uid = s.UID,
                    Name = s.StrNombre,
                    s.StrHoraInicio,
                    s.StrHoraFin,
                    s.IntEstatus
                })
            };
            return Json(result);
        }
        public HttpResponseMessage GetBuscarSeccion_movil(string UIDSECCION = "", string UIDOFERTA = "", string NOMBRE = "", string HORAINICIO = "", string HORAFIN = "", string Estatus = "", string UidDirecccion = "", string UidEstado = "", string UidColonia = "")
        {
            MVSeccion = new VMSeccion();
            if (string.IsNullOrEmpty(UidDirecccion))
            {
                UidDirecccion = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UIDOFERTA))
            {
                UIDOFERTA = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UIDSECCION))
            {
                UIDSECCION = Guid.Empty.ToString();
            }
            MVSeccion.Buscar(new Guid(UIDSECCION), new Guid(UIDOFERTA), NOMBRE, HORAINICIO, HORAFIN, Estatus, new Guid(UidDirecccion), UidEstado, UidColonia);
            return Request.CreateResponse(MVSeccion);
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
