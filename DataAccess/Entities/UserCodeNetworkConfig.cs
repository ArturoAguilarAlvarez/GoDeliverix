using DataAccess.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("UserCodeNetworkConfig")]
    public class UserCodeNetworkConfig
    {
        [Key]
        public Guid Uid { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? UserShareLimit { get; set; }
        public int? GlobalShareLimit { get; set; }

        // -- Parent 
        public CodeRewardType OwnerRewardType { get; set; }
        public CodeRewardValueType OwnerRewardValueType { get; set; }
        public decimal OwnerRewardValue { get; set; }
        // -- Parent 

        // -- Owner 
        public CodeRewardType GuestRewardType { get; set; }
        public CodeRewardValueType GuestRewardValueType { get; set; }
        public decimal GuestRewardValue { get; set; }
        // -- Owner
    }
}
