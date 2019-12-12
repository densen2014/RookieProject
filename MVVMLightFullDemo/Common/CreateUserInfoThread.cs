using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using MVVMLightDemo.Model;
using System;
using System.Threading;

namespace MVVMLightDemo.Common
{
    public class CreateUserInfoThread
    {
        //待创建信息
        public UserParam up { get; set; }

        public CreateUserInfoThread(UserParam _up)
        {
            up = _up;
        }

        public void Create()
        {
            Thread t = new Thread(Start);//抛出一个新线程
            t.Start();
        }

        private void Start()
        {
            TopUserInfo ui = new TopUserInfo();

            //ToDo：编写创建用户的DataAccess代码
            for (Int32 idx = 1; idx <= 9; idx++)
            {
                Thread.Sleep(1000);
                ui = new TopUserInfo() {
                    isFinish = false,
                    process = idx*10,
                     userInfo =null
                };
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    Messenger.Default.Send<TopUserInfo>(ui, "UserMessenger");
                });
            }
            Thread.Sleep(1000);
            ui = new TopUserInfo()
            {
                isFinish = true,
                process = 100,
                userInfo = up
            };
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                Messenger.Default.Send<TopUserInfo>(ui, "UserMessenger");
            });
        }



        /// <summary>
        /// 用户信息对象
        /// </summary>
        public class TopUserInfo
        {
            /// <summary>
            /// 是否创建结束
            /// </summary>
            public Boolean isFinish { get; set; }
            /// <summary>
            /// 进度
            /// </summary>
            public Int32 process { get; set; }
            /// <summary>
            /// 处理后的用户信息
            /// </summary>
            public UserParam userInfo { get; set; }
        }
    }
}
