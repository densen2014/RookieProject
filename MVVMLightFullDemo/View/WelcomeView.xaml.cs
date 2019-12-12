using MVVMLightDemo.ViewModel;
using System.Windows;

namespace MVVMLightDemo.View
{
    /// <summary>
    /// Interaction logic for WelcomeView.xaml
    /// </summary>
    public partial class WelcomeView : Window
    {
        public WelcomeView()
        {
            InitializeComponent();
            this.DataContext = new WelcomeViewModel();
        }
    }
}
