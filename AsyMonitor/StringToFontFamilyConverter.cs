using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace AsyMonitor.Converters
{
    public class StringToFontFamilyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;
            FontFamilyConverter converter = new FontFamilyConverter();
            if (str != null && str.Length > 0)
            {
                return converter.ConvertFromString(str);

            }
            else
            {
                return converter.ConvertFromString("宋体");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
