using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class BranchePayment
    {
        public Guid UidOrden { get; set; }

        public Guid UidUsuario { get; set; }

        public Guid UidDireccion { get; set; }

        public Guid UidSucursal { get; set; }

        public Guid UidRelacionOrdenSucursal { get; set; }

        public Guid UidTarifario { get; set; }

        public long CodigoEntrega { get; set; }

        public decimal Monto { get; set; }

        public decimal MontoSucursal { get; set; }

        public decimal? DescuentoMonedero { get; set; }

        public decimal? ComisionTarjeta { get; set; }

        public decimal? ComisionTarjetaRepartidor { get; set; }
    }
}
