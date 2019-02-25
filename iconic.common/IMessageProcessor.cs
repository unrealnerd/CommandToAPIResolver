using System.Threading.Tasks;

namespace iconic.common.Services
{
    public interface IMessageProcessor
    {
         Task Process(string message);
    }
}