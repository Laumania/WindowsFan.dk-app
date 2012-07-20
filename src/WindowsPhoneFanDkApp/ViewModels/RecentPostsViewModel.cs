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
using WindowsPhoneFanDkApp.Common;
using WindowsPhoneFanDkApp.Data;
using WindowsPhoneFanDkApp.Models;

namespace WindowsPhoneFanDkApp.ViewModels
{
    [CachePolicy(CachePolicy.NoCache)]
    public class RecentPostsViewModel : ModelItemBase<DummyLoadContext>
    {
        //AgFx needs and empty contructor on ModelItems/ViewModels
        public RecentPostsViewModel()
        {
            
        }

        public RecentPostsViewModel(int identifier) :
            base(new DummyLoadContext(identifier)) 
        { 
            
        }


        private string _status;
        [JsonProperty("status")]
        public string Status
        {
            get { return _status; }
            set 
            { 
                _status = value;
                RaisePropertyChanged("Status");
            }
        }

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

        public class RecentPostsViewModelDataLoader : IDataLoader<DummyLoadContext>
        {
            private const string recentPostUriFormat = Constants.BaseApiPath + "get_recent_posts/";

            public LoadRequest GetLoadRequest(DummyLoadContext loadContext, Type objectType)
            {
                string uri = recentPostUriFormat;
                return new WebLoadRequest(loadContext, new Uri(uri));
            }

            public object Deserialize(DummyLoadContext loadContext, Type objectType, Stream stream)
            {
                var reader = new StreamReader(stream);
                var response = reader.ReadToEnd();

                var viewModel = JsonConvert.DeserializeObject<RecentPostsViewModel>(response);
                viewModel.LoadContext = loadContext;

                return viewModel;
            }
        }
    }
}
