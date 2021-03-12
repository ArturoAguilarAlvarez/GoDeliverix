using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class StoreGiro
    {
        public Guid Uid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }
    }
}
