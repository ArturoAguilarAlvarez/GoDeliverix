using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class Phone
    {
        public Guid Uid { get; set; }

        public Guid UidTipo { get; set; }

        public string Numero { get; set; }

        public string Tipo { get; set; }
    }
}
