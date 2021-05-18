using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class OrderHistoryDetailProduct
    {
        public Guid ProductUid { get; set; }
        public Guid OrderUid { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
        public decimal Total { get; set; }
    }
}
