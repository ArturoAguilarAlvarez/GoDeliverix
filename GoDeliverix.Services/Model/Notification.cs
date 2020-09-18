using Dapper;
using GoDeliverix.Services.Enum;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GoDeliverix.Services.Model
{
    [Table("Notifications")]
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

    public class NotificationTitle
    {
        [MaxLength(20)]
        public string Es { get; set; }

        [MaxLength(20)]
        public string En { get; set; }
    }

    public class NotificationContent
    {
        [MaxLength(60)]
        public string Es { get; set; }

        [MaxLength(60)]
        public string En { get; set; }
    }
}
