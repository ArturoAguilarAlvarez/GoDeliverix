using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("Codes")]
    public class Codes
    {
        public Guid Uid { get; set; }
        public int Index { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Code { get; set; }
        public int Type { get; set; }
    }
}
