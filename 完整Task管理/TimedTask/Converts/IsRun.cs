using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TimedTask.Converts
{
    /// <summary>
    /// 将Boolean转换为Visibility的转换器
    /// </summary>
    public class IsRun : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null || value.ToString().Trim() == "0") ? "失败" : "成功";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
