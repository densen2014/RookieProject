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
    public class Enable : System.Windows.Data.IValueConverter//此接口有以下两个方法
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return "";

            string tmp = "";
            switch ((string)value)
            {
                case "0": tmp = "禁用"; break;
                case "1": tmp = "启用"; break;
                case "2": tmp = "失效"; break;
                case "3": tmp = "过期"; break;
                default: tmp = "未知"; break;
            }
            return tmp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
