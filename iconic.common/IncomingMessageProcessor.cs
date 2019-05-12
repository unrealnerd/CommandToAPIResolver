using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using featureprovider.core.Models;
using iconic.common.Services;

namespace iconic.common
{
    public class IncomingMessageProcessor : IMessageProcessor
    {
        private readonly IEnumerable<ICustomService> _customServices;
        private readonly IEnumerable<IFeatureEvaluator> _featureEvaluators;
        public IncomingMessageProcessor(IEnumerable<ICustomService> customServices, IEnumerable<IFeatureEvaluator> featureEvaluators)
        {
            _customServices = customServices;
            _featureEvaluators = featureEvaluators;
        }

        public async Task<string> Process(string message)
        {
            ICustomService customService = null;

            if(new FeatureProvider(_featureEvaluators).Evaluate("Features:NLUEnabled") == "true")
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