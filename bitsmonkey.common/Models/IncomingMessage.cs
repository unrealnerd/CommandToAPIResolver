using System.Collections.Generic;

namespace bitsmonkey.common.Models
{
    public class IncomingMessage
    {
        public string Command { get; set; }
        public IDictionary<string, object> Request { get; set; }
    }
}