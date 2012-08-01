using System;
using System.Windows.Data;

namespace WindowsPhoneFanDkApp.Controls
{
    public class PostInfoConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "Skrevet af " + value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
