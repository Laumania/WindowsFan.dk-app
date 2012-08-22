using System.Collections.ObjectModel;
using System.ComponentModel;
using AgFx;
using GalaSoft.MvvmLight;
using WindowsPhoneFanDkApp.Api.Models;

namespace WindowsPhoneFanDkApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        private readonly RecentPosts _recentPosts;

        private readonly FetchCategories fetchedCategories;
        private readonly ObservableCollection<Category> categories = new ObservableCollection<Category>();

        private string _status;

        public MainPageViewModel()
        {
            if (!IsInDesignMode)
            {
                _recentPosts = DataManager.Current.Load<RecentPosts>(-1); // We have no identifiers for this object.
                _recentPosts.PropertyChanged += RecentPostsOnPropertyChanged;

                fetchedCategories = DataManager.Current.Load<FetchCategories>(-1);
                fetchedCategories.PropertyChanged += FetchedCategoriesOnPropertyChanged;
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged("Status");
            }
        }

        public ObservableCollection<Post> Posts
        {
            get { return _posts; }
            set
            {
                if (_posts != null)
                {
                    _posts.Clear();
                    foreach (Post report in value)
                    {
                        _posts.Add(report);
                    }
                }
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set
            {
                if (categories != null)
                {
                    categories.Clear();
                    foreach (Category category in value)
                    {
                        categories.Add(category);
                    }
                }
            }
        }

        private void RecentPostsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            Status = _recentPosts.Status.ToString();
            Posts = _recentPosts.Posts;
        }

        private void FetchedCategoriesOnPropertyChanged(object sender, PropertyChangedEventArgs progressChangedEventArgs)
        {
            Categories = fetchedCategories.Categories;
        }
    }
}