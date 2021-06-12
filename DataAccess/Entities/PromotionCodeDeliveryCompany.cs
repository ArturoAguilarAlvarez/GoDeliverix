using System;
using Dapper;


namespace DataAccess.Entities
{
    [Table("PromotionCodeDeliveryCompanies")]
    public class PromotionCodeDeliveryCompany
    {
        public Guid Uid { get; set; }
        public Guid PromotionCodeUid { get; set; }
        public Guid CompanyUid { get; set; }
        public Guid? BranchUid { get; set; }

        [IgnoreInsert]
        [IgnoreUpdate]
        public int Level { get; set; }

        [IgnoreInsert]
        [IgnoreUpdate]
        public DateTime CreatedAt { get; set; }
    }
}
