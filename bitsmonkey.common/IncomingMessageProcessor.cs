using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using featureprovider.core.Models;
using bitsmonkey.common.Services;
using bitsmonkey.common.Search;
using bitsmonkey.common.Models;

namespace bitsmonkey.common
{
    public class IncomingMessageProcessor : IMessageProcessor
    {
        private readonly IFeatureProvider _featureProvider;
        private readonly RestExecutioner RestExecutioner;
        public IncomingMessageProcessor(
            IFeatureProvider featureprovider,
            RestExecutioner restExecutioner
        )
        {
            _featureProvider = featureprovider;
            RestExecutioner = restExecutioner;
        }

        public async Task<dynamic> Process(IncomingMessage incomingMessage)
        {
            if (_featureProvider.Evaluate("NLUEnabled") == "true")
            {
                //TODO: Talk to NLU Service to extract Intent & Entity
            }

            if (int.TryParse(incomingMessage.Query, out int serviceId) &&
                ServiceMapper.ServiceMap.TryGetValue(serviceId, out Service service))
            {
                return await RestExecutioner.Execute(service, incomingMessage);
            }

            if (ServiceMapper.TaggedServiceMap.TryGetValue(incomingMessage.Query, out List<int> serviceIds))
            {
                var firstService = ServiceMapper.ServiceMap[serviceIds[0]];

                // if not a parent service
                if (serviceIds.Count == 1 &&
                    !firstService.IsParent)
                {
                    return await RestExecutioner.Execute(ServiceMapper.ServiceMap[serviceIds[0]], incomingMessage);
                }
                //one is parent another is child service
                else if (serviceIds.Count == 2 &&
                    (firstService.IsParent ^
                    ServiceMapper.ServiceMap[serviceIds[1]].IsParent))
                {
                    return await RestExecutioner.Execute(firstService.IsParent ? ServiceMapper.ServiceMap[serviceIds[1]] : firstService, incomingMessage);
                }
                else
                {
                    return await ReturnServiceMapResponse(serviceIds.Select(sid =>
                            ServiceMapper.ServiceMap[sid]));
                }

            }
            else
            {
                return await ReturnServiceMapResponse(ServiceMapper.ServiceMap.Values.ToArray());
            }
        }

        private async Task<dynamic> ReturnServiceMapResponse(IEnumerable<Service> services)
        {
            services.ToList().ForEach(s => s.Services = null);
            return await Task.Run<dynamic>(() => new
            {
                message = services,
                template = Constant.Template.SERVICEMAP
            });
        }
    }
}