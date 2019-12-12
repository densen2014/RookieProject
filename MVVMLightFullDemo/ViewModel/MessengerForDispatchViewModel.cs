using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using MVVMLightDemo.Common;
using MVVMLightDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MVVMLightDemo.Common.CreateUserInfoThread;

namespace MVVMLightDemo.ViewModel
{
   public class MessengerForDispatchViewModel:ViewModelBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MessengerForDispatchViewModel()
        {
            InitData();
            DispatcherHelper.Initialize();

            ///Messenger：信使
            ///Recipient：收件人          
            Messenger.Default.Register<TopUserInfo>(this, "UserMessenger", FeedBack);
           
        }



        #region 全局属性

        private ObservableCollection<UserParam> userList;
        /// <summary>
        /// 数据列表
        /// </summary>
        public ObservableCollection<UserParam> UserList
        {
            get { return userList; }
            set { userList = value; RaisePropertyChanged(() => UserList); }
        }

        private UserParam user;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserParam User
        {
            get { return user; }
            set { user = value; RaisePropertyChanged(() => User); }
        }


        private Boolean isEnableForm;
        /// <summary>
        /// 是否表单可用
        /// </summary>
        public bool IsEnableForm
        {
            get { return isEnableForm; }
            set { isEnableForm = value; RaisePropertyChanged(() => IsEnableForm); }
        }

        private Boolean isWaitingDisplay;
        /// <summary>
        /// 是都显示延迟旋转框
        /// </summary>
        public bool IsWaitingDisplay
        {
            get { return isWaitingDisplay; }
            set { isWaitingDisplay = value; RaisePropertyChanged(() => IsWaitingDisplay); }
        }

        private Int32 processRange;
        /// <summary>
        /// 进度比例
        /// </summary>
        public int ProcessRange
        {
            get { return processRange; }
            set { processRange = value; RaisePropertyChanged(() => ProcessRange); }
        }

        #endregion

        #region 全局命令
        private RelayCommand addRecordCmd;
        /// <summary>
        /// 添加资源
        /// </summary>
        public RelayCommand AddRecordCmd
        {
            get
            {
                if (addRecordCmd == null) addRecordCmd = new RelayCommand(() => ExcuteAddRecordCmd());
                return addRecordCmd;
            }
            set
            {
                addRecordCmd = value;
            }
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            UserList = new ObservableCollection<UserParam>()
            {
                 new UserParam(){ UserName="周杰伦", UserAdd="周杰伦地址", UserPhone ="88888888", UserSex="男" },
                 new UserParam(){ UserName="刘德华", UserAdd="刘德华地址", UserPhone ="88888888", UserSex="男" },
                 new UserParam(){ UserName="刘若英", UserAdd="刘若英地址", UserPhone ="88888888", UserSex="女" }
            };
            User = new UserParam();
            IsEnableForm = true;
            IsWaitingDisplay = false;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        private void ExcuteAddRecordCmd()
        {
            UserParam up = new UserParam { UserAdd = User.UserAdd, UserName = User.UserName, UserPhone = User.UserPhone, UserSex = User.UserSex };
            CreateUserInfoThread creatUser = new CreateUserInfoThread(up);
            creatUser.Create();
            IsEnableForm = false;
            IsWaitingDisplay = true;
        }


        /// <summary>
        /// 创建进度
        /// </summary>
        /// <param name="top"></param>
        public void FeedBack(TopUserInfo top)
        {
            if (top.isFinish)
            {
                if (top.userInfo != null)
                {
                    UserList.Add(top.userInfo);
                }
                User = new UserParam();
                IsEnableForm = true;
                IsWaitingDisplay = false;
            }
            else
            {
                ProcessRange = top.process;
            }
        }

        /// <summary>
        /// 手动调用释放注册信息（该视图模型内的所有注册信息全部释放）
        /// </summary>
        public void ReleaseRegister()
        {
            Messenger.Default.Unregister(this);             
        }

        #endregion
    }
}
