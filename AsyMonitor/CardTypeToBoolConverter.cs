using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AsyMonitor
{
    class CardTypeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string choose = IniFile.INIGetStringValue(System.AppDomain.CurrentDomain.BaseDirectory + "SysData.ini", "IOMonitor", "defaultChoose", "");

            return choose.Equals(parameter.ToString());
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
