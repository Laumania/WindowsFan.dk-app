using System;
using System.Collections.ObjectModel;
using AgFx;
using Newtonsoft.Json;

namespace WindowsFanDkApp.Api.Models
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
        
        [JsonProperty("slug")]
        public string Slug { get; set; }
        
        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }
        
        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        public string ThumbnailComputed
        {
            
            get
            {
                if (Attachments.Count > 0)
                    return Attachments[0].Images.Large.Url;
                else
                {
                    return Thumbnail;
                }
            }
        }
        
        [JsonProperty("author")]
        public Author Author { get; set; }
        
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        
        [JsonProperty("comment_status")]
        public CommentStatus CommentStatus { get; set; }

        [JsonProperty("comment_count")]
        public int CommentCount { get; set; }

        [JsonProperty("comments")]
        public ObservableCollection<Comment> Comments { get; set; }
        
        [JsonProperty("attachments")]
        public ObservableCollection<Attachments> Attachments { get; set; }

        public string PostSignature
        {
            get { return Author.Name + " " + Date; }
        }

    }
}
