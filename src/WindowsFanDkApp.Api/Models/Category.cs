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

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("post_count")]
        public int PostCount { get; set; }

        [JsonProperty("parent")]
        public int Parent { get; set; }
    }
}
