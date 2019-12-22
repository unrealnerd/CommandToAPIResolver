using System.Threading.Tasks;
namespace bitsmonkey.common.Services
{
    public interface ICustomService
    {
        Task<dynamic> Execute(string message);

        bool CanExecute(string messageKey);
    }
}
