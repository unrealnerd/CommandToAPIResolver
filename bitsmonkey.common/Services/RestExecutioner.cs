using System.Net.Http;
using System.Threading.Tasks;
using bitsmonkey.common.Search;
using System.Text.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Dynamic;
using System.Text;
using bitsmonkey.common.Models;

namespace bitsmonkey.common.Services
{
    public class RestExecutioner
    {
        public async Task<dynamic> Execute(Service service, IncomingMessage incomingMessage)
        {
            var response = string.Empty;

            if (string.IsNullOrEmpty(service.Method) ||
                service.Method.Equals("GET", System.StringComparison.InvariantCultureIgnoreCase))
            {
                response = await ExecuteGetMethod(service);
            }
            else if (string.IsNullOrEmpty(service.Method) ||
                service.Method.Equals("POST", System.StringComparison.InvariantCultureIgnoreCase))
            {
                response = await ExecutePostMethod(service, incomingMessage);
            }

            object result = service.Response != null && service.Response.IsArray ?
                JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(response).ToMappings(service.Response.Mappings) :
                JsonConvert.DeserializeObject<Dictionary<string, object>>(response).ToMappings(service.Response?.Mappings);

            return new
            {
                Message = result,
                Template = service.ResponseTemplate
            };

        }

        private async Task<dynamic> ExecuteGetMethod(Service service)
        {
            var response = string.Empty;

            using (HttpClient _client = new HttpClient())
            {
                response = await _client.GetStringAsync(service.Url);
            }

            return response;
        }

        private async Task<dynamic> ExecutePostMethod(Service service, IncomingMessage incomingMessage)
        {
            var response = string.Empty;
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, service.Url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(incomingMessage.Request["body"]), Encoding.UTF8, service.MediaType)
            };

            using (HttpClient _client = new HttpClient())
            {
                var res = await _client.SendAsync(requestMessage);
                response = res.IsSuccessStatusCode ? await res.Content.ReadAsStringAsync() : response;
            }

            return response;
        }
    }
}