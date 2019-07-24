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
        public ResponseHelper GetBuscarUsuarios(string UidUsuario = "", string UidEmpresa = "", string NOMBRE = "", string USER = "", string APELLIDO = "", string ESTATUS = "", string UIDPERFIL = "")
        {
            MVUsuario = new VMUsuarios();

            if (string.IsNullOrEmpty(UidEmpresa))
            {
                UidEmpresa = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UidUsuario))
            {
                UidUsuario = Guid.Empty.ToString();
            }

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
        public ResponseHelper GetGuardarUsuario(string UidUsuario, string perfil, string Nombre = "", string ApellidoPaterno = "", string ApellidoMaterno = "", string usuario = "", string password = "", string fnacimiento = "",  string estatus = "", string TIPODEUSUARIO = "", string UidEmpresa = "", string UidSucursal = "")
        {
            if (string.IsNullOrEmpty(UidEmpresa))
            {
                UidEmpresa = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UidSucursal))
            {
                UidSucursal = Guid.Empty.ToString();
            }
            MVUsuario = new VMUsuarios();
            MVUsuario.GuardaUsuario(new Guid(UidUsuario), Nombre, ApellidoPaterno, ApellidoMaterno, usuario, password, fnacimiento, perfil, estatus, TIPODEUSUARIO, new Guid(UidEmpresa), new Guid(UidSucursal));

            Respuesta = new ResponseHelper();

            Respuesta.Data = "Registro guardado";
            
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetGuardarusuarioCliente(string UidUsuario,string nombre,string apellidoP,string apellidoM,string usuario,string contrasena,string fechaNacimiento,string correo)
        {
            ResponseHelper respuesta = new ResponseHelper();
            VMUsuarios MVUsuarios = new VMUsuarios();
            VMTelefono MVTelefono = new VMTelefono();
            VMAcceso MVAcceso = new VMAcceso();

            Guid uidusuaro = new Guid(UidUsuario);
            Guid uidcorreo = Guid.NewGuid();

            VMCorreoElectronico MVCorreoElectronico = new VMCorreoElectronico();

            respuesta.Data = MVUsuarios.GuardaUsuario(UidUsuario: uidusuaro, Nombre: nombre, ApellidoPaterno: apellidoP, ApellidoMaterno: apellidoM, usuario: usuario, password: contrasena, fnacimiento: fechaNacimiento, perfil: "4f1e1c4b-3253-4225-9e46-dd7d1940da19", estatus: "2", TIPODEUSUARIO: "Cliente");
            //MVTelefono.AgregaTelefonoALista("f7bdd1d0-28e5-4f52-bc26-a17cd5c297de", telefono, "Principal");
           // MVCorreoElectronico.AgregarCorreo(uidusuaro, "Usuario", correo, uidcorreo);
            MVAcceso.CorreoDeConfirmacion(uidusuaro, correo, usuario, contrasena, nombre, apellidoM + " " + apellidoM);
            return respuesta;
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

        public void GetActualizarUsuario(
            string UidUsuario,
            string perfil,
            string Nombre = "",
            string ApellidoPaterno = "", 
            string ApellidoMaterno = "", 
            string usuario = "", 
            string password = "", 
            string fnacimiento = "",              
            string estatus = "",
            string UidEmpresa = "", 
            string UidSucursal = "")
        {
            if (string.IsNullOrEmpty(UidEmpresa))
            {
                UidEmpresa = Guid.Empty.ToString();
            }
            if (string.IsNullOrEmpty(UidSucursal))
            {
                UidSucursal = Guid.Empty.ToString();
            }
            VMUsuarios MVUsuarios = new VMUsuarios();
            MVUsuarios.ActualizarUsuario(
                UidUsuario: new Guid(UidUsuario),
                Nombre: Nombre,
                ApellidoPaterno: ApellidoPaterno,
                ApellidoMaterno: ApellidoMaterno,
                usuario: usuario,
                password: password,
                fnacimiento: fnacimiento,
                perfil: perfil);
        }
        

    }
}
