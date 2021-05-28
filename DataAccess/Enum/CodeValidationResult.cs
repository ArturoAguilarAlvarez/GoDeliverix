using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Enum
{
    public enum CodeValidationResult
    {
        Error = -3,
        UserHasCode = -2,
        CodeNotFound = -1,
        Expired = 0,
        Activated,
        ActivatedAndRewarded,
        Valid,
        Invalid
    }
}
