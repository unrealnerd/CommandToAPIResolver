using System.Collections.Generic;
using System.Linq;

namespace bitsmonkey.common.Search
{
    public static class ServiceMapper
    {
        public static Dictionary<int, Service> ServiceMap { get; set; }
        public static Dictionary<string, List<int>> TaggedServiceMap { get; set; }
        public static void Initialize(ServicesSettings servicesSettings)
        {
            InitServiceMap(servicesSettings);
            InitTaggedServiceMap();
        }

        private static void InitServiceMap(ServicesSettings servicesSettings)
        {
            ServiceMap = new Dictionary<int, Service>();

            for (int i = 0; i < servicesSettings.Services.Length; i++)
            {
                var service = servicesSettings.Services[i];
                ServiceMap.Add(i + 1, new Service
                {
                    Url = service.Url,
                    Tags = service.Tags
                });

                int y = 10;

                for (int j = 1; j < servicesSettings.Services[i].Services.Length + 1; j++)
                {
                    if ((j) % 10 == 0)
                    {
                        y *= 10;
                    }
                    int id = ((i + 1) * y) + j;
                    ServiceMap.Add(id, servicesSettings.Services[i].Services[j - 1]);
                }
            }
        }

        private static void InitTaggedServiceMap()
        {
            TaggedServiceMap = new Dictionary<string, List<int>>();

            foreach (var service in ServiceMap)
            {
                foreach (var tag in service.Value.Tags ?? Enumerable.Empty<string>())
                {
                    if (TaggedServiceMap.TryGetValue(tag, out List<int> value))
                    {
                        value.Add(service.Key);
                    }
                    else
                    {
                        TaggedServiceMap.Add(tag, new List<int>() { service.Key });
                    }
                }
            }
        }
    }
}