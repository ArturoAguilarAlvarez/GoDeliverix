﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VistaDelModelo;
using WebApplication1.App_Start;

namespace WebApplication1.Controllers
{
    public class EmpresaController : ApiController
    {
        VMEmpresas MVEmpresa;
        ResponseHelper Respuesta;

        // GET:
        public ResponseHelper GetObtenerEmpresaCliente(string StrParametroBusqueda, string StrDia, Guid UidDireccion, Guid UidBusquedaCategorias, string StrNombreEmpresa = "")
        {
            MVEmpresa = new VMEmpresas();
            MVEmpresa.BuscarEmpresaBusquedaCliente(StrParametroBusqueda, StrDia,UidDireccion,UidBusquedaCategorias,StrNombreEmpresa);

            Respuesta = new ResponseHelper();
            Respuesta.Data = MVEmpresa.LISTADEEMPRESAS;
            Respuesta.Status = true;
            Respuesta.Message = "Informacion recibida satisfactoriamente";
            return Respuesta;
        }


        //Busqueda de empresas
        public ResponseHelper GetBuscarEmpresas(Guid UidEmpresa = new Guid(), string RazonSocial = "", string NombreComercial = "", string RFC = "", int tipo = 0, int status = 0)
        {
            MVEmpresa = new VMEmpresas();
            MVEmpresa.BuscarEmpresas(UidEmpresa,RazonSocial,NombreComercial,RFC,tipo,status);

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
