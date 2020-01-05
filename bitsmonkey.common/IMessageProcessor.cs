using System.Threading.Tasks;
using bitsmonkey.common.Models;

namespace bitsmonkey.common.Services
{
    public interface IMessageProcessor
    {
        Task<dynamic> Process(IncomingMessage incomingMessage);
    }
}