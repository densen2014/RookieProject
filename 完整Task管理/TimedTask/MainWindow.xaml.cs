using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using TimedTask.Bll;
using System.Text;
using TimedTask.ViewModel;
using TimedTask.WeatherService;

namespace TimedTask
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private XmlHelper _xml = null;
        private WindowState _lastWinState = WindowState.Normal;//记录上一次WindowState 
        private bool _reallyExit = false;//是否真的关闭窗口  
        private Bll.Weather _balWeather = new Bll.Weather();
        private Dal.TaskLog _dalTaskLog = new Dal.TaskLog();

        private Module.TaskListModule _tlModule = new Module.TaskListModule();//任务列表模块
        private Module.NoteListModule _nlModule = new Module.NoteListModule();//记事本列表模块
        private Module.MainModule _mmModule = new Module.MainModule();//主页统计模块

        public MainWindow()
        {
            InitializeComponent();

            OnInit();
            ControlHelper.Instance.SetBackground(this.mainBoder, Entity.App.AppBgImg);
            this.Loaded += new RoutedEventHandler(Window_Loaded);
            Helper.Speek("程序运行成功！");
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void OnInit()
        {
            Entity.Area area = this._balWeather.GetCurrentArea();

            try
            {
                if (area != null)
                    AsyncWeather(area.Name);

                this.lblVerson.Content = "版本 V" + Helper.GetVersion();
                Entity.App.StartPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                Entity.App.Config = Entity.App.StartPath + "\\config.xml";
                Log.LogPath = Entity.App.StartPath + "\\Log\\";
                if (!File.Exists(Entity.App.Config))
                {
                    MessageBox.Show("配置文件丢失！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Application.Current.Shutdown();
                }
                XmlHelper xmlConfig = new XmlHelper(Entity.App.Config);
                Entity.App.AppBgImg = xmlConfig.SelectNodeText("Configuration/AppBgImg").Trim();
                Entity.App.LockBgImg = xmlConfig.SelectNodeText("Configuration/LockBgImg").Trim();
                Entity.App.MinToTray = xmlConfig.SelectNodeText("Configuration/MinToTray").Trim() == "1" ? true : false;
                string minutes = xmlConfig.SelectNodeText("Configuration/LockMinute").Trim();
                Entity.App.LockMinute = minutes.Length == 0 ? 3 : Convert.ToInt32(minutes);
                Entity.App.SaveLog = xmlConfig.SelectNodeText("Configuration/SaveLog").Trim() == "1" ? true : false;

                if (Entity.App.AppBgImg.Length == 0)
                    Entity.App.AppBgImg = Entity.App.StartPath + "\\Bg\\bg.jpg";
                if (Entity.App.LockBgImg.Length == 0)
                    Entity.App.LockBgImg = Entity.App.StartPath + "\\Bg\\lock.jpg";
            }
            catch (Exception ex)
            {
                Log.SaveLog("MainWindow.xaml.cs Window_Loaded", ex.ToString());
            }
        }

        #region 异步获取天气信息

        private void AsyncWeather(string citiName)
        {
            try
            {
                Func<string, string[]> func = new Func<string, string[]>(this._balWeather.GetWeather);
                func.BeginInvoke(citiName, ar =>
                {
                    try
                    {
                        string[] weatherArr = func.EndInvoke(ar);
                        if (weatherArr != null && weatherArr.Length > 0)
                        {
                            Bll.WeatherInfo _balWeatherInfo = new WeatherInfo(weatherArr);

                            this.lblWeather.Dispatcher.Invoke(new Action(delegate()
                            {
                                if (_balWeatherInfo.CityName == null)
                                    return;
                                this.lblWeather.Content =
                                    _balWeatherInfo.CityName +
                                    "：" + _balWeatherInfo.TodaySurvey + _balWeatherInfo.TodayTemperature + " " + _balWeatherInfo.TomorrowWind;
                                this.imgWeather.Source = new System.Windows.Media.Imaging.BitmapImage(
                                    new Uri(_balWeatherInfo.TodayStartImage.Replace("a_", ""), UriKind.Absolute));

                                this.txtSurvey.Text =
                                    _balWeatherInfo.TomorrowSurvey + _balWeatherInfo.TomorrowTemperature + " " + _balWeatherInfo.TomorrowWind + "\r\n"
                                    + _balWeatherInfo.HtSurvey + _balWeatherInfo.HtTemperature + " " + _balWeatherInfo.HtWind + "\r\n\r\n"
                                    + _balWeatherInfo.TodayWeatherSummary;
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.SaveLog("MainWindow.xaml.cs EndInvokeWeather", ex.ToString());
                        ControlHelper.Instance.SetControlText(this.lblWeather, "天气获取失败...");
                    }

                }, null);
            }
            catch (Exception ex)
            {
                Log.SaveLog("MainWindow.xaml.cs EndInvokeWeather", ex.ToString());
                ControlHelper.Instance.SetControlText(this.lblWeather, "天气获取失败...");
            }
        }

        #endregion

        #region 托盘相关

        /// <summary>
        /// 菜单项"显示主窗口"点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miShow_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsVisible)
            {
                this.Hide();
                this.miShowWindow.Header = "显示窗口";
            }
            else
            {
                this.Show();
                this.miShowWindow.Header = "隐藏窗口";
            }
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
                this.WindowState = _lastWinState;
            }
            this.Activate();

            //设置托盘图标
            //TaskbarIcon tbi = new TaskbarIcon();
            //tbi.Icon = Resources.Error;
            //tbi.Visibility = Visibility.Collapsed;
        }
        //托盘
        private void Window_Close(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("确定要退出托盘应用程序吗？",
                                               "托盘应用程序",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Question,
                                               MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
                this.tbIcon.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// 关闭时,判断是缩小到托盘还是退出程序
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!_reallyExit)
            {
                e.Cancel = true;
                _lastWinState = this.WindowState;
                this.Hide();
            }
            this.tbIcon.Dispose();
        }

        #endregion

        #region 窗体

        /// <summary>
        /// 窗体移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        /// <summary>
        /// 窗体最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
            this.Visibility = Visibility.Hidden;//最小化到托盘
            this.miShowWindow.Header = "显示窗口";
        }
        /// <summary>
        /// 窗口最小化
        /// </summary>
        private void Minimized()
        {
            this.WindowState = System.Windows.WindowState.Minimized;
            this.Visibility = Visibility.Hidden;//最小化到托盘
            this.miShowWindow.Header = "显示窗口";
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (Entity.App.MinToTray)
            {
                Minimized();
                return;
            }
            System.Windows.Application.Current.Shutdown();
        }
        //说明
        private void miHelp_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", Entity.App.StartPath + "\\" + "说明.txt");
        }
        //启动宠物
        private void miPet_Click(object sender, RoutedEventArgs e)
        {
            //string path = Entity.ApplicationModel.StartPath + "\\tiger\\Tiger.exe";
            //if (File.Exists(path))
            //    Helper.Run(@"taskkill /im Tiger.exe /f");
            //Helper.Run(path);
        }
        //窗体加载
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.brMain.Child = this._mmModule;

            try
            {
                Task.Instance.GetTaskList(true);
                Helper.CreateFolder(Log.LogPath);
                _xml = new XmlHelper(Entity.App.TaskConfig);

                #region 温馨提示

                Bll.Calendar calender = new Bll.Calendar(DateTime.Now);
                string calendar = "农历：" + calender.ChineseDateString + "\r\n";
                calendar += " 时辰：" + calender.ChineseHour + "\r\n";
                calendar += " 属相：" + calender.AnimalString + "\r\n";
                calendar += (calender.ChineseTwentyFourDay.Length > 0) ? " 节气：" + calender.ChineseTwentyFourDay + "\r\n" : "";
                calendar += (calender.DateHoliday.Length > 0) ? " 节日：" + calender.DateHoliday + "\r\n" : "";
                //calendar += (calender.NextDateHoliday.Length > 0) ? " 下一个节日：" + calender.NextDateHoliday + "\r\n" : "";
                calendar += " 星座：" + calender.Constellation + "\r\n";

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    View.PopUP pop = new View.PopUP();
                    pop.Subject = "";
                    pop.Info = calendar;
                    pop.PopTitle = "温馨提示！";
                    pop.Show();
                }));
                #endregion

                #region 任务

                System.Timers.Timer timerTask = new System.Timers.Timer();
                timerTask.Interval = 60000;//1分钟执行一次
                timerTask.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Task);
                timerTask.Start();
                #endregion

                //删除4天前日志文件
                Helper.DropFiles(Log.LogPath, null, new string[] { ".txt", ".log" }, 4);
                this._dalTaskLog.DeleteHistory();
            }
            catch (Exception ex)
            {
                Log.SaveLog("MainWindow", ex.ToString());
            }
        }

        #endregion

        #region 菜单事件

        //任务
        private void mibtnTask_Click(object sender, RoutedEventArgs e)
        {
            if (this.brMain.Child != this._tlModule)
                this.brMain.Child = this._tlModule;
        }
        //记事
        private void mibtnNote_Click(object sender, RoutedEventArgs e)
        {
            if (this.brMain.Child != this._nlModule)
                this.brMain.Child = this._nlModule;
        }
        //首页
        private void mibtnMain_Click(object sender, RoutedEventArgs e)
        {
            if (this.brMain.Child != this._mmModule)
                this.brMain.Child = this._mmModule;
        }
        //退出
        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //设置
        private void miSet_Click(object sender, RoutedEventArgs e)
        {
            View.Config config = new View.Config();
            config.ShowDialog();
        }

        #endregion

        #region 定时器

        private void Timer_Task(object sender, System.Timers.ElapsedEventArgs e)
        {
            Bll.Task.Instance.StartTask();
        }
        #endregion

        #region xml监控

        //文件监控
        //Thread tWork = new Thread(XmlMonitor);
        //tWork.IsBackground = true;
        //tWork.Start();

        /// <summary>
        /// 
        /// </summary>
        //public void XmlMonitor()
        //{
        //    FileSystemWatcher fw = new FileSystemWatcher();
        //    fw.Path = Entity.App.TaskConfig.Substring(0, Entity.App.TaskConfig.LastIndexOf("\\"));
        //    fw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite;
        //    fw.Filter = "*.xml";
        //    fw.Changed += new FileSystemEventHandler(OnChanged);
        //    fw.EnableRaisingEvents = true;//是否提交事
        //}

        //private void OnChanged(object source, FileSystemEventArgs e)
        //{
        //    try
        //    {
        //        Bind();
        //    }
        //    catch (IOException)
        //    {
        //        Thread.Sleep(1000);
        //        Bind();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.SaveLog("MainWindow OnChanged", ex.ToString());
        //    }
        //}
        #endregion
    }
}
