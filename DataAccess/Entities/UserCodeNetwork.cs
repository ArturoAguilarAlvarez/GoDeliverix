using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("UserCodeNetwork")]
    public class UserCodeNetwork
    {
        [Key]
        public Guid Uid { get; set; }

        public Guid UserUid { get; set; }

        public Guid CodeUid { get; set; }

        public Guid? OwnerCodeUid { get; set; }

        [Editable(false)]
        public DateTime CreatedDate { get; set; }

        public int Activations { get; set; }

        /// <summary>
        /// Numero de veces que el usuario ha recibido la recompensa
        /// </summary>
        public int Rewards { get; set; }
    }
}
