using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class PurchaseHistory
    {
        public Guid PurchaseUid { get; set; }
        public Guid PaymentMethodUid { get; set; }
        public Guid PaymentStatusUid { get; set; }
        public string Folio { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string AddressIdentifier { get; set; }
        public decimal? WalletDiscount
        {
            get
            {
                return Orders.Where(o => o.WalletDiscount.HasValue).Any() ? Orders.Where(o => o.WalletDiscount.HasValue).Sum(o => o.WalletDiscount) : null;
            }
        }
        public List<OrderHistory> Orders { get; set; }

        public PurchaseHistory()
        {
            this.Orders = new List<OrderHistory>();
        }

        public int Count { get; set; }
    }
}
