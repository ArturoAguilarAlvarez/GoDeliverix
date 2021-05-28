using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Util
{
    public static class DateTimeUtils
    {
        public static DateTime GetTimeZoneDate()
        {
            DateTime tNow = DateTime.Now;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            return TimeZoneInfo.ConvertTime(tNow, tzi);
        }
    }
}
