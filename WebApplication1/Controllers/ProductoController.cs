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
