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
using WindowsPhoneFanDkApp.Api.Data;

namespace WindowsPhoneFanDkApp.Api.Models
{
    [CachePolicy(CachePolicy.NoCache)]
    public class FetchCategories : ModelItemBase<CategoriesLoadContext>
    {
        public FetchCategories(){}

        public FetchCategories(int identifier) : base (new CategoriesLoadContext(identifier))
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

                var model = JsonConvert.DeserializeObject<FetchCategories>(response);
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
