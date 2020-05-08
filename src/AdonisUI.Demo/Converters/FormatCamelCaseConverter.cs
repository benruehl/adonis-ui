using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace AdonisUI.Demo.Converters
{
    class FormatCamelCaseConverter
        : IValueConverter
    {
        public static readonly FormatCamelCaseConverter Instance = new FormatCamelCaseConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value?.ToString();

            if (string.IsNullOrWhiteSpace(s))
                return s;

            string result = s[0].ToString();

            foreach (char c in s.Skip(1))
            {
                if (char.IsUpper(c) || char.IsNumber(c))
                    result += " ";

                result += c;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
