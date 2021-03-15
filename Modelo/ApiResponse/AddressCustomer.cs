using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class AddressCustomer
    {
        public Guid Uid { get; set; }
        public Guid UidPais { get; set; }
        public Guid UidEstado { get; set; }
        public Guid UidMunicipio { get; set; }
        public Guid UidCiudad { get; set; }
        public Guid UidColonia { get; set; }
        public string Calle0 { get; set; }
        public string Calle1 { get; set; }
        public string Calle2 { get; set; }
        public string Manzana { get; set; }
        public string Lote { get; set; }
        public string CodigoPostal { get; set; }
        public string Referencia { get; set; }
        public string Identificador { get; set; }
        public bool BPredeterminada { get; set; }
        public bool BEstatus { get; set; }
    }
}
