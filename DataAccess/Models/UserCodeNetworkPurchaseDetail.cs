using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserCodeNetworkPurchaseDetail
    {
        public Guid UidPurchase { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Orders { get; set; }
        public int Finished { get; set; }
        public int Cancelled { get; set; }
        public int Delivered { get; set; }
        /// <summary>
        /// Subtotal de las ordenes entregadas
        /// </summary>
        public decimal PaidAmount { get; set; }
        public decimal Tips { get; set; }
        public decimal Delivery { get; set; }
        public decimal Comissions { get; set; }
        public decimal PurchaseTotal { get; set; }
        public bool IsCompleted { get; set; }
        public decimal? AvailableToRefundOwner { get; set; }
        public decimal? AvailableToRefundGuest { get; set; }

        public CodeRewardType OwnerRewardType { get; set; }
        public CodeRewardType GuestRewardType { get; set; }

    }
}
