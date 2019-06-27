using System;
using System.Web.Http;
using VistaDelModelo;
using WebApplication1.App_Start;
namespace WebApplication1.Controllers
{
    public class TarifarioController : ApiController
    {
        VMTarifario MVTarifario;
        ResponseHelper Respuesta;

        public ResponseHelper GetGuardarTarifario(Guid UidOrdenSucursal, Guid UidTarifario)
        {
            MVTarifario = new VMTarifario();
            Respuesta = new ResponseHelper();
            MVTarifario.AgregarTarifarioOrden(UidOrden: UidOrdenSucursal, UidTarifario: UidTarifario);



            Respuesta.Data = "Registro guardado";
            Respuesta.Status = true;
            Respuesta.Message = "Informacion agregada satisfactoriamente";
            return Respuesta;
        }
    }
}
