using DataAccess.Common;
using DataAccess.Entities;
using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class UserCodeNetworkService : BaseDapper
    {
        public CodeValidationResult VerifyCode(string Code)
        {
            UserCodeNetworkConfig config = this.QuerySingleOrDefault<UserCodeNetworkConfig>("SELECT TOP 1 * FROM UserCodeNetworkConfig", null);

            if (config == null)
                return CodeValidationResult.Error;

            if (config.ExpirationDate.HasValue)
                if (DateTime.Compare(config.ExpirationDate.Value, DateTime.UtcNow) < 0)
                    return CodeValidationResult.Expired;


            return CodeValidationResult.Activated;
        }
    }
}
