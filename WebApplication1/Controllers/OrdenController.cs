using System;
using System.Web.Http;
using VistaDelModelo;
using System.Collections;
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
        public ResponseHelper GetAgregaEstatusALaOrden(Guid UidEstatus, string StrParametro, string Mensaje = "", string UidOrden = "", long LngFolio = 0, string UidLicencia = "", string UidSucursal = "")
        {
            MVOrden = new VMOrden();

            if (string.IsNullOrEmpty(UidSucursal))
            {
                UidSucursal = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UidLicencia))
            {
                UidLicencia = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UidOrden))
            {
                UidOrden = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                Mensaje = Guid.Empty.ToString();
            }
            MVOrden.AgregaEstatusALaOrden(UidEstatus, StrParametro, new Guid(Mensaje), new Guid(UidOrden), LngFolio, new Guid(UidLicencia), new Guid(UidSucursal));
            Respuesta = new ResponseHelper();
            Respuesta.Data = "Registro guardado";
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetAgregarEstatusOrdenEnSucursal(string UidEstatus, string cTipoDeSucursal, string UidLicencia = "", string UidOrden = "", long LngFolio = 0, string UidMensaje = "")
        {
            if (string.IsNullOrEmpty(UidLicencia))
            {
                UidLicencia = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UidMensaje))
            {
                UidMensaje = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UidOrden))
            {
                UidOrden = Guid.Empty.ToString();
            }
            MVOrden = new VMOrden();
            MVOrden.AgregarEstatusOrdenEnSucursal(new Guid(UidEstatus), cTipoDeSucursal, UidLicencia, new Guid(UidOrden), LngFolio, new Guid(UidMensaje));
            ResponseHelper respuesta = new ResponseHelper();
            respuesta.Data = MVOrden;
            respuesta.Status = true;
            respuesta.Message = "Se ha agregado el registro";
            return respuesta;
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
            Respuesta.Data = MVOrden;
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
            Respuesta.Data = MVOrden;
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
        public ResponseHelper GetBuscarOrdenAsiganadaRepartidor(Guid UidTurnoRepartidor)
        {
            MVOrden = new VMOrden();
            MVOrden.BuscarOrdenAsiganadaRepartidor(UidTurnoRepartidor);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOrden;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
        public ResponseHelper GetBuscarOrdenRepartidor(string UidCodigo, string UidLicencia)
        {
            MVOrden = new VMOrden();
            MVOrden.BuscarOrdenRepartidor(UidCodigo, UidLicencia);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOrden;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetObtenerNotaDeProducto(string uidProductoEnOrden)
        {
            MVOrden = new VMOrden();
            MVOrden.ObtenerNotaDeProductoEnOrden(new Guid(uidProductoEnOrden));
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOrden;
            Respuesta.Status = true;
            Respuesta.Message = "Nota Obtenida satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetOrdenesSucursal(string Licencia, string Estatus, string tipoSucursal)
        {
            switch (Estatus)
            {
                case "Pendientesaconfirmar":
                    Estatus = "Pendientes a confirmar";
                    break;
                case "Pendienteparaelaborar":
                    Estatus = "Pendiente para elaborar";
                    break;
                case "Listaaenviar":
                    Estatus = "Lista a enviar";
                    break;
                case "Canceladas":
                    Estatus = "Canceladas";
                    break;
                default:
                    break;
            }
            VMOrden MVOrden = new VMOrden();
            Respuesta = new ResponseHelper();
            MVOrden.BuscarOrdenes(
                "Sucursal",
                UidLicencia: new Guid(Licencia),
                EstatusSucursal: Estatus,
                TipoDeSucursal: tipoSucursal);
            Respuesta.Data = MVOrden;
            return Respuesta;
        }


        public ResponseHelper GetBuscarOrdenPorCodigoQR(string strCodigo, string UidTurnoRepartidor)
        {
            MVOrden = new VMOrden();

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOrden.ValidarCodigoUsuario(strCodigo, UidTurnoRepartidor);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }



        public ResponseHelper GetObtenerCodigoOrdenTarifario(Guid uidOrdenTarifario)
        {
            MVOrden = new VMOrden();
            MVOrden.ObtenerCodigoOrdenTarifario(uidOrdenTarifario);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOrden;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }



        public ResponseHelper GetCancelarOrden(string Licencia, string LNGFolio, string IdMensaje, string UidOrden = "")
        {
            VMOrden MVOrden = new VMOrden();
            Respuesta = new ResponseHelper();
            MVOrden.AgregaEstatusALaOrden(new Guid("A2D33D7C-2E2E-4DC6-97E3-73F382F30D93"), "S", Mensaje: new Guid(IdMensaje), UidOrden: new Guid(UidOrden), LngFolio: long.Parse(LNGFolio), UidLicencia: new Guid(Licencia));
            MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("EAE7A7E6-3F19-405E-87A9-3162D36CE21B"), "S", Licencia, LngFolio: long.Parse(LNGFolio), UidMensaje: new Guid(IdMensaje));
            return Respuesta;
        }

        public ResponseHelper GetFinalizarOrden(string Licencia, string Uidorden)
        {
            VMOrden MVOrden = new VMOrden();
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVOrden.AgregaEstatusALaOrden(new Guid("c412d367-7d05-45d8-aeca-b8fabbf129d9"), UidOrden: new Guid(Uidorden), UidLicencia: new Guid(Licencia), StrParametro: "S");
            return Respuesta;
        }

        #endregion


        #region Aplicacion Suministradora
        public ResponseHelper GetConfirmarOrden(string Licencia, string Uidorden)
        {
            VMOrden MVOrden = new VMOrden();
            Respuesta = new ResponseHelper();
            VMTarifario MVTarifario = new VMTarifario();
            MVOrden.AgregarEstatusOrdenEnSucursal(new Guid("EC09BCDE-ADAC-441D-8CC1-798BC211E46E"), "S", Licencia, UidOrden: new Guid(Uidorden));
            MVOrden.AgregaEstatusALaOrden(new Guid("2d2f38b8-7757-45fb-9ca6-6ecfe20356ed"), UidOrden: new Guid(Uidorden), UidLicencia: new Guid(Licencia), StrParametro: "S");
            MVTarifario.AgregarCodigoAOrdenTarifario(UidCodigo: Guid.NewGuid(), UidLicencia: new Guid(Licencia), uidorden: new Guid(Uidorden));
            return Respuesta;
        }

        #endregion

    }
}
