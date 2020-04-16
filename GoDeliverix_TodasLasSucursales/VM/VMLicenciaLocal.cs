using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoDeliverix_TodasLasSucursales.VM
{
    public class VMLicenciaLocal : NotifyBase
    {
        Conexion oConexion;
        string query;
        internal int AgregarLicencia(string licencia)
        {
            //respuestas de retorno 
            // 0 = Satisfactorio
            // 1 = Licencia existente
            // 2 = Error
            try
            {
                query = string.Empty;
                int respuesta = 2;
                oConexion = new Conexion();
                query = "select * from licencias where uidlicencia = '" + licencia + "'";

                if (oConexion.Consultas(query).Rows.Count == 0)
                {
                    query = "insert into Licencias(UidLicencia)values('" + licencia + "')";
                    oConexion.Consultas(query);
                    respuesta = 0;
                }
                else
                {
                    respuesta = 1;
                }

                return respuesta;


            }
            catch (Exception)
            {
                throw;
            }
        }

        internal DataTable obtenerLicencias()
        {
            try
            {
                query = "select * from Licencias";
                oConexion = new Conexion();
                return oConexion.Consultas(query);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal bool EliminarLicencia(Guid licencia)
        {
            bool response = false;
            try
            {
                query = "delete from Licencias where uidlicencia = '" + licencia.ToString() + "'";
                oConexion = new Conexion();
                oConexion.Consultas(query);
                response = true;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
    }
}
