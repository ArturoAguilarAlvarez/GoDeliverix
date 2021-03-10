using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class ProductDetailRequest
    {
        public Guid UidProducto { get; set; }

        public Guid UidEstado { get; set; } 

        public Guid UidColonia { get; set; } 

        public string Dia { get; set; }
    }
}
