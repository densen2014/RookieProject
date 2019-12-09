using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using TimedTask.Entity;

namespace TimedTask.View
{
    /// <summary>
    /// TaskDetail.xaml 的交互逻辑
    /// </summary>
    public partial class TaskDetail : Window
    {
        private TimeType _timeType = 0;
        private string _statusText = "";
        private string _audio = "";//声音
        Dal.AutoTask dalAutoTask = new Dal.AutoTask();
        //ObservableCollection实现了INotifyPropertyChanged和INotifyCollectionChanged接口 表示一个动态数据集合，在添加项、移除项或刷新整个列表时，此集合将提供通知
        //public readonly ObservableCollection<String> months;

        public Int64 ID { get; set; }

        public TaskDetail()
        {
            InitializeComponent();

            ControlHelper.Instance.SetBackground(this.mainBoder, Entity.App.AppBgImg);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 1; i < 32; i++)
            {
                dic.Add(i.ToString(), i.ToString());
            }
            ControlHelper.Instance.CboAdd(this.cboDay, dic);
            dic.Clear();
            for (int i = 3; i < 61; i++)
            {
                dic.Add(i.ToString(), i.ToString());
            }
            ControlHelper.Instance.CboAdd(this.cboMinute, dic);
            dic.Clear();
            for (int i = 1; i < 13; i++)
            {
                dic.Add(i.ToString(), i.ToString());
            }
            ControlHelper.Instance.CboAdd(this.cboMonth, dic);
            dic.Clear();
            this.cboAudio.Items.Add("无");
            if (Entity.App.AudioList.Count > 0)
            {
                foreach (DictionaryEntry k in Entity.App.AudioList)
                {
                    this.cboAudio.Items.Add(k.Key);
                }
            }
            string ttype = "";
            foreach (object s in Enum.GetValues(typeof(TaskType)))
            {
                ttype = ((int)s).ToString();
                if (Entity.App.HtTaskType.Count == 0)
                    break;

                this.cboTaskType.Items.Add(Entity.App.HtTaskType[ttype].ToString());
            }

            this.cboMonth.SelectedIndex = 0;
            this.cboDay.SelectedIndex = 0;
            this.cboMinute.SelectedIndex = 0;
            this.cboTaskType.SelectedIndex = 0;
            this.cboAudio.SelectedIndex = 0;
        }

        private void OnInit()
        {
            if (ID == 0)
            {
                this.btnOK.Content = "添加任务";
                this.rbtMinute.IsChecked = true;
                this.cboEnable.IsChecked = true;
                rbtItem_Click(null, null);
            }
            else
            {
                this.btnOK.Content = "保 存";
                this.cboMinute.SelectedIndex = 0;
            }
            this.dp_StartDate.Text = DateTime.Now.AddMinutes(5).ToString("yyyy/MM/dd");
            this.dp_StartTime.Value = DateTime.Now;
            this.dp_StopDate.Text = DateTime.Now.AddDays(10).ToString("yyyy/MM/dd");
            this.dp_StopTime.Value = DateTime.Now;

            Entity.AutoTask model = dalAutoTask.GetEntity(" Id=" + ID);
            if (model != null)
            {
                this.txtPath.Text = model.ApplicationPath;
                this.txtTitle.Text = model.Title;
                this.txtStartParameter.Text = model.StartParameters;
                this.cboEnable.IsChecked = model.Enable == "1" ? true : false;
                this.txtRemark.Text = model.Remark;
                this.cboTaskType.SelectedIndex =
                    (model.TaskType == "" || model.TaskType == TaskType.TimingTask.ToString())
                    ? 0
                    : Convert.ToInt32(model.TaskType);

                this.cboAudio.SelectedItem = model.AudioPath.Length == 0 ? "无" : model.AudioPath;

                this.cboMinute.SelectedIndex = 0;
                this.cboDay.SelectedIndex = 0;

                switch (model.TimeType)
                {
                    case "1": this.rbtMonth.IsChecked = true;
                        _timeType = TimeType.Month;
                        this.cboDay.SelectedValue = model.Dayth;
                        break;
                    case "2":
                        this.rbtDay.IsChecked = true;
                        _timeType = TimeType.Day;
                        break;
                    case "3": this.rbtHour.IsChecked = true; _timeType = TimeType.Hour; break;
                    case "4":
                        this.rbtMinute.IsChecked = true;
                        _timeType = TimeType.Minute;
                        this.cboMinute.SelectedValue = model.Interval;
                        break;
                    case "5": this.rbtOnce.IsChecked = true; _timeType = TimeType.Once; break;
                }
                try
                {
                    switch (_timeType)
                    {
                        case TimeType.Once: this.rbtOnce.IsChecked = true; break;
                        case TimeType.Month: this.rbtMonth.IsChecked = true; break;
                        case TimeType.Day: this.rbtDay.IsChecked = true; break;
                        case TimeType.Hour: this.rbtHour.IsChecked = true; break;
                        case TimeType.Minute: this.rbtMinute.IsChecked = true; break;
                    }
                    rbtItem_Click(null, null);

                    this.dp_StartDate.Text = Convert.ToDateTime(model.StartDate).ToString("yyyy-MM-dd");
                    this.dp_StartTime.Value = model.StartDate;
                    this.dp_StopDate.Text = Convert.ToDateTime(model.StopDate).ToString("yyyy-MM-dd");
                    this.dp_StopTime.Value = model.StopDate;
                }
                catch (Exception ex)
                {
                    Log.SaveLog("TaskDetail OnInit", ex.ToString());
                }
            }
        }
        //窗体加载
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Timer timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(Timer_Click);
            timer.Interval = 100;
            timer.Enabled = true;

            this.lstTask.Dispatcher.Invoke(new Action(delegate
            {
                this.lstTask.ItemsSource = dalAutoTask.GetList(" 1=1 ", null, " CreateDate DESC");
            }));
            OnInit();
        }

        private void Timer_Click(object sender, EventArgs e)
        {
            ControlHelper.Instance.SetControlText(this.lblState, "状态：" + this._statusText);
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
        //浏览文件
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "可执行文件|*.exe";//全部|*.*||批处理|*.bat";
            if (ofd.ShowDialog() == true)
            {
                this.txtPath.Text = ofd.FileName;
                this.txtRemark.Text = Helper.GetFileDetailInfo(ofd.FileName, 34);
            }
        }
        //保存
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string path = this.txtPath.Text.Trim();
            string title = this.txtTitle.Text.Trim();
            string remark = this.txtRemark.Text.Trim();
            string startParameter = this.txtStartParameter.Text.Trim();
            string startDate = "", stopDate = "", nextStartDate = "";//起始 停止 下次启动时间

            if (this.rbtMonth.IsChecked == true) _timeType = TimeType.Month;
            if (this.rbtDay.IsChecked == true) _timeType = TimeType.Day;
            if (this.rbtHour.IsChecked == true) _timeType = TimeType.Hour;
            if (this.rbtMinute.IsChecked == true) _timeType = TimeType.Minute;
            if (this.rbtOnce.IsChecked == true) _timeType = TimeType.Once;

            AutoTask model = new AutoTask();
            try
            {
                bool flag = ID == 0 ? true : false;//是否是新增

                if (this.cboTaskType.SelectedIndex == 0)
                {
                    if (path.Length == 0 || !path.EndsWith(".exe"))
                    {
                        _statusText = "不是可执行文件或文件路径不能为空！";
                        return;
                    }
                }
                startDate = this.dp_StartDate.Text + " " + ((DateTime)this.dp_StartTime.Value).ToString("HH:mm:00");
                stopDate = this.dp_StopDate.Text + " " + Convert.ToDateTime(this.dp_StopTime.Value).ToString("HH:mm:00");
                nextStartDate = GetFirstStartDate();

                model.StartParameters = startParameter;
                model.ApplicationPath = path;
                model.Title = title;
                model.Enable = (bool)this.cboEnable.IsChecked ? "1" : "0";
                model.StartDate = Convert.ToDateTime(startDate);
                model.StopDate = Convert.ToDateTime(stopDate);
                model.Remark = remark;
                model.AudioPath = this._audio;
                model.TaskType = this.cboTaskType.SelectedIndex.ToString();
                model.Status = "";
                model.TimeType = ((int)_timeType).ToString();
                model.Interval = (this.rbtMinute.IsChecked == true) ? Convert.ToInt32(this.cboMinute.SelectedValue.ToString()) : 0;
                model.Dayth = (this.rbtMonth.IsChecked == true) ? Convert.ToInt32(this.cboDay.SelectedValue.ToString()) : 0;
                model.NextStartDate = Convert.ToDateTime(nextStartDate);

                if (!flag)//修改
                {
                    dalAutoTask.Update(model, " Id=" + ID);
                    _statusText = "保存设置成功！";
                }
                else//新增
                {
                    TimedTask.Dal.AutoTask dalTask = new Dal.AutoTask();
                    dalTask.Add(model);

                    _statusText = "新增成功！";
                }
            }
            catch (Exception ex)
            {
                _statusText = "保存设置失败，可能原因是：未找到指定的配置文件！";
                Log.SaveLog("TaskDetail btnOK_Click", _statusText + ex.ToString());
            }
        }
        //重置
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            this.ID = 0;
            this.txtPath.Text = "";
            this.txtRemark.Text = "";
            this.txtStartParameter.Text = "";
            this.txtTitle.Text = "";
            this.cboTaskType.SelectedIndex = 0;
            this.cboAudio.SelectedIndex = 0;

            OnInit();
        }
        //获取首次启动时间
        private string GetFirstStartDate()
        {
            string result = "";
            DateTime nowTime = DateTime.Now;
            string startTime = DateTime.Now.ToString("yyyy-MM-dd");
            if (Convert.ToDateTime(this.dp_StartDate.Text) > DateTime.Now)
                startTime = Convert.ToDateTime(this.dp_StartDate.Text).ToString("yyyy-MM-dd");

            switch (_timeType)
            {
                case TimeType.Once://一次
                    result = Convert.ToDateTime(startTime + " " + ((DateTime)this.dp_StartTime.Value).ToString("HH:mm:00")).
                        ToString("yyyy-MM-dd HH:mm:00");
                    break;
                case TimeType.Month:
                    result = nowTime.Year + "-" + nowTime.Month + "-" + this.cboDay.SelectedValue + " " + ((DateTime)this.dp_StartTime.Value).
                        ToString("HH:mm:00");
                    break;
                case TimeType.Day:
                    result = Convert.ToDateTime(startTime + " " + ((DateTime)this.dp_StartTime.Value).ToString("HH:mm:00")).
                        ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case TimeType.Hour:
                    result = Convert.ToDateTime(startTime + " " + ((DateTime)this.dp_StartTime.Value).ToString("HH:mm:00")).
                        ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case TimeType.Minute:
                    result = Convert.ToDateTime(startTime + " " + ((DateTime)this.dp_StartTime.Value).ToString("HH:mm:00")).ToString("yyyy-MM-dd HH:mm:00");
                    break;
            }
            if (Convert.ToDateTime(result) < DateTime.Now)
            {
                string interval = (this.rbtMinute.IsChecked == true) ? this.cboMinute.SelectedValue.ToString() : "0";
                string dayth = (this.rbtMonth.IsChecked == true) ? this.cboDay.SelectedValue.ToString() : "0";
                result = TimedTask.Bll.Task.Instance.GetNextStartDate((int)_timeType, Convert.ToInt32(dayth), Convert.ToInt32(interval));
            }
            return result;
        }
        //
        private void rbtItem_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbtOnce.IsChecked == true)//一次
            {
                this.cboMonth.IsEnabled = false;
                this.cboDay.IsEnabled = false;
                this.dp_StartDate.IsEnabled = true;
                this.dp_StartTime.IsEnabled = true;
                this.dp_StopDate.IsEnabled = false;
                this.dp_StopTime.IsEnabled = false;
                this.cboMinute.IsEnabled = false;
            }
            else if (this.rbtMonth.IsChecked == true)//每月
            {
                this.cboMonth.IsEnabled = false;
                this.cboDay.IsEnabled = true;
                this.dp_StartDate.IsEnabled = true;
                this.dp_StartTime.IsEnabled = true;
                this.dp_StopDate.IsEnabled = true;
                this.dp_StopTime.IsEnabled = true;
                this.cboMinute.IsEnabled = false;
            }
            else if (this.rbtDay.IsChecked == true)//每天
            {
                this.cboMonth.IsEnabled = false;
                this.cboDay.IsEnabled = false;
                this.dp_StartDate.IsEnabled = false;
                this.dp_StartTime.IsEnabled = true;
                this.dp_StopDate.IsEnabled = true;
                this.dp_StopTime.IsEnabled = true;
                this.cboMinute.IsEnabled = false;
            }
            else if (this.rbtHour.IsChecked == true)//每小时
            {
                this.cboMonth.IsEnabled = false;
                this.cboDay.IsEnabled = false;
                this.cboMinute.IsEnabled = false;
                this.dp_StartDate.IsEnabled = false;
                this.dp_StartTime.IsEnabled = true;
                this.dp_StopDate.IsEnabled = true;
                this.dp_StopTime.IsEnabled = true;
            }
            else if (this.rbtMinute.IsChecked == true)//间隔分钟
            {
                this.cboMonth.IsEnabled = false;
                this.cboDay.IsEnabled = false;
                this.dp_StartDate.IsEnabled = true;
                this.dp_StartTime.IsEnabled = true;
                this.dp_StopDate.IsEnabled = true;
                this.dp_StopTime.IsEnabled = true;
                this.cboMinute.IsEnabled = true;
            }
        }
        //任务类型
        private void cboTaskType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cboTaskType.SelectedIndex != 0)
            {
                this.txtPath.IsEnabled = false;
                this.txtStartParameter.IsEnabled = false;
                this.btnOpenFile.IsEnabled = false;
                if (this.txtTitle.Text.Length == 0)
                {
                    this.txtTitle.Text = this.cboTaskType.SelectedValue.ToString();
                    this.txtRemark.Text = "开启" + this.cboTaskType.SelectedValue.ToString();
                }
            }
            else//定时任务
            {
                this.txtPath.IsEnabled = true;
                this.txtStartParameter.IsEnabled = true;
                if (this.txtTitle.Text.StartsWith("定时")) this.txtTitle.Text = "";
            }
        }
        //声音选项
        private void cboAudio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._audio = this.cboAudio.SelectedValue.ToString();
            Helper.StopAudio();
            Helper.PalyAudio(this._audio, 100);
        }
        //配置选项
        private void lstTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.lstTask.SelectedItems.Count > 0)
            {
                Entity.AutoTask task = lstTask.SelectedItem as Entity.AutoTask;
                this.ID = task.Id;
                OnInit();
            }
        }
        //窗口关闭
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Helper.StopAudio();
        }
    }
}
