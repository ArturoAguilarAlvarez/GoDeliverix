﻿using System;
using System.Web.Http;
using VistaDelModelo;
using WebApplication1.App_Start;
namespace WebApplication1.Controllers
{
    public class SucursalesController : ApiController
    {
        VMSucursales MVSucursales;
        ResponseHelper Respuesta;
        // GET: api/Profile/5
        public ResponseHelper GetBuscarSucursalesDeUnProducto(Guid uidEmpresa, string day, Guid UidEstado, Guid UidColonia)
        {
            MVSucursales = new VMSucursales();
            MVSucursales.BuscarSucursalesCliente(uidEmpresa, day, UidEstado, UidColonia);

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVSucursales;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }


        public ResponseHelper GetObtenSucursalDeLicencia(string licencia)
        {
            MVSucursales = new VMSucursales();
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVSucursales.ObtenSucursalDeLicencia(licencia);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetVerificaEstatusSucursal(string UidSucursal)
        {
            MVSucursales = new VMSucursales();
            Respuesta = new ResponseHelper();
            Respuesta.Data = MVSucursales.VerificaEstatusSucursal(UidSucursal);
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }

        public ResponseHelper GetBuscarSucursales(string identificador = "", string horaApertura = "", string horaCierre = "", string IdColonia = "", string Uidempresa = "", string UidSucursal = "")
        {
            MVSucursales = new VMSucursales();
            Respuesta = new ResponseHelper();
            if (string.IsNullOrEmpty(IdColonia))
            {
                IdColonia = Guid.Empty.ToString();
            }
            MVSucursales.BuscarSucursales(UidSucursal: UidSucursal,identificador: identificador,horaApertura: horaApertura,horaCierre: horaCierre,IdColonia: new Guid(IdColonia), Uidempresa: Uidempresa);

            if (!string.IsNullOrEmpty(UidSucursal))
            {
                Respuesta.Data = MVSucursales;
            }
            else
            {
                Respuesta.Data = MVSucursales.LISTADESUCURSALES;
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
