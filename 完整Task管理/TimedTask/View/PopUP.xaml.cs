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
using System.Windows.Threading;

namespace TimedTask.View
{
    /// <summary>
    /// PopUP.xaml 的交互逻辑
    /// </summary>
    public partial class PopUP : Window
    {
        private DispatcherTimer winTimer;
        public double EndTop { get; set; }
        /// <summary>
        /// 消息主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 窗口标题
        /// </summary>
        public string PopTitle { get; set; }

        public PopUP()
        {
            InitializeComponent();

            this.Left = SystemParameters.WorkArea.Width - this.Width;
            this.EndTop = SystemParameters.WorkArea.Height - this.Height;
            this.Top = SystemParameters.WorkArea.Height;
        }
        void start_Tick(object sender, EventArgs e)
        {
            while (this.Top > EndTop)
            {
                this.Top -= 10;
            }
        }
        void end_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
        private void PopWin_Loaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;//窗口置顶

            winTimer = new DispatcherTimer();
            winTimer.Interval = TimeSpan.FromMilliseconds(300);
            winTimer.Tick += start_Tick;
            winTimer.Start();
            //15秒关闭
            winTimer = new DispatcherTimer();
            winTimer.Interval = TimeSpan.FromMilliseconds(15000);
            winTimer.Tick += end_Tick;
            winTimer.Start();

            this.Info = this.Info == null ? "" : this.Info;
            this.Subject = this.Subject == null ? "" : this.Subject;
            this.lblTitle.Content = this.PopTitle == null ? "系统信息" : this.PopTitle;

            if (this.Subject.Length == 0)
            {
                this.txtInfo.Inlines.Remove(this.txtSubject);
            }
            else
            {
                this.txtSubject.Text = this.Subject.Length > 18 ? this.Subject.Substring(0, 18) + "..." : this.Subject;
                this.txtSubject.Text += "\r\n";
                //this.txtInfo.Inlines.Add(new LineBreak());
            }
            this.txtContent.Text = (this.Info.Length > 58 ? this.Info.Substring(0, 58) + "..." : this.Info);
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Helper.StopAudio();
        }
    }
}
