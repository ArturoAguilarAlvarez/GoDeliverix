using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.OneSignal
{
    public class NotificationContent
    {
        [MaxLength(60)]
        [JsonProperty("es")]
        public string Es { get; set; }

        [MaxLength(60)]
        [JsonProperty("en")]
        public string En { get; set; }
    }
}
