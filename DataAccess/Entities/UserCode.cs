using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("UserCodes")]
    public class UserCode
    {
        public Guid Uid { get; set; }
        public Guid UserGuid { get; set; }
        public Guid CodeUid { get; set; }
        public Guid OwnerUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TimesShared { get; set; }
        public int Type { get; set; }
    }
}
