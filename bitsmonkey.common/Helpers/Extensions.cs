using System;
using System.Text.Json;

namespace bitsmonkey.common.Helpers
{
    public static class Extensions
    {
        public static object ToJson<T>(this string obj) 
        {
            return JsonSerializer.Deserialize<T>(obj);
        }
    }
}
