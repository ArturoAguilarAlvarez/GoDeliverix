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
    public class TurnoController : ApiController
    {
        VMTurno MVTurno;
        ResponseHelper Respuesta;
        public ResponseHelper GetTurnoRepartidor(Guid UidUsuario, Guid UidTurno)
        {
            Respuesta = new ResponseHelper();
            MVTurno = new VMTurno();
            MVTurno.EstatusTurno(UidUsuario, UidTurno);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion actualizada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetInformacionDeOrdenesPorTuno( Guid UidTurno)
        {
            Respuesta = new ResponseHelper();
            MVTurno = new VMTurno();
            MVTurno.InformacionDeOrdenesPorTuno( UidTurno);
            Respuesta.Data = MVTurno;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion obtenida satisfactoriamente";
            return Respuesta;
        }

        // GET: api/Turno
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Turno/5
        public ResponseHelper GetConsultaUltimoTurno(Guid UidUsuario)
        {
            Respuesta = new ResponseHelper();
            MVTurno = new VMTurno();
            MVTurno.ConsultaUltimoTurno(UidUsuario); 
            Respuesta.Data = MVTurno;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        // POST: api/Turno
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Turno/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Turno/5
        public void Delete(int id)
        {
        }
    }
}
