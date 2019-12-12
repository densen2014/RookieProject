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
    public class MessengerForSourceViewModel : ViewModelBase
    {
        private Messenger myMessenger;
        public MessengerForSourceViewModel()
        {
            //构造函数    
            myMessenger = new Messenger();
            SimpleIoc.Default.Register(() => myMessenger, "MyMessenger"); //注入一个Key为MyMessenger的Messenger对象

            myMessenger.Register<NotificationMessage>(this, message =>  //注册myMessenger,开启监听
            {
                // 判断来源来接受消息
                MsgInfo = message.Notification;
            });


            
            Messenger.Default.Register<PropertyChangedMessage<String>>(this, message =>
            {
                if (message.PropertyName == PropertyChangedViewModel.PropertyName) //接受特定属性值相关信道的消息
                {
                    PropertyChangedInfo = (message.OldValue + " --> " + message.NewValue);//输出旧值到新值的内容
                }
            });
        }

        #region 全局属性
        private String msgInfo;
        private String propertyChangedInfo;


        public string MsgInfo
        {
            get
            {
                return msgInfo;                
            }
            set
            {
                msgInfo = value;
                RaisePropertyChanged(()=>MsgInfo);
            }
        }

        public string PropertyChangedInfo
        {
            get
            {
                return propertyChangedInfo;
            }

            set
            {
                propertyChangedInfo = value;
                RaisePropertyChanged(()=>PropertyChangedInfo);
            }
        }

        #endregion
    }
}