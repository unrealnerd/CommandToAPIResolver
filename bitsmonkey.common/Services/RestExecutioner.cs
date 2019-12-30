using System.Net.Http;
using System.Threading.Tasks;
using bitsmonkey.common.Search;
using Newtonsoft.Json;

namespace bitsmonkey.common.Services
{
    public class RestExecutioner
    {
        public async Task<dynamic> Execute(Service service)
        {
            var response = string.Empty;

            if (string.IsNullOrEmpty(service.Method) ||
                service.Method.Equals("GET", System.StringComparison.InvariantCultureIgnoreCase))
            {
                response = await ExecuteGetMethod(service);
            }

            return new
            {
                Message = JsonConvert.DeserializeObject<dynamic>(response),
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
    }
}