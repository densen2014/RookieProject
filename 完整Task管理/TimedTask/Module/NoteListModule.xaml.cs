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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimedTask.Bll;
using TimedTask.ViewModel;

namespace TimedTask.Module
{
    /// <summary>
    /// NoteListModule.xaml 的交互逻辑
    /// </summary>
    public partial class NoteListModule : UserControl
    {
        private Dal.Note _dalNote = new Dal.Note();
        private Dal.TypeList _dalType = new Dal.TypeList();
        private List<Entity.Note> _noteList = null;
        private Entity.Note _model = new Entity.Note();
        private long _id = 0;

        public NoteListModule()
        {
            InitializeComponent();
            this.DataContext = new NoteViewModel();
            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Entity.TypeList> list = _dalType.GetList(" FatherId=1 ", "Id,Name", "Id");
            if (list != null && list.Count > 0)
            {
                this.cboType.ItemsSource = list;
                this.cboType.SelectedIndex = 1;
            }
        }
        private void lstNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._id = 0;
            if (this.lstNote.SelectedItems.Count > 0)
            {
                this.btnOk.Content = "修改";
                this._model = lstNote.SelectedItem as Entity.Note;
                this._id = this._model.Id;
                this.txtTitle.Text = this._model.Title;
                this.txtContent.Text = this._model.Content;
                this.cboType.SelectedValue = this._model.TypeId;
                this._id = this._model.Id;
            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="isFirstLoad">是否是第一次加载</param>
        public void Bind()
        {
            this.lstNote.Dispatcher.Invoke(new Action(delegate
            {
                _noteList = _dalNote.GetList(" 1=1 ", null, " CreateDate DESC");//记事列表
                bool result = false;
                this.lstNote.ItemsSource = _noteList;
            }));
        }
        //列表菜单
        private void Item_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            this._model = (Entity.Note)btn.DataContext;
            string btnContent = btn.Content.ToString().Replace(" ", "");

            if (btnContent == "删除")
            {
                MessageBoxResult mbr = MessageBox.Show("确定删除？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (mbr == MessageBoxResult.Yes)
                {
                    try
                    {
                        _dalNote.Delete(" Id=" + this._model.Id);
                    }
                    catch (Exception ex)
                    {
                        Log.SaveLog("MainWindow DropList 删除选中项", ex.ToString());
                    }
                    Bind();
                }
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this._model = new Entity.Note();
            string title = this.txtTitle.Text.Trim();
            string content = this.txtContent.Text.Trim();
            if (title.Length == 0 || content.Length == 0)
            {
                MessageBox.Show("标题或内容不能为空！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            this._model.Title = title;
            this._model.Content = content;
            this._model.CreateDate = DateTime.Now;
            this._model.TypeId = Convert.ToInt64(this.cboType.SelectedValue.ToString());
            try
            {
                if (this._id == 0)//新增
                {
                    this._dalNote.Add(this._model);
                    MessageBox.Show("添加成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    this._model.ModifyDate = DateTime.Now;
                    this._dalNote.Update(this._model, " Id=" + this._id);
                    MessageBox.Show("修改成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                btnReset_Click(null, null);
                return;
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
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            this._id = 0;
            this._model = null;
            this.txtTitle.Text = "";
            this.txtContent.Text = "";
            this.btnOk.Content = "添加";
        }
    }
}
