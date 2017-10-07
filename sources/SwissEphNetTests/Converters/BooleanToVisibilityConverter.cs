using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SwissEphNetTests.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = (value is bool bVal) ? bVal : false;
            Visibility notVisibility = Visibility.Collapsed;
            if (parameter != null)
            {
                var options = parameter.ToString().Split(new char[] { ',', ';', ' ' });
                if (options.Contains("not"))
                    bValue = !bValue;
                if (options.Contains("hidden"))
                    notVisibility = Visibility.Hidden;
            }
            return bValue ? Visibility.Visible : notVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
