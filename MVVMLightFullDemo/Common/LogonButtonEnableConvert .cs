using MVVMLightDemo.Model;
using System;
using System.Linq;
using System.Windows.Data;

namespace MVVMLightDemo.Common
{
    public class UserInfoConvert : IMultiValueConverter
    {
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="values">所绑定的源的值</param>
        /// <param name="targetType">目标的类型</param>
        /// <param name="parameter">绑定时所传递的参数</param>
        /// <param name="culture"><系统语言等信息</param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!values.Cast<string>().Any(text => string.IsNullOrEmpty(text)) && values.Count() == 4)
            {
                UserParam up = new UserParam() { UserName = values[0].ToString(), UserSex = values[1].ToString(), UserPhone = values[2].ToString(), UserAdd = values[3].ToString() };
                return up;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
