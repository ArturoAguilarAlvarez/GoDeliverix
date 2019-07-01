﻿using System;
using System.Web.Http;
using VistaDelModelo;
using WebApplication1.App_Start;
namespace WebApplication1.Controllers
{
    public class DireccionController : ApiController
    {
        VMDireccion MVDireccion;
        VMUbicacion MVUbicacion;
        ResponseHelper Respuesta;
        // GET: api/Profile/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UidSucursal"></param>
        /// <returns></returns>
        public ResponseHelper GetObtenerDireccionCompletaDeSucursal(string UidSucursal)
        {
            MVDireccion = new VMDireccion();
            MVDireccion.ObtenerDireccionSucursal(UidSucursal);

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVDireccion;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UidUsuario"></param>
        /// <returns></returns>
        public ResponseHelper GetObtenerDireccionUsuario(string UidUsuario)
        {
            MVDireccion = new VMDireccion();
            MVDireccion.ObtenerDireccionesUsuario(UidUsuario);

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVDireccion.ListaDIRECCIONES;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        /// <summary>
        /// Guarda en la base de datos una direccion
        /// </summary>
        /// <param name="UidUsuario"></param>
        /// <param name="UidPais"></param>
        /// <param name="UidEstado"></param>
        /// <param name="UidMunicipio"></param>
        /// <param name="UidCiudad"></param>
        /// <param name="UidColonia"></param>
        /// <param name="CallePrincipal"></param>
        /// <param name="CalleAux1"></param>
        /// <param name="CalleAux2"></param>
        /// <param name="Manzana"></param>
        /// <param name="Lote"></param>
        /// <param name="CodigoPostal"></param>
        /// <param name="Referencia"></param>
        /// <param name="NOMBRECIUDAD"></param>
        /// <param name="NOMBRECOLONIA"></param>
        /// <param name="Identificador"></param>
        /// <param name="Latitud"></param>
        /// <param name="Longitud"></param>
        /// <param name="UidDireccion"></param>
        /// <returns></returns>
        public ResponseHelper GetGuardarDireccion(Guid UidUsuario, Guid UidPais, Guid UidEstado, Guid UidMunicipio, Guid UidCiudad, Guid UidColonia, string CallePrincipal, string CalleAux1, string CalleAux2, string Manzana, string Lote, string CodigoPostal, string Referencia, string NOMBRECIUDAD, string NOMBRECOLONIA, string Identificador, string Latitud, string Longitud)
        {
            MVDireccion = new VMDireccion();
            MVUbicacion = new VMUbicacion();
            Respuesta = new ResponseHelper();
            Guid uidDirecion = Guid.NewGuid();
            MVDireccion.AgregaDireccion("asp_AgregaDireccionUsuario", UidUsuario, uidDirecion, UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, CallePrincipal, CalleAux1, CalleAux2, Manzana, Lote, CodigoPostal, Referencia, Identificador);
            MVUbicacion.GuardaUbicacionDireccion(uidDirecion,Guid.NewGuid(), Latitud, Longitud);
            Respuesta.Message = "Informacion agregada satisfactoriamente";

            Respuesta.Data = "";
            Respuesta.Status = true;
            return Respuesta;
        }

        public ResponseHelper GetActualizarDireccion( Guid UidPais, Guid UidEstado, Guid UidMunicipio, Guid UidCiudad, Guid UidColonia, string CallePrincipal, string CalleAux1, string CalleAux2, string Manzana, string Lote, string CodigoPostal, string Referencia, string NOMBRECIUDAD, string NOMBRECOLONIA, string Identificador, string Latitud, string Longitud, string UidDireccion)
        {
            MVDireccion = new VMDireccion();
            Respuesta = new ResponseHelper();
            MVUbicacion = new VMUbicacion();
            MVDireccion.ActualizaDireccion( new Guid(UidDireccion), UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, CallePrincipal, CalleAux1, CalleAux2, Manzana, Lote, CodigoPostal, Referencia, Identificador);
            MVUbicacion.GuardaUbicacionDireccion(new Guid(UidDireccion), Guid.NewGuid(), Latitud, Longitud);
            Respuesta.Message = "Informacion actualizada satisfactoriamente";

            Respuesta.Data = "";
            Respuesta.Status = true;
            return Respuesta;
        }

        // POST: api/Profile
        public void Post([FromBody]string value)
        {
            bool aguilarEsChoto = true;
            if (aguilarEsChoto)
            {
                String manuel = "rechoto";
            }
        }

        // PUT: api/Profile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Profile/5
        public ResponseHelper DeleteDireccionUsuario(string UidDireccion)
        {
            MVDireccion = new VMDireccion();
            MVDireccion.EliminaDireccionUsuario(UidDireccion);
            Respuesta = new ResponseHelper();
            Respuesta.Status = true;
            Respuesta.Message = "Informacion eliminada satisfactoriamente";
            return Respuesta;
        }
    }
}
