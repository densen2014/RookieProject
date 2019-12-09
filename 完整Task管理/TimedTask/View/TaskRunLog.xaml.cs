using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TimedTask.View
{
    /// <summary>
    /// TaskRunLog.xaml 的交互逻辑
    /// </summary>
    public partial class TaskRunLog : Window
    {
        public Int64 ID { get; set; }
        private Dal.TaskLog _dalTaskLog = new Dal.TaskLog();
        public TaskRunLog()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Bind();
        }
        /// <summary>
        /// 窗体移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #region 数据加载

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="isFirstLoad">是否是第一次加载</param>
        public void Bind()
        {
            if (ID == 0)
            {
                return;
            }

            this.dgMain.Dispatcher.Invoke(new Action(delegate
            {
                this.dgMain.ItemsSource = this._dalTaskLog.GetList(" TaskId=" + ID, "", "CreateDate DESC");
            }));
        }
        #endregion
    }
}
