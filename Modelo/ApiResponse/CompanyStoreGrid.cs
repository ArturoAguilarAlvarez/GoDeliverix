using Newtonsoft.Json;
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

        public string OpenAt { get; set; }

        public string ClosedAt { get; set; }

        public bool BeforeOpen { get; set; }

        public bool AfterClose { get; set; }

        public bool Available { get; set; }
    }
}
