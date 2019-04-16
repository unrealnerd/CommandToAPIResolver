using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types;

namespace iconic.api.Medium.Telegram
{
    public class TelegramService
    {
        private HttpClient _client { get; }
        public TelegramService(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration["Telegram:BaseAddress"]);

            _client = client;
        }

        public async Task SendMessage(string message, string chatId)
        {
            await _client.GetStringAsync($"/sendMessage?chat_id={chatId}&text={message}");
        }
    }
}