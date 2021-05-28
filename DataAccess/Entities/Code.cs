using DataAccess.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("Codes")]
    public class Codes
    {
        [Key]
        public Guid Uid { get; set; }

        [Editable(false)]
        public int IndexC { get; set; }

        [Editable(false)]
        public DateTime CreatedDate { get; set; }

        public string Code { get; set; }

        public CodeType Type { get; set; }
    }
}
