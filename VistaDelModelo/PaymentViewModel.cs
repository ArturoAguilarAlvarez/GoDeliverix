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


        public bool AddBranchePayment(BranchePayment payment)
        {
            bool result = this.PaymentDb.RegistryBranchePayment(payment.UidOrden, payment.UidUsuario, payment.UidDireccion, payment.UidSucursal, payment.UidRelacionOrdenSucursal, payment.UidTarifario, payment.CodigoEntrega, payment.Monto, payment.MontoSucursal, payment.IncludeCPTD, payment.IncludeCPTS, payment.DescuentoMonedero, payment.ComisionTarjeta, payment.ComisionTarjetaRepartidor);

            // Este proceso se realiza desde el backend
            //if (result && payment.DescuentoMonedero.HasValue)
            //{
            //    this.PaymentDb.RegistryWalletTransaction(payment.UidUsuario, TipoMovimientoUid.Retirar, ConceptoUid.PagoEnGoDeliverix, payment.UidDireccion, payment.DescuentoMonedero.Value);
            //}

            return result;
        }
    }
}
