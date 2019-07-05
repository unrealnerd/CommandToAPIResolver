using System.Threading.Tasks;

namespace bitsmonkey.common.Services.CopyCat
{
    public class CopyCatRepeater : ICustomService
    {
        public bool CanExecute(string messageKey)
        {
            return messageKey.Equals(Constants.CopyCat);
        }

        public Task<string> Execute(string message)
        {
            return Task.Run(() => $"The Copy Cat says: {message}");
        }
    }
}