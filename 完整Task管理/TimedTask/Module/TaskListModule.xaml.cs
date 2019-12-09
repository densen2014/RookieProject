using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimedTask.Bll;

namespace TimedTask.Module
{
    /// <summary>
    /// TaskListModule.xaml 的交互逻辑
    /// </summary>
    public partial class TaskListModule : UserControl
    {
        private Dal.AutoTask dalAutoTask = new Dal.AutoTask();
        private Entity.AutoTask mod = null;

        public TaskListModule()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.cboAudio.Items.Add("无");
            if (Entity.App.AudioList.Count > 0)
            {
                foreach (DictionaryEntry k in Entity.App.AudioList)
                {
                    this.cboAudio.Items.Add(k.Key);
                }
            }
            this.cboAudio.SelectedIndex = 0;
            Bind();
        }
        #region 数据加载

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="isFirstLoad">是否是第一次加载</param>
        public void Bind()
        {
            this.lstMain.Dispatcher.Invoke(new Action(delegate
            {
                this.lstMain.ItemsSource = Task.Instance.GetTaskList(false);
            }));
        }
        #endregion

        private void lstMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.lstMain.SelectedItems.Count > 0)
            {
                this.mod = lstMain.SelectedItem as Entity.AutoTask;
                this.cboAudio.SelectedItem = this.mod.AudioPath.Length == 0 ? "无" : this.mod.AudioPath;
                this.chkEnable.IsChecked = this.mod.AudioEnable == "1" ? true : false;
                this.sdVolume.Value = this.mod.AudioVolume;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.mod == null)
            {
                MessageBox.Show("没有任何选中项！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                this.mod.AudioEnable = this.chkEnable.IsChecked == true ? "1" : "0";
                this.mod.AudioVolume = (long)this.sdVolume.Value;
                this.mod.AudioPath = this.cboAudio.SelectedItem.ToString();

                dalAutoTask.Update(this.mod, " Id=" + this.mod.Id);
                MessageBox.Show("操作成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Log.SaveLog("NoteListModule btnOK_Click", ex.ToString());
                MessageBox.Show("系统异常，操作失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Bind();
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            View.TaskDetail vTask = new View.TaskDetail();
            vTask.ShowDialog();
        }
        //声音选项
        private void cboAudio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string audio = this.cboAudio.SelectedItem.ToString();
            Helper.StopAudio();
            Helper.PalyAudio(audio, this.sdVolume.Value);
        }
        //上下文菜单
        private void cmClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;

            string type = item.Header.ToString().Trim();
            string proccessName = "";
            Entity.AutoTask mod = null;
            if (type.Equals("查看") || type.Equals("运行记录") || type.Equals("停止实例") || type.Equals("运行"))
            {
                mod = (Entity.AutoTask)lstMain.SelectedItem;
                if (mod.ApplicationPath != null)
                    proccessName = mod.ApplicationPath.Substring(mod.ApplicationPath.LastIndexOf("\\") + 1).Replace(".exe", "");
            }
            try
            {
                switch (item.Header.ToString().Trim())
                {
                    case "运行记录":
                        {
                            View.TaskRunLog trl = new View.TaskRunLog();
                            trl.ID = mod.Id;
                            trl.Show();
                        }
                        break;
                    case "停止实例":
                        {
                            if (mod.TaskType == "0")
                                Helper.EndApp(proccessName);
                            else
                                Helper.StopAudio();

                            break;
                        }
                    case "运行": StartItem(mod, proccessName); break;
                }
            }
            catch (Exception ex)
            {
                Log.SaveLog("MainWindow cmClick 运行", ex.ToString());
                MessageBox.Show("操作失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //运行选中项
        private void StartItem(Entity.AutoTask model, string proccessName)
        {
            if (model == null)
                return;
            if (model.TaskType != null && model.TaskType != "0")
            {
                Bll.Task.Instance.StartWarn(model, true);
                return;
            }
            if (!File.Exists(model.ApplicationPath))
            {
                MessageBox.Show("运行失败，程序没有找到！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {   //杀死
                Helper.EndApp(proccessName);
                if (model.StartParameters.Length > 0)
                {
                    System.Diagnostics.Process.Start(model.ApplicationPath, model.StartParameters);
                }
                else
                {
                    System.Diagnostics.Process.Start(model.ApplicationPath);//可调用bat文件
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                Log.SaveLog("MainWindow DropList 删除选中项", ex.ToString());
            }
        }
        //列表菜单
        private void Item_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Entity.AutoTask model = (Entity.AutoTask)btn.DataContext;
            string btnContent = btn.Content.ToString().Replace(" ", "");
            if (btnContent == "查看")
            {
                View.TaskDetail vTask = new View.TaskDetail();
                vTask.ID = model.Id;
                vTask.ShowDialog();
            }
            else if (btnContent == "删除")
            {
                MessageBoxResult mbr = MessageBox.Show("确定删除？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (mbr == MessageBoxResult.Yes)
                {
                    try
                    {
                        dalAutoTask.Delete(" Id=" + model.Id);
                        MessageBox.Show("操作成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("操作成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        Log.SaveLog("MainWindow DropList 删除选中项", ex.ToString());
                    }
                    Bind();
                }
            }
            else if (btnContent == "禁用" || btnContent == "启用")
            {
                model.Enable = btnContent == "禁用" ? "0" : "1";
                dalAutoTask.Update(model, " Id=" + model.Id);
                MessageBox.Show("操作成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                Bind();
            }
        }
    }
}
