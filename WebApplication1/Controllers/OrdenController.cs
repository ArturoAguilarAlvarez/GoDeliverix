using System;
using System.Web.Http;
using VistaDelModelo;
using WebApplication1.App_Start;
namespace WebApplication1.Controllers
{
    public class OrdenController : ApiController
    {
        VMOrden MVOrden;
        ResponseHelper Respuesta;


        #region Aplicacion cliente
        /// <summary>
        /// Guarda la orden del cliente al pagarla
        /// </summary>
        /// <param name="UIDORDEN"></param>
        /// <param name="Total"></param>
        /// <param name="Uidusuario"></param>
        /// <param name="UidDireccion"></param>
        /// <param name="Uidsucursal"></param>
        /// <param name="totalSucursal"></param>
        /// <param name="UidRelacionOrdenSucursal"></param>
        /// <param name="LngCodigoDeEntrega"></param>
        /// <returns></returns>
        public ResponseHelper GetGuardarOrden(Guid UIDORDEN, decimal Total, Guid Uidusuario, Guid UidDireccion, Guid Uidsucursal, decimal totalSucursal, Guid UidRelacionOrdenSucursal, long LngCodigoDeEntrega)
        {
            MVOrden = new VMOrden();
            MVOrden.GuardaOrden(UIDORDEN, Total, Uidusuario, UidDireccion, Uidsucursal, totalSucursal, UidRelacionOrdenSucursal, LngCodigoDeEntrega);
            Respuesta = new ResponseHelper();
            Respuesta.Data = "Registro guardado";
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Guarda los productos de una orden pagada
        /// </summary>
        /// <param name="UIDORDEN"></param>
        /// <param name="UIDSECCIONPRODUCTO"></param>
        /// <param name="INTCANTIDAD"></param>
        /// <param name="STRCOSTO"></param>
        /// <param name="UidSucursal"></param>
        /// <param name="UidRegistroEncarrito"></param>
        /// <param name="UidNota"></param>
        /// <param name="StrMensaje"></param>
        /// <returns></returns>
        public ResponseHelper GetGuardarProductos(Guid UIDORDEN, Guid UIDSECCIONPRODUCTO, int INTCANTIDAD, string STRCOSTO, Guid UidSucursal, Guid UidRegistroEncarrito, Guid UidNota, String StrMensaje)
        {
            MVOrden = new VMOrden();
            MVOrden.GuardaProducto(UIDORDEN, UIDSECCIONPRODUCTO, INTCANTIDAD, STRCOSTO, UidSucursal, UidRegistroEncarrito, UidNota, StrMensaje);
            Respuesta = new ResponseHelper();
            Respuesta.Data = "Registro guardado";
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Este metodo modifica el estatus de la orden sucursal
        /// </summary>
        /// <param name="UidEstatus"></param>
        /// <param name="StrParametro"></param>
        /// <param name="Mensaje"></param>
        /// <param name="UidOrden"></param>
        /// <param name="LngFolio"></param>
        /// <param name="UidLicencia"></param>
        /// <param name="UidSucursal"></param>
        /// <returns></returns>
        public ResponseHelper GetAgregaEstatusALaOrden(Guid UidEstatus, string StrParametro, Guid Mensaje = new Guid(), Guid UidOrden = new Guid(), long LngFolio = 0, Guid UidLicencia = new Guid(), Guid UidSucursal = new Guid())
        {
            MVOrden = new VMOrden();
            if (Mensaje == null)
            {
                Mensaje = Guid.Empty;
            }
            MVOrden.AgregaEstatusALaOrden(UidEstatus, StrParametro, Mensaje, UidOrden, LngFolio, UidLicencia, UidSucursal);
            Respuesta = new ResponseHelper();
            Respuesta.Data = "Registro guardado";
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Obtiene el historico de ordenes del cliente 
        /// </summary>
        /// <param name="UidUsuario"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        public ResponseHelper GetObtenerOrdenesCliente(string UidUsuario, string parametro)
        {
            MVOrden = new VMOrden();
            MVOrden.ObtenerOrdenesCliente(UidUsuario, parametro);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOrden.ListaDeOrdenes;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Obtiene el estatus actual de la orden sucursal
        /// </summary>
        /// <param name="UidOrden"></param>
        /// <returns></returns>
        public ResponseHelper GetObtenerEstatusDeOrden(string UidOrden)
        {
            MVOrden = new VMOrden();
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOrden.ObtenerEstatusOrden(UidOrden);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Obtiene la informacion de una sucursal dentro de una orden
        /// </summary>
        /// <param name="UidOrden"></param>
        /// <returns></returns>
        public ResponseHelper GetObtenerSucursaleDeOrden(Guid UidOrden)
        {
            MVOrden = new VMOrden();

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOrden.ObtenerSucursaleDeOrden(UidOrden);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Obtiene los productos de una orden (Orden sucursal)
        /// </summary>
        /// <param name="UidOrden"></param>
        /// <returns></returns>
        public ResponseHelper GetObtenerProductosDeOrden(string UidOrden)
        {
            MVOrden = new VMOrden();
            MVOrden.ObtenerProductosDeOrden(UidOrden);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOrden.ListaDeProductos;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
        #endregion




        /// <summary>
        /// Busca las ordenes para el modulo de busquedas en el historico
        /// </summary>
        /// <param name="Parametro"></param>
        /// <param name="Uidusuario"></param>
        /// <param name="FechaInicial"></param>
        /// <param name="FechaFinal"></param>
        /// <param name="NumeroOrden"></param>
        /// <param name="UidLicencia"></param>
        /// <param name="EstatusSucursal"></param>
        /// <param name="TipoDeSucursal"></param>
        /// <param name="UidOrdenSucursal"></param>
        /// <returns></returns>
        public ResponseHelper GetBuscarOrdenes(string Parametro, Guid Uidusuario = new Guid(), string FechaInicial = "", string FechaFinal = "", string NumeroOrden = "", Guid UidLicencia = new Guid(), string EstatusSucursal = "", string TipoDeSucursal = "", Guid UidOrdenSucursal = new Guid())
        {
            MVOrden = new VMOrden();
            MVOrden.BuscarOrdenes(Parametro, Uidusuario, FechaInicial, FechaFinal, NumeroOrden, UidLicencia, EstatusSucursal, TipoDeSucursal, UidOrdenSucursal);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOrden.ListaDeOrdenes;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }


        #region Aplicacion repartidor
        /// <summary>
        /// usca las ordenes asignadas al repartidor
        /// </summary>
        /// <param name="UidUsuario"></param>
        /// <returns></returns>
        public ResponseHelper GetBuscarOrdenAsiganadaRepartidor(Guid UidUsuario)
        {
            MVOrden = new VMOrden();
            MVOrden.BuscarOrdenAsiganadaRepartidor(UidUsuario);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOrden;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }


       
        #endregion

    }
}
