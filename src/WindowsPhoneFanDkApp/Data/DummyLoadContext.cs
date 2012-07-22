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

namespace WindowsPhoneFanDkApp.Data
{
    /// <summary>
    /// This LoadContext is used in cases where we don't really have an identifier
    /// and doesn't plan to cache anything.
    /// </summary>
    public class DummyLoadContext : LoadContext
    {
        public DummyLoadContext(int dummyIdentifier)
            : base(dummyIdentifier)
        {
        }

        public int DummyIdentifier
        {
            get
            {
                return (int)Identity;
            }
        }
    }
}
