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
    public class CorreoElectronicoController : ApiController
    {
        VMCorreoElectronico MVCorreoElectronico;
        ResponseHelper Respuesta;
        public ResponseHelper GetAgregarCorreo(string UidPropietario, string strParametroDeInsercion, string strCorreoElectronico, string UidCorreoElectronico)
        {
            MVCorreoElectronico = new VMCorreoElectronico();
            MVCorreoElectronico.AgregarCorreo(new Guid(UidPropietario),strParametroDeInsercion,strCorreoElectronico,new Guid(UidCorreoElectronico));

            Respuesta = new ResponseHelper();

            Respuesta.Data = "Registro guardado";
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
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
