using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("Movimientos")]
    public class Movimiento
    {
        public Guid UidMovimiento { get; set; }
        public Guid UidTipoDeMovimiento { get; set; }
        public Guid UidConcepto { get; set; }
        public Guid UidMonedero { get; set; }
        public DateTime DtmFechaRegistro { get; set; }
        public long LngFolio { get; set; }
        public decimal MMonto { get; set; }
    }
}
