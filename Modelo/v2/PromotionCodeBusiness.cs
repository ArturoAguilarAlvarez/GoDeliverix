using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.v2
{
    [Serializable]
    public class PromotionCodeBusiness
    {
        public Guid? UidCompany { get; set; }
        public Guid? UidCompanyBranch { get; set; }
    }
}
