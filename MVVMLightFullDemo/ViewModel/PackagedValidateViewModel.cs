using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVMLightDemo.Model;
using System.Windows;

namespace MVVMLightDemo.ViewModel
{
    public class PackagedValidateViewModel:ViewModelBase
    {
        public PackagedValidateViewModel()
        {
            ValidateUI = new Model.ValidateUserInfo();
        }

        #region 全局属性
        private ValidateUserInfo validateUI;
        /// <summary>
        /// 用户信息
        /// </summary>
        public ValidateUserInfo ValidateUI
        {
            get
            {
                return validateUI;
            }

            set
            {
                validateUI = value;
                RaisePropertyChanged(()=>ValidateUI);
            }
        }             
        #endregion

        #region 全局命令
        private RelayCommand submitCmd;
        public RelayCommand SubmitCmd
        {
            get
            {
                if(submitCmd == null) return new RelayCommand(() => ExcuteValidForm());
                return submitCmd;
            }

            set
            {
                submitCmd = value;
            }
        }
        #endregion

        #region 附属方法
        /// <summary>
        /// 验证表单
        /// </summary>
        private void ExcuteValidForm()
        {
            if (ValidateUI.IsValidated) MessageBox.Show("验证通过！");
            else MessageBox.Show("验证失败！");
        }
        #endregion
    }
}
