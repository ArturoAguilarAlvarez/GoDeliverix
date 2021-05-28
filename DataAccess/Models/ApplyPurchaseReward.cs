using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ApplyPurchaseReward
    {
        public Guid UidUser { get; set; }
        public Guid UidPurchase { get; set; }
    }
}
