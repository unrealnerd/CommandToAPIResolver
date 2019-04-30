using Newtonsoft.Json;

namespace iconic.slack.Models
{
    public class AppMention
    {
        public string Type { get; set; }
        public string User { get; set; }
        public string Text { get; set; }

        [JsonProperty(PropertyName = "ts")]
        public string TimeStamp { get; set; }
        public string Channel { get; set; }

        [JsonProperty(PropertyName = "event_ts")]
        public string EventTimeStamp { get; set; }

    }
}