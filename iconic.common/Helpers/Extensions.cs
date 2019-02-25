using System;
using Newtonsoft.Json;

namespace iconic.common.Helpers
{
    public static class Extensions
    {
        public static object ToJson<T>(this string obj) 
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
