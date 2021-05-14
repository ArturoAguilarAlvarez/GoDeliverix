using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    /// <summary>
    /// Registro de los codigos de usuario
    /// </summary>
    [Table("UserCodes")]
    public class UserCodes
    {
        public Guid Uid { get; set; }
        public Guid UserUid { get; set; }
        public Guid CodeUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ActivationDate { get; set; }
        public UserCodeStatus Status { get; set; }
    }
}
