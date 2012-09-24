using System;
using System.Collections.ObjectModel;
using System.IO;
using AgFx;
using Newtonsoft.Json;
using WindowsFanDkApp.Api.Data;

namespace WindowsFanDkApp.Api.Models
{
    /// <summary>
    /// A class containing posts from the website.
    /// </summary>
    [CachePolicy(CachePolicy.AutoRefresh)]
    public class RecentPosts : ModelItemBase<RecentPostsLoadContext>
    {
        public RecentPosts() {}

        public RecentPosts(int identifier) :
            base(new RecentPostsLoadContext(identifier)) 
        { 
            
        }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("count_total")]
        public int CountTotal { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        private readonly ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        [JsonProperty("posts")]
        public ObservableCollection<Post> Posts
        {
            get { return _posts; }
            set
            {
                if (_posts != null)
                {
                    _posts.Clear();
                    foreach (var report in value)
                    {
                        _posts.Add(report);
                    }
                }
                RaisePropertyChanged("Posts");
            }
        }

        public class RecentPostsDataLoader : IDataLoader<RecentPostsLoadContext>
        {
            private const string RecentPostUriFormat = Constants.BaseApiPath + "get_recent_posts/";

            public LoadRequest GetLoadRequest(RecentPostsLoadContext loadContext, Type objectType)
            {
                const string uri = RecentPostUriFormat;
                return new WebLoadRequest(loadContext, new Uri(uri));
            }

            public object Deserialize(RecentPostsLoadContext loadContext, Type objectType, Stream stream)
            {
                var reader = new StreamReader(stream);
                var response = reader.ReadToEnd();

                var model = JsonConvert.DeserializeObject<RecentPosts>(response);
                model.LoadContext = loadContext;

                return model;
            }
        }
    }
}