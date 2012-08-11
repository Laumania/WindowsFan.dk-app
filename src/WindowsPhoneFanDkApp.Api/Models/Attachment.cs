using System.Collections.Generic;
using AgFx;
using Newtonsoft.Json;

namespace WindowsPhoneFanDkApp.Api.Models
{
    [CachePolicy(CachePolicy.CacheThenRefresh)]
    public class Attachment : ModelItemBase
    {
        public Attachment(){}
        
        [JsonProperty("id")]
        public int Id { get; set; } 
        
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("parent")]
        public string Parent { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

    }
}
