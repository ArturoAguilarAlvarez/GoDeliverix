using System;
using System.Collections.Generic;
using System.Linq;
using VistaDelModelo;
using System.Collections;
using WebApplication1.App_Start;
using System.Net.Http;
using Modelo;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ComisionesController : ApiController
    {

        #region Xamarin movil
        public IHttpActionResult ObtenerComisionEmpresa(string provedor = "MITec")
        {
            var viewmodelComisiones = new VMComision();
            viewmodelComisiones.ObtenerComisionPasarelaDeCobro(provedor);
            var bIncluyeComisionConTarjeta = viewmodelComisiones.BIncluyeComisionTarjeta;
            viewmodelComisiones.ObtenerComisionPasarelaDeCobro(provedor);
            var PorcentajeDeComision = viewmodelComisiones.FValor;
            var result = new { IncluyeComisionConTarjeta = bIncluyeComisionConTarjeta, ValorDeComision = PorcentajeDeComision };
            return Json(result);
        }
        #endregion

        [HttpGet]
        public IHttpActionResult ReadComission()
        {
            var viewmodelComisiones = new VMComision();
            viewmodelComisiones.ObtenerComisionDefault();

            var result = new { 
                IncluyeComisionConTarjeta = false, 
                Comision = viewmodelComisiones.ComisionPagoTarjeta
            };
            return Json(result);
        }
    }
}
