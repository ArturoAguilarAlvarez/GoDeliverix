using Dapper;
using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("PromotionCodes")]
    public class PromotionCode
    {
        public Guid Uid { get; set; }
        public Guid CodeUid { get; set; }
        public Guid? ExpirationUid { get; set; }
        public CodeRewardType RewardType { get; set; }
        public CodeRewardValueType ValueType { get; set; }
        public decimal Value { get; set; }
        public PromotionCode ActivationType { get; set; }
        public int Activations { get; set; }
    }
}
