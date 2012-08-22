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
    public class CategoryWithPostsLoadContext : LoadContext
    {
        public CategoryWithPostsLoadContext(int identifier): base(identifier)
        {
            
        }

        public int Identifier
        {
            get { return (int) Identity; }
        }
    }
}
