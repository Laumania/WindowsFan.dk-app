using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
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
            this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Settings saved");
        }
    }
}