using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.v2
{
    [Serializable]
    public class PromotionCodeGeography
    {
        public Guid? CountryUid { get; set; } = null;
        public Guid? StateUid { get; set; } = null;
        public Guid? MunicipalityUid { get; set; } = null;
        public Guid? CityUid { get; set; } = null;
        public Guid? NeighborhoodUid { get; set; } = null;
    }
}
