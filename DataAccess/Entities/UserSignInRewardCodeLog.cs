using Modelo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.DbContext
{
    public class UserSignInRewardCodeLog
    {
        public Guid Uid { get; set; }
        public Guid UserCodeUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserWebSharedCodeLogType Type { get; set; }
    }
}
