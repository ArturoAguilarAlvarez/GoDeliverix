using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Diagnostics;

namespace DeliverixSucursales
{
    public class VMInstalacion
    {
        

        //public bool CrearBaseDeDatos()
        //{
        //    bool resultado = false;
        //    try
        //    {
        //        oConexion = new Conexion();
        //        oConexion.InstalarSql();
        //        oConexion.CreateInstance();
        //        oConexion.CreateDatabase();
        //        resultado = true;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return resultado;
        //}

        /// <summary>
        /// Verifica que exista el sqlserver en la maquina del cliente
        /// </summary>
        /// <returns></returns>
        
        /// <summary>
        /// Instala el archivo de sqlserver
        /// </summary>
        public void InstalarSql()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("SQLEXPR_x86_ENU.exe");
            Process process = Process.Start(startInfo);
            process.WaitForExit();
        }
    }
}
