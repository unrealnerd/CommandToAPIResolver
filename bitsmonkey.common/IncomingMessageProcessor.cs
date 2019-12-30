using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using featureprovider.core.Models;
using bitsmonkey.common.Services;
using bitsmonkey.common.Search;

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

        public async Task<dynamic> Process(string message)
        {
            if (_featureProvider.Evaluate("NLUEnabled") == "true")
            {
                //TODO: Talk to NLU Service to extract Intent & Entity
            }

            if (ServiceMapper.TaggedServiceMap.TryGetValue(message, out List<int> serviceIds))
            {
                // if its only one service is returned for the tag then execute otherwise list the list the services that falls under that tag
                if (serviceIds.Count == 1 &&
                    (ServiceMapper.ServiceMap[serviceIds[0]]?.Services == null ||
                    ServiceMapper.ServiceMap[serviceIds[0]]?.Services.Length == 1)
                    )
                {
                    if (ServiceMapper.ServiceMap[serviceIds[0]].Services.Length == 1)
                        return await RestExecutioner.Execute(ServiceMapper.ServiceMap[serviceIds[0]].Services[0]);
                    else
                        return await RestExecutioner.Execute(ServiceMapper.ServiceMap[serviceIds[0]]);
                }
                else
                {
                    return await Task.Run<dynamic>(() => new
                    {
                        message = serviceIds.Select(sid => new
                        {
                            id = sid,
                            service = ServiceMapper.ServiceMap[sid]
                        }),
                        template = Constant.Template.SERVICEMAP
                    });
                }

            }
            else
            {
                return await Task.Run<dynamic>(() => new
                {
                    message = ServiceMapper.ServiceMap,
                    template = Constant.Template.SERVICEMAP
                });
            }
        }
    }
}