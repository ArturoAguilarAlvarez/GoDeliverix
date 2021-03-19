using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class NeighborhoodSearch
    {
        public Guid UidColonia { get; set; }
        public string NombreColonia { get; set; }
        public string CodigoPostal { get; set; }

        public Guid UidCiudad { get; set; }
        public string NombreCiudad { get; set; }

        public Guid UidMunicipio { get; set; }
        public string NombreMunicipio { get; set; }

        public Guid UidEstado { get; set; }
        public string NombreEstado { get; set; }

        public Guid UidPais { get; set; }
        public string NombrePais { get; set; }
    }
}
