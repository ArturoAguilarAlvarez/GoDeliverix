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
    public class TelefonoController : ApiController
    {
        VMTelefono MVTelefono;
        ResponseHelper Respuesta;
        public ResponseHelper GetGuardaTelefono(string uidUsuario, string Parametro)
        {
            Respuesta = new ResponseHelper();
            MVTelefono = new VMTelefono();

            MVTelefono.GuardaTelefono(new Guid(uidUsuario), Parametro);

            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        /// <summary>
        /// Metodo para guardar registros independientes los telefonos
        /// </summary>
        /// <param name="uidUsuario"></param>
        /// <param name="Parametro"></param>
        /// <param name="UidTelefono"></param>
        /// <param name="Numero"></param>
        /// <param name="UidTipoDeTelefono"></param>
        /// <returns></returns>
        public ResponseHelper GetGuardaTelefonoApi(Guid uidUsuario, string Parametro, Guid UidTelefono, string Numero, string UidTipoDeTelefono)
        {
            Respuesta = new ResponseHelper();
            MVTelefono = new VMTelefono();

            MVTelefono.GuardaTelefonoWepApi(uidUsuario, Parametro, UidTelefono, Numero, UidTipoDeTelefono);

            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UidTelefono"></param>
        /// <param name="Numero"></param>
        /// <param name="UidTipoDeTelefono"></param>
        /// <returns></returns>
        public ResponseHelper GetActualizaTelefonoApi( Guid UidTelefono, string Numero, string UidTipoDeTelefono)
        {
            Respuesta = new ResponseHelper();
            MVTelefono = new VMTelefono();
            MVTelefono.ActualizaTelefonoWepApi( UidTelefono, Numero, UidTipoDeTelefono);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetObtenerTipoDetelefono(string uidUsuario, string Parametro)
        {
            Respuesta = new ResponseHelper();
            MVTelefono = new VMTelefono();

            MVTelefono.TipoDeTelefonos();
            Respuesta.Data = MVTelefono.TIPOTELEFONO;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetBuscarTelefonos(string UidPropietario, string ParadetroDeBusqueda, string UidTelefono = "", string strTelefono = "")
        {
            Respuesta = new ResponseHelper();
            MVTelefono = new VMTelefono();
            if (String.IsNullOrEmpty(UidTelefono))
            {
                UidTelefono = Guid.Empty.ToString();
            }
            MVTelefono.BuscarTelefonos(new Guid(UidPropietario) , ParadetroDeBusqueda,new Guid(UidTelefono)  , strTelefono);
            Respuesta.Data = MVTelefono.ListaDeTelefonos;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        // GET: api/Profile/5
        public string Get(int id)
        {
            return "value";
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
        public ResponseHelper DeleteTelefonoUsuario(string UidTelefono)
        {
            Respuesta = new ResponseHelper();
            MVTelefono = new VMTelefono();
            MVTelefono.EliminaTelefonoUsuario(UidTelefono);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion eliminada satisfactoriamente";
            return Respuesta;
        }
    }
}
