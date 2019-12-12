using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightDemo.ViewModel
{
    public class MessengerSenderViewModel:ViewModelBase
    {
        public MessengerSenderViewModel()
        {

        }

        #region 属性
        private String sendInfo;
        /// <summary>
        /// 发送消息
        /// </summary>
        public String SendInfo
        {
            get { return sendInfo; }
            set { sendInfo = value; RaisePropertyChanged(()=>SendInfo); }
        }

        #endregion
        
        #region 命令

        private RelayCommand sendCommand;
        /// <summary>
        /// 发送命令
        /// </summary>
        public RelayCommand SendCommand
        {
            get
            {
                if (sendCommand == null)
                    sendCommand = new RelayCommand(() => ExcuteSendCommand());
                return sendCommand;

            }
            set { sendCommand = value; }
        }

        private void ExcuteSendCommand()
        {
            Messenger.Default.Send<String>(SendInfo, "Message");
        }

        #endregion
    }
}
