using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class OrderHistory
    {
        /// <summary>
        /// Primary Key [OrdenSucursal]
        /// </summary>
        public Guid OrderUid { get; set; }
        /// <summary>
        /// Primary Key de la sucursal
        /// </summary>
        public Guid BrancheUid { get; set; }
        public Guid StatusUid { get; set; }
        /// <summary>
        /// Primary key de la empresa
        /// </summary>
        public Guid CompanyUid { get; set; }
        public string Company { get; set; }
        public string CompanyImg { get; set; }
        public string Branch { get; set; }
        public string BranchFolio { get; set; }
        public string DeliveryCode { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public decimal Tips { get; set; }
        public decimal Delivery { get; set; }
        public decimal? WalletDiscount { get; set; }
        public decimal CardPaymentComission { get; set; }
        public decimal DeliveryCardPaymentComission { get; set; }
        public bool IncludeCPTD { get; set; }
        public bool IncludeCPTS { get; set; }
        public List<ProductOrderHistory> Products { get; set; }
        public int ProductCount { get { return this.Products.Sum(p => p.Quantity); } }

        public OrderHistory()
        {
            this.Products = new List<ProductOrderHistory>();
        }
    }
}
