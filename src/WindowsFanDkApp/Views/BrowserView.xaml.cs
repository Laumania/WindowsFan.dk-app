using System;
using Microsoft.Phone.Controls;

namespace WindowsFanDkApp.Views
{
    public partial class BrowserView : PhoneApplicationPage
    {
        public BrowserView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string url = "";

            if (NavigationContext.QueryString.TryGetValue("url", out url))
                browser.Navigate(new Uri(url, UriKind.Absolute));

               
        }
    }
}