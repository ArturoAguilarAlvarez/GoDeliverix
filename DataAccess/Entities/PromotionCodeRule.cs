using DataAccess.Enum;
using System;
using Dapper;

namespace DataAccess.Entities
{
    [Table("PromotionCodeRules")]
    public class PromotionCodeRule
    {
        [Key]
        public Guid Uid { get; set; }
        public Guid PromotionCodeUid { get; set; }
        public int Position { get; set; }
        public ComparisonOperator Operator { get; set; }
        public PromotionCodeRuleValueType ValueType { get; set; }
        public string Value { get; set; }
        public LogicalOperator Union { get; set; }
    }
}
