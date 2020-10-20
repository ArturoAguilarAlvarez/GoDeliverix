using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class DeliveryManLoginResult
    {
        public Guid Uid { get; set; }

        public string Usuario { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public Guid UidPerfil { get; set; }

        public Guid UidRepartidor { get; set; }

        public string CorreoElectronico { get; set; }
    }
}
