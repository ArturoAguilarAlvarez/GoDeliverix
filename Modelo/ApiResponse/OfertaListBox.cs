using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class OfertaListBox
    {
        public Guid Uid { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        public bool Available { get; set; }
    }
}
