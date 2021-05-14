using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Enum
{
    public enum ComparisonOperator
    {
        /// <summary>
        /// Igual
        /// </summary>
        Equality,
        /// <summary>
        /// Menor a 
        /// </summary>
        LessThan,
        /// <summary>
        /// Mayor a
        /// </summary>
        GreaterThan,
        /// <summary>
        /// Menor o igual a 
        /// </summary>
        LessThanOrEqualTo,
        /// <summary>
        /// Mayor o igual a
        /// </summary>
        GreaterThanOrEqualTo,
        /// <summary>
        /// Diferente
        /// </summary>
        Inequality
    }
}
