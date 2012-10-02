using System;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsFanDkApp.Analytics;
using WindowsFanDkApp.Api.Models;
using WindowsFanDkApp.Common;
using WindowsFanDkApp.ViewModels;

namespace WindowsFanDkApp.Views
{
    public partial class PostPageView : PhoneApplicationPage
    {
        public PostPageView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var post = PhoneApplicationService.Current.State["selectedPost"] as Post;

            if (post != null)
            {
                GlobalProgressIndicator.Current.IsLoading = true;
                ViewModel.Setup(post);
                
                //check if we are allowed to comment on post.
                if (post != null && post.CommentStatus == CommentStatus.open)
                    ApplicationBar.IsVisible = true;
                
                AnalyticsHelper.TrackPageView("PostPageView");
                GlobalProgressIndicator.Current.IsLoading = false;
            }
            else
            {
                //navigate back to start screen, if we cant find the post
                NavigationService.GoBack();
            }
        }
       
        private void btnAddComment_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PostCommentView.xaml", UriKind.Relative));
        }

        //required for the hyperlink fix / hack.
        public void Browse(string url)
        {
            NavigationService.Navigate(new Uri("/Views/BrowserView.xaml?url="+ url, UriKind.Relative));
        }

        public PostPageViewModel ViewModel
        {
            get { return DataContext as PostPageViewModel; }  
        }
    }
}