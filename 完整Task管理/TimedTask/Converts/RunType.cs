using System;
using System.Text;

namespace TimedTask.Converts
{
    public class RunType : System.Windows.Data.IValueConverter//此接口有以下两个方法
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return "";

            /*
              Year = 0,
                 Month = 1,
                 Day = 2,
                 Hour = 3,
                 Minute = 4,
                 Once = 5
             */
            string type = "";
            switch ((string)value)
            {
                case "0": type = "[每年]"; break;
                case "1": type = "[每月]"; break;
                case "2": type = "[每天]"; break;
                case "3": type = "[每小时]"; break;
                case "4": type = "[间隔分钟]"; break;
                case "5": type = "[仅一次]"; break;
                default:
                    break;
            }
            return type;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
