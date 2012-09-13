using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using WindowsFanDkApp.Api.Models;
using WindowsFanDkApp.Common;
using WindowsFanDkApp.ViewModels;

namespace WindowsFanDkApp.Views
{
    public partial class SettingsPageView : PhoneApplicationPage
    {
        public SettingsPageView()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Settings saved");
        }
    }
}