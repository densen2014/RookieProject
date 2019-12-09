using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interactivity;

namespace LoginDemo.ViewModel.Common
{
    /// <summary>
    /// 验证异常行为
    /// </summary>
    public class ValidationExceptionBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// 记录异常的数量
        /// </summary>
        /// <remarks>在一个页面里面，所有控件的验证错误信息都会传到这个类上，每个控制需不需要显示验证错误，需要分别记录</remarks>
        private Dictionary<UIElement, int> ExceptionCount;
        /// <summary>
        /// 缓存页面的提示装饰器
        /// </summary>
        private Dictionary<UIElement, NotifyAdorner> AdornerDict;

        protected override void OnAttached()
        {
            ExceptionCount = new Dictionary<UIElement, int>();
            AdornerDict = new Dictionary<UIElement, NotifyAdorner>();

            this.AssociatedObject.AddHandler(Validation.ErrorEvent, new EventHandler<ValidationErrorEventArgs>(OnValidationError));
        }

        /// <summary>
        /// 当验证错误信息改变时，首先调用此函数
        /// </summary>
        private void OnValidationError(object sender, ValidationErrorEventArgs e)
        {
            try
            {
                var handler = GetValidationExceptionHandler();//插入<c:ValidationExceptionBehavior></c:ValidationExceptionBehavior>此语句的窗口的DataContext，也就是ViewModel
                var element = e.OriginalSource as UIElement;//错误信息发生改变的控件
                if (handler == null || element == null)
                {
                    return;
                }

                if (e.Action == ValidationErrorEventAction.Added)
                {
                    if (ExceptionCount.ContainsKey(element))
                    {
                        ExceptionCount[element]++;
                    }
                    else
                    {
                        ExceptionCount.Add(element, 1);
                    }
                }
                else if (e.Action == ValidationErrorEventAction.Removed)
                {
                    if (ExceptionCount.ContainsKey(element))
                    {
                        ExceptionCount[element]--;
                    }
                    else
                    {
                        ExceptionCount.Add(element, -1);
                    }
                }

                if (ExceptionCount[element] <= 0)
                {
                    HideAdorner(element);
                }
                else
                {
                    ShowAdorner(element, e.Error.ErrorContent.ToString());
                }

                int TotalExceptionCount = 0;
                foreach (KeyValuePair<UIElement, int> kvp in ExceptionCount)
                {
                    TotalExceptionCount += kvp.Value;
                }

                handler.IsValid = (TotalExceptionCount <= 0);//ViewModel里面的IsValid
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得行为所在窗口的DataContext
        /// </summary>
        private NotificationObject GetValidationExceptionHandler()
        {
            if (this.AssociatedObject.DataContext is NotificationObject)
            {
                var handler = this.AssociatedObject.DataContext as NotificationObject;

                return handler;
            }

            return null;
        }

        /// <summary>
        /// 显示错误信息提示
        /// </summary>
        private void ShowAdorner(UIElement element, string errorMessage)
        {
            if (AdornerDict.ContainsKey(element))
            {
                AdornerDict[element].ChangeToolTip(errorMessage);
            }
            else
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(element);
                NotifyAdorner adorner = new NotifyAdorner(element, errorMessage);
                adornerLayer.Add(adorner);
                AdornerDict.Add(element, adorner);
            }
        }

        /// <summary>
        /// 隐藏错误信息提示
        /// </summary>
        private void HideAdorner(UIElement element)
        {
            if (AdornerDict.ContainsKey(element))
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(element);
                adornerLayer.Remove(AdornerDict[element]);
                AdornerDict.Remove(element);
            }
        }
    }
}
