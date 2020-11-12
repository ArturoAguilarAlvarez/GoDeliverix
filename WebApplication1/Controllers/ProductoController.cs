using Swashbuckle.Swagger;
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
    public class ProductoController : ApiController
    {
        VMProducto MVProducto;
        ResponseHelper Respuesta;
        // GET: api/Profile/5
        public ResponseHelper GetBuscarProductosCliente(string StrParametroBusqueda, string StrDia, Guid UidEstado, Guid UidColonia, Guid UidBusquedaCategorias, string StrNombreEmpresa = "")
        {
            MVProducto = new VMProducto();
            MVProducto.buscarProductosEmpresaDesdeCliente(StrParametroBusqueda, StrDia, UidEstado, UidColonia, UidBusquedaCategorias, StrNombreEmpresa);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVProducto;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        public HttpResponseMessage GetBuscarProductosCliente_Movil(string StrParametroBusqueda,
            string StrDia,
            Guid UidEstado,
            Guid UidColonia,
            Guid UidBusquedaCategorias,
            string StrNombreEmpresa = "",
            string UidOferta = "",
            string UidSeccion = "",
            string UidEmpresa = "")
        {
            MVProducto = new VMProducto();
            MVProducto.buscarProductosEmpresaDesdeCliente(StrParametroBusqueda, StrDia, UidEstado, UidColonia, UidBusquedaCategorias, StrNombreEmpresa, UidOferta,UidSeccion,UidEmpresa);
            return Request.CreateResponse(MVProducto.ListaDeProductos);
        }

        
        public ResponseHelper GetObtenerInformacionDeProductoDeLaSucursal(string StrParametroBusqueda, string StrDia, string UidColonia, string UidEstado, string UidBusquedaCategorias, string UidProducto)
        {
            MVProducto = new VMProducto();
            MVProducto.BuscarProductoPorSucursal(StrParametroBusqueda, StrDia, new Guid(UidColonia), new Guid(UidEstado), new Guid(UidBusquedaCategorias), new Guid(UidProducto));

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVProducto;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }
        #region Xamarin Api

        public HttpResponseMessage GetObtenerInformacionDeProductoDeLaSucursal_Movil(string StrParametroBusqueda, string StrDia, string UidColonia, string UidEstado, string UidBusquedaCategorias, string UidProducto)
        {
            MVProducto = new VMProducto();
            MVProducto.BuscarProductoPorSucursal(StrParametroBusqueda, StrDia, new Guid(UidColonia), new Guid(UidEstado), new Guid(UidBusquedaCategorias), new Guid(UidProducto));
            return Request.CreateResponse(MVProducto.ListaDePreciosSucursales);
        }





        public HttpResponseMessage GetInformacionProductoAlCarrito(string UidSeccion, string UidSucursal, string UidProducto, string UidColoniaAEntregar)
        {
            //Obtiene la informacion del producto
            VMProducto producto = new VMProducto();
            producto.UID = new Guid(UidProducto);
            producto.UidSeccion = new Guid(UidSeccion);
            producto.UidSucursal = new Guid(UidSucursal);
            producto.InformacionDeProductoParaAgregarAlCarrito();
            //Obtiene la informacion del tarifario
            VMTarifario Tarifario = new VMTarifario();
            Tarifario.BuscarTarifario("Cliente", ZonaEntrega: UidColoniaAEntregar, uidSucursal: UidSucursal);
            var CostoDeEnvio = Tarifario.ListaDeTarifarios[0];
            producto.UidTarifario = CostoDeEnvio.UidTarifario;
            producto.CostoEnvio = CostoDeEnvio.DPrecio;
            producto.STRRUTAImagenEmpresa = CostoDeEnvio.StrRuta;
            producto.StrDeliveryBranch = CostoDeEnvio.StrNombreSucursal;
            producto.strDeliveryCompany = CostoDeEnvio.StrNombreEmpresa;

            var viewmodelSucursal = new VMSucursales();
            var viewmodelComision = new VMComision();

            viewmodelSucursal.BuscarSucursales(UidSucursal: UidSucursal);
            //Comision de empresa suministradora
            viewmodelComision.ObtenerComisionPorEmpresa(viewmodelSucursal.UidEmpresa);
            producto.IncluyeComisionTarjetaProducto = viewmodelComision.BIncluyeComisionTarjeta;
            //Comision de empresa distribuidora
            viewmodelComision.ObtenerComisionPorEmpresa(CostoDeEnvio.GuidSucursalDistribuidora);
            producto.IncluyeComisionTarjetaEnvio = viewmodelComision.BIncluyeComisionTarjeta;

            return Request.CreateResponse(producto);
        }
        #endregion

        public ResponseHelper GetObtenerProductosDeLaSeccion(string UidSeccion)
        {
            MVProducto = new VMProducto();
            MVProducto.BuscarProductosSeccion(new Guid(UidSeccion));

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVProducto;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }



        // POST: api/Profile
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Profile/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Profile/5
        public void Delete(int id)
        {
        }

    }
}
