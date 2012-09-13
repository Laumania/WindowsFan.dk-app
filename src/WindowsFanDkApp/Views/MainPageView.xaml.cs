using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AgFx;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsFanDkApp.Analytics;
using WindowsFanDkApp.Api.Models;
using WindowsFanDkApp.Common;

namespace WindowsFanDkApp.Views
{
    public partial class MainPageView : PhoneApplicationPage
    {
        private Dictionary<string,CategoryWithPosts> currentLoadedFeeds = new Dictionary<string, CategoryWithPosts>();

        public MainPageView()
        {
            InitializeComponent();

        }
       

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            AnalyticsHelper.TrackPageView("MainPageView");
            listPosts.SelectedIndex = -1;
            listcategories.SelectedIndex = -1;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListBox)sender).SelectedIndex == -1)
                return;

            //add selected post to app service.
            PhoneApplicationService.Current.State["selectedPost"] = e.AddedItems[0];

            //navigate to post.
            NavigationService.Navigate(new Uri("/WindowsFanDkApp;component/Views/PostPageView.xaml", UriKind.RelativeOrAbsolute));

            //fix for selection wierdness
            ((ListBox) sender).SelectedIndex = -1;

        }

        private void listcategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listcategories.SelectedIndex == -1)
                return;

            //add selected category to app service
            PhoneApplicationService.Current.State["selectedCategory"] = e.AddedItems[0];

            //navigate to posts
            NavigationService.Navigate(new Uri("/WindowsFanDkApp;component/Views/PostsByCatPageView.xaml", UriKind.RelativeOrAbsolute));

        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/WindowsFanDkApp;component/Views/SettingsPageView.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}