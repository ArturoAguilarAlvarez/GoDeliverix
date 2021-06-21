using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class PromotionCodeEditView
    {
        public Guid Uid { get; set; }
        public PromotionCodeLevel Level { get; set; }
        public CodeRewardType RewardType { get; set; }
        public CodeRewardValueType ValueType { get; set; }
        public decimal Value { get; set; }
        public PromotionCodeActivationType ActivationType { get; set; }
        public int Activations { get; set; }
        public DateTime CreatedAt { get; set; }
        public PromotionCodeStatus Status { get; set; }

        public Guid CodeUid { get; set; }
        public string Code { set; get; }

        public Guid ExpirationUid { get; set; }
        public DateTime StartAt { get; set; }
        public CodeExpirationType ExpirationType { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? ActivationsLimit { get; set; }
        public int? DaysAfterActivation { get; set; }

        public Guid? CodeRegionUid { get; set; }
        public Guid? CountryUid { get; set; }
        public Guid? StateUid { get; set; }
        public Guid? MunicipalityUid { get; set; }
        public Guid? CityUid { get; set; }
        public Guid? NeighborhoodUid { get; set; }

        public Guid? CodeCompanyUid { get; set; }
        public Guid? CompanyUid { get; set; }
        public Guid? CompanyBranchUid { get; set; }

        public Guid? CodeDeliveryCompanyUid { get; set; }
        public Guid? DeliveryCompanyUid { get; set; }
        public Guid? DeliveryCompanyBranchUid { get; set; }
    }
}
