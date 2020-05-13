using MusicApp.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace MusicApp.Core.Converters
{
    public class ContentTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return ContentType.PlayList;

            return Enum.Parse(targetType, parameterString);
        }
    }
}
