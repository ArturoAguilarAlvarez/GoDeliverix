using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class LastWorkShift
    {
        public Guid Uid { get; set; }

        public Guid UidUsuario { get; set; }

        public Guid UidEstatusActual { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public int Folio { get; set; }

        public decimal Fondo { get; set; }
    }
}
