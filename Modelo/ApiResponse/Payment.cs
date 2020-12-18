using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class Payment
    {
        public Guid UidPago { get; set; }

        public Guid UidUsuario { get; set; }

        public Guid UidFormaCobro { get; set; }

        public Guid UidDireccion { get; set; }

        public Guid UidEstatusCobro { get; set; }

        public Guid UidOrden { get; set; }

        public decimal Monto { get; set; }

        public decimal? DescuentoMonedero { get; set; }
    }
}
