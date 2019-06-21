using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.App_Start;
using VistaDelModelo;

namespace WebApplication1.Controllers
{
    public class UsuarioController : ApiController
    {
        VMUsuarios MVUsuario;
        ResponseHelper Respuesta;
        // GET: api/Profile/5
        public ResponseHelper GetBuscarUsuarios(string UidUsuario, string UidEmpresa = "", string NOMBRE = "", string USER = "", string APELLIDO = "", string ESTATUS = "", string UIDPERFIL = "")
        {
            MVUsuario = new VMUsuarios();
            MVUsuario.BusquedaDeUsuario(new Guid(UidUsuario),new Guid(UidEmpresa),NOMBRE,USER,APELLIDO,ESTATUS,new Guid(UIDPERFIL));

            Respuesta = new ResponseHelper();
            if (!string.IsNullOrEmpty(UidUsuario))
            {
                Respuesta.Data = MVUsuario;
            }
            else
            {
                Respuesta.Data = MVUsuario.LISTADEUSUARIOS;
            }
           
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
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
