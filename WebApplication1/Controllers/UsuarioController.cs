using System;
using System.Web.Http;
using VistaDelModelo;
using WebApplication1.App_Start;

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
            MVUsuario.BusquedaDeUsuario(new Guid(UidUsuario), new Guid(UidEmpresa), NOMBRE, USER, APELLIDO, ESTATUS, new Guid(UIDPERFIL));

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

        //public ResponseHelper GetUsuario(string UidUsuario)
        //{
        //    //MVUsuario = new VMUsuarios();
        //    //MVUsuario.obtenerUsuario(UidUsuario);
        //    //Respuesta = new ResponseHelper();
        //    //VMUsuarios lista = new VMUsuarios
        //    //    (
        //    //    LISTADEUSUARIOS= MVUsuario.LISTADEUSUARIOS);
        //    //Respuesta.Data = MVUsuario;
        //    //Respuesta.Status = true;
        //    //Respuesta.Message = "Informacion recibida satisfactoriamente";
        //    //return Respuesta;

        //}
        public ResponseHelper GetGuardarUsuario(string UidUsuario, string Nombre, string ApellidoPaterno, string ApellidoMaterno, string usuario, string password, string fnacimiento, string perfil, string estatus, string TIPODEUSUARIO, string UidEmpresa, string UidSucursal)
        {
            MVUsuario = new VMUsuarios();
            MVUsuario.GuardaUsuario(new Guid(UidUsuario), Nombre, ApellidoPaterno, ApellidoMaterno, usuario, password, fnacimiento, perfil, estatus, TIPODEUSUARIO, new Guid(UidEmpresa), new Guid(UidSucursal));

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
