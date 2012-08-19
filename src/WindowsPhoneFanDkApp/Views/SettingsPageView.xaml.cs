using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using WindowsPhoneFanDkApp.Api.Models;
using WindowsPhoneFanDkApp.Common;
using WindowsPhoneFanDkApp.ViewModels;

namespace WindowsPhoneFanDkApp.Views
{
    public partial class SettingsPageView : PhoneApplicationPage
    {
        private Settings settings;
        public SettingsPageView()
        {
            InitializeComponent();

            //read settings
            settings = Helper.ReadSettings();

            //listen when loading of categories is done
            ((MainPageViewModel)this.DataContext).Categories.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Categories_CollectionChanged);
        }

        void Categories_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                //set selected value according to settings
                foreach (Category category in e.NewItems)
                {
                    if (settings.FeedsIds.Contains(category.Id))
                    {
                        category.Selected = true;
                    }
                    else category.Selected = false;
                } 
            }
        }

        private void listcategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //selection bugfix
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
            MessageBox.Show("Settings saved");
        }
    }
}