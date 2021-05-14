using DataAccess;
using DataAccess.Models;
using DBControl;
using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class OrderViewModel
    {
        private PaymentDataAccess PaymentDb { get; }
        private OrderDb OrderDb { get; }

        public OrderViewModel()
        {
            this.PaymentDb = new PaymentDataAccess();
            this.OrderDb = new OrderDb();
        }

        public bool CancelOrder(Guid UidOrdenSucursal, Guid UidUsuario, Guid UidDireccion)
        {
            return this.PaymentDb.CancelOrderAndApplyDiscount(UidOrdenSucursal, UidUsuario, UidDireccion);
        }

        public CommonListViewSource<PurchaseHistory> ReadAllUserPurchases(Guid UidUser)
        {
            return this.OrderDb.ReadAllUserPurchases(UidUser);
        }
    }
}
