using LoginDemo.ViewModel.Common;
using LoginDemo.ViewModel.Login;
using System.Windows;

namespace LoginDemo
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            this.DataContext = new LoginViewModel();

            WindowManager.Register<MainWindow>("MainWindow");
        }
    }
}
