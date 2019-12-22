using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Linq;

namespace bitsmonkey.common.Services.CorporateBuzzWords
{
    [DataContract]
    public class RandomDogGeneratorResponse
    {
        [DataMember(Name = "message")]
        public string ImgUrl { get; set; }
    }

    public class RandomDogGenerator : ICustomService
    {
        public bool CanExecute(string messageKey)
        {
            return messageKey.Equals(Constant.Services.RandomDogGenerator);
        }

        public async Task<dynamic> Execute(string message)
        {
            return await GenerateRandomDog();
        }

        private async Task<dynamic> GenerateRandomDog()
        {
            RandomDogGeneratorResponse randomDogGeneratorResponse = null;

            //TODO: Make this a method which takes generic and return response from a service
            using (HttpClient _client = new HttpClient())
            {
                //TODO: Move this URL to configurations
                var randomDogStream = _client.GetStreamAsync("https://dog.ceo/api/breeds/image/random");

                randomDogGeneratorResponse = new DataContractJsonSerializer(typeof(RandomDogGeneratorResponse)).ReadObject(await randomDogStream) as RandomDogGeneratorResponse;
            }

            return new
            {
                ImageUrl = randomDogGeneratorResponse?.ImgUrl,
                Title = ExtractBreedFromUrl(randomDogGeneratorResponse?.ImgUrl),
                Template = Constant.Template.IMAGE
            };
        }

        private string ExtractBreedFromUrl(string url)
        {
            var splitUrl = url.Split('/');

            return splitUrl?.GetValue(splitUrl.GetLength(0) - 2)?.ToString();
        }
    }

}