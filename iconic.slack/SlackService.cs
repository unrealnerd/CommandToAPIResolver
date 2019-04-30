using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace iconic.slack
{
    public class SlackService
    {
        private HttpClient _client { get; }
        public SlackService(HttpClient client, IOptions<SlackOptions> options)
        {
            client.BaseAddress = new Uri($"{options.Value.SlackBaseAddress}{options.Value.SlackAPIKey}");

            _client = client;
        }

        public async Task SendMessage(string message, long chatId)
        {
            await _client.PostAsync($"{_client.BaseAddress}/chat.postMessage",new HttpContent{  });
        }
    }
}
