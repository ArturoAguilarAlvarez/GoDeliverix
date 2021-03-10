using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class StoreProductBranch
    {
        public Guid Uid { get; set; }

        public string Identifier { get; set; }

        public bool ViewInformation { get; set; }

        public Guid UidCommissionType { get; set; }

        public bool BranchIncludeCardPaymentCommission { get; set; }

        public string ShortAddress { get; set; }

        public decimal Price { get; set; }

        public string OpenAt { get; set; }

        public string ClosedAt { get; set; }
    }
}
