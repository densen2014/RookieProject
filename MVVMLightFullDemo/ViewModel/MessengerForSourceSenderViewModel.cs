using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightDemo.ViewModel
{
    public class ForSourceSenderViewModel:ViewModelBase
    {
        public ForSourceSenderViewModel(){}

        #region 全局命令
        private RelayCommand sendMsg;
        /// <summary>
        /// 发送消息
        /// </summary>
        public RelayCommand SendMsg
        {
            get
            {
                if (sendMsg == null) sendMsg = new RelayCommand(() => ExcuteSendMsh());
                return sendMsg;
            }
            set
            {
                sendMsg = value;
            }
        }
                #endregion

        #region 附属方法
        private void ExcuteSendMsh()
        {
            NotificationMessage nm = new NotificationMessage(this,String.Format("发送消息：{0}",DateTime.Now));
            Messenger myMessenger = SimpleIoc.Default.GetInstance<Messenger>("MyMessenger");//获取已存在的Messenger实例
            myMessenger.Send<NotificationMessage>(nm);//消息发送
        }
        #endregion

    }
}