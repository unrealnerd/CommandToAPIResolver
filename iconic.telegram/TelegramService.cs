using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace iconic.telegram
{
    public class TelegramService
    {
        private HttpClient _client { get; }
        public TelegramService(HttpClient client, IOptions<TelegramOptions> options)
        {
            client.BaseAddress = new Uri($"{options.Value.TelegramBaseAddress}{options.Value.TelegramAPIKey}");

            _client = client;
        }

        public async Task SendMessage(string message, long chatId)
        {
            await _client.GetAsync($"{_client.BaseAddress}/sendMessage?chat_id={chatId}&text={message}");
        }
    }
}