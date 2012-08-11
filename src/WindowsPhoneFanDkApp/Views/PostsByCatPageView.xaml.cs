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
using AgFx;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsPhoneFanDkApp.Analytics;
using WindowsPhoneFanDkApp.Api.Models;

namespace WindowsPhoneFanDkApp.Views
{
    public partial class PostsByCatPageView : PhoneApplicationPage
    {
        private CategoryWithPosts categoryWithPosts;

        public PostsByCatPageView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Category category = PhoneApplicationService.Current.State["selectedCategory"] as Category;
            base.OnNavigatedTo(e);
            if (category != null) AnalyticsHelper.TrackPageView("PostsByCategory " + category.Title);
            listPosts.SelectedIndex = -1;


            if (category != null)
            {
                this.categoryWithPosts = DataManager.Current.Load<CategoryWithPosts>(category.Id);
                categoryWithPosts.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(categoryWithPosts_PropertyChanged);
            }
        }

        void categoryWithPosts_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            listPosts.ItemsSource = categoryWithPosts.Posts;
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
    }
}