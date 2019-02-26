using System.Threading.Tasks;
namespace iconic.common.Services
{
    public interface ICustomService
    {
        Task<string> Execute(string message);

        bool CanExecute(string messageKey);
    }
}
