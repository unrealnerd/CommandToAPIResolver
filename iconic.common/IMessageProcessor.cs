using System.Threading.Tasks;

namespace iconic.common.Services
{
    public interface IMessageProcessor
    {
         Task<string> Process(string message);
    }
}