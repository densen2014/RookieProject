using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Controls;

namespace TimedTask.Module
{
    /// <summary>
    /// WPF MVVC设计模式ViewMode基本功能类
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region 属性
        /// <summary>
        /// 显示名称
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        private bool _loading;

        /// <summary>
        /// 是否在加载状态
        /// </summary>
        public bool IsLoading
        {
            get { return _loading; }
            set
            {
                if (_loading != value)
                {
                    _loading = value;
                    OnPropertyChanged("IsLoading");
                }
            }
        }

        /// <summary>
        /// 是否在设计模式
        /// </summary>
        public bool IsInDesignMode
        {
            get
            {
                return System.ComponentModel.DesignerProperties.GetIsInDesignMode(new Button());
            }
        }
        #endregion

        #region 构造器
        /// <summary>
        /// 实例化一个ViewModelBase对象
        /// </summary>
        protected ViewModelBase()
        {
        }
        #endregion

        #region 方法
        
        ///// <summary>
        ///// 在UI线程上同步执行方法
        ///// </summary>
        ///// <param name="action"></param>
        //public void InvokeOnUIDispatcher(Action action)
        //{
        //    if (action != null)
        //    {
        //        Application.Current.Dispatcher.Invoke(action);
        //    }
        //}

        ///// <summary>
        ///// 发布一个事件
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="e"></param>
        //public void PublishEvent<T>(T e)
        //{
        //    this.EventAggregator.GetEvent<CompositePresentationEvent<T>>().Publish(e);
        //}

        ///// <summary>
        ///// 以默认的配置订阅一个事件
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="callback"></param>
        //public void SubscribeEvent<T>(Action<T> callback)
        //{
        //    this.EventAggregator.GetEvent<CompositePresentationEvent<T>>().Subscribe(callback);
        //}

        #endregion

        #region INotifyPropertyChanged Members
        /// <summary>
        /// 触发属性发生变更事件
        /// </summary>
        /// <typeparam name="T">泛型标记，会匹配函数返回类型，不必手动填写</typeparam>
        /// <param name="action">以函数表达式方式传入属性名称，表达式如下即可：()=>YourViewModelProperty</param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }
        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            this.OnDispose();
        }
        /// <summary>
        /// 若支持IDisposable，请重写此方法，当被调用Dispose时会执行此方法。
        /// </summary>
        protected virtual void OnDispose()
        {
        }
        #endregion
    }
}