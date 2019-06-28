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
    public class LicenciaController : ApiController
    {
        VMLicencia MVLicencia;
        ResponseHelper Respuesta;

        public ResponseHelper GetValidaExistenciaDeLicencia(string Licencia)
        {
            Respuesta = new ResponseHelper();
            MVLicencia = new VMLicencia();
            Respuesta.Data = MVLicencia.ValidaExistenciaDeLicencia(Licencia);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetVerificaEstatusDeLicenciaSucursal(string Licencia)
        {
            Respuesta = new ResponseHelper();
            MVLicencia = new VMLicencia();
            Respuesta.Data = MVLicencia.VerificaEstatusDeLicenciaSucursal(Licencia);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetVerificaDisponibilidad(string Licencia)
        {
            Respuesta = new ResponseHelper();
            MVLicencia = new VMLicencia();
            Respuesta.Data = MVLicencia.VerificaDisponibilidad(Licencia);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetCambiaDisponibilidadDeLicencia(string Licencia)
        {
            Respuesta = new ResponseHelper();
            MVLicencia = new VMLicencia();
            MVLicencia.CambiaDisponibilidadDeLicencia(Licencia);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetActualizarLicenciaSucursal(Guid UidLicencia, int IdEstatus = 0, bool bdisponibilidad = false, string strIdentificador = "")
        {
            Respuesta = new ResponseHelper();
            MVLicencia = new VMLicencia();
            MVLicencia.ActualizarLicenciaSucursal(UidLicencia,IdEstatus,bdisponibilidad,strIdentificador);
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
           
            return Respuesta;
        }
    }
}
