using System;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
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

        public ResponseHelper GetObtenerFolioCliente(string UidUsuario = "")
        {
            MVUsuario = new VMUsuarios();

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVUsuario.ObtenerFolio(UidUsuario);

            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetObtenerHora(string UidEstado = "")
        {
            MVUsuario = new VMUsuarios();

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVUsuario.ObtenerHoraActual(UidEstado);

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
        public ResponseHelper GetGuardarUsuario(string UidUsuario, string perfil, string Nombre = "", string ApellidoPaterno = "", string ApellidoMaterno = "", string usuario = "", string password = "", string fnacimiento = "", string estatus = "", string TIPODEUSUARIO = "", string UidEmpresa = "", string UidSucursal = "")
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
            MVUsuario.GuardaUsuario(new Guid(UidUsuario), Nombre, ApellidoPaterno,  usuario, password, fnacimiento, perfil, estatus, TIPODEUSUARIO, new Guid(UidEmpresa), new Guid(UidSucursal), ApellidoMaterno);

            Respuesta = new ResponseHelper();

            Respuesta.Data = "Registro guardado";

            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetGuardarusuarioCliente(string UidUsuario, string nombre, string apellidoP, string apellidoM, string usuario, string contrasena, string fechaNacimiento, string correo)
        {
            ResponseHelper respuesta = new ResponseHelper();
            VMUsuarios MVUsuarios = new VMUsuarios();
            VMAcceso MVAcceso = new VMAcceso();

            Guid uidusuaro = new Guid(UidUsuario);

            respuesta.Data = MVUsuarios.GuardaUsuario(UidUsuario: uidusuaro, Nombre: nombre, ApellidoPaterno: apellidoP, ApellidoMaterno: apellidoM, usuario: usuario, password: contrasena, fnacimiento: fechaNacimiento, perfil: "4f1e1c4b-3253-4225-9e46-dd7d1940da19", estatus: "2", TIPODEUSUARIO: "Cliente");
            //MVTelefono.AgregaTelefonoALista("f7bdd1d0-28e5-4f52-bc26-a17cd5c297de", telefono, "Principal");
            // MVCorreoElectronico.AgregarCorreo(uidusuaro, "Usuario", correo, uidcorreo);
            MVAcceso.CorreoDeConfirmacion(uidusuaro, correo, usuario, contrasena, nombre, apellidoM + " " + apellidoM);
            return respuesta;
        }
        // POST: api/Profile
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Profile/5
        public void Put(int id, [FromBody] string value)
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
        #region Xamarin ApiClient
        public IHttpActionResult GetActualizarUsuario_Movil(
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
            var respo = MVUsuarios.ActualizarUsuario(
                UidUsuario: new Guid(UidUsuario),
                Nombre: Nombre,
                ApellidoPaterno: ApellidoPaterno,
                ApellidoMaterno: ApellidoMaterno,
                usuario: usuario,
                password: password,
                fnacimiento: fnacimiento,
                perfil: perfil);
            var result = new { resultado = respo };
            return Json(result);
        }
        public IHttpActionResult GetBuscarUsuarios_Movil(string UidUsuario = "", string UidEmpresa = "", string NOMBRE = "", string USER = "", string APELLIDO = "", string ESTATUS = "", string UIDPERFIL = "")
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

            object Respuesta;
            if (!string.IsNullOrEmpty(UidUsuario) && UidUsuario != Guid.Empty.ToString())
            {
                var viewmodelCorreo = new VMCorreoElectronico();
                viewmodelCorreo.BuscarCorreos(UidPropietario: new Guid(UidUsuario),strParametroDebusqueda: "Usuario");
                var viewmodelTelefono = new VMTelefono();
                viewmodelTelefono.BuscarTelefonos(UidPropietario: new Guid(UidUsuario), ParadetroDeBusqueda: "Usuario");

                var telefono = new VMTelefono();
                if (viewmodelTelefono.ListaDeTelefonos != null)
                {
                    telefono = viewmodelTelefono.ListaDeTelefonos[0];
                }
                Respuesta = new { InformacionDeUsuario = MVUsuario, CorreoElectronico = viewmodelCorreo.CORREO, InformacionDelTelefono = telefono };
            }
            else
            {
                Respuesta = new
                {
                    ListaDeUsuarios = MVUsuario.LISTADEUSUARIOS.Select(p => new
                    {
                        p.Uid,
                        p.StrNombre,
                        p.StrUsuario,
                        p.StrApellidoPaterno,
                        p.DtmFechaDeNacimiento,
                        p.UidEmpresa,
                        p.StrEstatus,
                        p.StrPerfil,
                        p.StrNombreDeSucursal,
                        p.StrCotrasena
                    })
                };
            }
            return Json(Respuesta);

        }

        public HttpResponseMessage GetEnviarCodigoDeConfirmacion(string nombre, string apellidoP, string codigo, string correo, string idioma)
        {
            VMAcceso MVAcceso = new VMAcceso();
            MVAcceso.EnviarCodigoDeActivacion(nombre, apellidoP, correo, codigo, idioma);
            return Request.CreateResponse("Correo enviado");
        }

        public HttpResponseMessage GetGuardarusuarioCliente_Movil(string UidUsuario, string nombre, string apellidoP,  string usuario, string contrasena, string fechaNacimiento)
        {
            ResponseHelper respuesta = new ResponseHelper();
            VMUsuarios MVUsuarios = new VMUsuarios();
            VMAcceso MVAcceso = new VMAcceso();

            Guid uidusuaro = new Guid(UidUsuario);
            respuesta.Data = MVUsuarios.GuardaUsuario(UidUsuario: uidusuaro, Nombre: nombre, ApellidoPaterno: apellidoP,  usuario: usuario, password: contrasena, fnacimiento: fechaNacimiento, perfil: "4f1e1c4b-3253-4225-9e46-dd7d1940da19", estatus: "1", TIPODEUSUARIO: "Cliente");
            return Request.CreateResponse();
        }

        public HttpResponseMessage GetObtenerFolioCliente_Movil(string UidUsuario = "")
        {
            MVUsuario = new VMUsuarios();
            return Request.CreateResponse(MVUsuario.ObtenerFolio(UidUsuario));
        }
        #endregion

    }
}
