using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("UserCodeNetworkConfig")]
    public class UserCodeNetworkConfig
    {
        public Guid Uid { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? UserShareLimit { get; set; }
        public int? GlobalShareLimit { get; set; }
        public int Type { get; set; }
        public CodeRewardType OwnerRewardType { get; set; }
        public CodeRewardValueType OwnerRewardValueType { get; set; }
        public decimal OwnerRewardValue { get; set; }
        public CodeRewardType GuestRewardType { get; set; }
        public CodeRewardValueType GuestRewardValueType { get; set; }
        public decimal GuestRewardValue { get; set; }
    }
}
