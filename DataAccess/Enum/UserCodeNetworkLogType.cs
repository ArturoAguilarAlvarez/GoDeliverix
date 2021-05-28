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
        /// El codigo del usuario fue registrado por otra persona
        /// </summary>
        Shared,

        /// <summary>
        /// El codigo del owner fue asociado
        /// </summary>
        AssociateOwner,

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
