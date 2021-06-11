using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Serializable]
    [Table("CodeRules")]
    public class PromotionCodeRule
    {
        public Guid Uid { get; set; }
        public Guid CodeUid { get; set; }
        public int Position { get; set; }
        public ComparisonOperator Operator { get; set; }
        public PromotionCodeRuleValueType ValueType { get; set; }
        public string Value { get; set; }
        public LogicalOperator Union { get; set; }
    }
}
