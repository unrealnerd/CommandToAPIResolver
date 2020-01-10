using System.Collections.Generic;

namespace bitsmonkey.common.Models
{
    public class IncomingMessage
    {
        public string Query { get; set; }
        public IDictionary<string, object> Request { get; set; }
    }
}