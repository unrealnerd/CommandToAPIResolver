using System;
using System.Collections.Generic;
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

        public Task Process(string message)
        {
            return null;
        }
    }
}