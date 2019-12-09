using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Config.xaml 的交互逻辑
    /// </summary>
    public partial class Config : Window
    {
        private XmlHelper _xml = null;
        private Entity.Area _area = new Entity.Area();
        private Bll.Weather _balWeather = new Bll.Weather();
        //private bool _isSelectArea = false;//是否选择日期

        public Config()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }
        //窗体加载
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 省份
            this._area = _balWeather.GetCurrentArea();

            LoadAreaData();
            #endregion


            ControlHelper.Instance.SetBackground(this.mainBoder, Entity.App.AppBgImg);
            this._xml = new XmlHelper(Entity.App.Config);

            for (int i = 1; i < 20; i++)
            {
                this.cboMinute.Items.Add(i);
            }
            this.cboMinute.SelectedIndex = 0;
            //自动启动
            if (Sys.IsAutoStartup())
                this.chkAutoRun.IsChecked = true;

            this.tbxBgImg.Text = _xml.SelectNodeText("Configuration/AppBgImg");
            this.tbxLockBgImg.Text = _xml.SelectNodeText("Configuration/LockBgImg");
            this.chkMinToTray.IsChecked = Entity.App.MinToTray;// _xml.SelectNodeText("Configuration/MinToTray") == "1" ? true : false;
            this.chkSaveLog.IsChecked = Entity.App.SaveLog;

            this.cboMinute.SelectedValue = Entity.App.LockMinute;
        }
        //浏览文件
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "JPG文件|*.jpg|PNG文件|*.png";//全部|*.*||批处理|*.bat";
            if (ofd.ShowDialog() == true)
            {
                Button btn = (Button)sender;
                if (btn.Name == "btnOpenAppImg")
                {
                    ControlHelper.Instance.SetControlText(this.tbxBgImg, ofd.FileName);
                }
                else
                {
                    ControlHelper.Instance.SetControlText(this.tbxLockBgImg, ofd.FileName);
                }
            }
        }
        //
        private void rbtCeeck_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbtAbout.IsChecked == true || this.rbtShortKey.IsChecked == true)
            {
                this.btnOK.Content = "确定";
            }
            else
            {
                this.btnOK.Content = "保存";
            }
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
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.btnOK.Content.ToString() == "确定")
            {
                this.Close();
                return;
            }
            Sys.AutoStartup((bool)this.chkAutoRun.IsChecked);
            Entity.App.MinToTray = (this.chkMinToTray.IsChecked == true);
            Entity.App.LockMinute = Convert.ToInt32(this.cboMinute.Text);

            _xml.SetXmlNodeValue("Configuration/LockMinute", this.cboMinute.Text);
            _xml.SetXmlNodeValue("Configuration/SaveLog", this.chkSaveLog.IsChecked == true ? "1" : "0");
            Entity.App.SaveLog = this.chkSaveLog.IsChecked == true;
            try
            {
                string path = this.tbxBgImg.Text.Trim();
                if (!System.IO.File.Exists(path) || (!path.ToLower().EndsWith(".jpg") && !path.ToLower().EndsWith(".png")))
                {
                    this.tbxBgImg.Text = "";
                    path = "";
                }
                _xml.SetXmlNodeValue("Configuration/AppBgImg", path);

                path = this.tbxLockBgImg.Text.Trim();
                if (!System.IO.File.Exists(path) || (!path.ToLower().EndsWith(".jpg") && !path.ToLower().EndsWith(".png")))
                {
                    this.tbxLockBgImg.Text = "";
                    path = "";
                }
                _xml.SetXmlNodeValue("Configuration/LockBgImg", path);
                _xml.SetXmlNodeValue("Configuration/MinToTray", Entity.App.MinToTray ? "1" : "0");

                _xml.Save();

                //保存城市信息
                this._area = (Entity.Area)this.chkCity.SelectedItem;
                if (this._area != null)
                    _balWeather.SaveCurrentArea(this._area);

                MessageBox.Show("保存成功，重启后生效！", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception ex)
            {
                Log.SaveLog("Config btnOK_Click", ex.ToString());
                MessageBox.Show("保存失败！", "警告", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// 省份选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkProvince_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string id = this.chkProvince.SelectedValue.ToString();
            CitySelect(id);
        }
        /// <summary>
        /// 显示城市列表
        /// </summary>
        /// <param name="provinceId">省份ID</param>
        private void CitySelect(string provinceId)
        {
            if (provinceId == null || provinceId.Length == 0)
                return;

            this.chkCity.Dispatcher.Invoke(new Action(delegate()
            {

                IEnumerable<Entity.Area> areas = this.AreaList.Where(
                    m => (m.ZoneID == Int32.Parse(provinceId))
                    );
                this.chkCity.ItemsSource = areas;
                this.chkCity.DisplayMemberPath = "Name";
                this.chkCity.SelectedValuePath = "ID";
                this.chkCity.SelectedIndex = 0;

            }));
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        void LoadAreaData()
        {
            TimedTask.WeatherService.WeatherWebServiceSoapClient client = new TimedTask.WeatherService.WeatherWebServiceSoapClient("WeatherWebServiceSoap");
            Func<DataSet> func = new Func<DataSet>(client.getSupportDataSet);
            func.BeginInvoke(ar =>
            {
                try
                {
                    var ds = func.EndInvoke(ar);
                    InitZoneFromDataSet(ds);
                }
                catch (Exception)
                {
                    return;
                }
                this.chkProvince.Dispatcher.Invoke(new Action(delegate()
                {
                    this.chkProvince.ItemsSource = this.ZoneList;
                    this.chkProvince.DisplayMemberPath = "Name";
                    this.chkProvince.SelectedValuePath = "ID";
                    this.chkProvince.SelectedIndex = 0;

                    if (this._area != null)
                    {
                        this.chkProvince.SelectedValue = this._area.ZoneID;
                        CitySelect(this._area.ZoneID.ToString());
                    }
                }));
            }, null);
        }

        /// <summary>
        /// 省份列表
        /// </summary>
        public ObservableCollection<Entity.Zone> ZoneList { get; set; }
        /// <summary>
        /// 城市列表
        /// </summary>
        public ObservableCollection<Entity.Area> AreaList { get; set; }

        /// <summary>
        /// 从DataSet中加载城市信息
        /// </summary>
        /// <param name="ds"></param>
        void InitZoneFromDataSet(DataSet ds)
        {
            this.ZoneList = new ObservableCollection<Entity.Zone>();
            this.AreaList = new ObservableCollection<Entity.Area>();
            var dtZone = ds.Tables[0];
            var dtArea = ds.Tables[1];
            this.ZoneList.Clear();
            foreach (DataRow dr in dtZone.Rows)
            {
                var zone = new Entity.Zone()
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    Name = dr["Zone"].ToString(),
                };
                var drAreas = dtArea.Select("ZoneID=" + zone.ID);
                foreach (DataRow drArea in drAreas)
                {
                    var area = new Entity.Area()
                    {
                        ID = Convert.ToInt32(drArea["ID"]),
                        ZoneID = Convert.ToInt32(drArea["ZoneID"]),
                        Name = drArea["Area"].ToString(),
                        AreaCode = drArea["AreaCode"].ToString()
                    };
                    this.AreaList.Add(area);
                }

                this.ZoneList.Add(zone);
            }
        }
    }
}
