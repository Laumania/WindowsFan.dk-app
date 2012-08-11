using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsPhoneFanDkApp.Analytics;

namespace WindowsPhoneFanDkApp.Views
{
    public partial class MainPageViewV2 : PhoneApplicationPage
    {
        public MainPageViewV2()
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
            if (listPosts.SelectedIndex == -1)
                return;

            //add selected post to app service.
            PhoneApplicationService.Current.State["selectedPost"] = e.AddedItems[0];

            //navigate to post.
            NavigationService.Navigate(new Uri("/WindowsPhoneFanDkApp;component/Views/PostPageView.xaml", UriKind.RelativeOrAbsolute));

        }

        private void listcategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listcategories.SelectedIndex == -1)
                return;

            //add selected category to app service
            PhoneApplicationService.Current.State["selectedCategory"] = e.AddedItems[0];

            //navigate to posts
            NavigationService.Navigate(new Uri("/WindowsPhoneFanDkApp;component/Views/PostsByCatPageView.xaml", UriKind.RelativeOrAbsolute));

        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/WindowsPhoneFanDkApp;component/Views/SettingsPageView.xaml", UriKind.RelativeOrAbsolute));
        }

    }
}