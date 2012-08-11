using AgFx;
using Newtonsoft.Json;

namespace WindowsPhoneFanDkApp.Api.Models
{
    [CachePolicy(CachePolicy.CacheThenRefresh)]
    public class Author : ModelItemBase
    {
        public Author()
        {
            
        }


        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("first_name")]
        public string First_name { get; set; }

        [JsonProperty("last_name")]
        public string Last_name { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }


    }
}
