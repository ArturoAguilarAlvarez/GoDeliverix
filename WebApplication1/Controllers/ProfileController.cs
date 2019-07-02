using System;
using System.Collections.Generic;
using System.Web.Http;
using VistaDelModelo;
using WebApplication1.App_Start;
namespace WebApplication1.Controllers
{
    public class ProfileController : ApiController
    {
        #region Propiedades
        public static VMAcceso MVAcceso;
        ResponseHelper Respuesta;
        #endregion

        // GET: api/Profile
        public IEnumerable<string> Get(string Usuario, string Contrasena)
        {
            if (!string.IsNullOrEmpty(Usuario) && !string.IsNullOrEmpty(Contrasena))
            {
                Guid id = Guid.Empty;
                MVAcceso = new VMAcceso();
                id = MVAcceso.Ingresar(Usuario, Contrasena);
                return new string[] { id.ToString() };
            }
            else
            {
                return new string[] { "No valido" };
            }

        }
        /// <summary>
        /// Envia el correo de confirmacion de la cuenta cliente
        /// </summary>
        /// <param name="UidUsuario"></param>
        /// <param name="correo"></param>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <param name="Nombre"></param>
        /// <param name="Apellidos"></param>
        public void GetCorreoDeConfirmacion(string UidUsuario, string correo, string usuario, string password, string Nombre, string Apellidos)
        {
            MVAcceso = new VMAcceso();
            MVAcceso.CorreoDeConfirmacion(new Guid(UidUsuario), correo, usuario, password, Nombre, Apellidos);

        }



        public ResponseHelper GetProfileType(string UidUsuario)
        {
            Respuesta = new ResponseHelper();
            MVAcceso = new VMAcceso();
            Respuesta.Status = true;
            Respuesta.Data = MVAcceso.PerfilDeUsuario(UidUsuario);
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Obtiene el ultimo estatus del registro de la bitacora
        /// </summary>
        /// <param name="UidUsuario"></param>
        /// <returns></returns>
        public ResponseHelper GetObtenerUltimoEstatusBitacoraRepartidor(Guid UidUsuario)
        {
            MVAcceso = new VMAcceso();

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVAcceso.ObtenerUltimoEstatusBitacoraRepartidor(UidUsuario);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
        /// <summary>
        /// Registro en bitacora de acceso de los supervisores
        /// </summary>
        /// <param name="UidUsuario"></param>
        /// <param name="UidEstatus"></param>
        /// <returns></returns>
        public ResponseHelper GetBitacoraRegistroSupervisores(Guid UidUsuario, Guid UidEstatus)
        {
            MVAcceso = new VMAcceso();
            MVAcceso.BitacoraRegistroSupervisores(UidUsuario, UidEstatus);
            Respuesta = new ResponseHelper();
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        /// <summary>
        /// Cambia el estatus del repartidor ya sea suyo o de la orden asignada
        /// </summary>
        /// <param name="StrParametro"></param>
        /// <param name="UidUsuario"></param>
        /// <param name="UidEstatus"></param>
        /// <param name="UidOrdenRepartidor"></param>
        /// <returns></returns>
        public ResponseHelper GetBitacoraRegistroRepartidores(char StrParametro, Guid UidUsuario, Guid UidEstatus, string UidOrdenRepartidor = "")
        {
            MVAcceso = new VMAcceso();
            Respuesta = new ResponseHelper();
            Guid UidOrdenAsignada = new Guid();
            if (string.IsNullOrEmpty(UidOrdenRepartidor))
            {
                MVAcceso.BitacoraRegistroRepartidores(StrParametro, UidUsuario, UidEstatus);
            }
            else
            {
                UidOrdenAsignada = new Guid(UidOrdenRepartidor);
                MVAcceso.BitacoraRegistroRepartidores(StrParametro, UidUsuario, UidEstatus, UidOrdenAsignada);
            }
            
            Respuesta.Status = true;
            Respuesta.Message = "Estatus actualizado";
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
        public void Delete(int id)
        {
        }
    }
}
