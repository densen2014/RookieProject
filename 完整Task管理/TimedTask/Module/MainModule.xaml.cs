using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Visifire.Charts;

namespace TimedTask.Module
{
    /// <summary>
    /// MainModule.xaml 的交互逻辑
    /// </summary>
    public partial class MainModule : UserControl
    {
        private Dal.Note _dalNote = new Dal.Note();
        private Dal.AutoTask _dalTask = new Dal.AutoTask();

        private string _info;//统计信息
        private Dictionary<string, string> _dic;

        private List<string> strListx = new List<string>();
        private List<string> strListy = new List<string>();

        public MainModule()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            strListx = new List<string>();
            strListy = new List<string>();
            Simon.Children.Clear();
            this._info = "";
            this._dic = new Dictionary<string, string>();

            Bll.Calendar calender = new Bll.Calendar(DateTime.Now);
            if (calender.ChineseTwentyFourDay.Length > 0)
                this._info += "今天是：" + calender.ChineseTwentyFourDay + "\r\n\r\n";
            if (calender.NextDateHoliday.Length > 0)
                this._info += calender.NextDateHoliday.Split('|')[1] + "是：" + calender.NextDateHoliday.Split('|')[0] + "\r\n\r\n";

            this._dic.Add("记事", this._dalNote.Count(" 1=1 ").ToString());
            this._dic.Add("定时任务", this._dalTask.Count(" TaskType=0 ").ToString());
            this._dic.Add("定时提醒", this._dalTask.Count(" TaskType=1 ").ToString());
            this._dic.Add("定时关机", this._dalTask.Count(" TaskType=2 ").ToString());
            this._dic.Add("关显示器", this._dalTask.Count(" TaskType in(3,4) ").ToString());
            this._dic.Add("定时锁屏", this._dalTask.Count(" TaskType=5 ").ToString());

            if (this._dic.Count > 0)
            {
                foreach (KeyValuePair<string, string> kvp in this._dic)
                {
                    strListx.Add(kvp.Key);
                    strListy.Add(kvp.Value.ToString());
                    this._info += "您有" + kvp.Key + " " + kvp.Value + " 条\r\n\r\n";
                }
            }
            this.txtStatistical.Text = this._info;

            Chart chart = Helper.CreatePie("任务统计图", 495, 319, strListx, strListy);
            if (chart != null)
            {
                chart.BorderThickness = new Thickness(0);
                Simon.Children.Add(chart);
            }
        }

        #region 点击事件
        //点击事件
        void dataPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DataPoint dp = sender as DataPoint;
            //MessageBox.Show(dp.YValue.ToString());
        }
        #endregion
    }
}
