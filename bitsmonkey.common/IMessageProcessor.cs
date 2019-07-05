using System.Threading.Tasks;

namespace bitsmonkey.common.Services
{
    public interface IMessageProcessor
    {
         Task<string> Process(string message);
    }
}