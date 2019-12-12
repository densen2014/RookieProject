using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MVVMLightDemo.Common
{
    public class BoolToVisibility : IValueConverter
    {
        #region "IValueConverter Members"

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            if (value is bool)
            {
                Boolean valueB = (Boolean)value;
                switch (valueB)
                {
                    case true:
                        return Visibility.Visible;
                    case false:
                        return Visibility.Collapsed;
                }
            }
            else if(value is String)
            {
                switch (value.ToString().ToLower())
                {
                    case "true":
                        return Visibility.Visible;
                    case "false":
                        return Visibility.Collapsed;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}