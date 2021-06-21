using Dapper;
using DataAccess.Enum;
using System;

namespace DataAccess.Entities
{
    [Table("PromotionCodes")]
    public class PromotionCode
    {
        [Key]
        public Guid Uid { get; set; }
        public Guid CodeUid { get; set; }
        public Guid? ExpirationUid { get; set; }
        public PromotionCodeLevel Level { get; set; }
        public CodeRewardType RewardType { get; set; }
        public CodeRewardValueType ValueType { get; set; }
        public decimal Value { get; set; }
        public PromotionCodeActivationType ActivationType { get; set; }
        public int Activations { get; set; }
        public PromotionCodeStatus Status { get; set; }
    }
}
