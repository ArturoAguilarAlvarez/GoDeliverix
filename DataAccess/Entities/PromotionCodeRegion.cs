using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("PromotionCodeRegions")]
    public class PromotionCodeRegion
    {
        public Guid Uid { get; set; }
        public Guid PromotionCodeUid { get; set; }
        public Guid CountryUid { get; set; }
        public Guid? StateUid { get; set; }
        public Guid? MunicipalityUid { get; set; }
        public Guid? CityUid { get; set; }   
        public Guid? NeighborhoodUid { get; set; }   
        public int Type { get; set; }
    }
}
