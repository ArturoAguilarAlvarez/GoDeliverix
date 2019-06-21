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

        public void GetCorreoDeConfirmacion(string UidUsuario, string correo, string usuario, string password, string Nombre, string Apellidos)
        {
            MVAcceso = new VMAcceso();
            MVAcceso.CorreoDeConfirmacion(new Guid(UidUsuario),correo,usuario,password,Nombre,Apellidos);
            
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
