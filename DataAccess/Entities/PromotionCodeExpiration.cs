using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("PromotionCodeExpirations")]
    public class PromotionCodeExpiration
    {
        public Guid Uid { get; set; }
        public DateTime StartAt { get; set; }
        public CodeExpirationType Type { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? ActivationsLimit { get; set; }
        public int? DaysAfterActivation { get; set; }
    }
}
