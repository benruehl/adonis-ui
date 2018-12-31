using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace AdonisUI.Converters
{
    public class ValuesToCornerRadiusConverter
        : IMultiValueConverter
    {
        public static ValuesToCornerRadiusConverter Instance = new ValuesToCornerRadiusConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double topLeft = System.Convert.ToDouble(values[0]);
            double topRight = System.Convert.ToDouble(values[1]);
            double bottomRight = System.Convert.ToDouble(values[2]);
            double bottomLeft = System.Convert.ToDouble(values[3]);

            return new CornerRadius(topLeft, topRight, bottomRight, bottomLeft);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
