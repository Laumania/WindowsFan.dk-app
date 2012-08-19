using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using WindowsPhoneFanDkApp.Api.Models;
using WindowsPhoneFanDkApp.Common;

namespace WindowsPhoneFanDkApp.Views
{
    public partial class SettingsPageView : PhoneApplicationPage
    {
        public SettingsPageView()
        {
            InitializeComponent();
        }


        private void listcategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listcategories.SelectedIndex == -1)
                return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            
            //create feeds string
            foreach (Category category in listcategories.ItemsSource)
            {
                if (category.Selected)
                    settings.FeedsIds.Enqueue(category.Id);

            }

            Helper.WriteSettings(settings);
        }
    }
}