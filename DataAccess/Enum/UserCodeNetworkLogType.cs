using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Enum
{
    public enum UserCodeNetworkLogType
    {
        /// <summary>
        /// El codigo fue creado
        /// </summary>
        Created,
        /// <summary>
        /// El codigo fue compartido (otro usuario lo activo)
        /// </summary>
        Shared,
        /// <summary>
        /// El usuario recibio la recompesa
        /// </summary>
        Rewarded,
        /// <summary>
        /// El usuario dio la recompensa
        /// </summary>
        Give
    }
}
