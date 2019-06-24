using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public ResponseHelper GetBuscarProductosCliente(string StrParametroBusqueda, string StrDia, Guid UidDireccion, Guid UidBusquedaCategorias, string StrNombreEmpresa)
        {
            MVProducto = new VMProducto();
            MVProducto.buscarProductosEmpresaDesdeCliente(StrParametroBusqueda, StrDia, UidDireccion, UidBusquedaCategorias, StrNombreEmpresa);
            
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVProducto.ListaDeProductos;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetObtenerInformacionDeProductoDeLaSucursal(string StrParametroBusqueda, string StrDia, Guid UidDireccion, Guid UidBusquedaCategorias, object UidProducto)
        {
            MVProducto = new VMProducto();
            MVProducto.BuscarProductoPorSucursal(StrParametroBusqueda,StrDia,UidDireccion,UidBusquedaCategorias,UidProducto);
            
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVProducto.ListaDeProductos;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetObtenerProductosDeLaSucursal(string UidSeccion)
        {
            MVProducto = new VMProducto();
            MVProducto.BuscarProductosSeccion(new Guid(UidSeccion));
            
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVProducto.ListaDeProductos;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        // POST: api/Profile
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Profile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Profile/5
        public void Delete(int id)
        {
        }

    }
}
