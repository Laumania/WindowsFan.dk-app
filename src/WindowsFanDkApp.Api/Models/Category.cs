using AgFx;
using Newtonsoft.Json;

namespace WindowsFanDkApp.Api.Models
{
    [CachePolicy(CachePolicy.CacheThenRefresh)]
    public class Category : ModelItemBase
    {
        public Category()
        {
            
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        public bool Selected { get; set; }

    }
}
