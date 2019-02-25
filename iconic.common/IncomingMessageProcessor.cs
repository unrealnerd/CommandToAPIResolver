using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iconic.common.Services;

namespace iconic.common
{
    public class IncomingMessageProcessor : IMessageProcessor
    {
        private readonly IEnumerable<ICustomService> _customServices;
        public IncomingMessageProcessor(IEnumerable<ICustomService> customServices)
        {
            _customServices = customServices;
        }

        public async Task<string> Process(string message)
        {
            ICustomService customService = null;

            switch (message.Split('/')[0])
            {
                case Constants.CorporateBullShitBuzzWord:
                    customService = _customServices.Where(cs=>cs.CanExecute(Constants.CorporateBullShitBuzzWord)).First();
                    break;
                default:
                    break;
            }

            return await customService?.Execute();
        }
    }
}