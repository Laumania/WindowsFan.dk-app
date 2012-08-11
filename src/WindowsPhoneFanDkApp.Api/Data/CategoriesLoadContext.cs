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
    public class CategoriesLoadContext : LoadContext
    {
        public CategoriesLoadContext(int categoriesIdentifier) : base (categoriesIdentifier)
        {
            
        }

        public int CategoriesIdentifier
        {
            get { return (int) Identity; }
        }
    }
}
