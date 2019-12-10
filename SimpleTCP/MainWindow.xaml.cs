using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
 
namespace SimpleTCP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

             Thread t = new Thread(delegate ()
            {
                // replace the IP with your system IP Address...
                Server myserver = new Server("127.0.0.1", 13000);
            });
            t.Start();

            Console.WriteLine("Server Started...!");

            log.Text = "Server Started...!";
        }

        void logMe()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Client tcp通讯 = new Client();

        }
    }
}
