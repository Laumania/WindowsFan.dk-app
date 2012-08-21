using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WindowsPhoneFanDkApp.Api.Models
{
    public class Settings
    {
        private Queue<int> feedIds = new Queue<int>();

        public Settings()
        {
            
        }

        public Queue<int> FeedsIds
        {
            get { return feedIds; }
        }

        public string Name { get; set; }

        public string Email { get; set; }


    }
}
