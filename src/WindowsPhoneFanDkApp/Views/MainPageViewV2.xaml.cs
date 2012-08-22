using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AgFx;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsPhoneFanDkApp.Analytics;
using WindowsPhoneFanDkApp.Api.Models;
using WindowsPhoneFanDkApp.Common;

namespace WindowsPhoneFanDkApp.Views
{
    public partial class MainPageViewV2 : PhoneApplicationPage
    {
        private Dictionary<string,CategoryWithPosts> currentLoadedFeeds = new Dictionary<string, CategoryWithPosts>();

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
            InitFeed();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListBox)sender).SelectedIndex == -1)
                return;

            //add selected post to app service.
            PhoneApplicationService.Current.State["selectedPost"] = e.AddedItems[0];

            //navigate to post.
            NavigationService.Navigate(new Uri("/WindowsPhoneFanDkApp;component/Views/PostPageView.xaml", UriKind.RelativeOrAbsolute));

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
            NavigationService.Navigate(new Uri("/WindowsPhoneFanDkApp;component/Views/PostsByCatPageView.xaml", UriKind.RelativeOrAbsolute));

        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/WindowsPhoneFanDkApp;component/Views/SettingsPageView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void InitFeed()
        {
            currentLoadedFeeds = new Dictionary<string, CategoryWithPosts>();
            int itemsCount;
            Queue<int> IDs = Helper.GetSettings().FeedsIds;
            if (IDs.Count > 0)
            {
                int idCount = IDs.Count;
                for (int i = 0; i < idCount; i++)
                {
                    int feedID = IDs.Dequeue();

                    if (feedID > 0)
                    {
                        if(!currentLoadedFeeds.ContainsKey(feedID.ToString()))
                            currentLoadedFeeds.Add(feedID.ToString(), new CategoryWithPosts());

                        currentLoadedFeeds[feedID.ToString()] = DataManager.Current.Load<CategoryWithPosts>(feedID);
                        currentLoadedFeeds[feedID.ToString()].PropertyChanged += Dictionary_PropertyChanged;
                    }
                }
            }
            else
            {
                //clean up feeds
                itemsCount = pivMain.Items.Count;
                if (itemsCount > 2)
                {
                    for (int i = itemsCount; i > 1; i--)
                    {
                        if (i != itemsCount)
                        {
                            pivMain.Items.RemoveAt(i - 1);
                        }
                    }
                }
            }
          
        }

        void Dictionary_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //Clean removed feeds
            int itemsCount = pivMain.Items.Count;
            for (int i = itemsCount - 1; i > 1 + currentLoadedFeeds.Count; i--)
            {
                string header = ((PivotItem) pivMain.Items[i - 1]).Header.ToString();
                if (!currentLoadedFeeds.Any(item => item.Value.Category != null && item.Value.Category.Title.Equals(header)))
                {
                    pivMain.Items.Remove(pivMain.Items[i - 1]);
                }
            }

            //Add selected feeds
            foreach (KeyValuePair<string, CategoryWithPosts> pair in currentLoadedFeeds)
            {
                if (pair.Value.Category != null && !pivMain.Items.Any(item => ((PivotItem)item).Header.Equals(pair.Value.Category.Title)))
                {
                    PivotItem item = new PivotItem();
                    item.DataContext = pair.Value.Category.Id;
                    item.Header = pair.Value.Category.Title;
                    ListBox lb = new ListBox();
                    lb.ItemsSource = pair.Value.Posts;
                    lb.ItemTemplate = this.Resources["pivotListboxTemplate"] as DataTemplate;
                    lb.SelectionChanged += ListBox_SelectionChanged;
                    item.Content = lb;
                    

                    pivMain.Items.Insert(1, item);
                }
            }

            pivMain.UpdateLayout();
        }

        
       
    }
}