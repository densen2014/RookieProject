using GalaSoft.MvvmLight.Messaging;
using LetMeWin.ViewModel;
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

namespace LetMeWin.View
{
    /// <summary>
    /// AddMember.xaml 的交互逻辑
    /// </summary>
    public partial class AddMemberView : Window
    {
        public AddMemberView()
        {
            InitializeComponent();
            DataContext = new AddMember();
            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }  
    }
}
