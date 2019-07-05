using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace bitsmonkey.slack
{
    public class SlackService
    {
        private HttpClient _client { get; }
        private IOptions<SlackOptions> Options { get; }
        public SlackService(HttpClient client, IOptions<SlackOptions> options)
        {
            Options = options;
            client.BaseAddress = new Uri($"{options.Value.SlackBaseAddress}");

            _client = client;
        }

        public async Task SendMessage(string message, string channel)
        {           
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Options.Value.SlackAPIKey);
            await _client.PostAsync($"{_client.BaseAddress}/chat.postMessage", new StringContent(JsonConvert.SerializeObject(new
                {                    
                    Text = message,
                    Channel = channel

                }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }), Encoding.UTF8, "application/json"));
        }
    }
}
