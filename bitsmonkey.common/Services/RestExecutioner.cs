using System.Net.Http;
using System.Threading.Tasks;
using bitsmonkey.common.Search;
using System.Text.Json;
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
                // incase an api os called without the input to it return back specifying the template to fill
                if (incomingMessage.Request == null)
                {
                    return new
                    {
                        Message = "Requires message body to post",
                        RequestTemplate = service.Request.Template,// template used to create the request for the API
                        Template = service.ResponseTemplate// the template to be used select the dynamic component type
                    };
                }
                else
                {
                    response = await ExecutePostMethod(service, incomingMessage);
                }

            }

            //response can be an array of json objects or a single one
            object result = service.Response != null && service.Response.IsArray ?
                JsonSerializer.Deserialize<List<Dictionary<string, object>>>(response).ToMappings(service.Response.Mappings) :
                JsonSerializer.Deserialize<Dictionary<string, object>>(response).ToMappings(service.Response?.Mappings);

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
                Content = new StringContent(JsonSerializer.Serialize(incomingMessage.Request["body"]), Encoding.UTF8, service.MediaType)
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