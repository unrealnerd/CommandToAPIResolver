using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace bitsmonkey.common.Services.CorporateBuzzWords
{    
    [DataContract]
    public class RandomDogGeneratorResponse
    {
        [DataMember(Name="message")]
        public string ImgUrl { get; set; }
    }

    public class RandomDogGenerator : ICustomService
    {
        public bool CanExecute(string messageKey)
        {
            return messageKey.Equals(Constants.RandomDogGenerator);
        }

        public async Task<string> Execute(string message)
        {
            return await GenerateRandomDog();
        }

        private async Task<string> GenerateRandomDog()
        {
            RandomDogGeneratorResponse randomDogGeneratorResponse = null;
            
            //TODO: Make this a method which takes generic and return response from a service
            using (HttpClient _client = new HttpClient())
            {
                //TODO: Move this URL to configurations
                var randomDogStream = _client.GetStreamAsync("https://dog.ceo/api/breeds/image/random");

                randomDogGeneratorResponse = new DataContractJsonSerializer(typeof(RandomDogGeneratorResponse)).ReadObject(await randomDogStream) as RandomDogGeneratorResponse;
            }
            return randomDogGeneratorResponse?.ImgUrl;
        }
    }

}