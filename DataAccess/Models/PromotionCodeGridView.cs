using System;

namespace DataAccess.Models
{
    public class PromotionCodeGridView
    {
        public Guid Uid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartAt { get; set; }
        public string Code { get; set; }
        public int Activations { get; set; }
        public string Level { get; set; }
        public string Expiration { get; set; }
    }
}
