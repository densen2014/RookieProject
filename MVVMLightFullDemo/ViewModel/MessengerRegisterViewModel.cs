using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MVVMLightDemo.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightDemo.ViewModel
{
    public class MessengerRegisterViewModel:ViewModelBase
    {
        public MessengerRegisterViewModel()
        {
            ///Messenger：信使
            ///Recipient：收件人
            Messenger.Default.Register<String>(this, "Message", msg =>
            {
                ReceiveInfo += msg + "\n";
            });
        }


        #region 属性

        private String receiveInfo;
        /// <summary>
        /// 接收到信使传递过来的值
        /// </summary>
        public String ReceiveInfo
        {
            get { return receiveInfo; }
            set { receiveInfo = value; RaisePropertyChanged(()=>ReceiveInfo); }
        }

        #endregion


        #region 启动新窗口

        private RelayCommand showSenderWindow;

        public RelayCommand ShowSenderWindow
        {
            get {
                if (showSenderWindow == null)
                    showSenderWindow = new RelayCommand(()=>ExcuteShowSenderWindow());
                return showSenderWindow; 
            
            }
            set { showSenderWindow = value; }
        }

        private void ExcuteShowSenderWindow()
        {
            MessengerSenderView sender = new MessengerSenderView();
            sender.Show();
        }

        #endregion 
        

        #region 辅助函数
        /// <summary>
        /// 显示收件的信息
        /// </summary>
        /// <param name="msg"></param>
        private void ShowReceiveInfo(String msg)
        {
            ReceiveInfo += msg+"\n";
        }
        #endregion
    }
}