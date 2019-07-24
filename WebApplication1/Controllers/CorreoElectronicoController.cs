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

            Respuesta = new ResponseHelper();

            Respuesta.Data = MVCorreoElectronico.AgregarCorreo(new Guid(UidPropietario), strParametroDeInsercion, strCorreoElectronico, new Guid(UidCorreoElectronico));

            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetBuscarCorreo(string UidPropietario, string strParametroDebusqueda, string strCorreoElectronico = "", string UidCorreoElectronico = "")
        {
            MVCorreoElectronico = new VMCorreoElectronico();
            if (string.IsNullOrEmpty(UidCorreoElectronico))
            {
                UidCorreoElectronico = Guid.Empty.ToString();
            }
            MVCorreoElectronico.BuscarCorreos(new Guid(UidPropietario), strParametroDebusqueda, strCorreoElectronico,new Guid(UidCorreoElectronico));

            Respuesta = new ResponseHelper();

            Respuesta.Data = MVCorreoElectronico;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }



        public ResponseHelper GetActualizarCorreo(string UidPropietario, string strParametroDeInsercion, string strCorreoElectronico, Guid UidCorreoElectronico)
        {
            Respuesta = new ResponseHelper();
            MVCorreoElectronico = new VMCorreoElectronico();
            MVCorreoElectronico.EliminaCorreoUsuario(UidPropietario);
            Respuesta.Data = MVCorreoElectronico.AgregarCorreo(new Guid(UidPropietario), strParametroDeInsercion, strCorreoElectronico, UidCorreoElectronico);
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
        public void Delete(Guid UidUsuario)
        {
        }
    }
}
