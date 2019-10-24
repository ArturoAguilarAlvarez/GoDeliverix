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
        
        
        public ResponseHelper GetTurnoSuministradora(Guid UidUsuario, Guid UidTurno)
        {
            Respuesta = new ResponseHelper();
            MVTurno = new VMTurno();
            MVTurno.TurnoSuministradora(UidUsuario, UidTurno);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion actualizada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetInformacionDeOrdenesPorTuno(Guid UidTurno)
        {
            Respuesta = new ResponseHelper();
            MVTurno = new VMTurno();
            MVTurno.InformacionDeOrdenesPorTuno(UidTurno);
            Respuesta.Data = MVTurno;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion obtenida satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Obtiene la informacion del turno actual
        /// </summary>
        /// <param name="UidLicencia"></param>
        /// <returns></returns>
        public ResponseHelper GetUltimoTurnoSuministradora(Guid UidLicencia)
        {
            Respuesta = new ResponseHelper();
            MVTurno = new VMTurno();
            MVTurno.ConsultarUltimoTurnoSuministradora(UidLicencia.ToString());
            Respuesta.Data = MVTurno;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion obtenida satisfactoriamente";
            return Respuesta;
        }

        /// <summary>
        /// Obtiene la orden y su total del turno
        /// </summary>
        /// <param name="UidTurno"></param>
        /// <returns></returns>
        public ResponseHelper GetInformacionHistoricoOrdenesTurno(Guid UidTurno)
        {
            Respuesta = new ResponseHelper();
            MVTurno = new VMTurno();
            MVTurno.InformacionHistoricoOrdenesTurno(UidTurno);
            Respuesta.Data = MVTurno;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion obtenida satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Obtiene el historico del repartidor
        /// </summary>
        /// <param name="UidUsuario"></param>
        /// <returns></returns>
        public ResponseHelper GetConsultaHisstorico(Guid UidUsuario)
        {
            Respuesta = new ResponseHelper();
            MVTurno = new VMTurno();
            MVTurno.ConsultarHistorico(UidUsuario);
            Respuesta.Data = MVTurno;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion de historico obtenida satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Obtiene la informacion de las liquidaciones del turnorepartidor
        /// </summary>
        /// <param name="UidTurnoRepartidor"></param>
        /// <returns></returns>
        public ResponseHelper GetConsultaLiquidacionesTurno(string UidTurnoRepartidor)
        {
            Respuesta = new ResponseHelper();
            MVTurno = new VMTurno();
            MVTurno.ObtenerInformacionLiquidacionesTuno(UidTurnoRepartidor);
            Respuesta.Data = MVTurno;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion de historico obtenida satisfactoriamente";
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

        public ResponseHelper GetAgregaEstatusTurnoRepartidor(string UidTurnoRepartidor, string UidEstatusTurno)
        {
            Respuesta = new ResponseHelper();
            MVTurno = new VMTurno();
            MVTurno.AgregaEstatusTurnoRepartidor(UidTurnoRepartidor, UidEstatusTurno);
            Respuesta.Data = MVTurno;
            Respuesta.Status = true;
            Respuesta.Message = "Estatus turno agregada satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Obtiene el estatus del turno del repartidor
        /// </summary>
        /// <param name="UidTurnoRepartidor"></param>
        /// <returns></returns>
        public ResponseHelper GetConsultaEstatusTurnoRepartidor(string UidTurnoRepartidor)
        {
            Respuesta = new ResponseHelper();
            MVTurno = new VMTurno();
            Respuesta.Data = MVTurno.ObtenerUltimoEstatusTurno(UidTurnoRepartidor);
            Respuesta.Status = true;
            Respuesta.Message = "Estatus turno agregada satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Obtiene el estatus del ultimo turno del repartidor
        /// </summary>
        /// <param name="UidTurnoRepartidor"></param>
        /// <returns></returns>
        public ResponseHelper GetConsultaEstatusUltimoTurnoRepartidor(string UidUsuario)
        {
            Respuesta = new ResponseHelper();
            MVTurno = new VMTurno();
            Respuesta.Data = MVTurno.ObtenerEstatusUltimoTurno(UidUsuario);
            Respuesta.Status = true;
            Respuesta.Message = "Estatus turno agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetConsultaCantidadMaximaAPortar(string UidRepartidor)
        {
            Respuesta = new ResponseHelper();
            MVTurno = new VMTurno();
            Respuesta.Data = MVTurno.ObtenerMontoAPortar(UidRepartidor);
            Respuesta.Status = true;
            Respuesta.Message = "Estatus turno agregada satisfactoriamente";
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
