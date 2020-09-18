using Dapper;
using GoDeliverix.Services.Data.Common;
using GoDeliverix.Services.Enum;
using GoDeliverix.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoDeliverix.Services.Data
{
    public class NotificationsData : BaseDapper
    {
        public NotificationsData() { 
        }


        public bool Add(Notification notification)
        {
            Guid uid = this.Insert<Notification>(notification);
            return uid == Guid.Empty ? false : true;
        }

        public IEnumerable<Notification> ReadAllNotifications(int target, Guid uid)
        {
            string query = "SELECT * FROM [Notifications] WHERE [Target] = @Target AND [TargetUid] = @Uid ORDER BY [CreatedDate] DESC";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Target", (int)target);
            parameters.Add("@Uid", uid);

            var rows = this.Query<Notification>(query, parameters);
            return rows.Any() ? rows.AsEnumerable() : new HashSet<Notification>();
        }
    }
}
