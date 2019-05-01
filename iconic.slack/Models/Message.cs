using System.Collections.Generic;

namespace iconic.slack
{
    public class Event
    {
        public string type { get; set; }
        public string user { get; set; }
        public string text { get; set; }
        public string ts { get; set; }
        public string channel { get; set; }
        public string event_ts { get; set; }

    }

    public class Message
    {
        public string token { get; set; }
        public string team_id { get; set; }
        public string api_app_id { get; set; }
        public Event @event { get; set; }
        public string type { get; set; }
        public string event_id { get; set; }
        public long event_time { get; set; }
        public List<string> authed_users { get; set; }        
        public string challenge { get; set; }
    }
}