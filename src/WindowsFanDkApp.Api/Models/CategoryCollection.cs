using System;
using System.Collections.ObjectModel;
using System.IO;
using AgFx;
using Newtonsoft.Json;
using WindowsFanDkApp.Api.Data;

namespace WindowsFanDkApp.Api.Models
{
    [CachePolicy(CachePolicy.AutoRefresh, 60*60*48)]
    public class CategoryCollection : ModelItemBase<CategoriesLoadContext>
    {
        public CategoryCollection(){}

        public CategoryCollection(int identifier) : base (new CategoriesLoadContext(identifier))
        {
            
        }


        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        private readonly ObservableCollection<Category> categories = new ObservableCollection<Category>();
        [JsonProperty("categories")]
        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set
            {
                if(categories != null)
                {
                    categories.Clear();
                    foreach (Category category in value)
                    {
                        categories.Add(category);
                    }

                    RaisePropertyChanged("Categories");
                }
            }
        }

        public class CategoriesDataLoader : IDataLoader<CategoriesLoadContext>
        {
            private const string CategoriesPostUriFormat = Constants.BaseApiPath + "get_category_index/";

            public object Deserialize(CategoriesLoadContext loadContext, Type objectType, Stream stream)
            {
                var reader = new StreamReader(stream);
                var response = reader.ReadToEnd();

                var model = JsonConvert.DeserializeObject<CategoryCollection>(response);
                model.LoadContext = loadContext;

                return model;
            }

            public LoadRequest GetLoadRequest(CategoriesLoadContext loadContext, Type objectType)
            {
                const string uri = CategoriesPostUriFormat;
                return new WebLoadRequest(loadContext, new Uri(uri));
            }
        }

    }

   
}
