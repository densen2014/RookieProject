using GalaSoft.MvvmLight.Messaging;
using MVVMLightDemo.ViewModel;
using System;
using System.Windows;

namespace MVVMLightDemo.View
{
    /// <summary>
    /// Interaction logic for NessagerForView.xaml
    /// </summary>
    public partial class MessagerForView : Window
    {
        public MessagerForView()
        {
            InitializeComponent();

            //消息标志token：ViewAlert，用于标识只阅读某个或者某些Sender发送的消息，并执行相应的处理，所以Sender那边的token要保持一致
            //执行方法Action：ShowReceiveInfo，用来执行接收到消息后的后续工作，注意这边是支持泛型能力的，所以传递参数很方便。
            Messenger.Default.Register<String>(this, "ViewAlert", ShowReceiveInfo);
            this.DataContext = new MessengerRegisterForVViewModel();
            //卸载当前(this)对象注册的所有MVVMLight消息
            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }

        /// <summary>
        /// 接收到消息后的后续工作：根据返回来的信息弹出消息框
        /// </summary>
        /// <param name="msg"></param>
        private void ShowReceiveInfo(String msg)
        {
            MessageBox.Show(msg);
        }
    }
}