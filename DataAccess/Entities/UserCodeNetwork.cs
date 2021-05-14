using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("UserCodeNetwork")]
    public class UserCodeNetwork
    {
        public Guid Uid { get; set; }
        public Guid UserUid { get; set; }
        public Guid CodeUid { get; set; }
        public Guid OwnerCodeUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Activations { get; set; }
    }
}
