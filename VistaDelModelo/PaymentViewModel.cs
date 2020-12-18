using DBControl;
using Modelo.ApiResponse;
using Modelo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class PaymentViewModel
    {
        private PaymentDataAccess PaymentDb { get; }

        public PaymentViewModel()
        {
            this.PaymentDb = new PaymentDataAccess();
        }


        public bool Add(Payment payment)
        {
            bool result = this.PaymentDb.Registry(Guid.NewGuid(), payment.UidFormaCobro, payment.UidOrden, payment.UidEstatusCobro, payment.Monto, payment.DescuentoMonedero);

            if (result && payment.DescuentoMonedero.HasValue)
            {
                this.PaymentDb.RegistryWalletTransaction(payment.UidUsuario, TipoMovimientoUid.Retirar, ConceptoUid.PagoEnGoDeliverix, payment.UidDireccion, payment.DescuentoMonedero.Value);
            }

            return result;
        }
    }
}
