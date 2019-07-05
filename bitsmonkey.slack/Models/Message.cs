using System.Collections.Generic;
using Newtonsoft.Json;

namespace bitsmonkey.slack
{
    public class Event
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("ts")]
        public string Ts { get; set; }
        [JsonProperty("channel")]
        public string Channel { get; set; }
        [JsonProperty("event_ts")]
        public string EventTs { get; set; }

    }

    public class Message
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("team_id")]
        public string TeamId { get; set; }
        [JsonProperty("api_app_id")]
        public string ApiAppId { get; set; }
        [JsonProperty("event")]
        public Event @Event { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("event_id")]
        public string EventId { get; set; }
        [JsonProperty("event_time")]
        public long EventTime { get; set; }
        [JsonProperty("authed_users")]
        public List<string> AuthedUsers { get; set; }        
        [JsonProperty("challenge")]
        public string Challenge { get; set; }
    }
}