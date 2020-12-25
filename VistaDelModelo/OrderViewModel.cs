using DBControl;
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

        public OrderViewModel()
        {
            this.PaymentDb = new PaymentDataAccess();
        }

        public bool CancelOrder(Guid UidOrdenSucursal, Guid UidUsuario, Guid UidDireccion)
        {
            return this.PaymentDb.CancelOrderAndApplyDiscount(UidOrdenSucursal, UidUsuario, UidDireccion);
        }
    }
}
