using Modelo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.DbContext
{
    public class SignInRewardCodesConfig
    {
        public Guid Uid { get; set; }

        /// <summary>
        /// Fecha en la que expira el codigo
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Numero de canjeos requeridos para aplicar promocion
        /// </summary>
        public int RedeemsRequired { get; set; }

        /// <summary>
        /// Limite de canjeos por usuario
        /// </summary>
        public int RedeemsLimit { get; set; }

        /// <summary>
        /// Tipo de recompensa del codigo usuarios principal
        /// </summary>
        public CodeRewardType ParentRewardType { get; set; }

        /// <summary>
        /// Valor de la recomplesa del usuario principal
        /// </summary>
        public decimal ParentRewardValue { get; set; }

        /// <summary>
        /// Tipo de recompensa del codigo secundario
        /// </summary>
        public CodeRewardType ChildRewardType { get; set; }

        /// <summary>
        /// Valor de la recompensa del codigo secundario
        /// </summary>
        public decimal ChildRewardValue { get; set; }
    }
}
