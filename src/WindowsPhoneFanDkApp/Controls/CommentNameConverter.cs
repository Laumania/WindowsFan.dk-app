using System;
using System.Windows.Data;
namespace WindowsPhoneFanDkApp.Controls
{
    public class CommentNameConverter : IValueConverter 
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value + " siger:";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
