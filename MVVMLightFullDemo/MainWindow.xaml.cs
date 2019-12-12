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

namespace MVVMLightDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //oneWay:使用 OneWay 绑定时，每当源发生变化，数据就会从源流向目标。
            //OneTime: 绑定也会将数据从源发送到目标；但是，仅当启动了应用程序或 DataContext 发生更改时才会如此操作，因此，它不会侦听源中的更改通知。
            //OneWayToSource: 绑定会将数据从目标发送到源。
            //TwoWay: 绑定会将源数据发送到目标，但如果目标属性的值发生变化，则会将它们发回给源。
            //Default: binding的模式根据实际情况来定，如果是可编辑的就是TwoWay,只读的就是OneWay.

//            如果用户更改前台控件的值，什么时候通知后台的数据源呢？
//这个就是UpdateSourceTrigger这枚举类型来决定的。
//关于此枚举的具体类型，可参照

//http://msdn.microsoft.com/zh-cn/library/system.windows.data.binding.updatesourcetrigger.aspx


// 绑定 TwoWay 或 OneWayToSource 侦听的 target 属性中所做的更改并将它们传播到源。 这称为对源进行更新。 通常，当目标属性发生更改时，可能发生这些更新。 这是相当不错的复选框和其他简单控件，但它通常并不适合文本字段。 正在更新后的每个击键可能导致性能降低并拒绝用户平常机会退格删除，并在提交新值之前修改键入错误。 因此，默认值 UpdateSourceTrigger 值 Text 属性是 LostFocus 和 not PropertyChanged。
// 如果您设置 UpdateSourceTrigger 值赋给 Explicit, ，必须调用 UpdateSource 方法或所做的更改将不会传播到源。


        }
    }
}