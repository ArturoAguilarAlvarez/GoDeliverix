using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class RepartidorUpdate
    {
        public Guid Uid { get; set; }

        public string Nombre { get; set; } = "";

        public string ApellidoPaterno { get; set; } = "";

        public string ApellidoMaterno { get; set; } = "";

        public DateTime? FechaNacimiento { get; set; }
    }
}
