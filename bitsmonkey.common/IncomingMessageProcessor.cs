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

        public async Task<dynamic> Process(string message)
        {
            ICustomService customService = null;

            if (_featureProvider.Evaluate("NLUEnabled") == "true")
            {
                //TODO: Talk to NLU Service to extract Intent & Entity
            }

            switch (message.Split('/')[0])
            {
                case Constant.Services.CorporateBullShitBuzzWord:
                    customService = _customServices.Where(cs => cs.CanExecute(Constant.Services.CorporateBullShitBuzzWord)).First();
                    break;
                case Constant.Services.RandomDogGenerator:
                    customService = _customServices.Where(cs => cs.CanExecute(Constant.Services.RandomDogGenerator)).First();
                    break;
                case Constant.Services.OpenDotaRetriever:
                    customService = _customServices.Where(cs => cs.CanExecute(Constant.Services.OpenDotaRetriever)).First();
                    break;
                default:
                    customService = _customServices.Where(cs => cs.CanExecute(Constant.Services.CopyCat)).First();
                    break;
            }

            return await customService.Execute(message);
        }
    }
}