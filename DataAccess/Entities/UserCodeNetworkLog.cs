using DataAccess.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("UserCodeNetworkLog")]
    public class UserCodeNetworkLog
    {
        [Key]
        public Guid Uid { get; set; }
        public Guid UserCodeNetworkUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserCodeNetworkLogType Type { get; set; }

        public Guid? OwnerCodeNetworkUid { get; set; }
        public decimal? Amount { get; set; }
    }
}
