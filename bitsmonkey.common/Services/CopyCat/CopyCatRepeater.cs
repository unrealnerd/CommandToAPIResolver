using System.Threading.Tasks;

namespace bitsmonkey.common.Services.CopyCat
{
    public class CopyCatRepeater : ICustomService
    {
        public bool CanExecute(string messageKey)
        {
            return messageKey.Equals(Constants.CopyCat);
        }

        public Task<dynamic> Execute(string message)
        {
            return Task.Run<dynamic>(() => $"The Copy Cat says: {message}");
        }
    }
}