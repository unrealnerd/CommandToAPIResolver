using System.Threading.Tasks;

namespace bitsmonkey.common.Services.CopyCat
{
    public class CopyCatRepeater : ICustomService
    {
        public bool CanExecute(string messageKey)
        {
            return messageKey.Equals(Constant.Services.CopyCat);
        }

        public Task<dynamic> Execute(string message)
        {
            return Task.Run<dynamic>(() => new
            {
                Message = $"The Copy Cat says: {message}",
                Template = Constant.Template.QUOTE
            });
        }
    }
}