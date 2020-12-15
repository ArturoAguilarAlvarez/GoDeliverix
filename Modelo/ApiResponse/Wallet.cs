using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class Wallet
    {
        public Guid Uid { get; set; }

        public Guid UidUser { get; set; }

        public decimal Amount { get; set; }

        public DateTime ? CreatedDate { get; set; }
    }
}
