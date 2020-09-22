using Modelo.Enums;
using Modelo.OneSignal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Notification
    {
        public Guid Uid { get; set; }

        public NotificationTarget Target { get; set; }

        public Guid TargetUid { get; set; }

        public string JsonTitle { get; set; }

        public NotificationTitle Title => string.IsNullOrEmpty(JsonTitle) ? null : JsonConvert.DeserializeObject<NotificationTitle>(JsonTitle);

        public string JsonContent { get; set; }

        public NotificationContent Content => string.IsNullOrEmpty(JsonContent) ? null : JsonConvert.DeserializeObject<NotificationContent>(JsonContent);

        public NotificationType Type { get; set; }

        public Guid EntityTypeUid { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
