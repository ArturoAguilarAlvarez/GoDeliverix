using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class PurchaseHistoryDetail
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
        public string AddressStreet0 { get; set; }
        public string AddressLatitude { get; set; }
        public string AddressLongitude { get; set; }
        public string AddressCountry { get; set; }
        public string AddressState { get; set; }
        public string AddressCity { get; set; }
        public string AddressNeighborhood { get; set; }
        public decimal? WalletDiscount
        {
            get
            {
                return Orders.Where(o => o.WalletDiscount.HasValue).Any() ? Orders.Where(o => o.WalletDiscount.HasValue).Sum(o => o.WalletDiscount) : null;
            }
        }
        public decimal Tips
        {
            get
            {
                return Orders.Count > 0 ? Orders.Sum(o => o.Tips) : 0;
            }
        }
        public decimal DeliveryRate
        {
            get
            {
                return Orders.Count > 0 ? Orders.Sum(o => o.Delivery) : 0;
            }
        }
        public List<PurchaseHistoryDetailOrder> Orders { get; set; }

        public PurchaseHistoryDetail()
        {
            this.Orders = new List<PurchaseHistoryDetailOrder>();
        }
    }
}
