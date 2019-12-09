using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TimedTask
{
    /// <summary>
    /// 快捷键
    /// </summary>
    public class ShortKey
    {
        /// <summary>
        /// 保存
        /// </summary>
        public static RoutedCommand Save = new RoutedCommand();
        /// <summary>
        /// 新建
        /// </summary>
        public static RoutedCommand New = new RoutedCommand();
        /// <summary>
        /// 文件
        /// </summary>
        public static RoutedCommand File = new RoutedCommand();
        /// <summary>
        /// 退出
        /// </summary>
        public static RoutedCommand Exit = new RoutedCommand();
        /// <summary>
        /// 关于
        /// </summary>
        public static RoutedCommand About = new RoutedCommand();
        /// <summary>
        /// 帮助 
        /// </summary>
        public static RoutedCommand Help = new RoutedCommand();

        static ShortKey()
        {
            Save.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));

            New.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));

            File.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));

            Exit.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control));

            About.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));

            Help.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Control));
        }
    }
}
