using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using WindowsFanDkApp.Api.Models;
using WindowsFanDkApp.Common;

namespace WindowsFanDkApp.ViewModels
{
    public class PostPageViewModel : ViewModelBase
    {
        public PostPageViewModel()
        {
            //Damm, designtime data doesn't work here. Blend crashes :( - Mads
            //if (IsInDesignMode)
            //{
            //    Setup(SampleDataGenerator.FeaturedPosts.Posts[1]);
            //}
        }

        public void Setup(Post post)
        {
            Post = post;
        }


        
        private Post _post;
        public Post Post
        {
            get { return _post; }
            set
            {
                _post = value;
                RaisePropertyChanged("Post");
            }
        }
    }
}
