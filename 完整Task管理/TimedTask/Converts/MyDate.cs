// 版权所有：
// 文 件  名：DateToString.cs
// 功能描述：
// 创建标识：Seven Song(m.sh.lin0328@163.com) 2014/1/19 13:28:35
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;

using System.Text;

namespace TimedTask.Converts
{
    public class MyDate : System.Windows.Data.IValueConverter//此接口有以下两个方法
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return "";

            DateTime now;
            if (DateTime.TryParse(value.ToString(), out now))
            {
                return "下次启动 " + now.ToString("MM-dd HH:mm") + "";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
