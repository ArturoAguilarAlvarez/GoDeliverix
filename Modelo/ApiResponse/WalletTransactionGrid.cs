using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ApiResponse
{
    public class WalletTransactionGrid
    {
        public Guid Uid { get; set; }

        public DateTime Date { get; set; }

        public long Folio { get; set; }

        public decimal Amount { get; set; }

        public Guid UidType{ get; set; }

        public string Type { get; set; }

        public Guid UidConcept { get; set; }

        public string Concept { get; set; }
    }
}
