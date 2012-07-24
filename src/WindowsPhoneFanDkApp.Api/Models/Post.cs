using AgFx;
using Newtonsoft.Json;

namespace WindowsPhoneFanDkApp.Api.Models
{
    /// <summary>
    /// A class containing posts from the website.
    /// </summary>
    [CachePolicy(CachePolicy.CacheThenRefresh)]
    public class Post : ModelItemBase
    {
        public Post()
        {
            //AgFx needs and empty contructor on ModelItems/ViewModels.
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("title_plain")]
        public string TitlePlain { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("status")]
        public Status Status { get; set; }
    }
}
