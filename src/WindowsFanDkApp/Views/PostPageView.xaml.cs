using System;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsFanDkApp.Analytics;
using WindowsFanDkApp.Api.Models;

namespace WindowsFanDkApp.Views
{
    public partial class PostPageView : PhoneApplicationPage
    {
        public PostPageView()
        {
            InitializeComponent();
        }

        private void Init()
        {
            Post post = PhoneApplicationService.Current.State["selectedPost"] as Post;
            this.DataContext = post;

            //navigate back to start screen, if we cant find the post
            if (post == null)
                NavigationService.GoBack();

            //check if we are allowed to comment on post.
            if (post != null && post.CommentStatus == CommentStatus.open)
                ApplicationBar.IsVisible = true;

            AnalyticsHelper.TrackPageView("PostPageView");
        }
       

        private void btnAddComment_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PostCommentView.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Init();
        }

        //required for the hyperlink fix / hack.
        public void Browse(string url)
        {
            NavigationService.Navigate(new Uri("/Views/BrowserView.xaml?url="+ url, UriKind.Relative));
        }
        
    }
}