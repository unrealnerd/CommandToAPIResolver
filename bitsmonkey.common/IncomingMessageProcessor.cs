using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using featureprovider.core.Models;
using bitsmonkey.common.Services;

namespace bitsmonkey.common
{
    public class IncomingMessageProcessor : IMessageProcessor
    {
        private readonly IEnumerable<ICustomService> _customServices;
        private readonly IFeatureProvider _featureProvider;
        public IncomingMessageProcessor(IEnumerable<ICustomService> customServices,
         IFeatureProvider featureprovider
        )
        {
            _customServices = customServices;
            _featureProvider = featureprovider;
        }

        public async Task<string> Process(string message)
        {
            ICustomService customService = null;

            if(_featureProvider.Evaluate("NLUEnabled") == "true")
            {
                //TODO: Talk to NLU Service to extract Intent & Entity
            }

            switch (message.Split('/')[0])
            {
                case Constants.CorporateBullShitBuzzWord:
                    customService = _customServices.Where(cs=>cs.CanExecute(Constants.CorporateBullShitBuzzWord)).First();
                    break;
                default:
                    customService = _customServices.Where(cs=>cs.CanExecute(Constants.CopyCat)).First();
                    break;
            }

            return await customService.Execute(message);
        }
    }
}