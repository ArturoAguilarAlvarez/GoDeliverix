using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class CompanyStoreGrid
    {
        public Guid Uid { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public int AvailableBranches { get; set; }
    }
}
