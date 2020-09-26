using System;
using System.Net.Http;
using System.Web.Http;
using VistaDelModelo;
using WebApplication1.App_Start;
namespace WebApplication1.Controllers
{
    public class TarifarioController : ApiController
    {
        VMTarifario MVTarifario;
        ResponseHelper Respuesta;
        /// <summary>
        /// Guarda el tarifario desde que el cliente crea una orden
        /// </summary>
        /// <param name="UidOrdenSucursal"></param>
        /// <param name="UidTarifario"></param>
        /// <returns></returns>
        public ResponseHelper GetGuardarTarifario(Guid UidOrdenSucursal, Guid UidTarifario, string DPropina)
        {
            MVTarifario = new VMTarifario();
            Respuesta = new ResponseHelper();
            MVTarifario.AgregarTarifarioOrden(UidOrden: UidOrdenSucursal, UidTarifario: UidTarifario, DPropina: decimal.Parse(DPropina));

            Respuesta.Data = "Registro guardado";
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetAgregarCodigoAOrdenTarifario(Guid UidCodigo, Guid UidLicencia, Guid uidorden)
        {
            MVTarifario = new VMTarifario();
            Respuesta = new ResponseHelper();
            MVTarifario.AgregarCodigoAOrdenTarifario(UidCodigo, UidLicencia, uidorden);

            Respuesta.Data = "Registro guardado";
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Modifica la cantidad del tarifario
        /// </summary>
        /// <param name="UidOrdenSucursal"></param>
        /// <param name="MPropina"></param>
        /// <returns></returns>
        public ResponseHelper GetModificarPropina(Guid UidOrdenSucursal, string MPropina)
        {
            MVTarifario = new VMTarifario();
            Respuesta = new ResponseHelper();
            MVTarifario.ModificarTarifario(UidOrdenSucursal, MPropina);

            Respuesta.Data = "Registro actualizado";
            Respuesta.Status = true;
            Respuesta.Message = "Informacion actualizada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetBuscarTarifario(string TipoDeBusqueda, string uidSucursal = "", string UidZonaRecolecta = "", string ZonaEntrega = "", string contrato = "", string UidSucursalDistribuidora = "")
        {
            MVTarifario = new VMTarifario();
            Respuesta = new ResponseHelper();
            MVTarifario.BuscarTarifario(TipoDeBusqueda, uidSucursal, UidZonaRecolecta, ZonaEntrega, contrato, UidSucursalDistribuidora);
            Respuesta.Data = MVTarifario;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        #region Xamarin Api
        public HttpResponseMessage GetBuscarTarifarios_Movil(string TipoDeBusqueda, string uidSucursal = "", string UidZonaRecolecta = "", string ZonaEntrega = "", string contrato = "", string UidSucursalDistribuidora = "")
        {
            MVTarifario = new VMTarifario();
            MVTarifario.BuscarTarifario(TipoDeBusqueda, uidSucursal, UidZonaRecolecta, ZonaEntrega, contrato, UidSucursalDistribuidora);
            return Request.CreateResponse(MVTarifario.ListaDeTarifarios);
        }
        /// <summary>
        /// Guarda el tarifario desde que el cliente crea una orden
        /// </summary>
        /// <param name="UidOrdenSucursal"></param>
        /// <param name="UidTarifario"></param>
        /// <returns></returns>
        public HttpResponseMessage GetGuardarTarifario_Movil(Guid UidOrdenSucursal, Guid UidTarifario, string DPropina)
        {
            MVTarifario = new VMTarifario();
            MVTarifario.AgregarTarifarioOrden(UidOrden: UidOrdenSucursal, UidTarifario: UidTarifario, DPropina: decimal.Parse(DPropina));
            return Request.CreateResponse(true);
            
        }
        #endregion
    }
}
