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
using AgFx;

namespace WindowsPhoneFanDkApp.Api.Data
{
    /// <summary>
    /// This LoadContext is used in cases where we don't really have an identifier
    /// and doesn't plan to cache anything.
    /// </summary>
    public class RecentPostsLoadContext : LoadContext
    {
        public RecentPostsLoadContext(int recentPostsIdentifier)
            : base(recentPostsIdentifier)
        {
        }

        public int RecentPostsIdentifier
        {
            get
            {
                return (int)Identity;
            }
        }
    }
}
