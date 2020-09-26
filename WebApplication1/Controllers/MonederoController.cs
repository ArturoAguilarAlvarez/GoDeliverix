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
    public class MonederoController : ApiController
    {
        VMMonedero MVMonedero;
        ResponseHelper Respuesta;
        // GET: api/Monedero
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        #region Xamarin api
        public HttpResponseMessage Get_Movil(string UidUsuario)
        {
            MVMonedero = new VMMonedero();
            MVMonedero.ObtenerMonedero(new Guid(UidUsuario));
            return Request.CreateResponse(MVMonedero.MMonto);
        }
        #endregion
        // GET: api/Monedero/5
        public ResponseHelper Get(string id)
        {
            Respuesta = new ResponseHelper();
            MVMonedero = new VMMonedero();
            MVMonedero.ObtenerMonedero(new Guid(id));
            Respuesta.Data = MVMonedero;
            return Respuesta;
        }

        public ResponseHelper GetObtenerMovimientos(string id)
        {
            Respuesta = new ResponseHelper();
            MVMonedero = new VMMonedero();
            MVMonedero.UidPropietario = new Guid(id);
            MVMonedero.ObtenerMovimientosMonedero();
            Respuesta.Data = MVMonedero;
            return Respuesta;
        }

        /// <summary>
        /// Controla los movimientos del monedero
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseHelper GetMovimientoMonedero(string TipoDeMovimiento, string Concepto, string DireccionMovimiento, string Monto, string UidUsuario = "", string UidOrdenSucursal = "")
        {
            Respuesta = new ResponseHelper();
            MVMonedero = new VMMonedero();

            if (!string.IsNullOrEmpty(UidOrdenSucursal))
            {
                MVMonedero.uidOrdenSucursal = new Guid(UidOrdenSucursal);
            }
            else
            {
                MVMonedero.UidPropietario = new Guid(UidUsuario);
            }

            MVMonedero.UidTipoDeMovimiento = new Guid(TipoDeMovimiento);
            MVMonedero.UidConcepto = new Guid(Concepto);
            MVMonedero.MMonto = decimal.Parse(Monto);
            MVMonedero.UidDireccionDeOperacion = new Guid(DireccionMovimiento);
            MVMonedero.MovimientoMonedero();
            Respuesta.Data = MVMonedero;
            return Respuesta;
        }

        public ResponseHelper GetMovimientosMonedero(string TipoDeMovimiento, string Concepto, string Monto, string UidUsuario = "", string DireccionMovimiento = "", string UidOrdenSucursal = "")
        {
            Respuesta = new ResponseHelper();
            MVMonedero = new VMMonedero();

            if (!string.IsNullOrEmpty(UidOrdenSucursal))
            {
                MVMonedero.uidOrdenSucursal = new Guid(UidOrdenSucursal);
            }
            else
            {
                MVMonedero.UidPropietario = new Guid(UidUsuario);
            }

            MVMonedero.UidTipoDeMovimiento = new Guid(TipoDeMovimiento);
            MVMonedero.UidConcepto = new Guid(Concepto);
            MVMonedero.MMonto = decimal.Parse(Monto);
            if (!string.IsNullOrEmpty(DireccionMovimiento))
            {
                MVMonedero.UidDireccionDeOperacion = new Guid(DireccionMovimiento);
            }
            MVMonedero.MovimientoMonedero();
            Respuesta.Data = MVMonedero;
            return Respuesta;
        }

        // POST: api/Monedero
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Monedero/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Monedero/5
        public void Delete(int id)
        {
        }
    }
}
