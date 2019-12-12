using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMLightDemo.Model
{
    /// <summary>
    /// 自定义类型
    /// </summary>
    public class CustomClass : FrameworkElement //继承于FrameworkElement
    {
        /// <summary>
        /// .net属性封装
        /// </summary>
        public int Age
        {
            get //读访问器
            {
                return (int)GetValue(AgeProperty);
            }
            set //写访问器
            {
                SetValue(AgeProperty, value);
            }
        }
        /// <summary>
        /// 声明并创建依赖项属性
        /// </summary>
        public static readonly DependencyProperty AgeProperty =
            DependencyProperty.Register("Age", typeof(int), typeof(CustomClass), new PropertyMetadata(0, CustomPropertyChangedCallback), CustomValidateValueCallback);
        

        /// <summary>
        /// 属性值更改回调方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void CustomPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e){ }

        /// <summary>
        /// 属性值验证回调方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool CustomValidateValueCallback(object value)
        {
            return true;
        }
    }
}
