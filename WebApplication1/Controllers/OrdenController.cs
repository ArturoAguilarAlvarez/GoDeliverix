using System;
using System.Web.Http;
using VistaDelModelo;
using System.Collections;
using WebApplication1.App_Start;
using System.Net.Http;
using Modelo;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WebApplication1.Controllers
{
    public class OrdenController : ApiController
    {
        VMOrden MVOrden;
        ResponseHelper Respuesta;


        #region Aplicacion cliente


        #region Xamarin Api
        //Estos metodos son los que obtienen las compras, los pedidos, el detalle del pedido.
        /// <summary>
        /// Obtiene el estatus actual de un pedido
        /// </summary>
        /// <param name="UidPedido"></param>
        /// <returns></returns>
        ///
        public HttpResponseMessage GetObtenerEstatusDeOrden_Movil(string UidPedido)
        {
            MVOrden = new VMOrden();
            return Request.CreateResponse(MVOrden.ObtenerEstatusOrden(UidPedido));
        }
        /// <summary>
        /// Obtiene el historico de ordenes del cliente 
        /// </summary>
        /// <param name="UidUsuario"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        /// 
        public IHttpActionResult GetObtenerOrdenesCliente_Movil(string UidUsuario, string parametro)
        {
            MVOrden = new VMOrden();
            MVOrden.ObtenerOrdenesCliente(UidUsuario, parametro);
            foreach (var item in MVOrden.ListaDeOrdenes)
            {
                var Ordenes = new VMOrden();
                item.IntCantidadDeOrdenes = Ordenes.ObtenerSucursaleDeOrden(item.Uidorden).Rows.Count;
                var productos = 0;
                foreach (DataRow orden in Ordenes.ObtenerSucursaleDeOrden(item.Uidorden).Rows)
                {
                    var ord = new VMOrden();
                    ord.ObtenerProductosDeOrden(orden["UidRelacionOrdenSucursal"].ToString());
                    productos += ord.ListaDeProductos.Count;
                }
                item.IntCantidadProductos = productos;
                item.ListaDeOrdenes = new List<VMOrden>();

                foreach (DataRow ord in MVOrden.ObtenerSucursaleDeOrden(item.Uidorden).Rows)
                {
                    var statusOrden = new VMOrden();
                    var UltimoEstatus = string.Empty;
                    foreach (DataRow it in statusOrden.ObtenerEstatusOrden(ord["UidRelacionOrdenSucursal"].ToString()).Rows)
                    {
                        UltimoEstatus = it["VchNombre"].ToString();
                    }
                    int cantidad = 0;
                    var vmord = new VMOrden();
                    var emp = new VMEmpresas();
                    emp.BuscarEmpresas(UidEmpresa: new Guid(ord["uidempresa"].ToString()));
                    vmord.ObtenerProductosDeOrden(ord["UidRelacionOrdenSucursal"].ToString());
                    cantidad = vmord.ListaDeProductos.Count;

                    item.ListaDeOrdenes.Add(new VMOrden()
                    {
                        UidRelacionOrdenSucursal = ord["UidRelacionOrdenSucursal"].ToString(),
                        Imagen = ord["NVchRuta"].ToString(),
                        Identificador = ord["Identificador"].ToString(),
                        LNGFolio = long.Parse(ord["LNGFolio"].ToString()),
                        StrEstatusOrdenSucursal = UltimoEstatus,
                        MTotal = decimal.Parse(ord["MTotal"].ToString()),
                        intCantidad = cantidad,
                        StrNombreEmpresa = emp.NOMBRECOMERCIAL,
                        LngCodigoDeEntrega = ord.IsNull("BintCodigoEntrega") ? 0 :(long)ord["BintCodigoEntrega"]
                    });
                }

            }
            var result = new
            {
                OrderList = MVOrden.ListaDeOrdenes.Select(o => new
                {
                    o.Uidorden,
                    o.EstatusCobro,
                    o.LngCodigoDeEntrega,
                    o.FechaDeOrden,
                    o.MTotal,
                    o.StrFormaDeCobro,
                    o.LNGFolio,
                    o.UidDireccionCliente,
                    o.StrDireccionDeEntrega,
                    o.IntCantidadDeOrdenes,
                    o.IntCantidadProductos,
                    o.ListaDeOrdenes,
                    o.intCantidad,
                    o.WalletDiscount,
                    o.CardPaymentCommission,
                    o.DeliveryCardPaymentCommission
                })
            };


            return Json(result);
        }


        /// <summary>
        /// Obtiene los pedidos  de una orden
        /// </summary>
        /// <param name="UidOrden"></param>
        /// <returns></returns>
        /// 
        public HttpResponseMessage GetObtenerPedidosDeOrden_Movil(Guid UidOrden)
        {
            MVOrden = new VMOrden();
            return Request.CreateResponse(MVOrden.ObtenerSucursaleDeOrden(UidOrden));
        }
        public IHttpActionResult GetObtenerInformacionDeCompra_Movil(Guid UidOrden, string UidUsuario)
        {
            try
            {


                MVOrden = new VMOrden();
                var MVDireccion = new VMDireccion();
                var RepuestaDetallePedido = new OrderDetail();
                RepuestaDetallePedido.PedidosList = new List<VMOrden>();
                foreach (DataRow item in MVOrden.ObtenerSucursaleDeOrden(UidOrden).Rows)
                {
                    var statusOrden = new VMOrden();
                    var UltimoEstatus = string.Empty;
                    foreach (DataRow it in statusOrden.ObtenerEstatusOrden(item["UidRelacionOrdenSucursal"].ToString()).Rows)
                    {
                        UltimoEstatus = it["VchNombre"].ToString();
                    }
                    try
                    {
                        MVOrden.ObtenerProductosDeOrden(item["UidRelacionOrdenSucursal"].ToString());
                        var TotalDeProductos = MVOrden.ListaDeProductos.Count;
                        RepuestaDetallePedido.PedidosList.Add(new VMOrden()
                        {
                            Uidorden = new Guid(item["UidRelacionOrdenSucursal"].ToString()),
                            LngCodigoDeEntrega = long.Parse(item["BintCodigoEntrega"].ToString()),
                            Imagen = item["NVchRuta"].ToString(),
                            Identificador = item["Identificador"].ToString(),
                            MPropina = decimal.Parse(item["MPropina"].ToString()),
                            MTotal = decimal.Parse(item["MTotal"].ToString()),
                            LNGFolio = long.Parse(item["LNGFolio"].ToString()),
                            CostoEnvio = item["CostoEnvio"].ToString(),
                            StrEstatusOrdenSucursal = UltimoEstatus,
                            StrFormaDeCobro = item["EstatusCobro"].ToString(),
                            intCantidad = TotalDeProductos,
                            WalletDiscount = item.IsNull("WalletDiscount") ? null : (decimal?)item["WalletDiscount"],
                            CardPaymentCommission = item.IsNull("ComisionPagoTarjeta") ? null : (decimal?)item["ComisionPagoTarjeta"],
                            DeliveryCardPaymentCommission = item.IsNull("ComisionPagoTarjetaRepartidor") ? null : (decimal?)item["ComisionPagoTarjetaRepartidor"],
                            IncludeCPTS = (bool)item["IncludeCPTS"],
                            IncludeCPTD = (bool)item["IncludeCPTD"],
                        });
                    }
                    catch (Exception e)
                    {
                        throw;
                    }

                }

                MVOrden.ObtenerOrdenesCliente(UidUsuario, "Usuario");
                var order = MVOrden.ListaDeOrdenes.Find(o => o.Uidorden == UidOrden);
                RepuestaDetallePedido.UidOrden = order.Uidorden.ToString();
                RepuestaDetallePedido.FolioOrden = order.LNGFolio.ToString();
                RepuestaDetallePedido.EstatusFormaDeCobro = RepuestaDetallePedido.PedidosList[0].StrFormaDeCobro;
                RepuestaDetallePedido.FormaDeCobro = order.StrFormaDeCobro;
                MVDireccion.ObtenerDireccionCompleta(order.UidDireccionCliente.ToString());
                RepuestaDetallePedido.oDeliveryAddress = MVDireccion;

                var result = new
                {
                    UidOrden = order.Uidorden,
                    FolioOrden = order.LNGFolio,
                    FormaDeCobro = order.StrFormaDeCobro,
                    EstatusCobro = RepuestaDetallePedido.PedidosList[0].StrFormaDeCobro,
                    oDeliveryAddress = new
                    {
                        MVDireccion.ID,
                        MVDireccion.IDENTIFICADOR,
                        MVDireccion.COLONIA,
                        MVDireccion.CIUDAD,
                        MVDireccion.MUNICIPIO,
                        MVDireccion.PAIS,
                        MVDireccion.NOMBRECIUDAD,
                        MVDireccion.ESTADO,
                        MVDireccion.CALLE0,
                        MVDireccion.CALLE1,
                        MVDireccion.CALLE2,
                        MVDireccion.MANZANA,
                        MVDireccion.LOTE,
                        MVDireccion.CodigoPostal,
                        MVDireccion.REFERENCIA,
                        MVDireccion.Longitud,
                        MVDireccion.Latitud
                    },
                    PedidosList = RepuestaDetallePedido.PedidosList.Select(p => new
                    {
                        p.Uidorden,
                        p.LngCodigoDeEntrega,
                        p.Imagen,
                        p.Identificador,
                        p.MPropina,
                        p.MTotal,
                        p.LNGFolio,
                        p.CostoEnvio,
                        p.StrEstatusOrdenSucursal,
                        p.intCantidad,
                        p.StrFormaDeCobro,
                        p.WalletDiscount,
                        p.CardPaymentCommission,
                        p.DeliveryCardPaymentCommission,
                        p.IncludeCPTD,
                        p.IncludeCPTS
                    })
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene los productos de una orden (Orden sucursal)
        /// </summary>
        /// <param name="UidOrden"></param>
        /// <returns></returns>
        /// 
        public IHttpActionResult GetObtenerInformacionDelPedido(string UidOrden)
        {
            MVOrden = new VMOrden();
            MVOrden.ObtenerProductosDeOrden(UidOrden);
            var productos = MVOrden.ListaDeProductos;
            MVOrden.BuscarOrdenes("Sucursal", TipoDeSucursal: "S", UidOrdenSucursal: UidOrden);
            var InformacionDeEstatus = new VMEstatus();
            InformacionDeEstatus.cargaEstatusOrdenSucursal(UidOrden);
            var viewmodelImage = new VMImagen();
            viewmodelImage.ObtenerImagenPerfilDeEmpresa(MVOrden.UidEmpresa.ToString());
            var Orden = new VMOrden()
            {
                Uidorden = MVOrden.Uidorden,
                LNGFolio = MVOrden.LNGFolio,
                Identificador = MVOrden.Identificador,
                MTotal = MVOrden.MTotal,
                FechaDeOrden = MVOrden.FechaDeOrden,
                Imagen = viewmodelImage.STRRUTA,
                StrNombreSucursal = MVOrden.StrNombreSucursal,
                UidEstatus = MVOrden.UidEstatus,
                UidEmpresa = MVOrden.UidEmpresa,
                WalletDiscount = MVOrden.WalletDiscount,
                DeliveryCardPaymentCommission = MVOrden.DeliveryCardPaymentCommission,
                CardPaymentCommission = MVOrden.CardPaymentCommission,
                ComisionPagoTarjetaPropina = MVOrden.ComisionPagoTarjetaPropina,
                IncludeCPTD = MVOrden.IncludeCPTD,
                IncludeCPTS = MVOrden.IncludeCPTS
            };
            var DeliveryViewModel = new VMTarifario();
            DeliveryViewModel.ObtenerTarifarioDeOrden(Orden.Uidorden);
            var StatusInformation = new VMEstatus();
            StatusInformation.cargaEstatusOrdenSucursal(Orden.Uidorden.ToString());
            var result = new
            {
                StatusInformation = StatusInformation.ListaEstatus.Select(e => new
                {
                    e.DtmFechaDeEstatus,
                    e.NOMBRE,
                    e.DtFecha
                }),
                OrderInformation = new
                {
                    Orden.Uidorden,
                    Orden.LNGFolio,
                    Orden.Identificador,
                    Orden.MTotal,
                    Orden.FechaDeOrden,
                    Orden.Imagen,
                    Orden.StrNombreSucursal,
                    Orden.UidEstatus,
                    Orden.WalletDiscount,
                    Orden.CardPaymentCommission,
                    Orden.DeliveryCardPaymentCommission
                },
                DeliveryInformation = new
                {
                    DeliveryViewModel.DPrecio,
                    DeliveryViewModel.StrNombreEmpresa,
                    DeliveryViewModel.StrCodigoDeEntrega,
                },
                ProductsInformation = productos.Select(p => new
                {
                    p.UidProducto,
                    p.UidProductoEnOrden,
                    p.StrNombreSucursal,
                    p.StrNombreProducto,
                    p.Imagen,
                    p.intCantidad,
                    p.UidSucursal,
                    p.MPropina,
                    p.MTotal,
                    p.MTotalSucursal,
                    p.MCostoTarifario,
                    p.VisibilidadNota
                })
            };
            return Json(result);
        }
        /// <summary>
        /// Metodo para controlar el estatus publico de la orden
        /// </summary>
        /// <param name="UidUsuario"></param>
        /// <param name="UidEstatus"></param>
        /// <param name="StrParametro"></param>
        /// <param name="Mensaje"></param>
        /// <param name="UidOrden"></param>
        /// <param name="LngFolio"></param>
        /// <param name="UidLicencia"></param>
        /// <param name="UidSucursal"></param>
        /// <returns></returns>
        public HttpResponseMessage GetAgregaEstatusALaOrden_Movil(string UidUsuario, Guid UidEstatus, string StrParametro, string Mensaje = "", string UidOrden = "", long LngFolio = 0, string UidLicencia = "", string UidSucursal = "")
        {
            MVOrden = new VMOrden();
            //Se crean variables del cuerpo de la notificacion multiidioma
            var TituloEs = string.Empty;
            var TituloEN = string.Empty;
            var ContenidoEs = string.Empty;
            var ContenidoEN = string.Empty;

            //Se valida el estatus
            switch (UidEstatus.ToString())
            {
                //Creada
                case "2D2F38B8-7757-45FB-9CA6-6ECFE20356ED":
                    TituloEs = "";
                    TituloEN = "";
                    ContenidoEs = "";
                    ContenidoEN = "";
                    break;
                //Confirmada
                case "27E80591-3D79-4F77-B736-56C21A9113F6":
                    TituloEs = "";
                    TituloEN = "";
                    ContenidoEs = "";
                    ContenidoEN = "";
                    break;
                //Espera de confirmacion
                case "DE294EFC-C549-4DDD-A0D1-B0E1E2039ECC":
                    TituloEs = "";
                    TituloEN = "";
                    ContenidoEs = "";
                    ContenidoEN = "";
                    break;
                //Elaborada
                case "C412D367-7D05-45D8-AECA-B8FABBF129D9":
                    break;
                //Enviada
                case "B6BFC834-7CC4-4E67-817D-5ECB0EB2FFA7":
                    break;
                //Cancelado
                case "A2D33D7C-2E2E-4DC6-97E3-73F382F30D93":
                    TituloEs = "";
                    TituloEN = "";
                    ContenidoEs = "";
                    ContenidoEN = "";
                    break;
                default:
                    break;
            }


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

            return Request.CreateResponse(MVOrden.AgregaEstatusALaOrden(UidEstatus, StrParametro, new Guid(Mensaje), new Guid(UidOrden), LngFolio, new Guid(UidLicencia), new Guid(UidSucursal)));
        }

        public HttpResponseMessage GetGuardarProductos_Movil(Guid UIDORDEN, Guid UIDSECCIONPRODUCTO, int INTCANTIDAD, string STRCOSTO, Guid UidSucursal, Guid UidRegistroEncarrito, string UidTarifario, string UidNota = "", String StrMensaje = "")
        {
            MVOrden = new VMOrden();
            return Request.CreateResponse(MVOrden.GuardaProducto(UIDORDEN, UIDSECCIONPRODUCTO, INTCANTIDAD, STRCOSTO, UidSucursal, UidRegistroEncarrito, new Guid(UidNota), StrMensaje, UidTarifario));
        }
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
        public HttpResponseMessage GetGuardarOrden_Movil(Guid UIDORDEN, decimal Total, Guid Uidusuario, Guid UidDireccion, Guid Uidsucursal, decimal totalSucursal, Guid UidRelacionOrdenSucursal, long LngCodigoDeEntrega, string UidTarifario)
        {
            MVOrden = new VMOrden();
            return Request.CreateResponse(MVOrden.GuardaOrden(UIDORDEN, Total, Uidusuario, UidDireccion, Uidsucursal, totalSucursal, UidRelacionOrdenSucursal, LngCodigoDeEntrega, UidTarifario));
        }
        #endregion

        //Viejo api
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
        public ResponseHelper GetGuardarOrden(Guid UIDORDEN, decimal Total, Guid Uidusuario, Guid UidDireccion, Guid Uidsucursal, decimal totalSucursal, Guid UidRelacionOrdenSucursal, long LngCodigoDeEntrega, string UidTarifario)
        {
            MVOrden = new VMOrden();
            MVOrden.GuardaOrden(UIDORDEN, Total, Uidusuario, UidDireccion, Uidsucursal, totalSucursal, UidRelacionOrdenSucursal, LngCodigoDeEntrega, UidTarifario);
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
        public ResponseHelper GetGuardarProductos(Guid UIDORDEN, Guid UIDSECCIONPRODUCTO, int INTCANTIDAD, string STRCOSTO, Guid UidSucursal, Guid UidRegistroEncarrito, Guid UidNota, String StrMensaje, string UidTarifario)
        {
            MVOrden = new VMOrden();
            MVOrden.GuardaProducto(UIDORDEN, UIDSECCIONPRODUCTO, INTCANTIDAD, STRCOSTO, UidSucursal, UidRegistroEncarrito, UidNota, StrMensaje, UidTarifario);
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
        public ResponseHelper GetBuscarOrdenes(string Parametro, Guid Uidusuario = new Guid(), string FechaInicial = "", string FechaFinal = "", string NumeroOrden = "", Guid UidLicencia = new Guid(), string EstatusSucursal = "", string TipoDeSucursal = "", string UidOrdenSucursal = "")
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

    public class OrderDetail
    {
        public string UidOrden { get; set; }
        public string FolioOrden { get; set; }
        public string EstatusFormaDeCobro { get; set; }
        public string FormaDeCobro { get; set; }
        public VMDireccion oDeliveryAddress { get; set; }
        public List<VMOrden> PedidosList { get; set; }
    }
}
