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
    /// Interaction logic for MessagerForDispatch.xaml
    /// </summary>
    public partial class MessagerForDispatch : Window
    {
        public MessagerForDispatch()
        {
            InitializeComponent();
            this.DataContext = new MessengerForDispatchViewModel();
        }
    }
}
