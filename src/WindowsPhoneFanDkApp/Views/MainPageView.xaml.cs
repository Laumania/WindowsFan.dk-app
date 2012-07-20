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
using System.Windows.Shapes;
using AgFx;
using Microsoft.Phone.Controls;
using WindowsPhoneFanDkApp.Analytics;
using WindowsPhoneFanDkApp.ViewModels;

namespace WindowsPhoneFanDkApp
{
    public partial class MainPageView : PhoneApplicationPage
    {
        // Constructor
        public MainPageView()
        {
            InitializeComponent();

            Loaded += delegate(object sender, RoutedEventArgs args)
                          {
                              ContentPanel.DataContext = DataManager.Current.Load<RecentPostsViewModel>(-1);
                          };
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            AnalyticsHelper.TrackPageView("MainPageView");
        }
    }
}