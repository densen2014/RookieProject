using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LoginDemo.ViewModel.Common
{
    public abstract class NotificationObject : INotifyPropertyChanged, IDataErrorInfo
    {
        #region 属性修改通知

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 发起通知
        /// </summary>
        /// <param name="propertyName">属性名</param>
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region 数据验证

        public string Error
        {
            get { return ""; }
        }

        public string this[string columnName]
        {
            get
            {
                var vc = new ValidationContext(this, null, null);
                vc.MemberName = columnName;
                var res = new List<ValidationResult>();
                var result = Validator.TryValidateProperty(this.GetType().GetProperty(columnName).GetValue(this, null), vc, res);
                if (res.Count > 0)
                {
                    return string.Join(Environment.NewLine, res.Select(r => r.ErrorMessage).ToArray());
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 页面中是否所有控制数据验证正确
        /// </summary>
        public virtual bool IsValid { get; set; }

        #endregion
    }
}
