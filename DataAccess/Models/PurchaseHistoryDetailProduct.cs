using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class PurchaseHistoryDetailProduct
    {
        public Guid ProductUid { get; set; }
        public Guid OrderUid { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
    }
}
