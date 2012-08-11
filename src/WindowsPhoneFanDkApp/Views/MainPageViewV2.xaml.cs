using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using AgFx;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsPhoneFanDkApp.Analytics;
using WindowsPhoneFanDkApp.Api.Models;

namespace WindowsPhoneFanDkApp.Views
{
    public partial class MainPageViewV2 : PhoneApplicationPage
    {
        readonly Queue<int> feedIds = new Queue<int>();
        private CategoryWithPosts currentLoadedFeed;

        public MainPageViewV2()
        {
            InitializeComponent();

            //added 2 test category feeds to mainscreen
            //TODO: implement dynamic logic from settings
            feedIds.Enqueue(374);
            feedIds.Enqueue(3);
            feedIds.Enqueue(12);

            InitFeed();

        }

        void currentLoadedFeed_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            currentLoadedFeed.PropertyChanged -= currentLoadedFeed_PropertyChanged;
            if (currentLoadedFeed.Category != null)
            {
                PivotItem item = new PivotItem();
                item.Header = currentLoadedFeed.Category.Title;
                ListBox lb = new ListBox();
                lb.ItemsSource = currentLoadedFeed.Posts;
                lb.ItemTemplate = this.Resources["pivotListboxTemplate"] as DataTemplate;
                lb.SelectionChanged += ListBox_SelectionChanged;
                item.Content = lb;
                
                pivMain.Items.Insert(1, item);
            }
            pivMain.UpdateLayout();
            InitFeed();
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

        private void InitFeed()
        {

            if (feedIds.Count > 0)
            {
                int feedID = feedIds.Dequeue();

                if (feedID > 0)
                {
                    currentLoadedFeed = DataManager.Current.Load<CategoryWithPosts>(feedID);
                    currentLoadedFeed.PropertyChanged += currentLoadedFeed_PropertyChanged;
                }
            }
        }
       
    }
}