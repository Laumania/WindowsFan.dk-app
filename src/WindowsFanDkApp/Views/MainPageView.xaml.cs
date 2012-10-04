using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using AgFx;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsFanDkApp.Analytics;
using WindowsFanDkApp.Api.Models;
using WindowsFanDkApp.Common;
using WindowsFanDkApp.ViewModels;

namespace WindowsFanDkApp.Views
{
    public partial class MainPageView : PhoneApplicationPage
    {
        public MainPageView()
        {
            InitializeComponent();
            this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);
        }
       

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            AnalyticsHelper.TrackPageView("MainPageView");
            recentPosts.SelectedIndex = -1;
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
            NavigationService.Navigate(new Uri("/WindowsFanDkApp;component/Views/PostsByCategoryPageView.xaml", UriKind.RelativeOrAbsolute));

        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/WindowsFanDkApp;component/Views/SettingsPageView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void AboutApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/YourLastAboutDialog;component/AboutPage.xaml", UriKind.Relative));
        }

        private void RefreshApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            ViewModel.Refresh();
        }

        public MainPageViewModel ViewModel
        {
            get { return DataContext as MainPageViewModel; }
        }
    }
}