using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using AgFx;
using Newtonsoft.Json;
using WindowsFanDkApp.Api.Data;

namespace WindowsFanDkApp.Api.Models
{
    [CachePolicy(CachePolicy.AutoRefresh)]
    public class TagPosts : ModelItemBase<TagPostsLoadContext>
    {
        public TagPosts() {}

        public TagPosts(string tag)
            : base(new TagPostsLoadContext(tag))
        {
            
        }

        [JsonProperty("status")]
        public Status Status { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
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


        public class TagPostsDataLoader : IDataLoader<TagPostsLoadContext>
        {
            //get_tag_posts/?tag_slug=featured
            private const string UriFormat = Constants.BaseApiPath + "get_tag_posts/?tag_slug=";

            public object Deserialize(TagPostsLoadContext loadContext, Type objectType, System.IO.Stream stream)
            {
                var reader = new StreamReader(stream);
                var response = reader.ReadToEnd();

                var model = JsonConvert.DeserializeObject<TagPosts>(response);
                model.LoadContext = loadContext;

                return model;
            }

            public LoadRequest GetLoadRequest(TagPostsLoadContext loadContext, Type objectType)
            {
                string uri = UriFormat + loadContext.Identifier;
                return new WebLoadRequest(loadContext, new Uri(uri));
            }
        }
    }
}
