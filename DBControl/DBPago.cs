using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControl
{
    public class DBPago
    {
        Conexion oConexion;
        public DataTable ValidaPagoConTarjeta(string UidFormaDeCobro) 
        {
            oConexion = new Conexion();
            string query = "select top 1 * from  PagosTarjeta where UidOrdenFormaDeCobro = '" + UidFormaDeCobro + "'";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerEstatusPago(Guid uidOrden)
        {
            oConexion = new Conexion();
            string query = "select dbo.ObtenerEstatusDeCobro('"+uidOrden.ToString()+"') as Estatus";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerPagoTarjeta(Guid uidOrdenFormaDeCobro)
        {
            oConexion = new Conexion();
            string query = "select * from PagosTarjeta where UidOrdenFormaDeCobro ='" + uidOrdenFormaDeCobro.ToString() + "'";
            return oConexion.Consultas(query);
        }
    }
}
