using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("UserCodeConfig")]
    public class UserCodeConfig
    {
        public Guid Uid { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ShareLimit { get; set; }
        public int Type { get; set; }
        public int OwnerRewardType { get; set; }
        public int OwnerRewardValueType { get; set; }
        public decimal OwnerRewardValue { get; set; }
        public int GuestRewardType { get; set; }
        public int GuestRewardValueType { get; set; }
        public decimal GuestRewardValue { get; set; }
    }
}
