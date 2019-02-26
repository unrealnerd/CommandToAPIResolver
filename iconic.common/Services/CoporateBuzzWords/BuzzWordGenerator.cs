using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace iconic.common.Services.CorporateBuzzWords
{    
    [DataContract]
    public class CorporateBuzzWordResponse
    {
        [DataMember(Name="phrase")]
        public string Phrase { get; set; }        
    }

    public class BuzzWordGenerator : ICustomService
    {
        public bool CanExecute(string messageKey)
        {
            return messageKey.Equals(Constants.CorporateBullShitBuzzWord);
        }

        public async Task<string> Execute(string message)
        {
            return await GenerateRandomBuzz();
        }

        private async Task<string> GenerateRandomBuzz()
        {
            CorporateBuzzWordResponse randomBuzzResponse = null;
            
            using (HttpClient _client = new HttpClient())
            {
                var randomBuzzStream = _client.GetStreamAsync("https://corporatebs-generator.sameerkumar.website");

                randomBuzzResponse = new DataContractJsonSerializer(typeof(CorporateBuzzWordResponse)).ReadObject(await randomBuzzStream) as CorporateBuzzWordResponse;
            }
            return randomBuzzResponse?.Phrase;
        }
    }

}