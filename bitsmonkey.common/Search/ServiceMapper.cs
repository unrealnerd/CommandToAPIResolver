using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using bitsmonkey.common.Services;

namespace bitsmonkey.common.Search
{
    //TODO: make this as a singleton class and on constructor trigger init
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
                service.Id = i + 1;
                ServiceMap.Add(service.Id, service);

                int y = 10;

                for (int j = 1; j < servicesSettings.Services[i].Services.Length + 1; j++)
                {
                    if ((j) % 10 == 0)
                    {
                        y *= 10;
                    }

                    int id = ((i + 1) * y) + j;

                    var childService = servicesSettings.Services[i].Services[j - 1];

                    childService.Id = id;
                    childService.Url = service.Url + childService.Url;
                    childService.Tags ??= new string[0];
                    childService.Tags = childService.Tags.Concat(service.Tags ??= new string[] { }).ToArray();
                    childService.ResponseTemplate ??= service.ResponseTemplate ?? Constant.Template.QUOTE;

                    ServiceMap.Add(id, childService);
                }
            }
        }

        private static void InitTaggedServiceMap()
        {
            TaggedServiceMap = new Dictionary<string, List<int>>(StringComparer.InvariantCultureIgnoreCase);

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

        public static bool IsParentService(this Service s)
        {
            return s.Services?.Length > 0;
        }
    }
}