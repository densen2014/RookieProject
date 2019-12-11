using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LetMeWin.Common;
using LetMeWin.Model;
using LetMeWin.View;
using SqliteService.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LetMeWin.ViewModel
{
    public class Login : ViewModelBase
    {
        public Login ()
        {
            ////实例化账号密码信息
            //validateUI = new LoginModel();

            //查询账号密码信息           
            validateUI = 数据库.查询();
           
        }
        # region 局部使用变量
        private readonly string[] 登录状态 = { "帐号密码错误", "登录成功", "", "帐号已经到期或者停用" };
        /// <summary>
        /// 数据库
        /// </summary>
        readonly UserService 数据库 = new UserService();
        #endregion

        #region 全局属性

        private LoginModel validateUI;
        /// <summary>
        /// 用户信息
        /// </summary>
        public LoginModel ValidateUI
        {
            get
            {
                return validateUI;
            }

            set
            {
                validateUI = value;
                RaisePropertyChanged(() => ValidateUI);
            }
        }


        /// <summary>
        /// 是否要关闭窗口
        /// </summary>
        private bool toClose;

        public bool ToClose
        {
            get { return toClose; }
            set
            {
                toClose = value;
                RaisePropertyChanged(() => ToClose);
            }
        }

        #endregion

        #region 全局命令
        /// <summary>
        /// 登录按钮
        /// </summary>
        private RelayCommand logCmd;
        public RelayCommand LogCmd
        {
            get
            {
                if (logCmd == null) return new RelayCommand(() => ExcuteValidForm());
                return logCmd;
            }

            set
            {
                logCmd = value;
            }
        }
        #endregion

        #region 附属方法
        /// <summary>
        /// 验证表单
        /// </summary>
        private void ExcuteValidForm()
        {

            if (ValidateUI.Record == 1)
                数据库.更新(validateUI);

            LoginLogic Post = new LoginLogic();
            var IntData = Post.登录(validateUI.User, validateUI.Password);

            if (IntData != 1)
            { Messenger.Default.Send<String>(登录状态[IntData], "ViewAlert");
                return;
            }

            VipHomeView sender = new VipHomeView();
            sender.Show();
            ToClose = true;

            //以ViewAlert为Tokon标志，进行消息发送

        }
        //static void Maina()
        //{
        //    dynamic person = new System.Dynamic.ExpandoObject();
        //    person.Name = "cary";
        //    person.Age = 25;
        //    person.ShowDescription = new Func<string>(() => person.Name + person.Age);

        //    Console.WriteLine(person.Name + person.Age + person.ShowDescription());
        //    Console.ReadLine();
        //}
        #endregion

    }
}
