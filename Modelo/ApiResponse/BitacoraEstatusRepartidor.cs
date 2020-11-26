using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class BitacoraEstatusRepartidor
    {
        public Guid Uid { get; set; }

        public Guid UidEstatus { get; set; }

        public DateTime Fecha { get; set; }

        public Guid UidUsuario { get; set; }

        public string Estatus { get; set; }
    }
}
