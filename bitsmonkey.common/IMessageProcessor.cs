using System.Threading.Tasks;

namespace bitsmonkey.common.Services
{
    public interface IMessageProcessor
    {
         Task<dynamic> Process(string message);
    }
}