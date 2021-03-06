﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Diagnostics;


namespace GoDeliverix_TodasLasSucursales.VM
{
    public class VMInstalacion
    {
        public bool VerificaExistenciaDeBaseDeDatos()
        {
            //Crea una variable para el resultado de la existencia de sql server
            bool Resultado = false;
            //Verifica las diferentes maneras en las que se presenta sql server en la maquina cliente
            string servicename = "MSSQL";
            string servicename2 = "SQLAgent";
            string servicename3 = "SQL Server";
            string servicename4 = "msftesql";

            //Variable de resultado de servicio
            string serviceoutput = string.Empty;
            //Obtiene los servicios en la maquina y los guarda en un arreglo string
            ServiceController[] services = ServiceController.GetServices();
            //Recorre los servicios y verifica que coincidan con los servicename declarados
            foreach (ServiceController service in services)
            {
                if (service == null)
                    continue;
                if (service.ServiceName.Contains(servicename) || service.ServiceName.Contains(servicename2) || service.ServiceName.Contains(servicename3) || service.ServiceName.Contains(servicename4))
                {
                    serviceoutput = serviceoutput + System.Environment.NewLine + "Service Name = " + service.ServiceName + System.Environment.NewLine + "Display Name = " + service.DisplayName + System.Environment.NewLine + "Status = " + service.Status + System.Environment.NewLine;
                }
            }
            //Si encuentra el programa en la computadora del cliente devuelve verdadero.
            if (!string.IsNullOrEmpty(serviceoutput))
            {
                Resultado = true;
            }

            return Resultado;
        }
    }
}
