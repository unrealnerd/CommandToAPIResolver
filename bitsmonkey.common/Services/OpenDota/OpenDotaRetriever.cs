using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace bitsmonkey.common.Services.CorporateBuzzWords
{
    [DataContract]
    public class PromatchesResponse
    {
        [DataMember(Name = "match_id")]
        public long MatchId { get; set; }

        [DataMember(Name = "duration")]
        public int Duration { get; set; }

        [DataMember(Name = "radiant_name")]
        public string RadiantName { get; set; }

        [DataMember(Name = "dire_name")]
        public string DireName { get; set; }

        [DataMember(Name = "radiant_score")]
        public int RadiantScore { get; set; }

        [DataMember(Name = "dire_score")]
        public int DireScore { get; set; }
    }

    public class OpenDotaRetriever : ICustomService
    {
        public bool CanExecute(string messageKey)
        {
            return messageKey.Equals(Constant.Services.OpenDotaRetriever);
        }

        public async Task<dynamic> Execute(string message)
        {
            return await GetPromatches();
        }

        private async Task<dynamic> GetPromatches()
        {
            PromatchesResponse[] promatchesResponse = null;

            //TODO: Make this a method which takes generic and return response from a service
            using (HttpClient _client = new HttpClient())
            {
                //TODO: Move this URL to configurations
                var randomBuzzStream = _client.GetStreamAsync("https://api.opendota.com/api/proMatches");

                promatchesResponse = new DataContractJsonSerializer(typeof(PromatchesResponse[])).ReadObject(await randomBuzzStream) as PromatchesResponse[];
            }
            return new
            {
                Message = promatchesResponse,
                Template = Constant.Template.DATAGRID
            };
        }
    }

}