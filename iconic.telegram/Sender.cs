using System.Threading.Tasks;

namespace iconic.telegram
{
    public class Sender
    {
        public TelegramService _telegramService { get; set; }
        public Sender(TelegramService telegramService)
        {
            _telegramService = telegramService;
        }
        public async Task SendMessage(string body, long chatId)
        {
            await _telegramService.SendMessage(body, chatId);
        }
    }
}