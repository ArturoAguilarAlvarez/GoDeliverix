using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class CompanyBranch
    {
        public Guid Uid { get; set; }

        public string OpenAt { get; set; }

        public string CloseAt { get; set; }

        public string Identifier { get; set; }

        public int Status { get; set; }

        public bool Available { get; set; }
    }
}
