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
        public ResponseHelper GetBuscarOferta(string UIDOFERTA = "", string UIDSUCURSAL = "", string NOMBRE = "", string ESTATUS = "", string UidEmpresa = "")
        {
            MVOferta = new VMOferta();
            if (string.IsNullOrEmpty(UIDOFERTA))
            {
                UIDOFERTA = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UIDSUCURSAL))
            {
                UIDSUCURSAL = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UidEmpresa))
            {
                UidEmpresa = Guid.Empty.ToString();
            }
            MVOferta.Buscar(new Guid(UIDOFERTA), new Guid(UIDSUCURSAL), NOMBRE, ESTATUS, new Guid(UidEmpresa));

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOferta;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        #region Xamarin Api
        public IHttpActionResult GetBusquedaDeOfertas(string dia,
            string UidSucursal)
        {
            MVOferta = new VMOferta();

            MVOferta.BuscarOfertasCliente(UidSucursal, dia);
            var result = MVOferta.ListaDeOfertas.Select(o => new
            {
                Uid = o.UID,
                Name = o.STRNOMBRE,
                Estatus = o.StrEstatus
            });
            return Json(result);
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
