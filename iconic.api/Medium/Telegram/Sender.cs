using System.Threading.Tasks;
using iconic.api.Medium.Telegram;
using Microsoft.Extensions.Configuration;

namespace Medium.Telegram
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