using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    /// <summary>
    /// Detalle del producto de la orden
    /// </summary>
    public class DeliveryOrderProductDetail
    {
        public string Nombre { get; set; }

        public int Cantidad { get; set; }
    }
}
