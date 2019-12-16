using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;
using WpfApp1.Model;
using WpfApp1.Serv;

namespace WpfApp1.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class 实体ViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
      
         public 实体ViewModel()
        {
            UserInfo = new 实体类();

            //下拉框的初始数据
            CombboxList = new List<ComplexInfoModel>()
            {
                  new ComplexInfoModel(){  Key="1",Text="苹果"},
                  new ComplexInfoModel(){  Key="2",Text="香蕉"}
            };

 
            Thread t = new Thread(delegate ()
            {
                // replace the IP with your system IP Address...
                Server myserver = new Server("127.0.0.1", 13000, UserInfo);
            });
            t.Start();

            Console.WriteLine("Server Started...!");
            Client tcp通讯 = new Client(UserInfo);
            LoadTcpClient = new RelayCommand(CQCQCQ);


            Thread t2 = new Thread(async delegate ()
            {
                // replace the IP with your system IP Address...
                Websocket myserver = new Websocket();
                await  myserver.Server();
            });
            t2.Start();

        }

        void CQCQCQ()
        {
            Client tcp通讯 = new Client(UserInfo);
            tcp通讯.启动();
            WebsocketClient();
        }
        void WebsocketClient()
        {
            Websocket myserver = new Websocket();
              myserver.Client();

        }

        #region 双向绑定

        private 实体类 userInfo;
        /// <summary>
        /// 用户信息
        /// </summary>
        public 实体类 UserInfo
        {
            get { return userInfo; }
            set { userInfo = value; RaisePropertyChanged(() => UserInfo); }
        }
        #endregion

        #region 下拉框
        private ComplexInfoModel combboxItem;
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        public ComplexInfoModel CombboxItem
        {
            get { return combboxItem; }
            set { combboxItem = value; RaisePropertyChanged(() => CombboxItem); }
        }


        private List<ComplexInfoModel> combboxList;
        /// <summary>
        /// 下拉框列表
        /// </summary>
        public List<ComplexInfoModel> CombboxList
        {
            get { return combboxList; }
            set { combboxList = value; RaisePropertyChanged(() => CombboxList); }
        }





        #endregion

        #region 命令
        public ICommand LoadTcpClient { get; private set; }

        #endregion

    }
}