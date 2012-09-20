using System;
using System.Diagnostics;
using System.Windows.Controls;
using AgFx;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsFanDkApp.Analytics;
using WindowsFanDkApp.Api.Models;

namespace WindowsFanDkApp.Views
{
    public partial class PostsByCatPageView : PhoneApplicationPage
    {
        private CategoryPosts categoryWithPosts;

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
                this.categoryWithPosts = DataManager.Current.Load<CategoryPosts>(category.Id);
                categoryWithPosts.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(categoryWithPosts_PropertyChanged);
            }
        }

        void categoryWithPosts_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                listPosts.ItemsSource = categoryWithPosts.Posts;
                txtHeader.Text = categoryWithPosts.Category.Title;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
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
    }
}