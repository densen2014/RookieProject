using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightDemo.ViewModel
{
    public class MessengerRegisterForVViewModel:ViewModelBase
    {

        public MessengerRegisterForVViewModel()
        {
          
        }

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
            //以ViewAlert位Tokon标志，进行消息发送
            Messenger.Default.Send<String>("ViewModel通知View弹出消息框", "ViewAlert");
        }

        #endregion
    }
}
