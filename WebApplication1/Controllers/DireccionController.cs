﻿using System;
using System.Data;
using System.Net.Http;
using System.Web.Helpers;
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
        /// Obtiene los paises del sistema
        /// </summary>
        /// <returns></returns>
        public ResponseHelper GetObtenerPaises()
        {
            MVDireccion = new VMDireccion();
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVDireccion.Paises();
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        /// <summary>
        /// Obtiene la direccion completa con los nombres del pais, estado,municipio,ciudad
        /// </summary>
        /// <param name="UidDireccion"></param>
        /// <returns></returns>
        public ResponseHelper GetDireccionCompleta(string UidDireccion)
        {
            MVDireccion = new VMDireccion();
            Respuesta = new ResponseHelper();
            MVDireccion.ObtenerDireccionCompleta(UidDireccion);
            Respuesta.Data = MVDireccion;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Busca una direccion por sucursal
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
        /// Busca direcciones por medio del Uid que se le envie
        /// </summary>
        /// <param name="UidDireccion"></param>
        /// <returns></returns>
        public ResponseHelper GetBuscarDireccion(string UidDireccion)
        {
            MVDireccion = new VMDireccion();
            MVDireccion.BuscarDireccionPorUid(UidDireccion);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVDireccion;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Obtiene la el uid de usuario a quien le pertenece la direccion
        /// </summary>
        /// <param name="UidDireccion"></param>
        /// <returns></returns>
        public ResponseHelper GetObtenerUidUsuarioDeUidDireccion(string UidDireccion)
        {
            MVDireccion = new VMDireccion();
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVDireccion.ObtenerUidUsuarioDeUidDireccion(UidDireccion); ;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetObtenerNombreDeLaColonia(string UidColonia)
        {
            MVDireccion = new VMDireccion();
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVDireccion.ObtenerNombreDeLaColonia(UidColonia);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }
        public ResponseHelper GetPais()
        {
            MVDireccion = new VMDireccion();
            Respuesta = new ResponseHelper();
            MVDireccion.Paises();
            Respuesta.Data = MVDireccion;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }
        public ResponseHelper GetEstado(Guid Pais, string busqueda = "", string Nombre = "")
        {
            MVDireccion = new VMDireccion();
            Respuesta = new ResponseHelper();
            MVDireccion.Estados(Pais, busqueda, Nombre);
            Respuesta.Data = MVDireccion;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }
        public ResponseHelper GetMunicipio(Guid Estado)
        {
            MVDireccion = new VMDireccion();
            Respuesta = new ResponseHelper();
            MVDireccion.Municipios(Estado);
            Respuesta.Data = MVDireccion;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }
        public ResponseHelper GetCiudad(Guid Municipio)
        {
            MVDireccion = new VMDireccion();
            Respuesta = new ResponseHelper();
            MVDireccion.Ciudades(Municipio);
            Respuesta.Data = MVDireccion;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }
        public ResponseHelper GetObtenerColonias(Guid Ciudad, string ubicacion = "", string Nombre = "")
        {
            MVDireccion = new VMDireccion();
            Respuesta = new ResponseHelper();
            MVDireccion.Colonias(Ciudad, ubicacion, Nombre);
            Respuesta.Data = MVDireccion;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetObtenerDireccionConDatosDeGoogle(string StrNombreCiudad, string CodigoEstado, string CodigoPais)
        {
            MVDireccion = new VMDireccion();
            Respuesta = new ResponseHelper();
            MVUbicacion = new VMUbicacion();
            string Estado = "";

            switch (CodigoEstado.ToUpper())
            {
                case "AGUASCALIENTES":
                    Estado = "AGS";
                    break;
                case "BAJA CALIFORNIA":
                    Estado = "BC ";
                    break;
                case "BAJA CALIFORNIA SUR":
                    Estado = "BCS";
                    break;
                case "CAMPECHE":
                    Estado = "CAMP";
                    break;
                case "CHIAPAS":
                    Estado = "CHIS";
                    break;
                case "CHIHUAHUA":
                    Estado = "CHIH";
                    break;
                case "COAHUILA DE ZARAGOZA":
                    Estado = "COAH";
                    break;
                case "COLIMA":
                    Estado = "COL";
                    break;
                case "DISTRITO FEDERAL":
                    Estado = "CDMX";
                    break;
                case "DURANGO":
                    Estado = "DGO";
                    break;
                case "GUANAJUATO":
                    Estado = "GTO";
                    break;
                case "GUERRERO":
                    Estado = "GRO";
                    break;
                case "HIDALGO":
                    Estado = "HGO";
                    break;
                case "JALISCO":
                    Estado = "JAL";
                    break;
                case "ESTADO DE MÉXICO":
                    Estado = "Edomex";
                    break;
                case "MICHOACÁN DE OCAMPO":
                    Estado = "MICH";
                    break;
                case "MORELOS":
                    Estado = "MOR";
                    break;
                case "NAYARIT":
                    Estado = "NAY";
                    break;
                case "NUEVO LEÓN":
                    Estado = "NL";
                    break;
                case "OAXACA":
                    Estado = "OAX";
                    break;
                case "PUEBLA":
                    Estado = "PUE";
                    break;
                case "QUERÉTARO":
                    Estado = "QRO";
                    break;
                case "QUINTANA ROO":
                    Estado = "QROO";
                    break;
                case "SAN LUIS POTOSÍ":
                    Estado = "S.L.P.";
                    break;
                case "SINALOA":
                    Estado = "SIN";
                    break;
                case "SONORA":
                    Estado = "SON";
                    break;
                case "TABASCO":
                    Estado = "TAB";
                    break;
                case "TAMAULIPAS":
                    Estado = "TAMPS";
                    break;
                case "TLAXCALA":
                    Estado = "TLAX";
                    break;
                case "VERACRUZ DE IGNACIO DE LA LLAVE":
                    Estado = "VER";
                    break;
                case "YUCATÁN":
                    Estado = "YUC";
                    break;
                case "ZACATECAS":
                    Estado = "ZAC";
                    break;
                default:
                    Estado = CodigoEstado;
                    break;
            }


            MVDireccion.ObtenerDireccionConGoogle(StrNombreCiudad, CodigoPais, Estado);

            if (MVDireccion.ListaDIRECCIONES.Count != 0)
            {
                if (MVDireccion.ListaDIRECCIONES[0].ID != Guid.Empty || MVDireccion.ListaDIRECCIONES[0].ID != null)
                {
                    Respuesta.Status = true;
                }
            }
            else
            {
                Respuesta.Status = false;
            }


            Respuesta.Data = MVDireccion;

            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }
        #region Xamarin api
        public IHttpActionResult GetGuardarDireccion_Movil(Guid UidUsuario, Guid UidPais, Guid UidEstado, Guid UidMunicipio, Guid UidCiudad, Guid UidColonia, string CallePrincipal, string CalleAux1, string CalleAux2, string Manzana, string Lote, string CodigoPostal, string Referencia, string NOMBRECIUDAD, string NOMBRECOLONIA, string Identificador, string Latitud, string Longitud, string UidDireccion = "")
        {
            MVDireccion = new VMDireccion();
            MVUbicacion = new VMUbicacion();
            Guid uidDirecion = new Guid();
            if (string.IsNullOrEmpty(UidDireccion))
            {
                uidDirecion = Guid.NewGuid();
            }
            else
            {
                uidDirecion = new Guid(UidDireccion);
            }

            MVDireccion.AgregaDireccion("asp_AgregaDireccionUsuario", UidUsuario, uidDirecion, UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, CallePrincipal, CalleAux1, CalleAux2, Manzana, Lote, CodigoPostal, Referencia, Identificador);
            var result = new { result = MVUbicacion.GuardaUbicacionDireccion(uidDirecion, Guid.NewGuid(), Latitud, Longitud) };
            return Json(result);
        }
        public IHttpActionResult GetActualizarDireccion_Movil(Guid UidPais, Guid UidEstado, Guid UidMunicipio, Guid UidCiudad, Guid UidColonia, string CallePrincipal, string CalleAux1, string CalleAux2, string Manzana, string Lote, string CodigoPostal, string Referencia, string NOMBRECIUDAD, string NOMBRECOLONIA, string Identificador, string Latitud, string Longitud, string UidDireccion)
        {
            MVDireccion = new VMDireccion();
            MVUbicacion = new VMUbicacion();
            MVDireccion.ActualizaDireccion(new Guid(UidDireccion), UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, CallePrincipal, CalleAux1, CalleAux2, Manzana, Lote, CodigoPostal, Referencia, Identificador);
            var resp = MVUbicacion.GuardaUbicacionDireccion(new Guid(UidDireccion), Guid.NewGuid(), Latitud, Longitud);
            var r = new { respuesta = resp };
            return Json(r);
        }
        /// <summary>
        /// Obtiene la direccion completa con los nombres del pais, estado,municipio,ciudad
        /// </summary>
        /// <param name="UidDireccion"></param>
        /// <returns></returns>
        public HttpResponseMessage GetDireccionCompleta_Movil(string UidDireccion)
        {
            MVDireccion = new VMDireccion();
            Respuesta = new ResponseHelper();
            MVDireccion.ObtenerDireccionCompleta(UidDireccion);
            return Request.CreateResponse(MVDireccion);
        }
        public HttpResponseMessage GetObtenerDireccionConDatosDeGoogle_Movil(string StrNombreCiudad, string CodigoEstado, string CodigoPais)
        {
            MVDireccion = new VMDireccion();
            MVUbicacion = new VMUbicacion();
            string Estado = "";
            string pais = "";
            if (CodigoPais == "México")
            {
                pais = "MX";
            }
            switch (CodigoEstado.ToUpper())
            {
                case "AGUASCALIENTES":
                    Estado = "AGS";
                    break;
                case "BAJA CALIFORNIA":
                    Estado = "BC ";
                    break;
                case "BAJA CALIFORNIA SUR":
                    Estado = "BCS";
                    break;
                case "CAMPECHE":
                    Estado = "CAMP";
                    break;
                case "CHIAPAS":
                    Estado = "CHIS";
                    break;
                case "CHIHUAHUA":
                    Estado = "CHIH";
                    break;
                case "COAHUILA DE ZARAGOZA":
                    Estado = "COAH";
                    break;
                case "COLIMA":
                    Estado = "COL";
                    break;
                case "DISTRITO FEDERAL":
                    Estado = "CDMX";
                    break;
                case "DURANGO":
                    Estado = "DGO";
                    break;
                case "GUANAJUATO":
                    Estado = "GTO";
                    break;
                case "GUERRERO":
                    Estado = "GRO";
                    break;
                case "HIDALGO":
                    Estado = "HGO";
                    break;
                case "JALISCO":
                    Estado = "JAL";
                    break;
                case "ESTADO DE MÉXICO":
                    Estado = "Edomex";
                    break;
                case "MICHOACÁN DE OCAMPO":
                    Estado = "MICH";
                    break;
                case "MORELOS":
                    Estado = "MOR";
                    break;
                case "NAYARIT":
                    Estado = "NAY";
                    break;
                case "NUEVO LEÓN":
                    Estado = "NL";
                    break;
                case "OAXACA":
                    Estado = "OAX";
                    break;
                case "PUEBLA":
                    Estado = "PUE";
                    break;
                case "QUERÉTARO":
                    Estado = "QRO";
                    break;
                case "QUINTANA ROO":
                    Estado = "QROO";
                    break;
                case "SAN LUIS POTOSÍ":
                    Estado = "S.L.P.";
                    break;
                case "SINALOA":
                    Estado = "SIN";
                    break;
                case "SONORA":
                    Estado = "SON";
                    break;
                case "TABASCO":
                    Estado = "TAB";
                    break;
                case "TAMAULIPAS":
                    Estado = "TAMPS";
                    break;
                case "TLAXCALA":
                    Estado = "TLAX";
                    break;
                case "VERACRUZ DE IGNACIO DE LA LLAVE":
                    Estado = "VER";
                    break;
                case "YUCATÁN":
                    Estado = "YUC";
                    break;
                case "ZACATECAS":
                    Estado = "ZAC";
                    break;
                default:
                    Estado = CodigoEstado;
                    break;
            }


            MVDireccion.ObtenerDireccionConGoogle(StrNombreCiudad, pais, Estado);

            return Request.CreateResponse(MVDireccion.ListaDIRECCIONES);

        }

        public HttpResponseMessage GetObtenerColonias_Movil(string UidCiudad)
        {
            VMDireccion Colonias = new VMDireccion();
            var dt = Colonias.Colonias(new Guid(UidCiudad));

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, dt);
        }

        public HttpResponseMessage GetObtenerDireccionUsuario_Movil(string UidUsuario)
        {
            MVDireccion = new VMDireccion();
            MVDireccion.ObtenerDireccionesUsuario(UidUsuario);
            return Request.CreateResponse(MVDireccion.ListaDIRECCIONES);
        }

        #endregion
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
            Respuesta.Data = MVDireccion;
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
        public ResponseHelper GetGuardarDireccion(Guid UidUsuario, Guid UidPais, Guid UidEstado, Guid UidMunicipio, Guid UidCiudad, Guid UidColonia, string CallePrincipal, string CalleAux1, string CalleAux2, string Manzana, string Lote, string CodigoPostal, string Referencia, string NOMBRECIUDAD, string NOMBRECOLONIA, string Identificador, string Latitud, string Longitud, string UidDireccion = "")
        {
            MVDireccion = new VMDireccion();
            MVUbicacion = new VMUbicacion();
            Respuesta = new ResponseHelper();
            Guid uidDirecion = new Guid();
            if (string.IsNullOrEmpty(UidDireccion))
            {
                uidDirecion = Guid.NewGuid();
            }
            else
            {
                uidDirecion = new Guid(UidDireccion);
            }

            MVDireccion.AgregaDireccion("asp_AgregaDireccionUsuario", UidUsuario, uidDirecion, UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, CallePrincipal, CalleAux1, CalleAux2, Manzana, Lote, CodigoPostal, Referencia, Identificador);
            MVUbicacion.GuardaUbicacionDireccion(uidDirecion, Guid.NewGuid(), Latitud, Longitud);
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            Respuesta.Data = "";
            Respuesta.Status = true;
            return Respuesta;
        }

        public ResponseHelper GetActualizarDireccion(Guid UidPais, Guid UidEstado, Guid UidMunicipio, Guid UidCiudad, Guid UidColonia, string CallePrincipal, string CalleAux1, string CalleAux2, string Manzana, string Lote, string CodigoPostal, string Referencia, string NOMBRECIUDAD, string NOMBRECOLONIA, string Identificador, string Latitud, string Longitud, string UidDireccion)
        {
            MVDireccion = new VMDireccion();
            Respuesta = new ResponseHelper();
            MVUbicacion = new VMUbicacion();
            MVDireccion.ActualizaDireccion(new Guid(UidDireccion), UidPais, UidEstado, UidMunicipio, UidCiudad, UidColonia, CallePrincipal, CalleAux1, CalleAux2, Manzana, Lote, CodigoPostal, Referencia, Identificador);
            MVUbicacion.GuardaUbicacionDireccion(new Guid(UidDireccion), Guid.NewGuid(), Latitud, Longitud);
            Respuesta.Message = "Informacion actualizada satisfactoriamente";
            Respuesta.Data = "";
            Respuesta.Status = true;
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
        public ResponseHelper DeleteDireccionUsuario(string UidDireccion)
        {
            MVDireccion = new VMDireccion();
            MVDireccion.EliminaDireccionUsuario(UidDireccion);
            Respuesta = new ResponseHelper();
            Respuesta.Status = true;
            Respuesta.Message = "Informacion eliminada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetDireccionConUbicacion(string UidDireccion)
        {
            MVDireccion = new VMDireccion();
            MVDireccion.EliminarDireccionUbicacion(UidDireccion);
            Respuesta = new ResponseHelper();
            Respuesta.Status = true;
            Respuesta.Message = "Informacion eliminada satisfactoriamente";
            return Respuesta;
        }
    }
}
