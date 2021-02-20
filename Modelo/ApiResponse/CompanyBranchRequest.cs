using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class CompanyBranchRequest
    {
        public Guid UidEmpresa { get; set; }

        public Guid UidEstado { get; set; }

        public Guid UidColonia { get; set; }
    }
}
