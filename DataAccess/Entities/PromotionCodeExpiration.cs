using DataAccess.Enum;
using System;
using Dapper;

namespace DataAccess.Entities
{
    [Table("PromotionCodeExpirations")]
    public class PromotionCodeExpiration
    {
        [Key]
        public Guid Uid { get; set; }
        public DateTime StartAt { get; set; }
        public CodeExpirationType Type { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? ActivationsLimit { get; set; }
        public int? DaysAfterActivation { get; set; }
    }
}
