using MVVMLightDemo.Common;
using MVVMLightDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightDemo.Model
{
    [MetadataType(typeof(BindDataAnnotationsViewModel))]
    public class ValidateUserInfo:ValidateModelBase
    {
        #region 属性 
        private String userName;
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public String UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(() => UserName); }
        }



        private String userPhone;
        /// <summary>
        /// 用户电话
        /// </summary>
        [Required]
        [RegularExpression(@"^[-]?[1-9]{8,11}\d*$|^[0]{1}$", ErrorMessage = "用户电话必须为8-11位的数值.")]
        public String UserPhone
        {
            get { return userPhone; }
            set { userPhone = value; RaisePropertyChanged(() => UserPhone); }
        }



        private String userEmail;
        /// <summary>
        /// 用户邮件
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [RegularExpression("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$", ErrorMessage = "请填写正确的邮箱地址.")]
        public String UserEmail
        {
            get { return userEmail; }
            set { userEmail = value; RaisePropertyChanged(() => UserEmail);  }
        }
        #endregion
    }
}