using System.Collections.Generic;
using bitsmonkey.common.Services;
using System.Linq;

namespace bitsmonkey.common.Search
{
    public static class Transformer
    {
        public static object ToMappings(this Dictionary<string, object> data, IDictionary<string, string> mappings)
        {
            if (mappings == null)
                return data;

            return TransformJObject(data, mappings);

        }

        public static object ToMappings(this List<Dictionary<string, object>> data, IDictionary<string, string> mappings)
        {
            if (mappings == null)
                return data;
                
            return TransformJArray(data, mappings);
        }

        private static Dictionary<string, object> TransformJObject(Dictionary<string, object> data, IDictionary<string, string> mappings)
        {
            var transformedResponse = new Dictionary<string, object>();
            mappings.ToList().ForEach(m => transformedResponse.Add(m.Key, data[m.Value]));

            return transformedResponse;
        }

        private static List<Dictionary<string, object>> TransformJArray(List<Dictionary<string, object>> data, IDictionary<string, string> mappings)
        {
            var transformedResponse = new List<Dictionary<string, object>>();

            data.ForEach(r => transformedResponse.Add(TransformJObject(r, mappings)));

            return transformedResponse;
        }
    }
}