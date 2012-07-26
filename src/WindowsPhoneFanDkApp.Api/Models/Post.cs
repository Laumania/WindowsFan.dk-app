using System;
using System.Collections.Generic;
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
        private string thumbnail;
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

        [JsonProperty("thumbnail")]
        public string Thumbnail
        {
            get
            {
                if(string.IsNullOrEmpty(thumbnail) || string.IsNullOrWhiteSpace(thumbnail))
                {
                    return "/WindowsPhoneFanDkApp;component/Content/PostThumb.png";
                }

                return thumbnail;
            }

            set { thumbnail = value; }
        }

        //public string Image
        //{
        //    get
        //    {
        //        if (Attachments.Count > 0 && Attachments[0] != null)
        //            return Attachments[0].Url;
        //        return string.Empty;
        //    }
        //}

        //[JsonProperty("attachments")]
        //public List<Attachment> Attachments { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("comment_status")]
        public CommentStatus CommentStatus { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }

        public string PostSignature
        {
            get { return Author.Name + " " + Date; }
        }

        
    }
}
