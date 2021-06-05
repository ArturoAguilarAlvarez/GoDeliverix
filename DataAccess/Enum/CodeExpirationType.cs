using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Enum
{
    public enum CodeExpirationType
    {
        /// <summary>
        /// No tiene expiracion
        /// </summary>
        None,
        /// <summary>
        /// Fecha de expiracion
        /// </summary>
        Date,
        /// <summary>
        /// Expira con un numero determinado de activaciones
        /// </summary>
        Activations,
        /// <summary>
        /// Expira en n dias despues de la activacion
        /// </summary>
        DaysBeforeActivations
    }
}
