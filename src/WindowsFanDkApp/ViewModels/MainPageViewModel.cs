using System.Collections.ObjectModel;
using System.ComponentModel;
using AgFx;
using GalaSoft.MvvmLight;
using WindowsFanDkApp.Api.Models;

namespace WindowsFanDkApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            if (!IsInDesignMode)
            {
                RecentPosts = DataManager.Current.Load<RecentPosts>(-1); // We have no identifiers for this object.
                FetchCategories = DataManager.Current.Load<FetchCategories>(-1);
            }
        }

        public RecentPosts RecentPosts { get; private set; }
        public FetchCategories FetchCategories { get; private set; }
    }
}