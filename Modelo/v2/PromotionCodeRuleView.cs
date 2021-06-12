using System;

namespace Modelo.v2
{
    [Serializable]
    public class PromotionCodeRuleView
    {
        public Guid Uid { get; set; }
        public Guid PromotionCodeUid { get; set; }
        public int Position { get; set; }

        public int Operator { get; set; }
        public string OperatorText { get; set; }

        public int ValueType { get; set; }
        public string ValueTypeText { get; set; }

        public string Value { get; set; }
        public string ValueText { get; set; }

        public int Union { get; set; }
    }
}
