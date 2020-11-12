using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using VistaDelModelo;
using WebApplication1.App_Start;

namespace WebApplication1.Controllers
{
    public class EmpresaController : ApiController
    {
        VMEmpresas MVEmpresa;
        ResponseHelper Respuesta;



        #region Xamarin api
        public IHttpActionResult GetBusquedaDeEmpresasDisponibles_Movil(string StrParametroBusqueda,
            string StrDia, Guid UidColonia, Guid UidEstado, Guid UidBusquedaCategorias, string StrNombreEmpresa = "")
        {
            MVEmpresa = new VMEmpresas();
            MVEmpresa.BuscarEmpresaBusquedaCliente(StrParametroBusqueda, StrDia, UidEstado, UidColonia, UidBusquedaCategorias, StrNombreEmpresa);
            return Json(MVEmpresa.LISTADEEMPRESAS);
        }
        #endregion
        // GET:
        public ResponseHelper GetObtenerEmpresaCliente(string StrParametroBusqueda, string StrDia, Guid UidColonia, Guid UidEstado, Guid UidBusquedaCategorias, string StrNombreEmpresa = "")
        {
            MVEmpresa = new VMEmpresas();
            MVEmpresa.BuscarEmpresaBusquedaCliente(StrParametroBusqueda, StrDia, UidEstado, UidColonia, UidBusquedaCategorias, StrNombreEmpresa);
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVEmpresa;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }


        //Busqueda de empresas
        public ResponseHelper GetBuscarEmpresas(Guid UidEmpresa = new Guid(),
            string RazonSocial = "", string NombreComercial = "", string RFC = "", int tipo = 0, int status = 0)
        {
            MVEmpresa = new VMEmpresas();
            MVEmpresa.BuscarEmpresas(UidEmpresa, RazonSocial, NombreComercial, RFC, tipo, status);

            Respuesta = new ResponseHelper();
            if (UidEmpresa != Guid.Empty)
            {
                Respuesta.Data = MVEmpresa;
            }
            else
            {
                Respuesta.Data = MVEmpresa.LISTADEEMPRESAS;
            }

            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        //Busqueda de empresas
        public ResponseHelper GetVerificaEstatusEmpresa(string UidEmpresa)
        {
            MVEmpresa = new VMEmpresas();
            Respuesta = new ResponseHelper();

            Respuesta.Data = MVEmpresa.VerificaEstatusEmpresa(UidEmpresa);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }


        public ResponseHelper GetNombreEmpresa(string UIdUsuario)
        {
            Respuesta = new ResponseHelper();
            VMEmpresas MVEmpresas = new VMEmpresas();

            MVEmpresas.ObtenerNombreComercial(UIdUsuario);
            Respuesta.Data = MVEmpresas.NOMBRECOMERCIAL;
            return Respuesta;
        }

        public ResponseHelper GetListaSucursalesDeEmpresa(string UidEmpresa)
        {
            Respuesta = new ResponseHelper();
            VMSucursales MVSucursal = new VMSucursales();
            MVSucursal.DatosGridViewBusquedaNormal(UidEmpresa);
            Respuesta.Data = MVSucursal;
            return Respuesta;
        }

        public ResponseHelper GetIdEmpresa(string Uidusuario)
        {
            Respuesta = new ResponseHelper();
            VMUsuarios MVUsuarios = new VMUsuarios();
            var Guid = MVUsuarios.ObtenerIdEmpresa(Uidusuario);
            Respuesta.Data = Guid;
            return Respuesta;
        }

        public ResponseHelper GetLicenciasEmpresa(string ID)
        {
            Respuesta = new ResponseHelper();
            VMLicencia MVLicencia = new VMLicencia();
            MVLicencia.ObtenerLicenciaSucursal(ID);
            Respuesta.Data = MVLicencia;
            return Respuesta;
        }

        public ResponseHelper GetMensajeSucursal(string Licencia)
        {
            Respuesta = new ResponseHelper();
            VMMensaje MVMensaje = new VMMensaje();
            MVMensaje.Buscar(strLicencia: Licencia);
            Respuesta.Data = MVMensaje;
            return Respuesta;
        }

        //// POST: api/Profile
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Profile/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Profile/5
        //public void Delete(int id)
        //{
        //}

    }
}
