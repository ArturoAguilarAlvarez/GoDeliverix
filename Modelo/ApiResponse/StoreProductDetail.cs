using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class StoreProductDetail
    {
        public Guid Uid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImgUrl { get; set; }

        public IEnumerable<StoreProductBranch> Branches { get; set; }

        public StoreProductDetail()
        {
            this.Branches = new HashSet<StoreProductBranch>();
        }
    }
}
