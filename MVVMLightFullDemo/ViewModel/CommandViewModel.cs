using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVMLightDemo.Model;
using System.Collections.ObjectModel;

namespace MVVMLightDemo.ViewModel
{
    public class CommandViewModel:ViewModelBase
    {
        public CommandViewModel()
        {
            //构造函数
            ValidateUI = new ValidateUserInfo();
            List = new ObservableCollection<ValidateUserInfo>();
        }

        #region 全局属性
        private ObservableCollection<ValidateUserInfo> list;
        /// <summary>
        /// 用户数据列表
        /// </summary>
        public ObservableCollection<ValidateUserInfo> List
        {
            get { return list; }
            set { list = value; }
        }

        private ValidateUserInfo validateUI;
        /// <summary>
        /// 当前操作的用户信息
        /// </summary>
        public ValidateUserInfo ValidateUI
        {
            get { return validateUI; }
            set
            {
                validateUI = value;
                RaisePropertyChanged(() => ValidateUI);
            }
        }
        #endregion

        #region 全局命令
        private RelayCommand submitCmd;
        /// <summary>
        /// 执行提交命令的方法
        /// </summary>
        public RelayCommand SubmitCmd
        {
            get
            {
                if (submitCmd == null) return new RelayCommand(() => ExcuteValidForm(),CanExcute);
                return submitCmd;
            }
            set { submitCmd = value; }
        }
        #endregion

        #region 附属方法
        /// <summary>
        /// 执行提交方法
        /// </summary>
        private void ExcuteValidForm()
        {
            List.Add(new ValidateUserInfo(){ UserEmail= ValidateUI.UserEmail, UserName = ValidateUI.UserName, UserPhone = ValidateUI.UserPhone });
        }

        /// <summary>
        /// 是否可执行（这边用表单是否验证通过来判断命令是否执行）
        /// </summary>
        /// <returns></returns>
        private bool CanExcute()
        {
            return ValidateUI.IsValidated;
        }
        #endregion

    }
}