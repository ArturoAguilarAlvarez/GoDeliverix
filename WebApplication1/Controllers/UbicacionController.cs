using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.App_Start;
using VistaDelModelo;
namespace WebApplication1.Controllers
{
    public class UbicacionController : ApiController
    {

        VMUbicacion MVUbicacion;
        ResponseHelper Respuesta;
        /// <summary>
        /// Recupera laubicacion de una sucursal
        /// </summary>
        /// <param name="UidSucursal"></param>
        /// <returns></returns>
        public ResponseHelper GetRecuperaUbicacionSucursal(string UidSucursal)
        {
            MVUbicacion = new VMUbicacion();
            MVUbicacion.RecuperaUbicacionSucursal(UidSucursal);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVUbicacion;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Recupera la ubicacion de una direccion
        /// </summary>
        /// <param name="UidDireccion"></param>
        /// <returns></returns>
        public ResponseHelper GetRecuperaUbicacionDireccion(string UidDireccion)
        {
            MVUbicacion = new VMUbicacion();
            MVUbicacion.RecuperaUbicacionDireccion(UidDireccion);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVUbicacion;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
    }
}
