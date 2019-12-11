using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeWin.Common
{
     /// <summary>
     ///   需要验证的模型就继承这个类
     /// </summary>
    public class 验证类 : ObservableObject, IDataErrorInfo
    {
        public 验证类()
        {

        }

        #region 属性 
        /// <summary>
        /// 装验证错误的集合
        /// </summary>
        private Dictionary<String, String> dataErrors = new Dictionary<String, String>();

        /// <summary>
        /// 是否验证通过
        /// </summary>
        public Boolean IsValidated
        {
            get
            {
                if (dataErrors != null && dataErrors.Count > 0)
                {
                    return false;
                }
                return true;  
            }
        }
        #endregion

        public string this[string columnName]
        {
            get
            {
                ValidationContext vc = new ValidationContext(this, null, null);
                vc.MemberName = columnName;
                var res = new List<ValidationResult>();
                var result = Validator.TryValidateProperty(this.GetType().GetProperty(columnName).GetValue(this, null), vc, res);
                if (res.Count > 0)
                {
                    AddDic(dataErrors, vc.MemberName);
                    return string.Join(Environment.NewLine, res.Select(r => r.ErrorMessage).ToArray());
                }
                RemoveDic(dataErrors, vc.MemberName);
                return null;
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        #region 附属方法

        /// <summary>
        /// 移除字典
        /// </summary>
        /// <param name="dics"></param>
        /// <param name="dicKey"></param>
        private void RemoveDic(Dictionary<String, String> dics, String dicKey)
        {
            dics.Remove(dicKey);
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="dics"></param>
        /// <param name="dicKey"></param>
        private void AddDic(Dictionary<String, String> dics, String dicKey)
        {
            if (!dics.ContainsKey(dicKey)) dics.Add(dicKey, "");
        }
        #endregion
    }

}
