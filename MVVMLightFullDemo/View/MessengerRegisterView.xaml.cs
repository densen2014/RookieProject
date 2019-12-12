using GalaSoft.MvvmLight.Messaging;
using MVVMLightDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MVVMLightDemo.View
{
    /// <summary>
    /// Interaction logic for MessageRegister.xaml
    /// </summary>
    public partial class MessengerRegisterView : Window
    {
        public MessengerRegisterView()
        {
            InitializeComponent();
            this.DataContext = new MessengerRegisterViewModel();
            //卸载当前(this)对象注册的所有MVVMLight消息
            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }
    }
}
