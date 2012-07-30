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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsPhoneFanDkApp.Api.Models;

namespace WindowsPhoneFanDkApp.Views
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
        
    }
}