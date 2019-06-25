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
    public class OrdenController : ApiController
    {
        VMOrden MVOrden;
        ResponseHelper Respuesta;

        public ResponseHelper GetGuardarOrden(Guid UIDORDEN, decimal Total, Guid Uidusuario, Guid UidDireccion, Guid Uidsucursal, decimal totalSucursal, Guid UidRelacionOrdenSucursal, long LngCodigoDeEntrega)
        {
            MVOrden = new VMOrden();
            MVOrden.GuardaOrden(UIDORDEN,Total,Uidusuario,UidDireccion,Uidsucursal,totalSucursal,UidRelacionOrdenSucursal,LngCodigoDeEntrega);
            Respuesta = new ResponseHelper();
            Respuesta.Data = "Registro guardado";
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }


        public ResponseHelper GetGuardarProductos(Guid UIDORDEN, Guid UIDSECCIONPRODUCTO, int INTCANTIDAD, string STRCOSTO, Guid UidSucursal, Guid UidRegistroEncarrito, Guid UidNota, String StrMensaje)
        {
            MVOrden = new VMOrden();
            MVOrden.GuardaProducto(UIDORDEN,UIDSECCIONPRODUCTO,INTCANTIDAD,STRCOSTO,UidSucursal,UidRegistroEncarrito,UidNota,StrMensaje);
            Respuesta = new ResponseHelper();
            Respuesta.Data = "Registro guardado";
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetAgregaEstatusALaOrden(Guid UidEstatus, string StrParametro, Guid Mensaje = new Guid(), Guid UidOrden = new Guid(), long LngFolio = 0, Guid UidLicencia = new Guid(), Guid UidSucursal = new Guid())
        {
            MVOrden = new VMOrden();
            MVOrden.AgregaEstatusALaOrden(UidEstatus,StrParametro,Mensaje,UidOrden,LngFolio,UidLicencia,UidSucursal);
            Respuesta = new ResponseHelper();
            Respuesta.Data = "Registro guardado";
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
    }
}
