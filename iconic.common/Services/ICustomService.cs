using System.Threading.Tasks;
namespace iconic.common.Services
{
    public interface ICustomService
    {
        Task<string> Execute();

        bool CanExecute(string message);
    }
}
