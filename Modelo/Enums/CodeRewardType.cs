using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Enums
{
    public enum CodeRewardType
    {
        /// <summary>
        /// Ninguna
        /// </summary>
        None,
        /// <summary>
        /// Abono al monedero
        /// </summary>
        WalletAmount,
        /// <summary>
        /// Envio gratis
        /// </summary>
        FreeDelivery,
        /// <summary>
        /// Porcentage de descuento
        /// </summary>
        PercentageDiscount
    }
}
