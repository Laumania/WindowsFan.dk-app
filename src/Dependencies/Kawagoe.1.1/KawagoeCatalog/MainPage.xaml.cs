// Copyright 2010 Andreas Saudemont (andreas.saudemont@gmail.com)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Windows;
using Microsoft.Phone.Controls;

namespace KawagoeCatalog
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void OpenImageCachePage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ImageCachePage.xaml", UriKind.Relative));
        }

        private void OpenMessagePopupPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MessagePopupPage.xaml", UriKind.Relative));
        }

        private void OpenOneShotDispatcherTimerPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/OneShotDispatcherTimerPage.xaml", UriKind.Relative));
        }
    }
}
