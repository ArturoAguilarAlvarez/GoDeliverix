using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class CompanyDetail
    {
        public Guid Uid { get; set; }

        public string Name { get; set; }

        public IEnumerable<CompanyBranch> Branches { get; set; }

        public CompanyDetail()
        {
            this.Branches = new HashSet<CompanyBranch>();
        }
    }
}
