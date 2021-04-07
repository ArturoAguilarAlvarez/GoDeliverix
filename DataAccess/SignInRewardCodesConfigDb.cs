using DataAccess.Common;
using Modelo.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SignInRewardCodesConfigDb : BaseDapper
    {
        public SignInRewardCodesConfigDb()
        {

        }

        public SignInRewardCodesConfig GetConfig()
        {
            return this.QuerySingleOrDefault<SignInRewardCodesConfig>("SELECT TOP 1 * FROM SignInRewardCodesConfig", null);
        }
    }
}
