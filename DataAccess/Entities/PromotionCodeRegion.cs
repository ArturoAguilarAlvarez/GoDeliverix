using Dapper;
using System;

namespace DataAccess.Entities
{
    [Table("PromotionCodeRegions")]
    public class PromotionCodeRegion
    {
        [Key]
        public Guid Uid { get; set; }
        public Guid PromotionCodeUid { get; set; }
        public Guid CountryUid { get; set; }
        public Guid? StateUid { get; set; }
        public Guid? MunicipalityUid { get; set; }
        public Guid? CityUid { get; set; }   
        public Guid? NeighborhoodUid { get; set; }   

        [IgnoreInsert]
        public int Type { get; set; }
    }
}
