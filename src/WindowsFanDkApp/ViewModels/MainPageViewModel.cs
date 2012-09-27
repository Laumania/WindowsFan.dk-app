using System.Collections.ObjectModel;
using System.ComponentModel;
using AgFx;
using GalaSoft.MvvmLight;
using WindowsFanDkApp.Api.Models;
using WindowsFanDkApp.Common;

namespace WindowsFanDkApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            if (!IsInDesignMode)
            {
                RecentPosts = DataManager.Current.Load<RecentPosts>(-1); // We have no identifiers for this object.
                CategoriesCollection = DataManager.Current.Load<CategoryCollection>(-1);
                FeaturedPosts = DataManager.Current.Load<TagPosts>("featured");
            }
#if DEBUG
            else
            {
                RecentPosts = SampleDataGenerator.RecentPosts;
                FeaturedPosts = SampleDataGenerator.FeaturedPosts;
                CategoriesCollection = SampleDataGenerator.CategoriesCollection;
            }
#endif
        }

        public void Refresh()
        {
            RecentPosts.Refresh();
            CategoriesCollection.Refresh();
            FeaturedPosts.Refresh();
        }

        public RecentPosts RecentPosts { get; private set; }
        public CategoryCollection CategoriesCollection { get; private set; }
        public TagPosts FeaturedPosts { get; private set; }
    }
}