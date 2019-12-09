using LoginDemo.ViewModel.Common;
using System;

namespace LoginDemo.ViewModel.Login
{
    public class LoginViewModel : NotificationObject
    {
        public LoginViewModel()
        {
            obj.UserName = "test";
            Gender = 1;
        }

        /// <summary>
        /// Model对象
        /// </summary>
        private LoginModel obj = new LoginModel();

        /// <summary>
        /// 用户名
        /// </summary>
        [NotEmptyCheck]
        [UserNameExists]
        public string UserName
        {
            get
            {
                return obj.UserName;
            }
            set
            {
                obj.UserName = value;
                this.RaisePropertyChanged("UserName");
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        [NotEmptyCheck]
        public string Password
        {
            get
            {
                return obj.Password;
            }
            set
            {
                obj.Password = value;
                this.RaisePropertyChanged("Password");
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender
        {
            get
            {
                return obj.Gender;
            }
            set
            {
                obj.Gender = value;
                this.RaisePropertyChanged("Gender");
            }
        }

        private bool toClose = false;
        /// <summary>
        /// 是否要关闭窗口
        /// </summary>
        public bool ToClose
        {
            get
            {
                return toClose;
            }
            set
            {
                toClose = value;
                if (toClose)
                {
                    this.RaisePropertyChanged("ToClose");
                }
            }
        }

        /// <summary>
        /// 数据填写正确
        /// </summary>
        public override bool IsValid
        {
            get
            {
                return obj.IsValid;
            }
            set
            {
                if (value == obj.IsValid)
                {
                    return;
                }
                obj.IsValid = value;
                this.RaisePropertyChanged("IsValid");
            }
        }

        private BaseCommand loginClick;
        /// <summary>
        /// 登录事件
        /// </summary>
        public BaseCommand LoginClick
        {
            get
            {
                if (loginClick == null)
                {
                    loginClick = new BaseCommand(new Action<object>(o =>
                    {
                        //执行登录逻辑
                        WindowManager.Show("MainWindow", null);
                        ToClose = true;
                    }));
                }
                return loginClick;
            }
        }
    }
}
