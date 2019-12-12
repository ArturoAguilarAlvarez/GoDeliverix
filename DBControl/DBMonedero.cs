using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControl
{
    public class DBMonedero
    {
        Conexion oConexion;

        public DataTable ObtenerMonedero(Guid UidUsuario)
        {
            oConexion = new Conexion();
            string query = "select * from Monedero where UidUsuario = '" + UidUsuario.ToString() + "'";
            return oConexion.Consultas(query);
        }

        public DataTable ObtenerMovimientos(Guid uidPropietario)
        {
            oConexion = new Conexion();
            string query = "  select m.LngFolio,c.VchNombre as concepto,tdm.VchNombre as tipoMovimiento,m.DtmFechaRegistro,m.MMonto from movimientos m  inner join Monedero mo on mo.UidMonedero = m.UidMonedero inner join TipoDeMovimiento tdm on tdm.UidTipoDeMovimiento = m.UidTipoDeMovimiento inner join Conceptos c on c.UidConcepto = m.UidConcepto where mo.UidUsuario = '"+uidPropietario.ToString()+ "' order by m.LngFolio desc";
            return oConexion.Consultas(query);
        }
    }
}
