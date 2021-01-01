using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class ProductStoreGrid
    {
        public Guid Uid { get; set; }
        public Guid UidCompany { get; set; }
        public string ImgUrl { get; set; }
        public string CompanyImgUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }
    }
}
