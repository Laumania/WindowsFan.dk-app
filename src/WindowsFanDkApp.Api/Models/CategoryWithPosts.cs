using System;
using System.Collections.ObjectModel;
using System.IO;
using AgFx;
using Newtonsoft.Json;
using WindowsFanDkApp.Api.Data;

namespace WindowsFanDkApp.Api.Models
{
    public class CategoryWithPosts : ModelItemBase<CategoryWithPostsLoadContext>
    {
        public CategoryWithPosts() {}

        public CategoryWithPosts(int identifier) : base (new CategoryWithPostsLoadContext(identifier))
        {
            
        }


        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }


        [JsonProperty("posts")]
        public ObservableCollection<Post> Posts { get; set; }


        public class CategoryWithPostsDataLoader : IDataLoader<CategoryWithPostsLoadContext>
        {
            private const string RecentPostUriFormat = Constants.BaseApiPath + "get_category_posts/?id=";

            public object Deserialize(CategoryWithPostsLoadContext loadContext, Type objectType, System.IO.Stream stream)
            {
                var reader = new StreamReader(stream);
                var response = reader.ReadToEnd();

                var model = JsonConvert.DeserializeObject<CategoryWithPosts>(response);
                model.LoadContext = loadContext;

                return model;
            }

            public LoadRequest GetLoadRequest(CategoryWithPostsLoadContext loadContext, Type objectType)
            {
                string uri = RecentPostUriFormat + loadContext.Identifier;
                return new WebLoadRequest(loadContext, new Uri(uri));
            }
        }
    }
}
