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

namespace WpfApplication1
{
    /// <summary>
    /// MyDialog.xaml 的交互逻辑
    /// </summary>
    public partial class MyDialog : Window
    {
        public MyDialog(Window owner)
        {
            this.Owner = owner;
            InitializeComponent();

            // 设置Binding对象
            Binding b = new Binding();
            b.Path = new PropertyPath(ComboBox.SelectedItemProperty);
            b.Source = cmb;
            b.Mode = BindingMode.OneWay;
            b.StringFormat = "您选择的交通工具为：{0}";

            MainWindow mw = Owner as MainWindow;
            // 使属性与Binding关联
            mw?.tbtxt.SetBinding(TextBlock.TextProperty, b);
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
