using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VistaDelModelo;

namespace WebApplication1.Controllers
{
    public class ProfileController : ApiController
    {
        #region Propiedades
        public static VMAcceso MVAcceso = new VMAcceso();
        #endregion

        // GET: api/Profile
        public IEnumerable<string> Get(string Usuario, string Contrasena)
        {
            if (!string.IsNullOrEmpty(Usuario) && !string.IsNullOrEmpty(Contrasena))
            {
                Guid id = Guid.Empty;
                id = MVAcceso.Ingresar(Usuario, Contrasena);
                return new string[] { id.ToString() };
            }
            else
            {
                return new string[] { "No valido"};
            }
            
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
