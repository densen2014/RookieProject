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
    public class ʵ��ViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
      
         public ʵ��ViewModel()
        {
            UserInfo = new ʵ����();

            //������ĳ�ʼ����
            CombboxList = new List<ComplexInfoModel>()
            {
                  new ComplexInfoModel(){  Key="1",Text="ƻ��"},
                  new ComplexInfoModel(){  Key="2",Text="�㽶"}
            };

 
            Thread t = new Thread(delegate ()
            {
                // replace the IP with your system IP Address...
                Server myserver = new Server("127.0.0.1", 13000, UserInfo);
            });
            t.Start();

            Console.WriteLine("Server Started...!");
            Client tcpͨѶ = new Client(UserInfo);
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
            Client tcpͨѶ = new Client(UserInfo);
            tcpͨѶ.����();
            WebsocketClient();
        }
        void WebsocketClient()
        {
            Websocket myserver = new Websocket();
              myserver.Client();

        }

        #region ˫���

        private ʵ���� userInfo;
        /// <summary>
        /// �û���Ϣ
        /// </summary>
        public ʵ���� UserInfo
        {
            get { return userInfo; }
            set { userInfo = value; RaisePropertyChanged(() => UserInfo); }
        }
        #endregion

        #region ������
        private ComplexInfoModel combboxItem;
        /// <summary>
        /// ������ѡ����Ϣ
        /// </summary>
        public ComplexInfoModel CombboxItem
        {
            get { return combboxItem; }
            set { combboxItem = value; RaisePropertyChanged(() => CombboxItem); }
        }


        private List<ComplexInfoModel> combboxList;
        /// <summary>
        /// �������б�
        /// </summary>
        public List<ComplexInfoModel> CombboxList
        {
            get { return combboxList; }
            set { combboxList = value; RaisePropertyChanged(() => CombboxList); }
        }





        #endregion

        #region ����
        public ICommand LoadTcpClient { get; private set; }

        #endregion

    }
}