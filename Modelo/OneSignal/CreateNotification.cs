using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.OneSignal
{
    public class CreateNotification
    {
        [JsonProperty("app_id")]
        public string ApplicationId { get; set; }

        [JsonProperty("included_segments")]
        public IEnumerable<string> IncludedSegments { get; set; }

        /// <summary>
        /// Usar minusculas
        /// </summary>
        [JsonProperty("include_external_user_ids")]
        public IEnumerable<string> IncludeExternalUserIds { get; set; }

        [JsonProperty("headings")]
        public NotificationTitle Title { get; set; }

        [JsonProperty("contents")]
        public NotificationContent Content { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        public CreateNotification()
        {
            this.IncludedSegments = new HashSet<string>();
            this.IncludeExternalUserIds = new HashSet<string>();
            this.Data = new { };
        }
    }
}
