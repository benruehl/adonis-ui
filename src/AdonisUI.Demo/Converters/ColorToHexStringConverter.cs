using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace AdonisUI.Demo.Converters
{
    class ColorToHexStringConverter
        : IValueConverter
    {
        public static readonly ColorToHexStringConverter Instance = new ColorToHexStringConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color;

            if (value is Color c)
                color = c;
            else if (value is SolidColorBrush b)
                color = b.Color;
            else if (value is ComponentResourceKey k)
            {
                var resource = Application.Current.TryFindResource(k);

                if (resource is Color rc)
                    color = rc;
                if (resource is SolidColorBrush rb)
                    color = rb.Color;
            }
            else
                return null;
            
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
