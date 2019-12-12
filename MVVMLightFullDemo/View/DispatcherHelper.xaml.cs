using MVVMLightDemo.ViewModel;
using System.Windows;

namespace MVVMLightDemo.View
{
    /// <summary>
    /// Interaction logic for DispatcherHelper.xaml
    /// </summary>
    public partial class DispatcherHelper : Window
    {
        public DispatcherHelper()
        {
            InitializeComponent();
            this.DataContext = new DispatcherHelperViewModel();
        }
    }
}