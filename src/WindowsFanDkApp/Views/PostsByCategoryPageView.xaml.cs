using System;
using System.Diagnostics;
using System.Windows.Controls;
using AgFx;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsFanDkApp.Analytics;
using WindowsFanDkApp.Api.Models;
using WindowsFanDkApp.Common;
using WindowsFanDkApp.ViewModels;

namespace WindowsFanDkApp.Views
{
    public partial class PostsByCategoryPageView : PhoneApplicationPage
    {
        private CategoryPosts categoryWithPosts;

        public PostsByCategoryPageView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var category = PhoneApplicationService.Current.State["selectedCategory"] as Category;
            
            if (category != null)
            {
                GlobalProgressIndicator.Current.IsLoading = true;
                ViewModel.Setup(category);
                GlobalProgressIndicator.Current.IsLoading = false;
                AnalyticsHelper.TrackPageView("PostsByCategory " + category.Title);    
            }
            else
            {
                //navigate back to start screen, if we cant find the category
                NavigationService.GoBack();
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listPosts.SelectedIndex == -1)
                return;

            //add selected post to app service.
            PhoneApplicationService.Current.State["selectedPost"] = e.AddedItems[0];

            //navigate to post.
            NavigationService.Navigate(new Uri("/WindowsFanDkApp;component/Views/PostPageView.xaml", UriKind.RelativeOrAbsolute));

        }

        public PostsByCategoryPageViewModel ViewModel
        {
            get { return DataContext as PostsByCategoryPageViewModel; }
        }
    }
}