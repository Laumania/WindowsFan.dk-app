﻿using System;
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

            Post post = PhoneApplicationService.Current.State["selectedPost"] as Post;
            if (post != null)
            {
                txtTitle.Html = post.Title;
                txtContent.Html = post.Content;
                txtPostInfo.Text = "Skrevet af " + post.Author.Name + " " + post.Date;
            }
                    
            
        }
    }
}