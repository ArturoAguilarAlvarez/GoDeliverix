using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControl
{
    public class DBOferta
    {
        #region Propiedades
        Conexion oConexion;
        #endregion
        #region metodos
        public DataTable ProductosExportar(string Uidsucursal)
        {
            string query = @"select  e.NombreComercial as Empresa,su.Identificador as Sucursal,o.VchNombre as Oferta,s.UidSeccion, s.VchNombre as Seccion, p.UidProducto,p.VchNombre as Producto, sp.Mcosto as Precio,sp.VchTiempoElaboracion as Tiempo
from Oferta o inner join Seccion s on s.UidOferta = o.UidOferta inner join SeccionProducto sp on sp.UidSeccion = s.UidSeccion
inner join Productos p on p.UidProducto = sp.UidProducto inner join Sucursales su on su.UidSucursal = o.Uidsucursal
inner join Empresa e on e.UidEmpresa = su.UidEmpresa where su.Uidsucursal  = '" + Uidsucursal + "'";
            oConexion = new Conexion();
            return oConexion.Consultas(query);
        }
        #endregion
    }
}
