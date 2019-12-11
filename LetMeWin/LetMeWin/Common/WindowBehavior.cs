using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interactivity;

namespace LetMeWin.Common
{
   
    /// <summary>
    /// 窗口行为
    /// </summary>
    public class WindowBehavior : Behavior<Window>
    {
        /// <summary>
        /// 关闭窗口
        /// </summary>
        public bool Close
        {
            get { return Convert.ToBoolean(GetValue(CloseProperty)); }
            set { SetValue(CloseProperty, value); }
        }
        public static readonly DependencyProperty CloseProperty = DependencyProperty.Register("Close", typeof(bool), typeof(WindowBehavior), new PropertyMetadata(false, OnCloseChanged));
        private static void OnCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            dynamic window = ((WindowBehavior)d).AssociatedObject;
            dynamic newValue = Convert.ToBoolean(e.NewValue);
            if (newValue)
            {
                window.Close();
            }
        }

    }

}
