using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Enums
{
    /// <summary>
    /// Enumerador con las entidades registradas en el sistema, se usa para generar referencias en tablas Pivote e identifier en que tabla realizar la busqueda
    /// !NUNCA CAMBIAR EL ORDEN DE LOS ITEMS!
    /// </summary>
    public enum EntityType
    {
        None = -1,
        Empresa,
        Sucursal,
        Productos,
        Monedero,
        Movimientos,
        Orden,
        OndenSucursal,
        UserSignInRewardCode,
        AllUserSignInRewardCode
    }
}
